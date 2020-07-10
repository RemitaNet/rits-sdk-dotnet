  
using Newtonsoft.Json;
  
using System;
using System.Collections.Generic;
  
using System.Text;
  
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsConfig;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsInit;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsValidateAccountOtp
{
    class ValidateAccountOtp
    {
        private ValidateAccountOtpResponseData result;

        public ValidateAccountOtpResponseData validateAccountOtp(ValidateAccountOtpPayload validateAccountOtpPayload, Credentials credentials)
        {
            ValidateAccountOtpResponseData altResult = new ValidateAccountOtpResponseData();
            if (!Config.isCredentialAvailable(credentials))
            {
                altResult.data = new ValidateAccountOtpDto();
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

                    string url = getHashValues["VALIDATE_ACC_OTP__URL"];
                    
                    long milliseconds = DateTime.Now.Ticks;
                    string requestId = validateAccountOtpPayload.RequestId;
                    string hash_string = getHashValues["API_KEY"] + requestId + getHashValues["API_TOKEN"];
                    string hashed = Config.SHA512(hash_string);
                    var transRef = requestId;

                    //HEADERS
                    List<Header> headers = new List<Header>();
                    headers.Add(new Header { header = "Content-Type", value = "application/json" });
                    headers.Add(new Header { header = "API_DETAILS_HASH", value = hashed });
                    headers.Add(new Header { header = "REQUEST_TS", value = Config.getTimeStamp() });
                    headers.Add(new Header { header = "REQUEST_ID", value = requestId });
                    headers.Add(new Header { header = "API_KEY", value = getHashValues["API_KEY"] });
                    headers.Add(new Header { header = "MERCHANT_ID", value = getHashValues["MERCHANT_ID"] });

                    Dictionary<string, string> mpOtp = new Dictionary<string, string>();
                    Dictionary<string, string> mpCard = new Dictionary<string, string>();
                    var remitaTransRef = validateAccountOtpPayload.RemitaTransRef;
                    var authParams = validateAccountOtpPayload._authParams;
                    mpOtp.Add("param1", "OTP");
                    mpOtp.Add("value", enUtil.Encrypt(validateAccountOtpPayload.Otp, getHashValues["KEY"], getHashValues["IV"]));
                    mpCard.Add("param2", "OTP");
                    mpCard.Add("value", enUtil.Encrypt(validateAccountOtpPayload.Card, getHashValues["KEY"], getHashValues["IV"]));
                    List<Dictionary<string, string>> getAuth = new List<Dictionary<string, string>>();
                    getAuth.Add(mpOtp);
                    getAuth.Add(mpCard);
                    var body = new
                    {
                        remitaTransRef = enUtil.Encrypt(remitaTransRef, getHashValues["KEY"], getHashValues["IV"]),
                        authParams = getAuth
                    };

                    try
                    {
                        var response = WebClientUtil.PostResponse(url, JsonConvert.SerializeObject(body), headers);
                        result = JsonConvert.DeserializeObject<ValidateAccountOtpResponseData>(response);
                    }
            
                    catch (Exception e1)
                    {
                        altResult.data = new ValidateAccountOtpDto();
                        altResult.status = SdkResponseCode.CredentialStatus;
                        altResult.data.responseCode = SdkResponseCode.ERROR_WHILE_CONNECTING_CODE;
                        altResult.data.responseDescription = SdkResponseCode.ERROR_WHILE_CONNECTING;
                        Console.WriteLine("ERROR : {0} ", SdkResponseCode.ERROR_WHILE_CONNECTING);
                        return altResult;
                    }
                }
                catch (Exception e2)
                {
                    altResult.data = new ValidateAccountOtpDto();
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
