using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsBulkPayment
{
    public class BulkPaymentResponseData
    {
        public string status { get; set; }
        public BulkPaymentDto data { get; set; }
    }
}
