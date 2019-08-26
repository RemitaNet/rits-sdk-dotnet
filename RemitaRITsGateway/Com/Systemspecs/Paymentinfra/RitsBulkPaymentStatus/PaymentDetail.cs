using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsBulkPaymentStatus
{
    public class PaymentDetail
    {
        public string transRef { get; set; }
        public string paymentReference { get; set; }
        public string authorizationId { get; set; }
        public string transDate { get; set; }
        public string paymentDate { get; set; }
        public string statusCode { get; set; }
        public string statusMessage { get; set; }
        public double amount { get; set; }
        public string paymentState { get; set; }
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
    }
}
