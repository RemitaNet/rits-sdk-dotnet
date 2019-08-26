using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsBulkPayment
{
    public class BulkPaymentDto
    {
        public string authorizationId { get; set; }
        public string transRef { get; set; }

        public string transDate { get; set; }

        public string paymentDate { get; set; }

        public string responseId { get; set; }

        public string responseCode { get; set; }

        public string responseDescription { get; set; }

        public string rrr { get; set; }
    }
}
