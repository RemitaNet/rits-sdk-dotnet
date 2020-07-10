using System;
using System.Collections.Generic;
  
using System.Text;
  

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsBulkPayment
{
    class EncryptBulkPaymentInfo
    {
        public string totalAmount { get; set; }

        public string paymentDetailsCount { get; set; }

        public string batchRef { get; set; }

        public long tsaServiceId { get; set; }

        public string debitAccount { get; set; }

        public string narration { get; set; }

        public string bankCode { get; set; }

        public string requestId { get; set; }
    }
}
