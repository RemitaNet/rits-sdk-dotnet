  
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
  
using System;
using System.Collections.Generic;
  
using System.Text;
  
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsConfig;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsInit;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsSinglePayment
{
    class SinglePayment
    {
        private SingleResponseData result;
        public SingleResponseData makeSinglePayment(SinglePaymentPayload singlePaymentPayload, Credentials credentials)
        {
            SingleResponseData altResult = new SingleResponseData();
            if (!Config.isCredentialAvailable(credentials)){
                altResult.data = new SinglePaymentDto();
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

                   string url = getHashValues["SINGLE_PAYMENT_URL"];
                    
                    long milliseconds = DateTime.Now.Ticks;
                    string requestId = singlePaymentPayload.RequestId;
                    string hash_string = getHashValues["API_KEY"] + requestId + getHashValues["API_TOKEN"];
                    string hashed = Config.SHA512(hash_string);
                    var fromBank = singlePaymentPayload.FromBank;
                    var debitAccount = singlePaymentPayload.DebitAccount;
                    var toBank = singlePaymentPayload.ToBank;
                    var creditAccount = singlePaymentPayload.CreditAccount;
                    var narration = singlePaymentPayload.Narration;
                    var amount = singlePaymentPayload.Amount;
                    var beneficiaryEmail = "qa@test.com";
                    var transRef = singlePaymentPayload.TransRef;

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
                        toBank = enUtil.Encrypt(toBank, getHashValues["KEY"], getHashValues["IV"]),
                        creditAccount = enUtil.Encrypt(creditAccount, getHashValues["KEY"], getHashValues["IV"]),
                        narration = enUtil.Encrypt(narration, getHashValues["KEY"], getHashValues["IV"]),
                        amount = enUtil.Encrypt(amount.ToString(), getHashValues["KEY"], getHashValues["IV"]),
                        transRef = enUtil.Encrypt(transRef, getHashValues["KEY"], getHashValues["IV"]),
                        fromBank = enUtil.Encrypt(fromBank, getHashValues["KEY"], getHashValues["IV"]),
                        debitAccount = enUtil.Encrypt(debitAccount, getHashValues["KEY"], getHashValues["IV"]),
                        beneficiaryEmail = enUtil.Encrypt(beneficiaryEmail, getHashValues["KEY"], getHashValues["IV"])
                    };

                    try
                    {
                        var response = WebClientUtil.PostResponse(url, JsonConvert.SerializeObject(body), headers);
                        result = JsonConvert.DeserializeObject<SingleResponseData>(response);
                    }
                   
                    catch (Exception e1)
                    {
                        altResult.data = new SinglePaymentDto();
                        altResult.status = SdkResponseCode.CredentialStatus;
                        altResult.data.responseCode = SdkResponseCode.ERROR_WHILE_CONNECTING_CODE;
                        altResult.data.responseDescription = SdkResponseCode.ERROR_WHILE_CONNECTING;
                        Console.WriteLine("ERROR : {0} ", SdkResponseCode.ERROR_WHILE_CONNECTING);
                        return altResult;
                    }

                }
                catch (Exception e2)
                {
                    altResult.data = new SinglePaymentDto();
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
