using System;
using System.Collections.Generic;
using System.Text;

namespace Ebaysharp.Entities
{
    public class LocationsRespones: BulkResponseBase
    {
        public List<InventoryLocation> locations { get; set; }
    }

    public class InventoryLocation
    {

        public Location location { get; set; }
        public string locationAdditionalInformation { get; set; }
        public string locationInstructions { get; set; }
        public List<string> locationTypes { get; set; }
        public enum LocationTypes { STORE, WHAREHOUSE };
        public string locationWebUrl { get; set; }
        public string merchantLocationStatus { get; set; }
        public string merchantLocationKey { get; set; }
        public enum MerchantLocationStatus { ENABLED, DISABLED };
        public string name { get; set; }
        public List<OperatingHour> operatingHours { get; set; }
        public string phone { get; set; }
        public List<SpecialHour> specialHours { get; set; }

    }

    public class Location
    {
        public Address address { get; set; }
        public GeoCoordinates geoCoordinates { get; set; }

    }

    public class Address
    {
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string postalCode { get; set; }
        public string stateOrProvince { get; set; }
    }

    public class GeoCoordinates
    {
        public int? longitude { get; set; }
        public int? latitude { get; set; }
    }

    public class OperatingHour
    {
        public enum DaysOfWeek { MONDAY, TUESDAY, WEDNESDAY, THURSDAY, FRIDAY, SATURDAY, SUNDAY };
        public string dayOfWeekEnum { get; set; }
        public List<Interval> intervals { get; set; }

    }

    public class SpecialHour
    {
        public string date { get; set; }
        public List<Interval> intervals { get; set; }

    }

    public class Interval
    {
        public string open { get; set; }
        public string close { get; set; }
    }
}
