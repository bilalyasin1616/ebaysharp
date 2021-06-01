using Ebaysharp.Entities;

namespace Ebaysharp.Services.Account
{
    public class PrivilegesService : RequestService
    {
        public PrivilegesService(ClientToken oauth, AccessToken token) : base(oauth, token)
        {
        }

        public Privilege GetPrivilege()
        {
            CreateAuthorizedRequest(AccountApiUrls.Privilege, RestSharp.Method.GET);
            return ExecuteRequest<Privilege>();
        }
    }
}
