using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ebaysharp.Entities
{
    public class RecipientAccountReference
    {
        public string referenceId { get; set; }
        public string referenceType { get; set; }
        enum ReferenceType
        {
            PAYPAL_EMAIL
        }
    }

    public class PaymentMethod
    {
        public List<string> brands { get; set; }
        public enum Brands
        {
            AMERICAN_EXPRESS, DISCOVER, MASTERCARD, VISA
        }
        public string paymentMethodType { get; set; }
        public enum PaymentMethodType
        {
            CASH_IN_PERSON, CASH_ON_DELIVERY, CASH_ON_PICKUP, CASHIER_CHECK, CREDIT_CARD, ESCROW, INTEGRATED_MERCHANT_CREDIT_CARD,
            LOAN_CHECK, MONEY_ORDER, PAISA_PAY, PAISA_PAY_ESCROW, PAISA_PAY_ESCROW_EMI, PAYPAL, PERSONAL_CHECK, OTHER
        }
        public RecipientAccountReference recipientAccountReference { get; set; }
    }

    public class Deposit
    {
        public Amount amount { get; set; }
        public Time dueIn { get; set; }
        public List<PaymentMethod> paymentMethods { get; set; }
    }


    public class PaymentPolicy
    {
        [Required]
        public List<CategoryType> categoryTypes { get; set; }
        public Deposit deposit { get; set; }
        public string description { get; set; }
        public Time fullPaymentDueIn { get; set; }
        public bool? immediatePay { get; set; }
        [Required]
        public string marketplaceId { get; set; }
        public enum MarketPlaceID
        {
            EBAY_US
        }
        [Required]
        public string name { get; set; }
        public string paymentInstructions { get; set; }
        [Required]
        public List<PaymentMethod> paymentMethods { get; set; }
        public string paymentPolicyId { get; set; }
    }

    public class PaymentPolicyResponse
    {
        public List<Error> errors { get; set; }
        public PaymentPolicy paymentPolicy { get; set; }
        public int responseCode { get; set; }
    }

    public class GetAllPaymentPoliciesResponse
    {
        public List<PaymentPolicy> paymentPolicies { get; set; }
    }
}
