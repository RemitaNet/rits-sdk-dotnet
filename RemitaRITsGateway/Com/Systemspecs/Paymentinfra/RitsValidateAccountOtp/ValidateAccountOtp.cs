using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    var client = new RestClient(getHashValues["VALIDATE_ACC_OTP__URL"]);
                    var request = new RestRequest(RestSharp.Method.POST);
                    long milliseconds = DateTime.Now.Ticks;
                    string requestId = validateAccountOtpPayload.RequestId;
                    string hash_string = getHashValues["API_KEY"] + requestId + getHashValues["API_TOKEN"];
                    string hashed = Config.SHA512(hash_string);
                    var transRef = requestId;
                    request.AddHeader("API_DETAILS_HASH", hashed);
                    request.AddHeader("REQUEST_TS", Config.getTimeStamp());
                    request.AddHeader("REQUEST_ID", requestId);
                    request.AddHeader("API_KEY", getHashValues["API_KEY"]);
                    request.AddHeader("MERCHANT_ID", getHashValues["MERCHANT_ID"]);
                    request.AddHeader("Content-Type", "application/json");
                    request.Timeout = getEnvTimeOut["TIMEOUT"];
                    request.ReadWriteTimeout = getEnvTimeOut["READ_WRITE_TIMEOUT"];
                    request.RequestFormat = DataFormat.Json;
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
                    request.AddJsonBody(body);
                    IRestResponse response = null;
                    try
                    {
                        response = client.Execute(request);
                        result = new JavaScriptSerializer().Deserialize<ValidateAccountOtpResponseData>(response.Content);
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
