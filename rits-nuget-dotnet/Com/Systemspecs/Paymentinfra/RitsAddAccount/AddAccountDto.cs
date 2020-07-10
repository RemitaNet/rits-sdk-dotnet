using System;
using System.Collections.Generic;
  
using System.Text;
  

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsAddAccount
{
    public class AuthParam
    {
        public string description2 { get; set; }
        public string label1 { get; set; }
        public string param1 { get; set; }
        public string label2 { get; set; }
        public string description1 { get; set; }
        public string param2 { get; set; }
    }

    public class AddAccountDto
    {
        public string remitaTransRef { get; set; }
        public IList<AuthParam> authParams { get; set; }
        public string responseId { get; set; }
        public string responseCode { get; set; }
        public string responseDescription { get; set; }
        public object mandateNumber { get; set; }
    }
}
