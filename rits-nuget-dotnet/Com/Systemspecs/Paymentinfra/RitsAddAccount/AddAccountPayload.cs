using System;
using System.Collections.Generic;
  
using System.Text;
  

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsAddAccount
{
    public class AddAccountPayload
    {
        private string _accountNo;

        private string _bankCode;

        private string _requestId;

        private string _transRef;



        public string AccountNo
        {
            get { return _accountNo; }
            set { _accountNo = value; }
        }

        public string BankCode
        {
            get { return _bankCode; }
            set { _bankCode = value; }
        }

        public string RequestId
        {
            get { return _requestId; }
            set { _requestId = value; }
        }

        public string TransRef
        {
            get { return _transRef; }
            set { _transRef = value; }
        }
    }
}
