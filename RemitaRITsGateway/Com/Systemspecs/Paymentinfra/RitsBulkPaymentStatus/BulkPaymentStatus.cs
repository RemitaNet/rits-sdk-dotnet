using Nancy.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsConfig;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsInit;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsBulkPaymentStatus
{
    class BulkPaymentStatus
    {
        private BulkPaymentStatusResponseData result;

        public BulkPaymentStatusResponseData checkBulkPaymentStatus(BulkPaymentStatusPayload bulkPaymentStatusPayload, Credentials credentials)
        {
            BulkPaymentStatusResponseData altResult = new BulkPaymentStatusResponseData();
            if (!Config.isCredentialAvailable(credentials))
            {
                altResult.data = new BulkPaymentStatusDto();
                altResult.data.bulkPaymentStatusInfo = new BulkPaymentStatusInfo();
                altResult.status = SdkResponseCode.CredentialStatus;
                altResult.data.bulkPaymentStatusInfo.responseDescription = Config.emptyCredentialResponse;
                altResult.data.bulkPaymentStatusInfo.responseCode = Config.emptyCredentialResponseCode;
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
                    var client = new RestClient(getHashValues["BULK_PAYMENT_STATUS_URL"]);
                    var request = new RestRequest(RestSharp.Method.POST);
                    long milliseconds = DateTime.Now.Ticks;
                    string requestId = bulkPaymentStatusPayload.RequestId;
                    string hash_string = getHashValues["API_KEY"] + requestId + getHashValues["API_TOKEN"];
                    string hashed = Config.SHA512(hash_string);
                    var batchRef = bulkPaymentStatusPayload.BatchRef;
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
                        batchRef = enUtil.Encrypt(batchRef, getHashValues["KEY"], getHashValues["IV"])
                    };
                    request.AddJsonBody(body);
                    IRestResponse response = null;
                    try
                    {
                        response = client.Execute(request);
                        result = new JavaScriptSerializer().Deserialize<BulkPaymentStatusResponseData>(response.Content);
                    }
                    catch (Exception e1)
                    {
                        altResult.data = new BulkPaymentStatusDto();
                        altResult.data.bulkPaymentStatusInfo = new BulkPaymentStatusInfo();
                        altResult.status = SdkResponseCode.CredentialStatus;
                        altResult.data.bulkPaymentStatusInfo.responseCode = SdkResponseCode.ERROR_WHILE_CONNECTING_CODE;
                        altResult.data.bulkPaymentStatusInfo.responseDescription = SdkResponseCode.ERROR_WHILE_CONNECTING;                        Console.WriteLine(SdkResponseCode.ERROR_WHILE_CONNECTING);
                        Console.WriteLine("ERROR : {0} ", SdkResponseCode.ERROR_WHILE_CONNECTING);
                        return altResult;
                    }
                }
                catch (Exception e2)
                {
                    altResult.data = new BulkPaymentStatusDto();
                    altResult.data.bulkPaymentStatusInfo = new BulkPaymentStatusInfo();
                    altResult.status = SdkResponseCode.CredentialStatus;
                    altResult.data.bulkPaymentStatusInfo.responseCode = SdkResponseCode.ERROR_PROCESSING_REQUEST_CODE;
                    altResult.data.bulkPaymentStatusInfo.responseDescription = SdkResponseCode.ERROR_PROCESSING_REQUEST;
                    Console.WriteLine("ERROR : {0} ", SdkResponseCode.ERROR_PROCESSING_REQUEST);
                    return altResult;
                }
                return result;
            }
        }
    }
}
