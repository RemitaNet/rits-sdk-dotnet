using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsBulkPaymentStatus
{
    public class BulkPaymentStatusResponseData
    {
        public string status { get; set; }
        public BulkPaymentStatusDto data { get; set; }
    }
}
