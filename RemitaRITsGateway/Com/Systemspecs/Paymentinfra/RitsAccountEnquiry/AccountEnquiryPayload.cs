using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsAccountEnquiry
{
    public class AccountEnquiryPayload
    {
         private string _accountNo;

        private string _bankCode;

        private string _requestId;

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
    }
}
