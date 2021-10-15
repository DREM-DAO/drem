import algosdk from "algosdk";

const actions = {
  async algodexSell({ dispatch }, { creator, price, assetIndex, amount }) {
    const appIndex = 22045522;
    //"BCAJAAEE0sbBCgaWnqcHAwLoByYBIJklNs5zSM6C9enNAzmPGJKWJbPKuKdkOdPKb9O7+vbGIjUJNAk4IDIDEkQ0CSMINQk0CTIEDED/6jIEJBIzARglEhAzAAAoEhAzAAcxABIQMwEAMQASEDMCADEAEhAzAgAzAhQSEDEAMwMUEhAzAwAoEhAzABAjEhAzARAhBBIQMwIQJBIQMwMQJBIQMwAIgaDCHg8QMwEIIhIQMwISIhIQMwMSIw8QIQUzAhESECEFMwMREhAzAAkyAxIQMwEJMgMSEDMCCTIDEhAzAwkyAxIQMwAZIhIQMwEZIxIQMwIZIhIQMwMZIhIQMwAVMgMSEDMBFTIDEhAzAhUyAxIQMwMVMgMSEEEAAiNDMgQkEjMAGCUSEDMACTIDEhAzABUyAxIQMwEJMgMSEDMBFSgSEDMCCSgSEDMCFTIDEhAzAwkyAxIQMwMVMgMSEDMAADEAEhAzAQAxABIQMwIAMQASEDMDACgSEDMABzIDEhAzARQoEhAzAgcoEhAzAwcoEhAzABAhBBIQMwEQJBIQMwIQIxIQMwMQIxIQMwAIIhIQMwEIIhIQMwESIhIQMwIIIhIQMwISIhIQMwMIIhIQMwMSIhIQMwAZIQYSEDMBGSISEDMCGSISEDMDGSISEEEAAiNDMwIQJBIzAhIiEhAzAgAzAhQSEDMCIDIDEhAzAhUyAxIQMwIAMQATEDUANAAhBwg1AjQAIQYINQMkNAAIMgQSRDMAADEAEjMBADEAExA0AjgAMQASEDMBBygSEDMAECEEEhAzARAjEhAzAhAkEhA0AjgQJBIQNAM4ECMSEDEBIQgOEDMAGCUSEDMACTIDEhAzAQkyAxIQNAI4CTIDEhAzABUyAxIQMwEVMgMSEDQCOBEhBRIQRDcAGgCAFWV4ZWN1dGVfd2l0aF9jbG9zZW91dBJAAEMzABkiEjQCOBUyAxIQNAM4CTIDEhA0AzgBIQgSEDQDOAiB0A8SEDQDOAcxABIQNAM4ADMBABIQNAM4ADEAExBEQgA1MwAZIQcSMwEJMgMSEDQCOBUoEhA0AzgJKBIQNAM4BygSEDQDOAAxABIQNAM4CCISEERCAAAzAQgjDzQCOBIjDxBBADI0AjgSgfWMpgUdNQI1ATMBCIGQTh01BDUDNAE0AwxAAA80ATQDEjQCNAQOEEAAAQAjQyJD"
    const appData = `#pragma version 4
    intcblock 0 1 4 ${22045522} 6 ${assetIndex} 3 2 1000
    bytecblock 0x992536ce7348ce82f5e9cd03398f18929625b3cab8a76439d3ca6fd3bbfaf6c6
    intc_0 // 0
    store 9
    load 9
    gtxns RekeyTo
    global ZeroAddress
    ==
    assert
    load 9
    intc_1 // 1
    +
    store 9
    load 9
    global GroupSize
    <
    bnz label1
    label1:
    global GroupSize
    intc_2 // 4
    ==
    gtxn 1 ApplicationID
    intc_3 // 22045522
    ==
    &&
    gtxn 0 Sender
    bytec_0 // addr TESTNTTTJDHIF5PJZUBTTDYYSKLCLM6KXCTWIOOTZJX5HO7263DPPMM2SU
    ==
    &&
    gtxn 0 Receiver
    txn Sender
    ==
    &&
    gtxn 1 Sender
    txn Sender
    ==
    &&
    gtxn 2 Sender
    txn Sender
    ==
    &&
    gtxn 2 Sender
    gtxn 2 AssetReceiver
    ==
    &&
    txn Sender
    gtxn 3 AssetReceiver
    ==
    &&
    gtxn 3 Sender
    bytec_0 // addr TESTNTTTJDHIF5PJZUBTTDYYSKLCLM6KXCTWIOOTZJX5HO7263DPPMM2SU
    ==
    &&
    gtxn 0 TypeEnum
    intc_1 // 1
    ==
    &&
    gtxn 1 TypeEnum
    intc 4 // 6
    ==
    &&
    gtxn 2 TypeEnum
    intc_2 // 4
    ==
    &&
    gtxn 3 TypeEnum
    intc_2 // 4
    ==
    &&
    gtxn 0 Amount
    pushint 500000
    >=
    &&
    gtxn 1 Amount
    intc_0 // 0
    ==
    &&
    gtxn 2 AssetAmount
    intc_0 // 0
    ==
    &&
    gtxn 3 AssetAmount
    intc_1 // 1
    >=
    &&
    intc 5 // 15322902
    gtxn 2 XferAsset
    ==
    &&
    intc 5 // 15322902
    gtxn 3 XferAsset
    ==
    &&
    gtxn 0 CloseRemainderTo
    global ZeroAddress
    ==
    &&
    gtxn 1 CloseRemainderTo
    global ZeroAddress
    ==
    &&
    gtxn 2 CloseRemainderTo
    global ZeroAddress
    ==
    &&
    gtxn 3 CloseRemainderTo
    global ZeroAddress
    ==
    &&
    gtxn 0 OnCompletion
    intc_0 // 0
    ==
    &&
    gtxn 1 OnCompletion
    intc_1 // 1
    ==
    &&
    gtxn 2 OnCompletion
    intc_0 // 0
    ==
    &&
    gtxn 3 OnCompletion
    intc_0 // 0
    ==
    &&
    gtxn 0 AssetCloseTo
    global ZeroAddress
    ==
    &&
    gtxn 1 AssetCloseTo
    global ZeroAddress
    ==
    &&
    gtxn 2 AssetCloseTo
    global ZeroAddress
    ==
    &&
    gtxn 3 AssetCloseTo
    global ZeroAddress
    ==
    &&
    bz label2
    intc_1 // 1
    return
    label2:
    global GroupSize
    intc_2 // 4
    ==
    gtxn 0 ApplicationID
    intc_3 // 22045522
    ==
    &&
    gtxn 0 CloseRemainderTo
    global ZeroAddress
    ==
    &&
    gtxn 0 AssetCloseTo
    global ZeroAddress
    ==
    &&
    gtxn 1 CloseRemainderTo
    global ZeroAddress
    ==
    &&
    gtxn 1 AssetCloseTo
    bytec_0 // addr TESTNTTTJDHIF5PJZUBTTDYYSKLCLM6KXCTWIOOTZJX5HO7263DPPMM2SU
    ==
    &&
    gtxn 2 CloseRemainderTo
    bytec_0 // addr TESTNTTTJDHIF5PJZUBTTDYYSKLCLM6KXCTWIOOTZJX5HO7263DPPMM2SU
    ==
    &&
    gtxn 2 AssetCloseTo
    global ZeroAddress
    ==
    &&
    gtxn 3 CloseRemainderTo
    global ZeroAddress
    ==
    &&
    gtxn 3 AssetCloseTo
    global ZeroAddress
    ==
    &&
    gtxn 0 Sender
    txn Sender
    ==
    &&
    gtxn 1 Sender
    txn Sender
    ==
    &&
    gtxn 2 Sender
    txn Sender
    ==
    &&
    gtxn 3 Sender
    bytec_0 // addr TESTNTTTJDHIF5PJZUBTTDYYSKLCLM6KXCTWIOOTZJX5HO7263DPPMM2SU
    ==
    &&
    gtxn 0 Receiver
    global ZeroAddress
    ==
    &&
    gtxn 1 AssetReceiver
    bytec_0 // addr TESTNTTTJDHIF5PJZUBTTDYYSKLCLM6KXCTWIOOTZJX5HO7263DPPMM2SU
    ==
    &&
    gtxn 2 Receiver
    bytec_0 // addr TESTNTTTJDHIF5PJZUBTTDYYSKLCLM6KXCTWIOOTZJX5HO7263DPPMM2SU
    ==
    &&
    gtxn 3 Receiver
    bytec_0 // addr TESTNTTTJDHIF5PJZUBTTDYYSKLCLM6KXCTWIOOTZJX5HO7263DPPMM2SU
    ==
    &&
    gtxn 0 TypeEnum
    intc 4 // 6
    ==
    &&
    gtxn 1 TypeEnum
    intc_2 // 4
    ==
    &&
    gtxn 2 TypeEnum
    intc_1 // 1
    ==
    &&
    gtxn 3 TypeEnum
    intc_1 // 1
    ==
    &&
    gtxn 0 Amount
    intc_0 // 0
    ==
    &&
    gtxn 1 Amount
    intc_0 // 0
    ==
    &&
    gtxn 1 AssetAmount
    intc_0 // 0
    ==
    &&
    gtxn 2 Amount
    intc_0 // 0
    ==
    &&
    gtxn 2 AssetAmount
    intc_0 // 0
    ==
    &&
    gtxn 3 Amount
    intc_0 // 0
    ==
    &&
    gtxn 3 AssetAmount
    intc_0 // 0
    ==
    &&
    gtxn 0 OnCompletion
    intc 6 // 3
    ==
    &&
    gtxn 1 OnCompletion
    intc_0 // 0
    ==
    &&
    gtxn 2 OnCompletion
    intc_0 // 0
    ==
    &&
    gtxn 3 OnCompletion
    intc_0 // 0
    ==
    &&
    bz label3
    intc_1 // 1
    return
    label3:
    gtxn 2 TypeEnum
    intc_2 // 4
    ==
    gtxn 2 AssetAmount
    intc_0 // 0
    ==
    &&
    gtxn 2 Sender
    gtxn 2 AssetReceiver
    ==
    &&
    gtxn 2 RekeyTo
    global ZeroAddress
    ==
    &&
    gtxn 2 AssetCloseTo
    global ZeroAddress
    ==
    &&
    gtxn 2 Sender
    txn Sender
    !=
    &&
    store 0
    load 0
    intc 7 // 2
    +
    store 2
    load 0
    intc 6 // 3
    +
    store 3
    intc_2 // 4
    load 0
    +
    global GroupSize
    ==
    assert
    gtxn 0 Sender
    txn Sender
    ==
    gtxn 1 Sender
    txn Sender
    !=
    &&
    load 2
    gtxns Sender
    txn Sender
    ==
    &&
    gtxn 1 Receiver
    bytec_0 // addr TESTNTTTJDHIF5PJZUBTTDYYSKLCLM6KXCTWIOOTZJX5HO7263DPPMM2SU
    ==
    &&
    gtxn 0 TypeEnum
    intc 4 // 6
    ==
    &&
    gtxn 1 TypeEnum
    intc_1 // 1
    ==
    &&
    gtxn 2 TypeEnum
    intc_2 // 4
    ==
    &&
    load 2
    gtxns TypeEnum
    intc_2 // 4
    ==
    &&
    load 3
    gtxns TypeEnum
    intc_1 // 1
    ==
    &&
    txn Fee
    intc 8 // 1000
    <=
    &&
    gtxn 0 ApplicationID
    intc_3 // 22045522
    ==
    &&
    gtxn 0 CloseRemainderTo
    global ZeroAddress
    ==
    &&
    gtxn 1 CloseRemainderTo
    global ZeroAddress
    ==
    &&
    load 2
    gtxns CloseRemainderTo
    global ZeroAddress
    ==
    &&
    gtxn 0 AssetCloseTo
    global ZeroAddress
    ==
    &&
    gtxn 1 AssetCloseTo
    global ZeroAddress
    ==
    &&
    load 2
    gtxns XferAsset
    intc 5 // 15322902
    ==
    &&
    assert
    gtxna 0 ApplicationArgs 0
    pushbytes 0x657865637574655f776974685f636c6f73656f7574 // "execute_with_closeout"
    ==
    bnz label4
    gtxn 0 OnCompletion
    intc_0 // 0
    ==
    load 2
    gtxns AssetCloseTo
    global ZeroAddress
    ==
    &&
    load 3
    gtxns CloseRemainderTo
    global ZeroAddress
    ==
    &&
    load 3
    gtxns Fee
    intc 8 // 1000
    ==
    &&
    load 3
    gtxns Amount
    pushint 2000
    ==
    &&
    load 3
    gtxns Receiver
    txn Sender
    ==
    &&
    load 3
    gtxns Sender
    gtxn 1 Sender
    ==
    &&
    load 3
    gtxns Sender
    txn Sender
    !=
    &&
    assert
    b label5
    label4:
    gtxn 0 OnCompletion
    intc 7 // 2
    ==
    gtxn 1 CloseRemainderTo
    global ZeroAddress
    ==
    &&
    load 2
    gtxns AssetCloseTo
    bytec_0 // addr TESTNTTTJDHIF5PJZUBTTDYYSKLCLM6KXCTWIOOTZJX5HO7263DPPMM2SU
    ==
    &&
    load 3
    gtxns CloseRemainderTo
    bytec_0 // addr TESTNTTTJDHIF5PJZUBTTDYYSKLCLM6KXCTWIOOTZJX5HO7263DPPMM2SU
    ==
    &&
    load 3
    gtxns Receiver
    bytec_0 // addr TESTNTTTJDHIF5PJZUBTTDYYSKLCLM6KXCTWIOOTZJX5HO7263DPPMM2SU
    ==
    &&
    load 3
    gtxns Sender
    txn Sender
    ==
    &&
    load 3
    gtxns Amount
    intc_0 // 0
    ==
    &&
    assert
    b label5
    label5:
    gtxn 1 Amount
    intc_1 // 1
    >=
    load 2
    gtxns AssetAmount
    intc_1 // 1
    >=
    &&
    bz label6
    load 2
    gtxns AssetAmount
    pushint ${price}
    mulw
    store 2
    store 1
    gtxn 1 Amount
    pushint 10000
    mulw
    store 4
    store 3
    load 1
    load 3
    <
    bnz label7
    load 1
    load 3
    ==
    load 2
    load 4
    <=
    &&
    bnz label7
    err
    label7:
    intc_1 // 1
    return
    label6:
    intc_0 // 0
    return`;

    const url = new URL(this.state.config.algod);

    let algodclient = new algosdk.Algodv2(
      this.state.config.algodToken,
      this.state.config.algod,
      url.port
    );
    let suggestedParams = await algodclient.getTransactionParams().do();
    suggestedParams.fee = 1000;
    suggestedParams.flatFee = true;

    let skCreator = null;
    let fromAcctCreator = "";
    console.log("creator.sk", creator);
    if (creator.sk) {
      console.log("creator.sk", creator, creator.sk);
      skCreator = Uint8Array.from(Object.values(creator.sk));
      fromAcctCreator = creator.addr + "";
    } else {
      fromAcctCreator = creator;
      skCreator = await dispatch(
        "wallet/getSK",
        { addr: creator },
        {
          root: true,
        }
      );
      console.log("skCreator", skCreator);
    }
    const appArgs = [
      Uint8Array.from(Buffer.from("b3Blbg==", "base64")),
      Uint8Array.from(Buffer.from(`10000-${price}-0-${assetIndex}`)),
      //Uint8Array.from(Buffer.from("100-1-0-12400859")),
      Uint8Array.from(Buffer.from("Aw==", "base64")),
    ];
    const results = await algodclient.compile(appData).do();
    let buff = Buffer.from(results.result, "base64");

    let str = buff.toString("hex");
    str = str.replace("040c40000032", "040c40ffea32");
    console.log("str", str);
    buff = Buffer.from(str, "hex");
    let b64 = buff.toString("base64");
    console.log("b64", b64);
    let appProgram = new Uint8Array(buff);
    let lsa = new algosdk.LogicSigAccount(appProgram);
    let note = Uint8Array.from(Buffer.from(""));

    const transaction0 = algosdk.makePaymentTxnWithSuggestedParams(
      fromAcctCreator,
      lsa.address(),
      500000,
      undefined,
      note,
      suggestedParams
    );
    const transaction1 = algosdk.makeApplicationOptInTxn(
      lsa.address(),
      suggestedParams,
      appIndex,
      appArgs
    );
    // asa opt in
    const transactionOptions2 = {
      from: lsa.address(),
      to: lsa.address(),
      assetIndex,
      amount: 0,
      note,
      suggestedParams,
    };
    const transaction2 = algosdk.makeAssetTransferTxnWithSuggestedParamsFromObject(
      transactionOptions2
    );

    const transactionOptions3 = {
      from: fromAcctCreator,
      to: lsa.address(),
      assetIndex,
      amount,
      note,
      suggestedParams,
    };
    const transaction3 = algosdk.makeAssetTransferTxnWithSuggestedParamsFromObject(
      transactionOptions3
    );
    let txns = [transaction0, transaction1, transaction2, transaction3];
    algosdk.assignGroupID(txns);
    console.log("txns", txns);

    let signedTxn0 = transaction0.signTxn(skCreator);
    let signedTxn1 = algosdk.signLogicSigTransactionObject(transaction1, lsa)
      .blob;
    let signedTxn2 = algosdk.signLogicSigTransactionObject(transaction2, lsa)
      .blob;
    let signedTxn3 = transaction3.signTxn(skCreator);

    let signed = [];
    signed.push(signedTxn0);
    signed.push(signedTxn1);
    signed.push(signedTxn2);
    signed.push(signedTxn3);

    const ret = await algodclient
      .sendRawTransaction(signed)
      .do()
      .catch((e) => {
        if (e && e.response && e.response.body && e.response.body.message) {
          dispatch("toast/openError", e.response.body.message, {
            root: true,
          });
        }
        console.log("e", e, e.message, e.data);

        for (var key in e) {
          console.log("e.key", key, e[key]);
        }
      });
    return ret;
  },
  async algodexBuy({ dispatch }, { creator }) {
    try {
      console.log("makeApplicationNoOpTxn", {
        creator,
        appIndex,
      });
      const url = new URL(this.state.config.algod);
      const appIndex = 22045503;
      let algodclient = new algosdk.Algodv2(
        this.state.config.algodToken,
        this.state.config.algod,
        url.port
      );

      if (algodclient == null) {
        throw "Bad arguments.";
      }
      let suggestedParams = await algodclient.getTransactionParams().do();
      suggestedParams.fee = 1000;
      suggestedParams.flatFee = true;

      let skCreator = null;
      let fromAcctCreator = "";
      console.log("creator.sk", creator);
      if (creator.sk) {
        console.log("creator.sk", creator, creator.sk);
        skCreator = Uint8Array.from(Object.values(creator.sk));
        fromAcctCreator = creator.addr + "";
      } else {
        fromAcctCreator = creator;
        skCreator = await dispatch(
          "wallet/getSK",
          { addr: creator },
          {
            root: true,
          }
        );
        console.log("skCreator", skCreator);
      }
      const price = 20004;
      const appArgs = [
        Uint8Array.from(Buffer.from("b3Blbg==", "base64")),
        Uint8Array.from(Buffer.from(`1000000-${price}-0-12400859`)),
        //Uint8Array.from(Buffer.from("100-1-0-12400859")),
        Uint8Array.from(Buffer.from("Aw==", "base64")),
      ];

      const appData = `#pragma version 4
      intcblock 1 0 3 6 4 2 ${appIndex} 12400859 1000
      bytecblock 0x992536ce7348ce82f5e9cd03398f18929625b3cab8a76439d3ca6fd3bbfaf6c6
      intc_1 // 0
      store 9
      load 9
      gtxns RekeyTo
      global ZeroAddress
      ==
      assert
      load 9
      gtxns AssetCloseTo
      global ZeroAddress
      ==
      assert
      load 9
      intc_0 // 1
      +
      store 9
      load 9
      global GroupSize
      <
      bnz label1
      label1:
      global GroupSize
      intc 5 // 2
      ==
      global GroupSize
      intc_2 // 3
      ==
      ||
      gtxn 0 TypeEnum
      intc_0 // 1
      ==
      &&
      gtxn 1 TypeEnum
      intc_3 // 6
      ==
      &&
      gtxn 0 Amount
      pushint 500000
      >=
      &&
      gtxn 1 Amount
      intc_1 // 0
      ==
      &&
      gtxn 0 CloseRemainderTo
      global ZeroAddress
      ==
      &&
      gtxn 1 CloseRemainderTo
      global ZeroAddress
      ==
      &&
      gtxn 0 OnCompletion
      intc_1 // 0
      ==
      &&
      gtxn 1 OnCompletion
      intc_0 // 1
      ==
      &&
      gtxn 1 ApplicationID
      intc 6 // 22045503
      ==
      &&
      gtxn 0 Sender
      bytec_0 // addr TESTNTTTJDHIF5PJZUBTTDYYSKLCLM6KXCTWIOOTZJX5HO7263DPPMM2SU
      ==
      &&
      gtxn 1 Sender
      txn Sender
      ==
      &&
      gtxn 0 Receiver
      txn Sender
      ==
      &&
      store 0
      global GroupSize
      intc 5 // 2
      ==
      store 1
      load 1
      bnz label2
      gtxn 2 TypeEnum
      intc 4 // 4
      ==
      gtxn 2 AssetAmount
      intc_1 // 0
      ==
      &&
      gtxn 2 Sender
      bytec_0 // addr TESTNTTTJDHIF5PJZUBTTDYYSKLCLM6KXCTWIOOTZJX5HO7263DPPMM2SU
      ==
      &&
      gtxn 2 OnCompletion
      intc_1 // 0
      ==
      &&
      intc 7 // 12400859
      gtxn 2 XferAsset
      ==
      &&
      store 1
      label2:
      load 0
      load 1
      &&
      bz label3
      intc_0 // 1
      return
      label3:
      global GroupSize
      intc_2 // 3
      ==
      gtxn 0 CloseRemainderTo
      global ZeroAddress
      ==
      &&
      gtxn 1 CloseRemainderTo
      bytec_0 // addr TESTNTTTJDHIF5PJZUBTTDYYSKLCLM6KXCTWIOOTZJX5HO7263DPPMM2SU
      ==
      &&
      gtxn 2 CloseRemainderTo
      global ZeroAddress
      ==
      &&
      gtxn 0 Sender
      txn Sender
      ==
      &&
      gtxn 1 Sender
      txn Sender
      ==
      &&
      gtxn 2 Sender
      bytec_0 // addr TESTNTTTJDHIF5PJZUBTTDYYSKLCLM6KXCTWIOOTZJX5HO7263DPPMM2SU
      ==
      &&
      gtxn 0 TypeEnum
      intc_3 // 6
      ==
      &&
      gtxn 1 TypeEnum
      intc_0 // 1
      ==
      &&
      gtxn 2 TypeEnum
      intc_0 // 1
      ==
      &&
      gtxn 0 Amount
      intc_1 // 0
      ==
      &&
      gtxn 1 Amount
      intc_1 // 0
      ==
      &&
      gtxn 2 Amount
      intc_1 // 0
      ==
      &&
      gtxn 0 OnCompletion
      intc_2 // 3
      ==
      &&
      gtxn 1 OnCompletion
      intc_1 // 0
      ==
      &&
      gtxn 2 OnCompletion
      intc_1 // 0
      ==
      &&
      bz label4
      intc_0 // 1
      return
      label4:
      gtxn 1 CloseRemainderTo
      global ZeroAddress
      ==
      bnz label5
      gtxn 0 OnCompletion
      intc 5 // 2
      ==
      global GroupSize
      intc_2 // 3
      ==
      &&
      gtxn 0 TypeEnum
      intc_3 // 6
      ==
      &&
      gtxn 1 TypeEnum
      intc_0 // 1
      ==
      &&
      gtxn 2 TypeEnum
      intc 4 // 4
      ==
      &&
      txn Fee
      intc 8 // 1000
      <=
      &&
      gtxn 0 ApplicationID
      intc 6 // 22045503
      ==
      &&
      gtxn 0 Sender
      txn Sender
      ==
      &&
      gtxn 1 Sender
      txn Sender
      ==
      &&
      gtxn 2 Sender
      txn Sender
      !=
      &&
      gtxn 1 Receiver
      gtxn 2 Sender
      ==
      &&
      gtxn 2 AssetReceiver
      bytec_0 // addr TESTNTTTJDHIF5PJZUBTTDYYSKLCLM6KXCTWIOOTZJX5HO7263DPPMM2SU
      ==
      &&
      gtxn 0 CloseRemainderTo
      global ZeroAddress
      ==
      &&
      gtxn 1 CloseRemainderTo
      bytec_0 // addr TESTNTTTJDHIF5PJZUBTTDYYSKLCLM6KXCTWIOOTZJX5HO7263DPPMM2SU
      ==
      &&
      gtxn 2 CloseRemainderTo
      global ZeroAddress
      ==
      &&
      assert
      b label6
      label5:
      gtxn 0 OnCompletion
      intc_1 // 0
      ==
      txn CloseRemainderTo
      global ZeroAddress
      ==
      &&
      global GroupSize
      intc 4 // 4
      ==
      &&
      gtxn 0 TypeEnum
      intc_3 // 6
      ==
      &&
      gtxn 1 TypeEnum
      intc_0 // 1
      ==
      &&
      gtxn 2 TypeEnum
      intc 4 // 4
      ==
      &&
      gtxn 3 TypeEnum
      intc_0 // 1
      ==
      &&
      gtxn 3 Amount
      pushint 2000
      >=
      &&
      gtxn 3 Receiver
      txn Sender
      ==
      &&
      gtxn 3 Sender
      gtxn 1 Receiver
      ==
      &&
      gtxn 0 Sender
      txn Sender
      ==
      &&
      gtxn 1 Sender
      txn Sender
      ==
      &&
      gtxn 2 Sender
      txn Sender
      !=
      &&
      gtxn 3 Sender
      txn Sender
      !=
      &&
      txn Fee
      intc 8 // 1000
      <=
      &&
      gtxn 0 ApplicationID
      intc 6 // 22045503
      ==
      &&
      gtxn 1 Sender
      txn Sender
      ==
      &&
      gtxn 1 Receiver
      gtxn 2 Sender
      ==
      &&
      gtxn 2 AssetReceiver
      bytec_0 // addr TESTNTTTJDHIF5PJZUBTTDYYSKLCLM6KXCTWIOOTZJX5HO7263DPPMM2SU
      ==
      &&
      gtxn 0 CloseRemainderTo
      global ZeroAddress
      ==
      &&
      gtxn 1 CloseRemainderTo
      global ZeroAddress
      ==
      &&
      gtxn 2 CloseRemainderTo
      global ZeroAddress
      ==
      &&
      gtxn 3 CloseRemainderTo
      global ZeroAddress
      ==
      &&
      assert
      label6:
      gtxn 1 Amount
      intc_0 // 1
      >=
      gtxn 2 AssetAmount
      intc_0 // 1
      >=
      &&
      intc 7 // 12400859
      gtxn 2 XferAsset
      ==
      &&
      assert
      gtxn 2 AssetAmount
      pushint ${price}
      mulw
      store 2
      store 1
      gtxn 1 Amount
      pushint 1000000
      mulw
      store 4
      store 3
      load 1
      load 3
      >
      bnz label7
      load 1
      load 3
      ==
      load 2
      load 4
      >=
      &&
      bnz label7
      err
      label7:
      intc_0 // 1
      return`;
      const results = await algodclient.compile(appData).do();
      let buff = Buffer.from(results.result, "base64");
      //const program = new Uint8Array();
      /*
      let buff = Buffer.from(
        `BCAJAQADBgQCv8bBCtvx9AXoByYBIJklNs5zSM6C9enNAzmPGJKWJbPKuKdkOdPKb9O7+vbGIzUJ
        NAk4IDIDEkQ0CTgVMgMSRDQJIgg1CTQJMgQMQAAAMgQhBRIyBCQSETMAECISEDMBECUSEDMACIGg
        wh4PEDMBCCMSEDMACTIDEhAzAQkyAxIQMwAZIxIQMwEZIhIQMwEYIQYSEDMAACgSEDMBADEAEhAz
        AAcxABIQNQAyBCEFEjUBNAFAACEzAhAhBBIzAhIjEhAzAgAoEhAzAhkjEhAhBzMCERIQNQE0ADQB
        EEEAAiJDMgQkEjMACTIDEhAzAQkoEhAzAgkyAxIQMwAAMQASEDMBADEAEhAzAgAoEhAzABAlEhAz
        ARAiEhAzAhAiEhAzAAgjEhAzAQgjEhAzAggjEhAzABkkEhAzARkjEhAzAhkjEhBBAAIiQzMBCTID
        EkAAZjMAGSEFEjIEJBIQMwAQJRIQMwEQIhIQMwIQIQQSEDEBIQgOEDMAGCEGEhAzAAAxABIQMwEA
        MQASEDMCADEAExAzAQczAgASEDMCFCgSEDMACTIDEhAzAQkoEhAzAgkyAxIQREIAnDMAGSMSMQky
        AxIQMgQhBBIQMwAQJRIQMwEQIhIQMwIQIQQSEDMDECISEDMDCIHQDw8QMwMHMQASEDMDADMBBxIQ
        MwAAMQASEDMBADEAEhAzAgAxABMQMwMAMQATEDEBIQgOEDMAGCEGEhAzAQAxABIQMwEHMwIAEhAz
        AhQoEhAzAAkyAxIQMwEJMgMSEDMCCTIDEhAzAwkyAxIQRDMBCCIPMwISIg8QIQczAhESEEQzAhKB
        o5wBHTUCNQEzAQiBwIQ9HTUENQM0ATQDDUAADzQBNAMSNAI0BA8QQAABACJD`,
        "base64"
      );*/
      let str = buff.toString("hex");
      str = str.replace("0c40000032", "0c40ffe232");
      console.log("str", str);
      buff = Buffer.from(str, "hex");
      let b64 = buff.toString("base64");
      console.log("b64", b64);
      let appProgram = new Uint8Array(buff);

      let lsa = new algosdk.LogicSigAccount(appProgram);
      //let lsaSign = lsa.sign(sk);
      console.log("lsa", lsa);
      console.log("lsa.address", lsa.address());
      //if (lsa.address()) return;
      let amount = price * 100;
      let note = Uint8Array.from(Buffer.from(""));
      const transaction0 = algosdk.makePaymentTxnWithSuggestedParams(
        fromAcctCreator,
        lsa.address(),
        amount,
        undefined,
        note,
        suggestedParams
      );

      const transaction1 = algosdk.makeApplicationOptInTxn(
        lsa.address(),
        suggestedParams,
        appIndex,
        appArgs
      );
      console.log("transaction1", transaction1, skCreator);

      const assetIndex = 12400859;
      const transactionOptions = {
        from: fromAcctCreator,
        to: fromAcctCreator,
        assetIndex,
        amount: 0,
        note,
        suggestedParams,
      };
      console.log("transactionOptions", transactionOptions);
      /*
      const txtOptIn = algosdk.makeAssetTransferTxnWithSuggestedParamsFromObject(
        transactionOptions
      );*/

      let txns = [transaction0, transaction1];
      let txgroup = algosdk.assignGroupID(txns);
      console.log("txgroup", txns, txgroup);

      let signedTxn0 = transaction0.signTxn(skCreator);
      //let signedTxn3 = txtOptIn.signTxn(skCreator);
      console.log("signedTxn0", signedTxn0);
      let signedTxn1 = algosdk.signLogicSigTransactionObject(transaction1, lsa)
        .blob;
      console.log("lsa", lsa, signedTxn1);
      //let signedTxn1 = transaction1.signTxn(sk);
      console.log("signedTxn1", signedTxn1);

      let signed = [];
      signed.push(signedTxn0);
      signed.push(signedTxn1);
      //signed.push(signedTxn3);
      console.log("signed", signed);

      const ret = await algodclient
        .sendRawTransaction(signed)
        .do()
        .catch((e) => {
          if (e && e.response && e.response.body && e.response.body.message) {
            dispatch("toast/openError", e.response.body.message, {
              root: true,
            });
          }
          console.log("e", e, e.message, e.data);

          for (var key in e) {
            console.log("e.key", key, e[key]);
          }
        });
      return ret;
    } catch (error) {
      console.log("error", error, dispatch);
    }
  },
};

export default {
  namespaced: true,
  actions,
};
