using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsGetBankList
{
    public class BankListResponseData
    {
        public string status { get; set; }
        public BankDataDto data { get; set; }
    }
}
