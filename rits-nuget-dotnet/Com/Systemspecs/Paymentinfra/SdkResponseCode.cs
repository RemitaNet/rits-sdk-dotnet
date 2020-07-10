using System;
using System.Collections.Generic;
  
using System.Text;
  

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra
{
    class SdkResponseCode
    {
        public const string CredentialStatus = "fail";

        public const string ERROR_WHILE_CONNECTING_CODE = "251";

        public const string ERROR_WHILE_CONNECTING = "Error occur while connecting to RemitaRITsGateway's service...";

        public const string ERROR_PROCESSING_REQUEST_CODE = "25";

        public const string ERROR_PROCESSING_REQUEST = "Error processing RemitaRITsGateway's request";
        
        public const string INVADE_ENVIRONMENT = "Invalid RemitaRITsGateway Environment...";

    }
}
