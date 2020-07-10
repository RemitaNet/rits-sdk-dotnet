using System;
using System.Collections.Generic;
using System.Text;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsInit;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsGetBankList;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsAccountEnquiry;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsAddAccount;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsBulkPayment;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsBulkPaymentStatus;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsSinglePayment;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsSinglePaymentStatus;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsValidateAccountOtp;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsConfig;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra
{
    public class RemitaRITs
    {
        public Credentials NewCredentials { get; set; }

        public RemitaRITs()
        {
        }

        public RemitaRITs(Credentials credentials)
        {
            NewCredentials = credentials;  
        }

        public BankListResponseData getActiveBanks(GetActiveBankPayload getActiveBankPayload)
        {
            GetActiveBank getActiveBank = new GetActiveBank();
            return getActiveBank.getListOfActiveBanks(getActiveBankPayload, NewCredentials);
        }

        public AccountEnquiryResponseData accountEnquiry(AccountEnquiryPayload accountEnquiryPayload)
        {
            AccountEnquiry accountEnquiry = new AccountEnquiry();
            return accountEnquiry.getAccountEnquiry(accountEnquiryPayload, NewCredentials);
        }

        public AddAccountResponseData addAccount(AddAccountPayload addAccountPayload)
        {
            AddAccount addAccount = new AddAccount();
            return addAccount.addBankAccount(addAccountPayload, NewCredentials);
        }

        public BulkPaymentResponseData bulkPayment(BulkPaymentPayload bulkPaymentPayload)
        {
            BulkPayment bulkPayment = new BulkPayment();
            return bulkPayment.makeBulkPayment(bulkPaymentPayload, NewCredentials);
        }

        public BulkPaymentStatusResponseData bulkPaymentStatus(BulkPaymentStatusPayload bulkPaymentStatusPayload)
        {
            BulkPaymentStatus bulkPaymentStatus = new BulkPaymentStatus();
            return bulkPaymentStatus.checkBulkPaymentStatus(bulkPaymentStatusPayload, NewCredentials);
        }

        public SingleResponseData singlePayment(SinglePaymentPayload singlePaymentPayload)
        {
            SinglePayment singlePayment = new SinglePayment();
            return singlePayment.makeSinglePayment(singlePaymentPayload, NewCredentials);

            
        }

        public SinglePaymentStatusReponseData singlePaymentStatus(SinglePaymentStatusPayload singlePaymentStatusPayload)
        {
            SinglePaymentStatus singlePaymentStatus = new SinglePaymentStatus();
            return singlePaymentStatus.checkSinglePaymentStatus(singlePaymentStatusPayload, NewCredentials);
        }

        public ValidateAccountOtpResponseData accountTokenValidation(ValidateAccountOtpPayload validateAccountOtpPayload)
        {
            ValidateAccountOtp validateAccount = new ValidateAccountOtp();
            return validateAccount.validateAccountOtp(validateAccountOtpPayload, NewCredentials);
        }
    }
}
