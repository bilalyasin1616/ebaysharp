using Ebaysharp.Entities;

namespace Ebaysharp.Services.Inventory
{
    public class LocationService : RequestService
    {
        public LocationService(ClientToken oauth, AccessToken token) : base(oauth, token)
        {
        }

        public void Add(string marchentLocationKey, InventoryLocation location)
        {
            CreateAuthorizedRequestJson($"{InventoryApiUrls.Location}/{marchentLocationKey}", RestSharp.Method.POST);
            Request.AddJsonBody(location);
            ExecuteRequest();
        }

        public void Delete(string marchentLocationKey)
        {
            CreateAuthorizedRequest($"{InventoryApiUrls.Location}/{marchentLocationKey}", RestSharp.Method.DELETE);
            ExecuteRequest();
        }

        public EbayList<InventoryLocation, EbayFilter> GetAll(EbayFilter ebayFilter)
        {
            CreateAuthorizedPagedRequest(ebayFilter, InventoryApiUrls.Location, RestSharp.Method.GET);
            var response = ExecuteRequest<LocationsRespones>();
            ebayFilter.NextPage = response.next;
            return new EbayList<InventoryLocation, EbayFilter>(ebayFilter, response.locations);
        }

        public InventoryLocation Get(string marchentLocationKey)
        {
            CreateAuthorizedRequest($"{InventoryApiUrls.Location}/{marchentLocationKey}", RestSharp.Method.GET);
            return ExecuteRequest<InventoryLocation>();
        }
    }
}
