using System;
using System.Collections.Generic;
  
using System.Text;
  

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsGetBankList
{
    public class BankListResponseData
    {
        public string status { get; set; }
        public BankDataDto data { get; set; }
    }
}
