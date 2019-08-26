using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsBulkPaymentStatus
{
    public class BulkPaymentStatusInfo
    {

        public string debitAccountToken { get; set; }

        public string statusCode { get; set; }

        public string statusMessage { get; set; }

        public double totalAmount { get; set; }

        public double feeAmount { get; set; }

        public string currencyCode { get; set; }

        public string responseCode { get; set; }

        public string responseDescription { get; set; }
    }
}
