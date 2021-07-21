using Ebaysharp.Entities;
using System.Threading.Tasks;

namespace Ebaysharp.Services.Inventory
{
    public class InventoryItemService : RequestService
    {
        public InventoryItemService(ClientToken oauth, AccessToken token) : base(oauth, token)
        {
        }

        public async Task CreateOrReplaceAsync(InventoryItem inventoryItem, string sku)
        {
            await CreateRequestWithTokenAndContentLanguageAsync($"{InventoryApiUrls.InventoryItemUrl}/{sku}", RestSharp.Method.PUT);
            var jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(inventoryItem);
            Request.AddParameter("application/json", jsonBody, RestSharp.ParameterType.RequestBody);
            await ExecuteRequestAsync();
        }

        public async Task<BulkInventoryItemResponses> BulkCreateOrReplaceAsync(BulkInventoryItem bulkInventoryItem)
        {
            await CreateAuthorizedRequestJsonAsync(InventoryApiUrls.bulkCreateOrReplaceInventoryItemUrl, RestSharp.Method.POST);
            var jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(bulkInventoryItem);
            Request.AddParameter("application/json", jsonBody, RestSharp.ParameterType.RequestBody);
            return await ExecuteRequestAsync<BulkInventoryItemResponses>();
        }

        public async Task<InventoryItem> GetAsync(string sku)
        {
            await CreateAuthorizedRequestAsync($"{InventoryApiUrls.InventoryItemUrl}/{sku}", RestSharp.Method.GET);
            return await ExecuteRequestAsync<InventoryItem>();
        }

        public async Task<EbayList<InventoryItem, EbayFilter>> GetAllAsync(EbayFilter ebayFilter)
        {
            await CreateAuthorizedPagedRequestAsync(ebayFilter, InventoryApiUrls.InventoryItemUrl, RestSharp.Method.GET);
            var response = await ExecuteRequestAsync<InventoryItems>();
            ebayFilter.NextPage = response.next;
            return new EbayList<InventoryItem, EbayFilter>(ebayFilter, response.inventoryItems);
        }

        public async Task DeleteAsync(string sku)
        {
            await CreateAuthorizedRequestAsync($"{InventoryApiUrls.InventoryItemUrl}/{sku}", RestSharp.Method.DELETE);
            await ExecuteRequestAsync();
        }
    }
}
