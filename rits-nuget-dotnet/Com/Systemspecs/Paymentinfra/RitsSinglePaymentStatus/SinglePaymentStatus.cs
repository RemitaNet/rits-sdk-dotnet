  
  
using System;
using System.Collections.Generic;
using System.Text;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsConfig;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsInit;
using Newtonsoft.Json;

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

                    string url = getHashValues["SINGLE_PAYMENT_STATUS_URL"];

                    long milliseconds = DateTime.Now.Ticks;
                    string requestId = singlePaymentStatusPayload.RequestId;
                    string hash_string = getHashValues["API_KEY"] + requestId + getHashValues["API_TOKEN"];
                    string hashed = Config.SHA512(hash_string);
                    var transRef = singlePaymentStatusPayload.TransRef;

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
                        transRef = enUtil.Encrypt(transRef, getHashValues["KEY"], getHashValues["IV"])
                    };

                    try
                    {
                        var response = WebClientUtil.PostResponse(url, JsonConvert.SerializeObject(body), headers);
                        result = JsonConvert.DeserializeObject<SinglePaymentStatusReponseData>(response);
                    }
                    
                    catch (Exception e1)
                    {

                        e1.ToString();
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
                    e2.ToString();
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
