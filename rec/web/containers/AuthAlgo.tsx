import React, { useState } from "react";
import { createContainer } from "unstated-next"; // Unstated-next containerization
import WalletConnect from "@walletconnect/client";
import QRCodeModal from "algorand-walletconnect-qrcode-modal";
import { Scenario, scenarios, signTxnWithTestAccount } from "../scenarios";
import { apiGetAccountAssets, apiSubmitTransactions, ChainType } from "../helpers/api";
import { IAssetData, IWalletTransaction, SignTxnParams } from "../helpers/types";
import Modal from "../components/Modal";
import algosdk from "algosdk";
import { formatJsonRpcRequest } from "@json-rpc-tools/utils";


interface IResult {
  method: string;
  body: Array<
    Array<{
      txID: string;
      signingAddress?: string;
      signature: string;
    } | null>
  >;
}

function useAuthAlgo() {

  const [connector, setConnector] = useState(null);
  const [fetching, setFetching] = useState(false);
  const [connected, setConnected] = useState(false);
  const [showModal, setShowModal] = useState(false);
  const [pendingRequest, setPendingRequest] = useState(false);
  const [signedTxns, setSignedTxns] = useState(null);
  const [pendingSubmissions, setPendingSubmissions] = useState([]);
  const [prevState, setPrevState] = useState(null)
  const [uri, setUri] = useState("");
  const [accounts, setAccounts] = useState([]);
  const [address, setAddress] = useState("");
  const [result, setResult] = useState<IResult>(null);
  const [chain, setChain] = useState(ChainType.TestNet);
  const [assets, setAssets] = useState<IAssetData[]>([]);

  const authenticateAlgo = async () => {
      if(!connector) await walletConnectInit();
  }

  const walletConnectInit = async () => {
    // bridge url
    const bridge = "https://bridge.walletconnect.org";

    // create new connector
    const _connector = new WalletConnect({ bridge, qrcodeModal: QRCodeModal });

    await setConnector(_connector);
    console.log("_CONNECTOR", _connector)
    
    // check if already connected
    if (!_connector.connected) {
      // create new session
      console.log('connetor - create a session')
      await _connector.createSession();
    }

    // subscribe to events
   subscribeToEvents(_connector);
  };


  const subscribeToEvents = async (connector) => {
    console.log("Subscribe Event Connector", connector)
    if (!connector) {
      return;
    }

    connector.on("session_update", async (error, payload) => {
      console.log(`connector.on("session_update")`);
      if (error) {
        throw error;
      }
      const { accounts } = payload.params[0];
      onSessionUpdate(accounts);
    });

    connector.on("connect", (error, payload) => {
      console.log(`connector.on("connect")`);
      if (error) {
        throw error;
      }
      onConnect(payload);
    });

    connector.on("disconnect", (error, payload) => {
      console.log(`connector.on("disconnect")`);
      if (error) {
        throw error;
      }
      onDisconnect();
    });

    console.log('if connected, set accounts, address')
    if (connector.connected) {
      const { accounts } = connector;
      const address = accounts[0];

      setAccounts(accounts);
      setAddress(address);
      setConnected(true);

      onSessionUpdate(accounts);
    }

    //this.setState({ connector });
    await setConnector(connector);

  };


  const killSession = async () => {
    if (connector) {
       connector.killSession();
    }
    resetApp();
  };

  const chainUpdate = (newChain) => {
    setChain(newChain);
  };

  const resetApp = async () => {
      setAccounts(null);
      setAddress(null);
      setConnected(false);
  };

  const onConnect = async (payload) => {
    const { accounts } = payload.params[0];
    const address = accounts[0];
    setAccounts(accounts);
    setAddress(address);
    setConnected(true);
  
    //getAccountAssets();
  };

  const onDisconnect = async () => {
    resetApp();
  };

  const onSessionUpdate = async (accounts: string[]) => {
    const address = accounts[0];
    setAddress(address);
    setAccounts(accounts);
    //await getAccountAssets();
  };

  const getAccountAssets = async () => {
    setFetching(true);
    try {
      // get accounts balances
      const assets = await apiGetAccountAssets(chain, address);
      setFetching(false);
      setAssets(assets);
      console.log('Assets:', assets);
    } catch (error) {
      console.error(error);
      setFetching(false);
    }
  };

  const toggleModal = () => {
    setShowModal(!showModal);
    setPendingSubmissions([]);
  }

  const signTxnScenario = async (scenario: Scenario) => {
    
    if (!connector) {
      return;
    }

    try {
      const txnsToSign = await scenario(chain, address);

      // open modal
      toggleModal();

      // toggle pending request indicator
      setPendingRequest(true);

      const flatTxns = txnsToSign.reduce((acc, val) => acc.concat(val), []);

      const walletTxns: IWalletTransaction[] = flatTxns.map(
        ({ txn, signers, authAddr, message }) => ({
          txn: Buffer.from(algosdk.encodeUnsignedTransaction(txn)).toString("base64"),
          signers, // TODO: put auth addr in signers array
          authAddr,
          message,
        }),
      );

      // sign transaction
      const requestParams: SignTxnParams = [walletTxns];
      const request = formatJsonRpcRequest("algo_signTxn", requestParams);
      const result: Array<string | null> = await connector.sendCustomRequest(request);

      console.log("Raw response:", result);

      const indexToGroup = (index: number) => {
        for (let group = 0; group < txnsToSign.length; group++) {
          const groupLength = txnsToSign[group].length;
          if (index < groupLength) {
            return [group, index];
          }

          index -= groupLength;
        }

        throw new Error(`Index too large for groups: ${index}`);
      };

      const signedPartialTxns: Array<Array<Uint8Array | null>> = txnsToSign.map(() => []);
      result.forEach((r, i) => {
        const [group, groupIndex] = indexToGroup(i);
        const toSign = txnsToSign[group][groupIndex];

        if (r == null) {
          if (toSign.signers !== undefined && toSign.signers?.length < 1) {
            signedPartialTxns[group].push(null);
            return;
          }
          throw new Error(`Transaction at index ${i}: was not signed when it should have been`);
        }

        if (toSign.signers !== undefined && toSign.signers?.length < 1) {
          throw new Error(`Transaction at index ${i} was signed when it should not have been`);
        }

        const rawSignedTxn = Buffer.from(r, "base64");
        signedPartialTxns[group].push(new Uint8Array(rawSignedTxn));
      });

      const signedTxns: Uint8Array[][] = signedPartialTxns.map(
        (signedPartialTxnsInternal, group) => {
          return signedPartialTxnsInternal.map((stxn, groupIndex) => {
            if (stxn) {
              return stxn;
            }

            return signTxnWithTestAccount(txnsToSign[group][groupIndex].txn);
          });
        },
      );

      const signedTxnInfo: Array<Array<{
        txID: string;
        signingAddress?: string;
        signature: string;
      } | null>> = signedPartialTxns.map((signedPartialTxnsInternal, group) => {
        return signedPartialTxnsInternal.map((rawSignedTxn, i) => {
          if (rawSignedTxn == null) {
            return null;
          }

          const signedTxn = algosdk.decodeSignedTransaction(rawSignedTxn);
          const txn = (signedTxn.txn as unknown) as algosdk.Transaction;
          const txID = txn.txID();
          const unsignedTxID = txnsToSign[group][i].txn.txID();

          if (txID !== unsignedTxID) {
            throw new Error(
              `Signed transaction at index ${i} differs from unsigned transaction. Got ${txID}, expected ${unsignedTxID}`,
            );
          }

          if (!signedTxn.sig) {
            throw new Error(`Signature not present on transaction at index ${i}`);
          }

          return {
            txID,
            signingAddress: signedTxn.sgnr ? algosdk.encodeAddress(signedTxn.sgnr) : undefined,
            signature: Buffer.from(signedTxn.sig).toString("base64"),
          };
        });
      });

      console.log("Signed txn info:", signedTxnInfo);

      // format displayed result
      const formattedResult: IResult = {
        method: "algo_signTxn",
        body: signedTxnInfo,
      };

      // display result
      setResult(formattedResult);
      setPendingRequest(false);
      setSignedTxns(signedTxns);

    } catch (error) {
      console.error(error);
      setPendingRequest(false);
      setResult(null);
    }
  };

  const submitSignedTransaction = () => {
    if (signedTxns == null) {
      throw new Error("Transactions to submit are null");
    }

    setPendingSubmissions(signedTxns.map(() => 0));
    //this.setState({ pendingSubmissions: signedTxns.map(() => 0) });

    signedTxns.forEach(async (signedTxn, index) => {
      try {
        const confirmedRound = await apiSubmitTransactions(chain, signedTxn);

        setPrevState(prevState => {
          return {
            pendingSubmissions: prevState.pendingSubmissions.map((v, i) => {
              if (index === i) {
                return confirmedRound;
              }
              return v;
            }),
          };
        });

        console.log(`Transaction confirmed at round ${confirmedRound}`);
      } catch (err) {
        setPrevState(prevState => {
          return {
            pendingSubmissions: prevState.pendingSubmissions.map((v, i) => {
              if (index === i) {
                return err;
              }
              return v;
            }),
          };
        });

        console.error(`Error submitting transaction at index ${index}:`, err);
      }
    });
  }




  return {
    authenticateAlgo,
    address,
    killSession,
    signTxnScenario,
    submitSignedTransaction,
    showModal,
    setShowModal,
    toggleModal
  };


}


// Create unstate-next container
const authAlgo = createContainer(useAuthAlgo);
export default authAlgo;