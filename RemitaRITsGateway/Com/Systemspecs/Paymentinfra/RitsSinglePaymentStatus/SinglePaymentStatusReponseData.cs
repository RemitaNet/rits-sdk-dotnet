using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsSinglePaymentStatus
{
    public class SinglePaymentStatusReponseData
    {
        public string status { get; set; }
        public SinglePaymentStatusDto data { get; set; }
    }
}
