using Ebaysharp.Entities;

namespace Ebaysharp.Services.Inventory
{
    public class InventoryItemGroupService : RequestService
    {
        public InventoryItemGroupService(ClientToken oauth, AccessToken token) : base(oauth, token)
        {
        }
        
        public void CreateOrReplaceInventoryItemGroup(InventoryItemGroup inventoryItemGroup, string inventoryItemGroupKey)
        {
            CreateRequestWithTokenAndContentLanguage($"{InventoryApiUrls.InventoryItemGroupUrl}/{inventoryItemGroupKey}", RestSharp.Method.PUT);
            var jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(inventoryItemGroup); //need to manually serialize it here due to IDictionary in the model which Restsharp does not serialize it
            Request.AddParameter("application/json", jsonBody, RestSharp.ParameterType.RequestBody);
            ExecuteRequest();
        }

        public InventoryItemGroup GetInventoryItemsGroup(string inventoryItemGroupKey)
        {
            CreateAuthorizedRequest($"{InventoryApiUrls.InventoryItemGroupUrl}/{inventoryItemGroupKey}", RestSharp.Method.GET);
            return ExecuteRequest<InventoryItemGroup>();
        }

        public void DeleteGroupInventoryItems(string inventoryItemGroupKey)
        {
            CreateAuthorizedRequest($"{InventoryApiUrls.InventoryItemGroupUrl}/{inventoryItemGroupKey}", RestSharp.Method.DELETE);
            ExecuteRequest();
        }

    }
}
