using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Ebaysharp.Entities.TradeApi
{
    [XmlRoot("GeteBayDetailsResponse", Namespace = "urn:ebay:apis:eBLBaseComponents")]
    public class GeteBayDetailsResponse
    {
        public GeteBayDetailsResponse()
        {
            ShippingServices = new List<ShippingServiceDetail>();
        }

        public string Timestamp { get; set; }
        public string Ack { get; set; }
        public string Version { get; set; }
        public string Build { get; set; }
        public string UpdateTime { get; set; }
        [XmlElement("ShippingServiceDetails")]
        public List<ShippingServiceDetail> ShippingServices { get; set; }
    }

    public class ShippingServiceDetail
    {
        public ShippingServiceDetail()
        {
            ServiceTypes = new List<string>();
            ShippingPackages = new List<string>();
        }

        [XmlElement("Description")]
        public string Description { get; set; }
        [XmlElement("InternationalService")]
        public bool InternationalService { get; set; }
        [XmlElement("ShippingService")]
        public string ShippingService { get; set; }
        [XmlElement("ShippingServiceID")]
        public int ShippingServiceID { get; set; }
        [XmlElement("ShippingTimeMax")]
        public int ShippingTimeMax { get; set; }
        [XmlElement("ShippingTimeMin")]
        public int ShippingTimeMin { get; set; }
        [XmlElement("ServiceType")]
        public List<string> ServiceTypes { get; set; }
        [XmlElement("ShippingPackage")]
        public List<string> ShippingPackages { get; set; }
        [XmlElement("ValidForSellingFlow")]
        public bool ValidForSellingFlow { get; set; }
        [XmlElement("DetailVersion")]
        public int DetailVersion { get; set; }
        [XmlElement("UpdateTime")]
        public string UpdateTime { get; set; }
        [XmlElement("ShippingCategory")]
        public string ShippingCategory { get; set; }
    }
}
