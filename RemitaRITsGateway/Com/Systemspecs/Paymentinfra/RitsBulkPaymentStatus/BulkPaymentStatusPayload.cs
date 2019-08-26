using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsBulkPaymentStatus
{
    public class BulkPaymentStatusPayload
    {
        private string _batchRef;

        private string _requestId;

        public string BatchRef
        {
            get { return _batchRef; }
            set { _batchRef = value; }
        }
        public string RequestId
        {
            get { return _requestId; }
            set { _requestId = value; }
        }
    }
}
