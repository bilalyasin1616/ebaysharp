using System.Xml.Serialization;

namespace Ebaysharp.Entities.TradeApi
{
    [XmlRoot(Namespace = "urn:ebay:apis:eBLBaseComponents")]
    public class GeteBayDetailsRequest
    {
        public string DetailName { get; set; }
        public string ErrorLanguage { get; set; }
        public string MessageID { get; set; }
        public string Version { get; set; }
        public string WarningLevel { get; set; }
    }

}
