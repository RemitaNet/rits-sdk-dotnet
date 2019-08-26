using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsGetBankList
{
    public class GetActiveBankPayload
    {
        private string _requestId;

        public string RequestId
        {
            get { return _requestId; }
            set { _requestId = value; }
        }

    }
}
