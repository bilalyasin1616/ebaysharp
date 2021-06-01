using Ebaysharp.Entities;
using Ebaysharp.Entities.Identity;

namespace Ebaysharp.Services.Commerce
{
    public class IdentityService : RequestService
    {
        public IdentityService(ClientToken oauth, AccessToken token) : base(oauth, token)
        {
        }

        public User GetUser()
        {
            CreateAuthorizedRequest(IdentityApiUrls.GetUser, RestSharp.Method.GET);
            return ExecuteRequest<User>();
        }
    }
}
