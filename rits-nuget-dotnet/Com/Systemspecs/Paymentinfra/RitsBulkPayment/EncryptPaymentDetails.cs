using System;
using System.Collections.Generic;
  
using System.Text;
  

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsBulkPayment
{
    class EncryptPaymentDetails
    {
        public string transRef { get; set; }

        public string paymentReference { get; set; }

        public string narration { get; set; }

        public string benficiaryName { get; set; }

        public string benficiaryEmail { get; set; }

        public string benficiaryPhone { get; set; }

        public string benficiaryLocation { get; set; }

        public string benficiaryBankCode { get; set; }

        public string benficiaryAccountNumber { get; set; }

        public string benficiaryBvn { get; set; }

        public string amount { get; set; }

        public string currencyCode { get; set; }

        public string originalName { get; set; }

        public string originalEmail { get; set; }

        public string originalPhone { get; set; }

        public string originalLocation { get; set; }

        public string originalBankCode { get; set; }

        public string originalAccountNumber { get; set; }

        public string rrrNumber { get; set; }
    }
}
