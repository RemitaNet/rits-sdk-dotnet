using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsInit;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsConfig
{
    class EnvironmentConfig
    {
        private string LIVE_URL = "https://login.remita.net/remita/exapp/api/v1/send/api/rpgsvc/rpg/api/v2/";
        private string TEST_URL = "https://remitademo.net/remita/exapp/api/v1/send/api/rpgsvc/rpg/api/v2/";

        public Dictionary<string, string> getRitsEnvironment(Credentials credentials)
        {
            Dictionary<string, String> environment = new Dictionary<string, String>();
            if (string.IsNullOrEmpty(credentials.Environment))
            {
                credentials.Environment = "TEST";
            }
            switch (credentials.Environment.ToUpper())
            {
                
                case "TEST":
                    environment.Add("MERCHANT_ID", credentials.MerchantId.Trim());
                    environment.Add("API_KEY", credentials.ApiKey.Trim());
                    environment.Add("API_TOKEN", credentials.ApiToken.Trim());
                    environment.Add("KEY", credentials.EncKey.Trim());
                    environment.Add("IV", credentials.EncVector.Trim());
                    environment.Add("ACCOUNT_ENQUIRY_URL", TEST_URL + "merc/fi/account/lookup");
                    environment.Add("GET_BANK_LIST_URL", TEST_URL + "fi/banks");
                    environment.Add("SINGLE_PAYMENT_URL", TEST_URL + "merc/payment/singlePayment.json");
                    environment.Add("SINGLE_PAYMENT_STATUS_URL", TEST_URL + "merc/payment/status");
                    environment.Add("BULK_PAYMENT_STATUS_URL", TEST_URL + "merc/bulk/payment/status");
                    environment.Add("BULK_PAYMENT_URL", TEST_URL + "merc/bulk/payment/send");
                    environment.Add("ADD_ACCOUNT_URL", TEST_URL + "merc/account/token/init");
                    environment.Add("VALIDATE_ACC_OTP__URL", TEST_URL + "merc/account/token/validate");
                    break;
                case "LIVE":
                    environment.Add("MERCHANT_ID", credentials.MerchantId.Trim());
                    environment.Add("API_KEY", credentials.ApiKey.Trim());
                    environment.Add("API_TOKEN", credentials.ApiToken.Trim());
                    environment.Add("KEY", credentials.EncKey.Trim());
                    environment.Add("IV", credentials.EncVector.Trim());
                    environment.Add("ACCOUNT_ENQUIRY_URL", LIVE_URL + "merc/fi/account/lookup");
                    environment.Add("GET_BANK_LIST_URL", LIVE_URL + "fi/banks");
                    environment.Add("SINGLE_PAYMENT_URL", LIVE_URL + "merc/payment/singlePayment.json");
                    environment.Add("SINGLE_PAYMENT_STATUS_URL", LIVE_URL + "merc/payment/status");
                    environment.Add("BULK_PAYMENT_STATUS_URL", LIVE_URL + "merc/bulk/payment/status");
                    environment.Add("BULK_PAYMENT_URL", LIVE_URL + "merc/bulk/payment/send");
                    environment.Add("ADD_ACCOUNT_URL", LIVE_URL + "merc/account/token/init");
                    environment.Add("VALIDATE_ACC_OTP__URL", LIVE_URL + "merc/account/token/validate");
                    break;
                default:
                    Console.WriteLine("REASON : {0} ", SdkResponseCode.INVADE_ENVIRONMENT);
                    break;
            }

            return environment;
        }

        public Dictionary<string, int> getTimeOut(Credentials credentials)
        {
            if (credentials.TimeoutInMilliSec == 0)
            {
                credentials.TimeoutInMilliSec = 30000;
            }
            if (credentials.ReadWriteTimeoutMilliSec == 0)
            {
                credentials.ReadWriteTimeoutMilliSec = 20000;
            }
            Dictionary<string, int> environment = new Dictionary<string, int>();
            environment.Add("TIMEOUT", credentials.TimeoutInMilliSec);
            environment.Add("READ_WRITE_TIMEOUT", credentials.ReadWriteTimeoutMilliSec);
            return environment;
        }
    }
}
