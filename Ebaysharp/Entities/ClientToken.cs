using System;
using System.Collections.Generic;
using System.Text;

namespace Ebaysharp.Entities
{
    /// <summary>
    /// Provides information about the client who is going to access ebay user's account
    /// Helps during auth process and refreshing the token
    /// </summary>
    public class ClientToken
    {
        /// <summary>
        /// ClientId form ebay developer portal.
        /// </summary>
        public string clientId { get; set; }
        /// <summary>
        /// Client secret from ebay developer portal.
        /// </summary>
        public string clientSecret { get; set; }
        /// <summary>
        /// This is base64 encoded clientId and clientSecret provided to ebay during auth.
        /// </summary>
        public string oauthCredentials { get; set; }
        /// <summary>
        /// Ebay devId.
        /// </summary>
        public string devId { get; set; }
        /// <summary>
        /// Ebay has a name for redirect url.
        /// </summary>
        public string ruName { get; set; }
        /// <summary>
        /// Scopes for auth request.
        /// </summary>
        public string scopes { get; set; }
        /// <summary>
        /// Information passed to ebay and received back during auth process.
        /// </summary>
        public string state { get; set; }
    }

    public class AccessToken
    {
        /// <summary>
        /// Oauth access token contains user's account information
        /// </summary>
        public string access_token { get; set; }
        public int? expires_in { get; set; }
        public string refresh_token { get; set; }
        public int? refresh_token_expires_in { get; set; }
        public string token_type { get; set; }
        /// <summary>
        /// Contains when the token was last refresed always pass when making any request to channel to refresh token if needed
        /// </summary>
        public DateTime? date_last_updated { get; set; }
        /// <summary>
        /// Will use in future when considering expiry of refresh tokens as well.
        /// </summary>
        public DateTime? date_created { get; set; }
    }
}
