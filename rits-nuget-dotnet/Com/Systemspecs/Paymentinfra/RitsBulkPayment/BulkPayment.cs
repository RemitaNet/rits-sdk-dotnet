  
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
  
using System;
using System.Collections;
using System.Collections.Generic;
  
using System.Text;
  
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsConfig;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsInit;


namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsBulkPayment
{
    class BulkPayment
    {
        private BulkPaymentResponseData result;
        public BulkPaymentResponseData makeBulkPayment(BulkPaymentPayload bulkPaymentPayload, Credentials credentials)
        {
            BulkPaymentResponseData altResult = new BulkPaymentResponseData();
            if (!Config.isCredentialAvailable(credentials))
            {
                altResult.data = new BulkPaymentDto();
                altResult.status = SdkResponseCode.CredentialStatus;
                altResult.data.responseDescription = Config.emptyCredentialResponse;
                altResult.data.responseCode = Config.emptyCredentialResponseCode;
                return altResult;
            }
            else
            {
                try
                {
                    EnvironmentConfig environmentConfig = new EnvironmentConfig();
                    Dictionary<string, string> getHashValues = environmentConfig.getRitsEnvironment(credentials);
                    Dictionary<string, int> getEnvTimeOut = environmentConfig.getTimeOut(credentials);
                    EncryptionUtil enUtil = new EncryptionUtil();

                    string url = getHashValues["BULK_PAYMENT_URL"];
                    long milliseconds = DateTime.Now.Ticks;
                    Dictionary<string, Object> paymentData = new Dictionary<string, Object>();
                    List<PaymentDetails> paymentDetailListFrmBulkPayload = bulkPaymentPayload.paymentDetails;
                    List<EncryptPaymentDetails> encryptPaymentListFrmBulkPayloadList = new List<EncryptPaymentDetails>();
                    double sumPaymentDetailAmount = 0;
                    EncryptPaymentDetails encryptPaymentListFrmBulkPayload = null;
                    foreach (PaymentDetails pDetails in paymentDetailListFrmBulkPayload)
                    {
                        encryptPaymentListFrmBulkPayload = new EncryptPaymentDetails();
                        encryptPaymentListFrmBulkPayload.transRef = enUtil.Encrypt(pDetails.transRef, getHashValues["KEY"], getHashValues["IV"]);
                        encryptPaymentListFrmBulkPayload.narration = enUtil.Encrypt(pDetails.narration, getHashValues["KEY"], getHashValues["IV"]);
                        encryptPaymentListFrmBulkPayload.benficiaryEmail = enUtil.Encrypt(pDetails.benficiaryEmail, getHashValues["KEY"], getHashValues["IV"]);
                        encryptPaymentListFrmBulkPayload.benficiaryBankCode = enUtil.Encrypt(pDetails.benficiaryBankCode, getHashValues["KEY"], getHashValues["IV"]);
                        encryptPaymentListFrmBulkPayload.benficiaryAccountNumber = enUtil.Encrypt(pDetails.benficiaryAccountNumber, getHashValues["KEY"], getHashValues["IV"]);
                        encryptPaymentListFrmBulkPayload.amount = enUtil.Encrypt(pDetails.amount.ToString(), getHashValues["KEY"], getHashValues["IV"]);
                        sumPaymentDetailAmount += pDetails.amount;
                        encryptPaymentListFrmBulkPayloadList.Add(encryptPaymentListFrmBulkPayload);
                    }
                    BulkPaymentInfo bulkPaymentInfoFrmBulkPaymentPayload = bulkPaymentPayload.bulkPaymentInfo;
                    var batchRef = enUtil.Encrypt(bulkPaymentInfoFrmBulkPaymentPayload.batchRef, getHashValues["KEY"], getHashValues["IV"]);
                    var debitAccount = enUtil.Encrypt(bulkPaymentInfoFrmBulkPaymentPayload.debitAccount, getHashValues["KEY"], getHashValues["IV"]);
                    var narration = enUtil.Encrypt(bulkPaymentInfoFrmBulkPaymentPayload.narration, getHashValues["KEY"], getHashValues["IV"]);
                    var bankCode = enUtil.Encrypt(bulkPaymentInfoFrmBulkPaymentPayload.bankCode, getHashValues["KEY"], getHashValues["IV"]);
                    var totalAmount = enUtil.Encrypt(sumPaymentDetailAmount.ToString(), getHashValues["KEY"], getHashValues["IV"]);
                    EncryptBulkPaymentInfo encryptedBulkPaymentInfoFrmBulkPaymentPayload = new EncryptBulkPaymentInfo();
                    encryptedBulkPaymentInfoFrmBulkPaymentPayload.batchRef = batchRef;
                    encryptedBulkPaymentInfoFrmBulkPaymentPayload.debitAccount = debitAccount;
                    encryptedBulkPaymentInfoFrmBulkPaymentPayload.narration = narration;
                    encryptedBulkPaymentInfoFrmBulkPaymentPayload.bankCode = bankCode;
                    encryptedBulkPaymentInfoFrmBulkPaymentPayload.totalAmount = totalAmount;
                    string requestId = bulkPaymentInfoFrmBulkPaymentPayload.requestId;
                    string hash_string = getHashValues["API_KEY"] + requestId + getHashValues["API_TOKEN"];
                    string hashed = Config.SHA512(hash_string);

                    //HEADERS
                    List<Header> headers = new List<Header>();
                    headers.Add(new Header { header = "Content-Type", value = "application/json" });
                    headers.Add(new Header { header = "API_DETAILS_HASH", value = hashed });
                    headers.Add(new Header { header = "REQUEST_TS", value = Config.getTimeStamp() });
                    headers.Add(new Header { header = "REQUEST_ID", value = requestId });
                    headers.Add(new Header { header = "API_KEY", value = getHashValues["API_KEY"] });
                    headers.Add(new Header { header = "MERCHANT_ID", value = getHashValues["MERCHANT_ID"] });

                    var body = new
                    {
                        bulkPaymentInfo = encryptedBulkPaymentInfoFrmBulkPaymentPayload,
                        paymentDetails = encryptPaymentListFrmBulkPayloadList
                    };

                    try
                    {
                        var response = WebClientUtil.PostResponse(url, JsonConvert.SerializeObject(body), headers);
                        result = JsonConvert.DeserializeObject<BulkPaymentResponseData>(response);
                    }

                    catch (Exception e1)
                    {
                        altResult.data = new BulkPaymentDto();
                        altResult.status = SdkResponseCode.CredentialStatus;
                        altResult.data.responseCode = SdkResponseCode.ERROR_WHILE_CONNECTING_CODE;
                        altResult.data.responseDescription = SdkResponseCode.ERROR_WHILE_CONNECTING;
                        Console.WriteLine("ERROR : {0} ", SdkResponseCode.ERROR_WHILE_CONNECTING);
                        return altResult;
                    }
                }
                catch (Exception e2)
                {
                    altResult.data = new BulkPaymentDto();
                    altResult.status = SdkResponseCode.CredentialStatus;
                    altResult.data.responseCode = SdkResponseCode.ERROR_PROCESSING_REQUEST_CODE;
                    altResult.data.responseDescription = SdkResponseCode.ERROR_PROCESSING_REQUEST;
                    Console.WriteLine("ERROR : {0} ", SdkResponseCode.ERROR_PROCESSING_REQUEST);
                    return altResult;
                }
                return result;
            }           
        }
    }
}
