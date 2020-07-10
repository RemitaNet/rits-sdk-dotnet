using System;
using System.Collections.Generic;
  
using System.Text;
  

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsValidateAccountOtp
{
    public class ValidateAccountOtpResponseData
    {
        public string status { get; set; }
        public ValidateAccountOtpDto data { get; set; }
    }
}
