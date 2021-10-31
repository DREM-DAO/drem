import React, { useState } from 'react';
import Layout from '@components/Layout'
import Button from '@components/Button';
import { authAlgo } from "@containers/index"; // Global state
import { Scenario, scenarios } from "../scenarios";
import Modal from "@components/Modal";
import Loader from '@components/Loader';

import styled from "styled-components";
import { fonts } from "../styles";

const SContainer = styled.div`
  height: 100%;
  min-height: 200px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  word-break: break-word;
`;

const SModalContainer = styled.div`
  width: 100%;
  position: relative;
  word-wrap: break-word;
`;

const SModalTitle = styled.div`
  margin: 1em 0;
  font-size: 20px;
  font-weight: 700;
`;

const SModalButton = styled.button`
  margin: 1em 0;
  font-size: 18px;
  font-weight: 700;
`;

const SModalParagraph = styled.p`
  margin-top: 30px;
`;

// @ts-ignore
const SBalances = styled(SLanding as any)`
  height: 100%;
  & h3 {
    padding-top: 30px;
  }
`;

const STable = styled(SContainer as any)`
  flex-direction: column;
  text-align: left;
`;

const SRow = styled.div`
  width: 100%;
  display: flex;
  margin: 6px 0;
`;

const SKey = styled.div`
  width: 30%;
  font-weight: 700;
`;

const SValue = styled.div`
  width: 70%;
  font-family: monospace;
`;

const STestButtonContainer = styled.div`
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  flex-wrap: wrap;
`;

const STestButton = styled(Button as any)`
  border-radius: 8px;
  font-size: ${fonts.size.medium};
  height: 64px;
  width: 100%;
  max-width: 175px;
  margin: 12px;
`;

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

const Review = () => {

    const [result, setResult] = useState<IResult | null>(null);
    const [pendingRequest, setPendingRequest] = useState("")
    const [pendingSubmissions, setPendingSubmissions] = useState([])

    const { signTxnScenario, submitSignedTransaction, 
            showModal, setShowModal, toggleModal } = authAlgo.useContainer(); // Global state
    
    const handleSign = async () => {
        await signTxnScenario(scenarios[5].scenario )
    }

    return (
        <Layout>
            <div>

                    <h2> Review an Asset</h2>

                    <Button onClick={handleSign}> Sign </Button>
                    <Modal show={showModal} toggleModal={setShowModal} >
                        <h1>chack your phone</h1>
                    </Modal>
                    <Modal show={showModal} toggleModal={toggleModal}>
          {pendingRequest ? (
            <SModalContainer>
              <SModalTitle>{"Pending Call Request"}</SModalTitle>
              <SContainer>
                <Loader />
                <SModalParagraph>{"Approve or reject request using your wallet"}</SModalParagraph>
              </SContainer>
            </SModalContainer>
          ) : result ? (
            <SModalContainer>
              <SModalTitle>{"Call Request Approved"}</SModalTitle>
              <STable>
                <SRow>
                  <SKey>Method</SKey>
                  <SValue>{result.method}</SValue>
                </SRow>
                {result.body.map((signedTxns, index) => (
                  <SRow key={index}>
                    <SKey>{`Atomic group ${index}`}</SKey>
                    <SValue>
                      {signedTxns.map((txn, txnIndex) => (
                        <div key={txnIndex}>
                          {!!txn?.txID && <p>TxID: {txn.txID}</p>}
                          {!!txn?.signature && <p>Sig: {txn.signature}</p>}
                          {!!txn?.signingAddress && <p>AuthAddr: {txn.signingAddress}</p>}
                        </div>
                      ))}
                    </SValue>
                  </SRow>
                ))}
              </STable>
              <SModalButton
                onClick={() => submitSignedTransaction()}
                disabled={pendingSubmissions.length !== 0}
              >
                {"Submit transaction to network."}
              </SModalButton>
              {pendingSubmissions.map((submissionInfo, index) => {
                const key = `${index}:${
                  typeof submissionInfo === "number" ? submissionInfo : "err"
                }`;
                const prefix = `Txn Group ${index}: `;
                let content: string;

                if (submissionInfo === 0) {
                  content = "Submitting...";
                } else if (typeof submissionInfo === "number") {
                  content = `Confirmed at round ${submissionInfo}`;
                } else {
                  content = "Rejected by network. See console for more information.";
                }

                return <SModalTitle key={key}>{prefix + content}</SModalTitle>;
              })}
            </SModalContainer>
          ) : (
            <SModalContainer>
              <SModalTitle>{"Call Request Rejected"}</SModalTitle>
            </SModalContainer>
          )}
        </Modal>





            </div>
        </Layout>
    )
}

export default Review;