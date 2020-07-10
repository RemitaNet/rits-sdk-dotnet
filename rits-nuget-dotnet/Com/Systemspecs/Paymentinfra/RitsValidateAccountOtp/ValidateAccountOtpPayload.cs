using System;
using System.Collections.Generic;
  
using System.Text;
  

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsValidateAccountOtp
{
    public class ValidateAccountOtpPayload
    {
        public List<Dictionary<string, string>> _authParams = new List<Dictionary<string, string>>();

        private string _requestId;

        private string _remitaTransRef;

        private string _otp;

        private string _card;

        public string RequestId
        {
            get { return _requestId; }
            set { _requestId = value; }
        }

        public string RemitaTransRef
        {
            get { return _remitaTransRef; }
            set { _remitaTransRef = value; }
        }

        public string Otp
        {
            get { return _otp; }
            set { _otp = value; }
        }

        public string Card
        {
            get { return _card; }
            set { _card = value; }
        }
    }
}
