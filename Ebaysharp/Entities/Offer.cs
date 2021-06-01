using System.Collections.Generic;

namespace Ebaysharp.Entities
{
    public class BulkOffer
    {
        public List<Offer> requests { get; set; }
    }

    public class OffersResponse : BulkResponseBase
    {
        public List<Offer> offers { get; set; }
    }

    public class PublishOffersByInventoryGroup
    {
        public string inventoryItemGroupKey { get; set; }
        public string marketplaceId { get; set; }
    }

    public class OfferBase
    {
        public int? availableQuantity { get; set; }
        public string categoryId { get; set; }
        public string listingDescription { get; set; }
        public ListingPolicy listingPolicies { get; set; }
        public string merchantLocationKey { get; set; }
        public PricingSummary pricingSummary { get; set; }
        public int? quantityLimitPerBuyer { get; set; }
        public Tax tax { get; set; }
        public List<string> storeCategoryNames { get; set; }
        public string listingDuration { get; set; }
        public enum ListingDurations
        {
            GTC
        }
        public int? lotSize { get; set; }
    }
    public class Offer : OfferBase
    {
        public string sku { get; set; }
        public string marketplaceId { get; set; }
        public enum MarketPlaceIds { EBAY_US }
        public string format { get; set; }
        public enum Formats { FIXED_PRICE }
        public Listing listing { get; set; }
        public string offerId { get; set; }
    }

    public class Listing
    {
        public string listingId { get; set; }
        public string listingStatus { get; set; }
        public int soldQuantity { get; set; }
        public enum ListingStatues
        {
            ACTIVE, OUT_OF_STOCK, INACTIVE, ENDED, EBAY_ENDED, NOT_LISTED
        }
    }

    public class Tax
    {
        public bool applyTax { get; set; }
        public string thirdPartyTaxCategory { get; set; }
        public int vatPercentage { get; set; }
    }
    public class PricingSummary
    {
        public string pricingVisibility { get; set; }
        public string originallySoldForRetailPriceOn { get; set; }
        public enum PricingVisibility
        {
            NONE, PRE_CHECKOUT, DURING_CHECKOUT
        }
        public enum OriginallySoldForRetailPriceOn
        {
            ON_EBAY, OFF_EBAY, ON_AND_OFF_EBAY
        }
        public Amount minimumAdvertisedPrice { get; set; }
        public Amount orignalRetailPrice { get; set; }
        public Amount price { get; set; }
    }

    public class ListingPolicy
    {
        public string paymentPolicyId { get; set; }
        public string returnPolicyId { get; set; }
        public List<ShippingCostOverride> shippingCostOverrides { get; set; }
        public string fulfillmentPolicyId { get; set; }
        public bool ebayPlusIfEligible { get; set; }

    }

    public class ShippingCostOverride
    {
        public Amount surcharge { get; set; }
        public Amount additionalShippingCost { get; set; }
        public Amount shippingCost { get; set; }
        public int priority { get; set; }
        public string shippingServiceType { get; set; }
        enum ShippingServiceTypes
        {
            DOMESTIC, INTERNATIONAL
        }
    }

    public class Amount
    {
        public string value { get; set; }
        public enum Currency
        {
            USD
        }
        public string currency { get; set; }
    }

    public class OfferResponse
    {
        public int statusCode { get; set; }
        public string sku { get; set; }
        public string offerId { get; set; }
        public string listingId { get; set; }
        public string marketplaceId { get; set; }
        public string format { get; set; }
        public List<Error> errors { get; set; }
    }
    public class Responses
    {
        public List<OfferResponse> responses { get; set; }
        public string listingId { get; set; }
    }

    public class OfferPublishByInventoryGroupResponse
    {
        public List<Error> warnings { get; set; }
        public string listingId { get; set; }
        public int responseCode { get; set; }
    }

    public class WithdrawByInventoryGroupResponse
    {
        public List<Error> warnings { get; set; }
        public List<Error> errors { get; set; }
        public string listingId { get; set; }
        public int responseCode { get; set; }
    }

    public class BulkOfferPublish
    {
        public List<OfferPublish> requests { get; set; }
    }

    public class OfferPublish
    {
        public string offerId { get; set; }
    }

}
