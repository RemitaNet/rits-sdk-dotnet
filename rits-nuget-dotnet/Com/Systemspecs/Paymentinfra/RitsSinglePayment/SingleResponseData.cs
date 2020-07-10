using System;
using System.Collections.Generic;
  
using System.Text;
  

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsSinglePayment
{
    public class SingleResponseData
    {
        public string status { get; set; }
        public SinglePaymentDto data { get; set; }
    }
}
