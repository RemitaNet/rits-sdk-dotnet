using System;
using System.Collections.Generic;
  
using System.Text;
  

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsSinglePayment
{
    public class SinglePaymentDto
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
