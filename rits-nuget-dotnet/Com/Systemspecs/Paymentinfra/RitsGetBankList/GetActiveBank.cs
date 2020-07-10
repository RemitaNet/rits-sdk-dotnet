using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsConfig;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsInit;


namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsGetBankList
{
    class GetActiveBank
    {
        private BankListResponseData result;

        public BankListResponseData getListOfActiveBanks(GetActiveBankPayload getActiveBankPayload, Credentials credentials)
        {
            BankListResponseData altResult = new BankListResponseData();
            if (!Config.isCredentialAvailable(credentials))
            {
                altResult.data = new BankDataDto();
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

                    string url = getHashValues["GET_BANK_LIST_URL"];
                    string requestId = getActiveBankPayload.RequestId;
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

                    try
                    {
                        var response = WebClientUtil.PostResponse(url, "", headers);
                        result  = JsonConvert.DeserializeObject<BankListResponseData>(response);

                    }
                    catch (Exception e1)
                    {
                        e1.ToString();
                        altResult.data = new BankDataDto();
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
                    altResult.data = new BankDataDto();
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
