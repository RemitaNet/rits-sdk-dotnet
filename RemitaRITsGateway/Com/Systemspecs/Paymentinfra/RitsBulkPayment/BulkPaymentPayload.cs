using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsBulkPayment
{
    public class BulkPaymentPayload
    {
    public BulkPaymentInfo bulkPaymentInfo;

    public List<PaymentDetails> paymentDetails;
    }
}
