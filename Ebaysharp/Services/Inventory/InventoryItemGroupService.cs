using Ebaysharp.Entities;
using System.Threading.Tasks;

namespace Ebaysharp.Services.Inventory
{
    public class InventoryItemGroupService : RequestService
    {
        public InventoryItemGroupService(ClientToken oauth, AccessToken token) : base(oauth, token)
        {
        }

        public virtual async Task CreateOrReplaceInventoryItemGroupAsync(InventoryItemGroup inventoryItemGroup, string inventoryItemGroupKey)
        {
            await CreateRequestWithTokenAndContentLanguageAsync($"{InventoryApiUrls.InventoryItemGroupUrl}/{inventoryItemGroupKey}", RestSharp.Method.PUT);
            var jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(inventoryItemGroup); //need to manually serialize it here due to IDictionary in the model which Restsharp does not serialize it
            Request.AddParameter("application/json", jsonBody, RestSharp.ParameterType.RequestBody);
            await ExecuteRequestAsync();
        }

        public virtual async Task<InventoryItemGroup> GetInventoryItemsGroupAsync(string inventoryItemGroupKey)
        {
            await CreateAuthorizedRequestAsync($"{InventoryApiUrls.InventoryItemGroupUrl}/{inventoryItemGroupKey}", RestSharp.Method.GET);
            return await ExecuteRequestAsync<InventoryItemGroup>();
        }

        public virtual async Task DeleteGroupInventoryItemsAsync(string inventoryItemGroupKey)
        {
            await CreateAuthorizedRequestAsync($"{InventoryApiUrls.InventoryItemGroupUrl}/{inventoryItemGroupKey}", RestSharp.Method.DELETE);
            await ExecuteRequestAsync();
        }

    }
}
