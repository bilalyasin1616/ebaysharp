using Ebaysharp.Entities;
using System.Threading.Tasks;

namespace Ebaysharp.Services.Account
{
    public class PrivilegesService : RequestService
    {
        public PrivilegesService(ClientToken oauth, AccessToken token) : base(oauth, token)
        {
        }

        public virtual async Task<Privilege> GetPrivilegeAsync()
        {
            await CreateAuthorizedRequestAsync(AccountApiUrls.Privilege, RestSharp.Method.GET);
            return await ExecuteRequestAsync<Privilege>();
        }
    }
}
