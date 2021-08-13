using Ebaysharp.Entities;
using Ebaysharp.Entities.Identity;
using System.Threading.Tasks;

namespace Ebaysharp.Services.Commerce
{
    public class IdentityService : RequestService
    {
        public IdentityService(ClientToken oauth, AccessToken token) : base(oauth, token)
        {
        }

        public virtual async Task<User> GetUserAsync()
        {
            await CreateAuthorizedRequestAsync(IdentityApiUrls.GetUser, RestSharp.Method.GET);
            return await ExecuteRequestAsync<User>();
        }
    }
}
