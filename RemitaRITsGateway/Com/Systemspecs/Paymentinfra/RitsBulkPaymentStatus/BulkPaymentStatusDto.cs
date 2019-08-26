using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsBulkPaymentStatus
{
    public class BulkPaymentStatusDto
    {
        public string bulkRef { get; set; }

        public string batchRef { get; set; }

        public BulkPaymentStatusInfo bulkPaymentStatusInfo { get; set; }

        public IList<PaymentDetail> paymentDetails { get; set; }
    }
}
