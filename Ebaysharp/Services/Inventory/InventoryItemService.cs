using Ebaysharp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ebaysharp.Services.Inventory
{
    public class InventoryItemService : RequestService
    {
        public InventoryItemService(ClientToken oauth, AccessToken token) : base(oauth, token)
        {
        }

        public void CreateOrReplace(InventoryItem inventoryItem, string sku)
        {
            CreateRequestWithTokenAndContentLanguage($"{InventoryApiUrls.InventoryItemUrl}/{sku}", RestSharp.Method.PUT);
            var jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(inventoryItem);
            Request.AddParameter("application/json", jsonBody, RestSharp.ParameterType.RequestBody);
            ExecuteRequest();
        }

        public BulkInventoryItemResponses BulkCreateOrReplace(BulkInventoryItem bulkInventoryItem)
        {
            CreateAuthorizedRequestJson(InventoryApiUrls.bulkCreateOrReplaceInventoryItemUrl, RestSharp.Method.POST);
            var jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(bulkInventoryItem);
            Request.AddParameter("application/json", jsonBody, RestSharp.ParameterType.RequestBody);
            return ExecuteRequest<BulkInventoryItemResponses>();
        }

        public InventoryItem Get(string sku)
        {
            CreateAuthorizedRequest($"{InventoryApiUrls.InventoryItemUrl}/{sku}", RestSharp.Method.GET);
            return ExecuteRequest<InventoryItem>();
        }

        public EbayList<InventoryItem, EbayFilter> GetAll(EbayFilter ebayFilter)
        {
            CreateAuthorizedPagedRequest(ebayFilter, InventoryApiUrls.InventoryItemUrl, RestSharp.Method.GET);
            var response = ExecuteRequest<InventoryItems>();
            ebayFilter.NextPage = response.next;
            return new EbayList<InventoryItem, EbayFilter>(ebayFilter, response.inventoryItems);
        }

        public void Delete(string sku)
        {
            CreateAuthorizedRequest($"{InventoryApiUrls.InventoryItemUrl}/{sku}", RestSharp.Method.DELETE);
            ExecuteRequest();
        }
    }
}
