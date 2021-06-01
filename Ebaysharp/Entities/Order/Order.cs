using System;
using System.Collections.Generic;
using System.Text;

namespace Ebaysharp.Entities.Order
{
    public class OrdersResponse : BulkResponseBase
    {
        public List<Order> orders { get; set; }
    }

    public class Order
    {
        public Buyer buyer { get; set; }
        public string buyerCheckoutNotes { get; set; }
        public CancelStatus cancelStatus { get; set; }
        public string creationDate { get; set; }
        public bool ebayCollectAndRemitTax { get; set; }
        public List<string> fulfillmentHrefs { get; set; }
        public List<FulfillmentStartInstruction> fulfillmentStartInstructions { get; set; }
        public string lastModifiedDate { get; set; }
        public string legacyOrderId { get; set; }
        public List<LineItem> lineItems { get; set; }
        public string orderFulfillmentStatus { get; set; }
        public enum FulfillmentStatuses
        {
            FULFILLED, IN_PROGRESS, NOT_STARTED
        }
        public string orderId { get; set; }
        public string orderPaymentStatus { get; set; }
        public enum PaymenentStatuses
        {
            FAILED, FULLY_REFUNDED, PAID, PARTIALLY_REFUNDED, PENDING
        }
        public PaymentSummary paymentSummary { get; set; }
        public OrderPricingSummary pricingSummary { get; set; }
        public string salesRecordReference { get; set; }
        public string sellerId { get; set; }
    }

    public class Buyer
    {
        public string username { get; set; }
    }

    public class CancelRequest
    {
        public string cancelCompletedDate { get; set; }
        public string cancelInitiator { get; set; }
        public string cancelReason { get; set; }
        public string cancelRequestedDate { get; set; }
        public string cancelRequestId { get; set; }
        public string cancelRequestState { get; set; }
        public enum CancelRequestStates
        {
            COMPLETED, REJECTED, REQUESTED
        }
    }

    public class CancelStatus
    {
        public string cancelledDate { get; set; }
        public List<CancelRequest> cancelRequests { get; set; }
        public string cancelState { get; set; }
        public enum CancelStates
        {
            CANCELED, IN_PROGRESS, NONE_REQUESTED
        }
    }

    public class FinalDestinationAddress
    {
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string city { get; set; }
        public string countryCode { get; set; }
        public enum CountryCodes
        {
            US
        }
        public string county { get; set; }
        public string postalCode { get; set; }
        public string stateOrProvince { get; set; }
    }

    public class PickupStep
    {
        public string merchantLocationKey { get; set; }
    }

    public class ContactAddress
    {
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string city { get; set; }
        public string countryCode { get; set; }
        public string county { get; set; }
        public string postalCode { get; set; }
        public string stateOrProvince { get; set; }
    }

    public class PrimaryPhone
    {
        public string phoneNumber { get; set; }
    }

    public class ShipTo
    {
        public string companyName { get; set; }
        public ContactAddress contactAddress { get; set; }
        public string email { get; set; }
        public string fullName { get; set; }
        public PrimaryPhone primaryPhone { get; set; }
    }

    public class ShippingStep
    {
        public string shippingCarrierCode { get; set; }
        public string shippingServiceCode { get; set; }
        public ShipTo shipTo { get; set; }
        public string shipToReferenceId { get; set; }
    }

    public class FulfillmentStartInstruction
    {
        public string destinationTimeZone { get; set; }
        public bool ebaySupportedFulfillment { get; set; }
        public FinalDestinationAddress finalDestinationAddress { get; set; }
        public string fulfillmentInstructionsType { get; set; }
        public enum FulfillmentInstructionsTypes
        {
            DIGITAL, PREPARE_FOR_PICKUP, SELLER_DEFINED, SHIP_TO
        }
        public string maxEstimatedDeliveryDate { get; set; }
        public string minEstimatedDeliveryDate { get; set; }
        public PickupStep pickupStep { get; set; }
        public ShippingStep shippingStep { get; set; }
    }

    public class AppliedPromotion
    {
        public string description { get; set; }
        public Charges discountAmount { get; set; }
        public string promotionId { get; set; }
    }

    public class Charges
    {
        public string convertedFromCurrency { get; set; }
        public string convertedFromValue { get; set; }
        public string currency { get; set; }
        public string value { get; set; }
    }

    public class DeliveryCost
    {
        public Charges importCharges { get; set; }
        public Charges shippingCost { get; set; }
        public Charges shippingIntermediationFee { get; set; }
    }

    public class EbayCollectAndRemitTax
    {
        public Charges amount { get; set; }
        public string taxType { get; set; }
        public enum TaxTypes
        {
            GST, PROVINCE_SALES_TAX, REGION, STATE_SALES_TAX, VAT
        }
        public string collectionMethod { get; set; }
        public enum CollectionMethods
        {
            INVOICE, NET
        }
    }

    public class GiftDetails
    {
        public string message { get; set; }
        public string recipientEmail { get; set; }
        public string senderName { get; set; }
    }

    public class LineItemFulfillmentInstructions
    {
        public string destinationTimeZone { get; set; }
        public bool guaranteedDelivery { get; set; }
        public string maxEstimatedDeliveryDate { get; set; }
        public string minEstimatedDeliveryDate { get; set; }
        public string shipByDate { get; set; }
        public string sourceTimeZone { get; set; }
    }

    public class Properties
    {
        public bool buyerProtection { get; set; }
        public bool fromBestOffer { get; set; }
        public bool soldViaAdCampaign { get; set; }
    }

    public class Refund
    {
        public Charges amount { get; set; }
        public string refundDate { get; set; }
        public string refundId { get; set; }
        public string refundReferenceId { get; set; }
    }

    public class OrderTax
    {
        public Charges amount { get; set; }
    }

    public class LineItem
    {
        public List<AppliedPromotion> appliedPromotions { get; set; }
        public DeliveryCost deliveryCost { get; set; }
        public Charges discountedLineItemCost { get; set; }
        public List<EbayCollectAndRemitTax> ebayCollectAndRemitTaxes { get; set; }
        public GiftDetails giftDetails { get; set; }
        public string legacyItemId { get; set; }
        public string legacyVariationId { get; set; }
        public Charges lineItemCost { get; set; }
        public LineItemFulfillmentInstructions lineItemFulfillmentInstructions { get; set; }
        public string lineItemFulfillmentStatus { get; set; }
        public enum LineItemFulfillmentStatus
        {
            FULFILLED, IN_PROGRESS, NOT_STARTED
        }
        public string lineItemId { get; set; }
        public string listingMarketplaceId { get; set; }
        public Properties properties { get; set; }
        public string purchaseMarketplaceId { get; set; }
        public int quantity { get; set; }
        public List<Refund> refunds { get; set; }
        public string sku { get; set; }
        public string soldFormat { get; set; }
        public enum SoldFormat
        {
            AUCTION, FIXED_PRICE, OTHER, SECOND_CHANCE_OFFER
        }
        public List<OrderTax> taxes { get; set; }
        public string title { get; set; }
        public Charges total { get; set; }
    }

    public class SellerActionsToRelease
    {
        public string sellerActionToRelease { get; set; }
    }

    public class PaymentHold
    {
        public string expectedReleaseDate { get; set; }
        public Charges holdAmount { get; set; }
        public string holdReason { get; set; }
        public string holdState { get; set; }
        public string releaseDate { get; set; }
        public List<SellerActionsToRelease> sellerActionsToRelease { get; set; }
    }

    public class Payment
    {
        public Charges amount { get; set; }
        public string paymentDate { get; set; }
        public List<PaymentHold> paymentHolds { get; set; }
        public string paymentMethod { get; set; }
        public enum PaymentMethods { CREDIT_CARD, PAYPAL }
        public string paymentReferenceId { get; set; }
        public string paymentStatus { get; set; }
        public enum PaymentStatuses { FAILED, PAID, PENDING }
    }

    public class Amount5
    {
        public string convertedFromCurrency { get; set; }
        public string convertedFromValue { get; set; }
        public string currency { get; set; }
        public string value { get; set; }
    }

    public class PaymentSummary
    {
        public List<Payment> payments { get; set; }
        public List<Refund> refunds { get; set; }
        public Charges totalDueSeller { get; set; }
    }

    public class OrderPricingSummary
    {
        public Charges adjustment { get; set; }
        public Charges deliveryCost { get; set; }
        public Charges deliveryDiscount { get; set; }
        public Charges fee { get; set; }
        public Charges priceDiscountSubtotal { get; set; }
        public Charges priceSubtotal { get; set; }
        public Charges tax { get; set; }
        public Charges total { get; set; }
    }

    public class Warning
    {
        public string category { get; set; }
        public string domain { get; set; }
        public string errorId { get; set; }
        public List<string> inputRefIds { get; set; }
        public string longMessage { get; set; }
        public string message { get; set; }
        public List<string> outputRefIds { get; set; }
        public List<Parameter> parameters { get; set; }
        public string subdomain { get; set; }
    }
}
