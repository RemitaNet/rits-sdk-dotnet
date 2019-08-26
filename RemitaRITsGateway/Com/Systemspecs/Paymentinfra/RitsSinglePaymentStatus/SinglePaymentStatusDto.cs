using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsSinglePaymentStatus
{
    public class SinglePaymentStatusDto
    {
        public string authorizationId { get; set; }
        public string transRef { get; set; }
        public string debitAccount { get; set; }
        public string toBank { get; set; }
        public string creditAccount { get; set; }
        public string narration { get; set; }
        public double amount { get; set; }
        public string feeAmount { get; set; }
        public string paymentStatus { get; set; }
        public string settlementDate { get; set; }
        public string paymentDate { get; set; }
        public string currencyCode { get; set; }
        public string paymentStatusCode { get; set; }
        public string paymentState { get; set; }
        public string responseCode { get; set; }
        public string responseDescription { get; set; }
    }
}
