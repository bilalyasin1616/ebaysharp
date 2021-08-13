using Ebaysharp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ebaysharp.Services.Account
{
    public class FulfillmentPolicyService: RequestService
    {
        public FulfillmentPolicyService(ClientToken oauth, AccessToken token) : base(oauth, token)
        {
        }

        public virtual async Task<FulfillmentPolicy> GetAsync(long fulfillmentPolicyId)
        {
            await CreateAuthorizedRequestAsync($"{AccountApiUrls.FulfillmentPolicy}/{fulfillmentPolicyId}", RestSharp.Method.GET);
            return await ExecuteRequestAsync<FulfillmentPolicy>();
        }

        public virtual async Task<List<FulfillmentPolicy>> GetAllAsync()
        {
            await CreateAuthorizedRequestAsync($"{AccountApiUrls.FulfillmentPolicy}?marketplace_id={UsEbayMarketPlaceId}", RestSharp.Method.GET);
            return (await ExecuteRequestAsync<GetAllFulfillmentPoliciesResponse>()).fulfillmentPolicies;
        }

        public virtual async Task<FulfillmentPolicy> AddAsync(FulfillmentPolicy fulfillmentPolicy)
        {
            await CreateAuthorizedRequestJsonAsync(AccountApiUrls.FulfillmentPolicy, RestSharp.Method.POST);
            Request.AddJsonBody(fulfillmentPolicy);
            return await ExecuteRequestAsync<FulfillmentPolicy>();
        }

        public virtual async Task<FulfillmentPolicy> UpdateAsync(FulfillmentPolicy fulfillmentPolicy)
        {
            await CreateAuthorizedRequestJsonAsync(AccountApiUrls.FulfillmentPolicy, RestSharp.Method.PUT);
            Request.AddJsonBody(fulfillmentPolicy);
            return await ExecuteRequestAsync<FulfillmentPolicy>();
        }

        public virtual async Task DeleteAsync(long fulfillmentPolicyId)
        {
            await CreateAuthorizedRequestAsync($"{AccountApiUrls.FulfillmentPolicy}/{fulfillmentPolicyId}", RestSharp.Method.DELETE);
            await ExecuteRequestAsync();
        }
    }
}
