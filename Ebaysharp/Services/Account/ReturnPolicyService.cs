using Ebaysharp.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ebaysharp.Services.Account
{
    public class ReturnPolicyService: RequestService
    {
        public ReturnPolicyService(ClientToken oauth, AccessToken token) : base(oauth, token)
        {
        }

        public virtual async Task<List<ReturnPolicy>> GetAllAsync()
        {
            await CreateAuthorizedRequestAsync($"{AccountApiUrls.returnPolicy}?marketplace_id={UsEbayMarketPlaceId}", RestSharp.Method.GET);
            return (await ExecuteRequestAsync<GetAllRetrunPoliciesResponse>()).returnPolicies;
        }

        public virtual async Task<ReturnPolicy> AddAsync(ReturnPolicy returnPolicy)
        {
            await CreateAuthorizedRequestJsonAsync(AccountApiUrls.returnPolicy, RestSharp.Method.POST);
            Request.AddJsonBody(returnPolicy);
            return await ExecuteRequestAsync<ReturnPolicy>();
            
        }

        public virtual async Task<ReturnPolicy> UpdateAsync(ReturnPolicy returnPolicy)
        {
            await CreateAuthorizedRequestJsonAsync($"{AccountApiUrls.returnPolicy}/{returnPolicy.returnPolicyId}", RestSharp.Method.PUT);
            Request.AddJsonBody(returnPolicy);
            return await ExecuteRequestAsync<ReturnPolicy>();
        }

        public virtual async Task DeleteAsync(long returnPolicyId)
        {
            await CreateAuthorizedRequestAsync($"{AccountApiUrls.returnPolicy}/{returnPolicyId}", RestSharp.Method.DELETE);
            await ExecuteRequestAsync();
        }

    }
}
