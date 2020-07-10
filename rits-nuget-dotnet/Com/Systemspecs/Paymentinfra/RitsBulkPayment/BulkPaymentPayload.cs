using System;
using System.Collections.Generic;
  
using System.Text;
  

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsBulkPayment
{
    public class BulkPaymentPayload
    {
    public BulkPaymentInfo bulkPaymentInfo;

    public List<PaymentDetails> paymentDetails;
    }
}
