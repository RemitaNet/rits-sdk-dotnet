using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    var client = new RestClient(getHashValues["SINGLE_PAYMENT_URL"]);
                    var request = new RestRequest(RestSharp.Method.POST);
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
                    request.AddHeader("API_DETAILS_HASH", hashed);
                    request.AddHeader("REQUEST_TS", Config.getTimeStamp());
                    request.AddHeader("REQUEST_ID", requestId);
                    request.AddHeader("API_KEY", getHashValues["API_KEY"]);
                    request.AddHeader("MERCHANT_ID", getHashValues["MERCHANT_ID"]);
                    request.AddHeader("Content-Type", "application/json");
                    request.Timeout = getEnvTimeOut["TIMEOUT"];
                    request.ReadWriteTimeout = getEnvTimeOut["READ_WRITE_TIMEOUT"];
                    request.RequestFormat = DataFormat.Json;
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
                    request.AddJsonBody(body);
                    IRestResponse response = null;
                    try
                    {
                        response = client.Execute(request);
                        result = new JavaScriptSerializer().Deserialize<SingleResponseData>(response.Content);
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
