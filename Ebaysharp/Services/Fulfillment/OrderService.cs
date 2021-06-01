using Ebaysharp.Entities;
using Ebaysharp.Entities.Order;
using System.Linq;

namespace Ebaysharp.Services.Fulfillment
{
    public class OrderService : RequestService
    {
        public OrderService(ClientToken oauth, AccessToken token) : base(oauth, token)
        {
        }

        public EbayList<Order, EbayFilter> GetAll(EbayFilter ebayFilter)
        {
            CreateAuthorizedPagedRequest(ebayFilter, FulfillmentApiUrls.Order, RestSharp.Method.GET);
            var response = ExecuteRequest<OrdersResponse>();
            ebayFilter.NextPage = response.next;
            return new EbayList<Order, EbayFilter>(ebayFilter, response.orders);
        }

        public string CreateFulfillment(ShippingFulfilment shippingFulfilment, string orderId)
        {
            CreateAuthorizedRequest($"{FulfillmentApiUrls.Order}/{orderId}/{FulfillmentApiUrls.ShippingFulfillment}", RestSharp.Method.POST);
            Request.AddJsonBody(shippingFulfilment);
            var response = ExecuteRequest();
            var locationHeader = response.Headers.ToList().Find(header => header.Name == "Location");
            var fulfillemtId = locationHeader.Value.ToString().Split("/").Last();
            return fulfillemtId;
        }
    }
}
