using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsAccountEnquiry
{
    public class AccountEnquiryResponseData
    {
        public string status { get; set; }
        public AccountEnquiryDto data { get; set; }

    }
}
