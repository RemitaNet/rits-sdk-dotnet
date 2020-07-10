using System;
using System.Collections.Generic;
  
using System.Text;
  

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsValidateAccountOtp
{
    public class ValidateAccountOtpDto
    {
        public string remitaTransRef { get; set; }
        public string accountToken { get; set; }
        public string responseId { get; set; }
        public string responseCode { get; set; }
        public string responseDescription { get; set; }
    }
}
