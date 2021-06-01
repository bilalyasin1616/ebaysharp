using Ebaysharp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ebaysharp.Services.Account
{
    public class ReturnPolicyService: RequestService
    {
        public ReturnPolicyService(ClientToken oauth, AccessToken token) : base(oauth, token)
        {
        }

        public List<ReturnPolicy> GetAll()
        {
            CreateAuthorizedRequest($"{AccountApiUrls.returnPolicy}?marketplace_id={UsEbayMarketPlaceId}", RestSharp.Method.GET);
            return ExecuteRequest<GetAllRetrunPoliciesResponse>().returnPolicies;
        }

        public ReturnPolicy Add(ReturnPolicy returnPolicy)
        {
            CreateAuthorizedRequestJson(AccountApiUrls.returnPolicy, RestSharp.Method.POST);
            Request.AddJsonBody(returnPolicy);
            return ExecuteRequest<ReturnPolicy>();
            
        }

        public ReturnPolicy Update(ReturnPolicy returnPolicy)
        {
            CreateAuthorizedRequestJson($"{AccountApiUrls.returnPolicy}/{returnPolicy.returnPolicyId}", RestSharp.Method.PUT);
            Request.AddJsonBody(returnPolicy);
            return ExecuteRequest<ReturnPolicy>();
        }

        public void Delete(long returnPolicyId)
        {
            CreateAuthorizedRequest($"{AccountApiUrls.returnPolicy}/{returnPolicyId}", RestSharp.Method.DELETE);
            ExecuteRequest();
        }

    }
}
