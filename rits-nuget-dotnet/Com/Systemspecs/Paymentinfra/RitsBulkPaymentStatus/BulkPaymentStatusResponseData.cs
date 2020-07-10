using System;
using System.Collections.Generic;
  
using System.Text;
  

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsBulkPaymentStatus
{
    public class BulkPaymentStatusResponseData
    {
        public string status { get; set; }
        public BulkPaymentStatusDto data { get; set; }
    }
}
