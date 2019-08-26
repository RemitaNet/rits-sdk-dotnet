using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsValidateAccountOtp
{
    public class ValidateAccountOtpResponseData
    {
        public string status { get; set; }
        public ValidateAccountOtpDto data { get; set; }
    }
}
