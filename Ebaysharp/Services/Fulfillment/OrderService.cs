using Ebaysharp.Entities;
using Ebaysharp.Entities.Order;
using System.Linq;
using System.Threading.Tasks;

namespace Ebaysharp.Services.Fulfillment
{
    public class OrderService : RequestService
    {
        public OrderService(ClientToken oauth, AccessToken token) : base(oauth, token)
        {
        }

        public virtual async Task<EbayList<Order, EbayFilter>> GetAllAsync(EbayFilter ebayFilter)
        {
            await CreateAuthorizedPagedRequestAsync(ebayFilter, FulfillmentApiUrls.Order, RestSharp.Method.GET);
            var response = await ExecuteRequestAsync<OrdersResponse>();
            ebayFilter.NextPage = response.next;
            return new EbayList<Order, EbayFilter>(ebayFilter, response.orders);
        }

        public virtual async Task<string> CreateFulfillmentAsync(ShippingFulfilment shippingFulfilment, string orderId)
        {
            await CreateAuthorizedRequestAsync($"{FulfillmentApiUrls.Order}/{orderId}/{FulfillmentApiUrls.ShippingFulfillment}", RestSharp.Method.POST);
            Request.AddJsonBody(shippingFulfilment);
            var response = await ExecuteRequestAsync();
            var locationHeader = response.Headers.ToList().Find(header => header.Name == "Location");
            var fulfillemtId = locationHeader.Value.ToString().Split("/").Last();
            return fulfillemtId;
        }
    }
}
