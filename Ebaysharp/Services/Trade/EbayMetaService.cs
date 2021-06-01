using Ebaysharp.Entities;
using Ebaysharp.Entities.TradeApi;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Ebaysharp.Services.Trade
{
    public class EbayMetaService : RequestService
    {
        public static string ShippingCarrierDetails { get; } = "ShippingCarrierDetails";
        public EbayMetaService(ClientToken oauth, AccessToken token) : base(oauth, token)
        {
        }

        public GeteBayDetailsResponse GetEbayDetails(string detailName = null)
        {
            RefreshToken();
            CreateRequest(EbayXmlProductionUrl, RestSharp.Method.POST);

            Request.AddHeader("X-EBAY-API-IAF-TOKEN", AccessToken.access_token);
            Request.AddHeader("X-EBAY-API-CALL-NAME", "GeteBayDetails");
            Request.AddHeader("X-EBAY-API-SITEID", "0"); //https://developer.ebay.com/devzone/XML/docs/Reference/eBay/types/SiteCodeType.html
            Request.AddHeader("Content-Type", "text/xml");
            Request.AddHeader("X-EBAY-API-COMPATIBILITY-LEVEL", "1179"); //https://developer.ebay.com/DevZone/XML/docs/ReleaseNotes.html
            Request.AddHeader("X-EBAY-API-DETAIL-LEVEL", "0");

            var getEbayDetailsRequest = new GeteBayDetailsRequest();
            getEbayDetailsRequest.DetailName = detailName;

            var xmlserializer = new XmlSerializer(typeof(GeteBayDetailsRequest));
            var stringWriter = new StringWriter();
            using (var writer = XmlWriter.Create(stringWriter))
            {
                xmlserializer.Serialize(writer, getEbayDetailsRequest);
                var xml = stringWriter.ToString();
                Request.AddParameter("Body", xml, "text/xml", RestSharp.ParameterType.RequestBody);
            }

            var response = RequestClient.Execute<GeteBayDetailsResponse>(Request);

            xmlserializer = new XmlSerializer(typeof(GeteBayDetailsResponse));
            using (TextReader reader = new StringReader(response.Content))
            {
                return (GeteBayDetailsResponse)xmlserializer.Deserialize(reader);
            }
        }
    }
}
