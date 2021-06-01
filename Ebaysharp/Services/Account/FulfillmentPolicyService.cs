using Ebaysharp.Entities;
using System.Collections.Generic;

namespace Ebaysharp.Services.Account
{
    public class FulfillmentPolicyService: RequestService
    {
        public FulfillmentPolicyService(ClientToken oauth, AccessToken token) : base(oauth, token)
        {
        }

        public List<FulfillmentPolicy> GetAll()
        {
            CreateAuthorizedRequest($"{AccountApiUrls.FulfillmentPolicy}?marketplace_id={UsEbayMarketPlaceId}", RestSharp.Method.GET);
            return ExecuteRequest<GetAllFulfillmentPoliciesResponse>().fulfillmentPolicies;
        }

        public FulfillmentPolicy Add(FulfillmentPolicy fulfillmentPolicy)
        {
            CreateAuthorizedRequestJson(AccountApiUrls.FulfillmentPolicy, RestSharp.Method.POST);
            Request.AddJsonBody(fulfillmentPolicy);
            return ExecuteRequest<FulfillmentPolicy>();
        }

        public FulfillmentPolicy Update(FulfillmentPolicy fulfillmentPolicy)
        {
            CreateAuthorizedRequestJson(AccountApiUrls.FulfillmentPolicy, RestSharp.Method.PUT);
            Request.AddJsonBody(fulfillmentPolicy);
            return ExecuteRequest<FulfillmentPolicy>();
        }

        public void Delete(long fulfillmentPolicyId)
        {
            CreateAuthorizedRequest($"{AccountApiUrls.FulfillmentPolicy}/{fulfillmentPolicyId}", RestSharp.Method.DELETE);
            ExecuteRequest();
        }
    }
}
