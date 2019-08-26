using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsSinglePaymentStatus
{
    public class SinglePaymentStatusPayload
    {
        private string _transRef;

        private string _requestId;

        public string TransRef
        {
            get { return _transRef; }
            set { _transRef = value; }
        }
        public string RequestId
        {
            get { return _requestId; }
            set { _requestId = value; }
        }
    }
}
