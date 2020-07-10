using System;
using System.Collections.Generic;
  
using System.Text;
  

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsSinglePayment
{
    public class SinglePaymentPayload
    {
        private string _transRef;
        private string _fromBank;
        private string _debitAccount;
        private string _toBank;
        private string _creditAccount;
        private string _narration;
        private double _amount;
        private string _beneficiaryEmail;
        private string _requestId;

        public string TransRef
        {
            get { return _transRef; }
            set { _transRef = value; }
        }
        public string FromBank
        {
            get { return _fromBank; }
            set { _fromBank = value; }
        }
        public string DebitAccount
        {
            get { return _debitAccount; }
            set { _debitAccount = value; }
        }
        public string ToBank
        {
            get { return _toBank; }
            set { _toBank = value; }
        }
        public string CreditAccount
        {
            get { return _creditAccount; }
            set { _creditAccount = value; }
        }
        public string Narration
        {
            get { return _narration; }
            set { _narration = value; }
        }
        public double Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public string BeneficiaryEmail
        {
            get { return _beneficiaryEmail; }
            set { _beneficiaryEmail = value; }
        }
        public string RequestId
        {
            get { return _requestId; }
            set { _requestId = value; }
        }
 
    }
}
