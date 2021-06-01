using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ebaysharp.Entities
{ 

    public class InternationalOverride
    {
        public string returnMethod { get; set; }
        public enum ReturnMethod
        {
            REPLACEMENT
        }
        public Time returnPeriod { get; set; }
        public bool? returnsAccepted { get; set; }
        public string returnShippingCostPayer { get; set; }
        public enum ReturnShippingCostPayer
        {
            BUYER,SELLER
        }
    }

    public class ReturnPolicy
    {
        public List<CategoryType> categoryTypes { get; set; }
        public string description { get; set; }
        public string extendedHolidayReturnsOffered { get; set; }
        public InternationalOverride internationalOverride { get; set; }
        [Required]
        public string marketplaceId { get; set; }
        [Required]
        public string name { get; set; }
        public string refundMethod { get; set; }
        public enum RefundMethod
        {
            MONEY_BACK
        }
        public string restockingFeePercentage { get; set; }
        public string returnInstructions { get; set; }
        public string returnMethod { get; set; }
        public enum ReturnMethod
        {
            REPLACEMENT
        }
        public Time returnPeriod { get; set; }
        [Required]
        public bool returnsAccepted { get; set; }
        public string returnShippingCostPayer { get; set; }
        public enum ReturnShippingCostPayer
        {
            BUYER, SELLER
        }
        public string returnPolicyId { get; set; }
    }

    public class ReturnPolicyResponse
    {
        public List<Error> errors { get; set; }
        public ReturnPolicy returnPolicy { get; set; }
        public int responseCode { get; set; }
    }

    public class GetAllRetrunPoliciesResponse
    {
        public List<ReturnPolicy> returnPolicies { get; set; }
    }
}
