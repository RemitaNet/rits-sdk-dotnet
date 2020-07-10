using System;
using System.Collections.Generic;
using System.Text;
  

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsInit
{
    public class Credentials
    {
        private string _merchantId;
        private string _apiKey;
        private string _apiToken;
        private string _encKey;
        private string _encVector;
        private string _environment;
        public int TimeoutInMilliSec { get; set; }

        public int ReadWriteTimeoutMilliSec { get; set; }

        public string MerchantId
        {
            get { return _merchantId; }
            set { _merchantId = value; }
        }

        public string ApiKey
        {
            get { return _apiKey; }
            set { _apiKey = value; }
        }

        public string ApiToken
        {
            get { return _apiToken; }
            set { _apiToken = value; }
        }

        public string EncKey
        {
            get { return _encKey; }
            set { _encKey = value; }
        }

        public string EncVector
        {
            get { return _encVector; }
            set { _encVector = value; }
        }

        public string Environment
        {
            get { return _environment; }
            set { _environment = value; }
        }

    }
}
