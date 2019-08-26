using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsAccountEnquiry
{
    public class AccountEnquiryDto
    {
        public string responseId { get; set; }

        public string responseCode { get; set; }

        public string responseDescription { get; set; }

        public string accountName { get; set; }

        public string accountNo { get; set; }

        public string bankCode { get; set; }

        public string phoneNumber { get; set; }

        public string email { get; set; }

    }
}
