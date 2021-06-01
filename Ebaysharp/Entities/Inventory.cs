using System;
using System.Collections.Generic;
using System.Text;

namespace Ebaysharp.Entities
{
    public class InventoryItem
    {
        public string condition { get; set; }
        public Availability availability { get; set; }
        public string conditionDescription { get; set; }
        public List<string> groupIds { get; set; }
        public List<string> inventoryItemGroupKeys { get; set; }
        public PackageWeightAndSize packageWeightAndSize { get; set; }
        public Product product { get; set; }
        public string sku { get; set; }
        public string locale { get; set; }
        public enum Conditions
        {
            NEW, LIKE_NEW, NEW_OTHER, NEW_WITH_DEFECTS, MANUFACTURER_REFURBISHED, SELLER_REFURBISHED, USED_EXCELLENT,
            USED_VERY_GOOD, USED_GOOD, USED_ACCEPTABLE, FOR_PARTS_OR_NOT_WORKING
        }
        public enum Locales
        {
            en_US, en_CA, fr_CA, en_GB, en_AU, en_IN, de_AT, fr_BE, fr_FR, de_DE, it_IT, nl_BE, nl_NL,
            es_ES, de_CH, fi_FI, zh_HK, hu_HU, en_PH, pl_PL, pt_PT, ru_RU, en_SG, en_IE, en_MY
        }
        public decimal? price { get; set; }
    }

    public class InventoryItems: BulkResponseBase
    {
        public List<InventoryItem> inventoryItems { get; set; }

    }

    public class BulkInventoryItem
    {
        public List<InventoryItem> requests { get; set; }
    }

    public class InventoryItemGroup {
        public Dictionary<string, List<string>> aspects { get; set; }
        public string description { get; set; }
        public string inventoryItemGroupKey { get; set; }
        public List<string> imageUrls { get; set; }
        public string subtitle { get; set; }
        public string title { get; set; }
        public List<string> variantSKUs { get; set; }
        public VariesBy variesBy { get; set; }
        public void AddAspect(string name, List<string> values)
        {
            if (aspects == null)
                aspects = new Dictionary<string, List<string>>();
            aspects[name] = values;
        }
        public void AddAspect(string name,string value)
        {
            if (aspects == null)
                aspects = new Dictionary<string, List<string>>();
            if (aspects[name] == null)
                aspects[name] = new List<string>();
            aspects[name].Add(value);
        }
        public void AddImageUrl(string imageUrl)
        {
            if (imageUrls == null)
                imageUrls = new List<string>();
            imageUrls.Add(imageUrl);
        }
        public void AddVariantSKU(string variantSKU)
        {
            if (variantSKUs == null)
                variantSKUs = new List<string>();
            variantSKUs.Add(variantSKU);
        }
    }

    public class VariesBy
    {
        public List<string> aspectsImageVariesBy { get; set; }
        public List<Specification> specifications { get; set; }
        public void AddAspectImageVaryBy(string aspect)
        {
            if (aspectsImageVariesBy == null)
                aspectsImageVariesBy = new List<string>();
            aspectsImageVariesBy.Add(aspect);
        }

    }

    public class Specification
    {
        public string name { get; set; }
        public List<string> values { get; set; }
    }

    public class Availability
    {
        public List<PickupAtLocationAvailability> pickupAtLocationAvailability { get; set; }
        public ShipToLocationAvailability shipToLocationAvailability { get; set; }
    }

    public class PickupAtLocationAvailability
    {
        public string availabilityType { get; set; }
        public FulfillmentTime fulfillmentTime { get; set; }
        public string merchantLocationKey { get; set; }
        public int? quantity { get; set; }
        public enum AvailabilityType { IN_STOCK, OUT_OF_STOCK, SHIP_TO_STORE };

    }

    public class ShipToLocationAvailability
    {
        public int? quantity { get; set; }
    }

    public class FulfillmentTime
    {
        public string unit { get; set; }
        public int? value { get; set; }
        public enum Units { YEAR, MONTH, DAY, HOUR, CALENDAR_DAY, BUSINESS_DAY, MINUTE, SECOND, MILLISECOND }

    }

    public class PackageWeightAndSize
    {
        public Dimensions dimensions { get; set; }
        public string packageType { get; set; }
        public Weight weight { get; set; }
        public enum PackageType
        {
            LETTER, BULKY_GOODS, CARAVAN, CARS, EUROPALLET, EXPANDABLE_TOUGH_BAGS, EXTRA_LARGE_PACK, FURNITURE, INDUSTRY_VEHICLES,
            LARGE_CANADA_POSTBOX, LARGE_CANADA_POST_BUBBLE_MAILER, LARGE_ENVELOPE, MAILING_BOX, MEDIUM_CANADA_POST_BOX, MOTORBIKES,
            MEDIUM_CANADA_POST_BUBBLE_MAILER, ONE_WAY_PALLET, PACKAGE_THICK_ENVELOPE, PADDED_BAGS, PARCEL_OR_PADDED_ENVELOPE, ROLL,
            SMALL_CANADA_POST_BOX, SMALL_CANADA_POST_BUBBLE_MAILER, TOUGH_BAGS, UPS_LETTER, USPS_FLAT_RATE_ENVELOPE, USPS_LARGE_PACK,
            VERY_LARGE_PACK, WINE_PAK
        }
    }

    public class Weight
    {
        public string unit { get; set; }
        public int? value { get; set; }
        public enum WeightUnit { POUND, KILOGRAM, OUNCE, GRAM }
    }

    public class Dimensions
    {
        public int? height { get; set; }
        public int? length { get; set; }
        public int? width { get; set; }
        public string unit { get; set; }
        public enum DimensionUnits { INCH, FEET, CENTIMETER, METER }
    }

    public class Product
    {
        public Dictionary<string,List<string>> aspects { get; set; }
        public string brand { get; set; }
        public string description { get; set; }
        public List<string> imageUrls { get; set; }
        public string mpn { get; set; }
        public string subTitle { get; set; }
        public string title { get; set; }
        public List<string> isbn { get; set; }
        public List<string> upc { get; set; }
        public List<string> ean { get; set; }
        public string epid { get; set; }

        public void AddAspect(string name, List<string> values)
        {
            if (aspects == null)
                aspects = new Dictionary<string, List<string>>();
            aspects[name] = values;
        }
    }

    public class BulkInventoryItemResponses
    {
        public List<InventoryItemResponse> responses { get; set; }
    }

    public class InventoryItemResponse
    {
        public int statusCode { get; set; }
        public string sku { get; set; }
        public string locale { get; set; }
        public List<string> warnings { get; set; }
        public List<string> errors { get; set; }
    }

}
