using Ebaysharp.Entities;
using System.Threading.Tasks;

namespace Ebaysharp.Services.Inventory
{
    public class LocationService : RequestService
    {
        public LocationService(ClientToken oauth, AccessToken token) : base(oauth, token)
        {
        }

        public async Task AddAsync(string marchentLocationKey, InventoryLocation location)
        {
            await CreateAuthorizedRequestJsonAsync($"{InventoryApiUrls.Location}/{marchentLocationKey}", RestSharp.Method.POST);
            Request.AddJsonBody(location);
            await ExecuteRequestAsync();
        }

        public async Task DeleteAsync(string marchentLocationKey)
        {
            await CreateAuthorizedRequestAsync($"{InventoryApiUrls.Location}/{marchentLocationKey}", RestSharp.Method.DELETE);
            await ExecuteRequestAsync();
        }

        public async Task<EbayList<InventoryLocation, EbayFilter>> GetAllAsync(EbayFilter ebayFilter)
        {
            await CreateAuthorizedPagedRequestAsync(ebayFilter, InventoryApiUrls.Location, RestSharp.Method.GET);
            var response = await ExecuteRequestAsync<LocationsRespones>();
            ebayFilter.NextPage = response.next;
            return new EbayList<InventoryLocation, EbayFilter>(ebayFilter, response.locations);
        }

        public async Task<InventoryLocation> GetAsync(string marchentLocationKey)
        {
            await CreateAuthorizedRequestAsync($"{InventoryApiUrls.Location}/{marchentLocationKey}", RestSharp.Method.GET);
            return await ExecuteRequestAsync<InventoryLocation>();
        }
    }
}
