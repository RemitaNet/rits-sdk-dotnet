using Nancy.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsConfig;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsInit;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsSinglePaymentStatus
{
    class SinglePaymentStatus
    {
        private SinglePaymentStatusReponseData result;

        public SinglePaymentStatusReponseData checkSinglePaymentStatus(SinglePaymentStatusPayload singlePaymentStatusPayload, Credentials credentials)
        {
            SinglePaymentStatusReponseData altResult = new SinglePaymentStatusReponseData();
            if (!Config.isCredentialAvailable(credentials))
            {
                altResult.data = new SinglePaymentStatusDto();
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
                    var client = new RestClient(getHashValues["SINGLE_PAYMENT_STATUS_URL"]);
                    var request = new RestRequest(RestSharp.Method.POST);
                    long milliseconds = DateTime.Now.Ticks;
                    string requestId = singlePaymentStatusPayload.RequestId;
                    string hash_string = getHashValues["API_KEY"] + requestId + getHashValues["API_TOKEN"];
                    string hashed = Config.SHA512(hash_string);
                    var transRef = singlePaymentStatusPayload.TransRef;
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
                        transRef = enUtil.Encrypt(transRef, getHashValues["KEY"], getHashValues["IV"])
                    };
                    request.AddJsonBody(body);
                    IRestResponse response = null;
                    try
                    {
                        response = client.Execute(request);
                        result = new JavaScriptSerializer().Deserialize<SinglePaymentStatusReponseData>(response.Content);
                    }
                    catch (Exception e1)
                    {
                        altResult.data = new SinglePaymentStatusDto();
                        altResult.status = SdkResponseCode.CredentialStatus;
                        altResult.data.responseCode = SdkResponseCode.ERROR_WHILE_CONNECTING_CODE;
                        altResult.data.responseDescription = SdkResponseCode.ERROR_WHILE_CONNECTING;
                        Console.WriteLine("ERROR : {0} ", SdkResponseCode.ERROR_WHILE_CONNECTING);
                        return altResult;
                    }
                }
                catch (Exception e2)
                {
                    altResult.data = new SinglePaymentStatusDto();
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
