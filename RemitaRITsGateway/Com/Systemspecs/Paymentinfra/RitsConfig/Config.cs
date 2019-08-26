using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsInit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsConfig
{
    static class Config
    {

        public static string emptyCredentialResponse { get; set; }
        public static string emptyCredentialResponseCode { get; set; }

        public static string SHA512(string hash_string)
        {
            System.Security.Cryptography.SHA512Managed sha512 = new System.Security.Cryptography.SHA512Managed();
            Byte[] EncryptedSHA512 = sha512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(hash_string));
            sha512.Clear();
            string hashed = BitConverter.ToString(EncryptedSHA512).Replace("-", "").ToLower();
            return hashed;
        }

        public static string getTimeStamp()
        {
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var day = DateTime.Now.Day;
            var hour = DateTime.Now.Hour;
            var minute = DateTime.Now.Minute;
            var second = DateTime.Now.Second;
            var timeStamp = year + "-" + month + "-" + day + "T" + hour + ":" + minute + ":" + second + "+000000";

            return timeStamp;
        }

        public static bool isCredentialAvailable(Credentials credentials)
        {
            if (string.IsNullOrEmpty(credentials.ApiKey))
            {
                emptyCredentialResponse = "APIKEY cannot be empty";
                emptyCredentialResponseCode = "012";
;                return false;
            }
            else if (string.IsNullOrEmpty(credentials.ApiToken))
            {
                emptyCredentialResponse = "APITOKEN cannot be empty";
                emptyCredentialResponseCode = "013";
                return false;
            }
            else if (string.IsNullOrEmpty(credentials.MerchantId))
            {
                emptyCredentialResponse = "MERCHANTID cannot be empty ";
                emptyCredentialResponseCode = "011";
                return false;
            }
            else if (string.IsNullOrEmpty(credentials.EncKey))
            {
                emptyCredentialResponse = "ENCKEY cannot be empty ";
                emptyCredentialResponseCode = "014";
                return false;
            }
            else if (string.IsNullOrEmpty(credentials.EncVector))
            {
                emptyCredentialResponse = "ENCVECTOR cannot be empty ";
                emptyCredentialResponseCode = "015";
                return false;
            }
            else if (string.IsNullOrEmpty(credentials.Environment) || credentials.Environment.ToUpper() == "TEST")
            {
                Console.WriteLine("============ You're on RemitaRITsGateway TEST Environment ============");
                return true;
            }
            else
            {
                return true;
            }
        }

    }
}
