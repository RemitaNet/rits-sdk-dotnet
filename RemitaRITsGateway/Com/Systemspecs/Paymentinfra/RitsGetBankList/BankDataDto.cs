using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsGetBankList;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsGetBankList
{
    public class BankDataDto
    {
        public string responseId { get; set; }
        public string responseCode { get; set; }
        public string responseDescription { get; set; }
        public IList<Bank> banks { get; set; }
    }
}
