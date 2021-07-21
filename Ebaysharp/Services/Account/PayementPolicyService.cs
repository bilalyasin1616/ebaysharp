﻿using Ebaysharp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ebaysharp.Services.Account
{
    public class PayementPolicyService: RequestService
    {
        public PayementPolicyService(ClientToken oauth, AccessToken token) : base(oauth, token)
        {
        }

        public async Task<List<PaymentPolicy>> GetAllAsync()
        {
            await CreateAuthorizedRequestAsync($"{AccountApiUrls.PayementPolicy}?marketplace_id={UsEbayMarketPlaceId}", RestSharp.Method.GET);
            return (await ExecuteRequestAsync<GetAllPaymentPoliciesResponse>()).paymentPolicies;
        }

        public async Task<PaymentPolicy> AddAsync(PaymentPolicy paymentPolicy)
        {
            await CreateAuthorizedRequestJsonAsync(AccountApiUrls.PayementPolicy, RestSharp.Method.POST);
            Request.AddJsonBody(paymentPolicy);
            return await ExecuteRequestAsync<PaymentPolicy>();
        }

        public async Task<PaymentPolicy> UpdateAsync(PaymentPolicy paymentPolicy)
        {
            await CreateAuthorizedRequestJsonAsync($"{AccountApiUrls.PayementPolicy}/{paymentPolicy.paymentPolicyId}", RestSharp.Method.PUT);
            Request.AddJsonBody(paymentPolicy);
            return await ExecuteRequestAsync<PaymentPolicy>();
        }

        public async Task DeleteAsync(long paymentPolicyId)
        {
            await CreateAuthorizedRequestAsync($"{AccountApiUrls.PayementPolicy}/{paymentPolicyId}", RestSharp.Method.DELETE);
            await ExecuteRequestAsync();
        }
    }
}
