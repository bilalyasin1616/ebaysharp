using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ebaysharp.Entities
{
    public class CategoryType
    {
        public bool? @default { get; set; }
        [Required]
        public string name { get; set; }
        public enum CategoryTypeEnum
        {
            MOTORS_VEHICLES, ALL_EXCLUDING_MOTORS_VEHICLES
        }
    }

    public class Time
    {
        public string unit { get; set; }
        public int value { get; set; }
        public enum TimeDuration
        {
            YEAR, MONTH, DAY, HOUR, CALENDAR_DAY, BUSINESS_DAY, MINUTE, SECOND, MILLISECOND
        }
    }


    public class Region
    {
        public string regionName { get; set; }
        public string regionType { get; set; }
        public enum RegionTppe
        {
            COUNTRY, COUNTRY_REGION, STATE_OR_PROVINCE, WORLD_REGION, WORLDWIDE
        }
    }

    public class ShipToLocations
    {
        public List<Region> regionExcluded { get; set; }
        public List<Region> regionIncluded { get; set; }
    }

    public class ShippingService
    {
        public Amount additionalShippingCost { get; set; }
        public bool? buyerResponsibleForPickup { get; set; }
        public bool? buyerResponsibleForShipping { get; set; }
        public Amount cashOnDeliveryFee { get; set; }
        public bool? freeShipping { get; set; }
        public string shippingCarrierCode { get; set; }
        public Amount shippingCost { get; set; }
        public string shippingServiceCode { get; set; }
        public ShipToLocations shipToLocations { get; set; }
        public int? sortOrder { get; set; }
        public Amount surcharge { get; set; }
    }

    public class ShippingOption
    {
        public string costType { get; set; }
        public enum CostType
        {
            CALCULATED, FLAT_RATE, NOT_SPECIFIED
        }
        public Amount insuranceFee { get; set; }
        public bool? insuranceOffered { get; set; }
        public string optionType { get; set; }
        public enum OptionType
        {
            DOMESTIC, INTERNATIONAL
        }
        public Amount packageHandlingCost { get; set; }
        public string rateTableId { get; set; }
        public List<ShippingService> shippingServices { get; set; }
    }


    public class FulfillmentPolicy
    {
        [Required]
        public List<CategoryType> categoryTypes { get; set; }
        public string description { get; set; }
        public bool? freightShipping { get; set; }
        public bool? globalShipping { get; set; }
        [Required]
        public Time handlingTime { get; set; }
        public bool? localPickup { get; set; }
        [Required]
        public string marketplaceId { get; set; }
        public enum MarketPlaceID
        {
            EBAY_US
        }
        [Required]
        public string name { get; set; }
        public bool? pickupDropOff { get; set; }
        public List<ShippingOption> shippingOptions { get; set; }
        public ShipToLocations shipToLocations { get; set; }
        public string fulfillmentPolicyId { get; set; }
    }

    public class FulfillmentPolicyResponse
    {
        public List<Error> errors { get; set; }
        public FulfillmentPolicy fulfillmentPolicy { get; set; }
        public int responseCode { get; set; }
    }

    public class GetAllFulfillmentPoliciesResponse
    {
        public List<FulfillmentPolicy> fulfillmentPolicies { get; set; }
    }
}