using Ebaysharp.Entities;
using System.Collections.Generic;

namespace Ebaysharp.Services.Account
{
    public class PayementPolicyService: RequestService
    {
        public PayementPolicyService(ClientToken oauth, AccessToken token) : base(oauth, token)
        {
        }

        public List<PaymentPolicy> GetAll()
        {
            CreateAuthorizedRequest($"{AccountApiUrls.PayementPolicy}?marketplace_id={UsEbayMarketPlaceId}", RestSharp.Method.GET);
            return ExecuteRequest<GetAllPaymentPoliciesResponse>().paymentPolicies;
        }

        public PaymentPolicy Add(PaymentPolicy paymentPolicy)
        {
            CreateAuthorizedRequestJson(AccountApiUrls.PayementPolicy, RestSharp.Method.POST);
            Request.AddJsonBody(paymentPolicy);
            return ExecuteRequest<PaymentPolicy>();
        }

        public PaymentPolicy Update(PaymentPolicy paymentPolicy)
        {
            CreateAuthorizedRequestJson($"{AccountApiUrls.PayementPolicy}/{paymentPolicy.paymentPolicyId}", RestSharp.Method.PUT);
            Request.AddJsonBody(paymentPolicy);
            return ExecuteRequest<PaymentPolicy>();
        }

        public void Delete(long paymentPolicyId)
        {
            CreateAuthorizedRequest($"{AccountApiUrls.PayementPolicy}/{paymentPolicyId}", RestSharp.Method.DELETE);
            ExecuteRequest();
        }
    }
}
