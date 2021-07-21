using Ebaysharp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ebaysharp.Services.Account
{
    public class PaymentPolicyService: RequestService
    {
        public PaymentPolicyService(ClientToken oauth, AccessToken token) : base(oauth, token)
        {
        }

        public async Task<List<PaymentPolicy>> GetAllAsync()
        {
            await CreateAuthorizedRequestAsync($"{AccountApiUrls.PaymentPolicy}?marketplace_id={UsEbayMarketPlaceId}", RestSharp.Method.GET);
            return (await ExecuteRequestAsync<GetAllPaymentPoliciesResponse>()).paymentPolicies;
        }

        public async Task<PaymentPolicy> AddAsync(PaymentPolicy paymentPolicy)
        {
            await CreateAuthorizedRequestJsonAsync(AccountApiUrls.PaymentPolicy, RestSharp.Method.POST);
            Request.AddJsonBody(paymentPolicy);
            return await ExecuteRequestAsync<PaymentPolicy>();
        }

        public async Task<PaymentPolicy> UpdateAsync(PaymentPolicy paymentPolicy)
        {
            await CreateAuthorizedRequestJsonAsync($"{AccountApiUrls.PaymentPolicy}/{paymentPolicy.paymentPolicyId}", RestSharp.Method.PUT);
            Request.AddJsonBody(paymentPolicy);
            return await ExecuteRequestAsync<PaymentPolicy>();
        }

        public async Task DeleteAsync(long paymentPolicyId)
        {
            await CreateAuthorizedRequestAsync($"{AccountApiUrls.PaymentPolicy}/{paymentPolicyId}", RestSharp.Method.DELETE);
            await ExecuteRequestAsync();
        }
    }
}
