using Ebaysharp.Entities;
using System;

namespace Ebaysharp.Services
{
    public class OauthService : RequestService
    {
        public OauthService(ClientToken clientToken) : base(clientToken, null)
        {
        }

        public string GetRedirectUrl()
        {
            return $"{OAuthUrls.AuthUrl}?" +
                $"client_id={ClientToken.clientId}&" +
                $"redirect_uri={ClientToken.ruName}&" +
                $"response_type=code&state={ClientToken.state}&" +
                $"scope={ClientToken.scopes}&prompt=login";
        }

        public AccessToken GetAccessToken(string code)
        {
            CreateRequest(OAuthUrls.TokenUrl, RestSharp.Method.POST);
            Request.AddHeader("Authorization", "Basic " + ClientToken.oauthCredentials);
            Request.AddHeader("content-type", "application/x-www-form-urlencoded");
            Request.AddParameter("grant_type", "authorization_code");
            Request.AddParameter("redirect_uri", ClientToken.ruName);
            Request.AddParameter("code", code);
            var response = RequestClient.Execute<AccessToken>(Request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response.Data.date_last_updated = DateTime.Now;
                return response.Data;
            }
            else
                throw new Exception("Ebay didn't respond with Ok, See inner exception for detail", new Exception(response.Content));
        }

        public AccessToken GetApplicationToken()
        {
            CreateRequest(OAuthUrls.TokenUrl, RestSharp.Method.POST);
            Request.AddHeader("Authorization", "Basic " + ClientToken.oauthCredentials);
            Request.AddHeader("content-type", "application/x-www-form-urlencoded");
            Request.AddParameter("grant_type", "client_credentials");
            Request.AddParameter("scope", "https://api.ebay.com/oauth/api_scope https://api.ebay.com/oauth/api_scope/buy.guest.order https://api.ebay.com/oauth/api_scope/buy.item.feedS https://api.ebay.com/oauth/api_scope/buy.marketing");
            var response = RequestClient.Execute<AccessToken>(Request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response.Data.date_last_updated = DateTime.Now;
                return response.Data;
            }
            else
                throw new Exception("Ebay didn't respond with Ok, See inner exception for detail", new Exception(response.Content));
        }

        public AccessToken RefreshToken(AccessToken token)
        {
            CreateRequest(OAuthUrls.RefreshTokenUrl, RestSharp.Method.POST);
            Request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            Request.AddHeader("Authorization", "Basic " + ClientToken.oauthCredentials);
            Request.AddParameter("grant_type", "refresh_token");
            Request.AddParameter("refresh_token", token.refresh_token);
            Request.AddParameter("scope", ClientToken.scopes);
            var refreshedToken = ExecuteRequest<AccessToken>();
            token.access_token = refreshedToken.access_token;
            token.expires_in = refreshedToken.expires_in;
            token.date_last_updated = DateTime.UtcNow;
            return token;
        }

        public static AccessToken RefreshToken(ClientToken clientToken, AccessToken accessToken)
        {
            if (accessToken.expires_in == null)
                throw new Exception("Access token expires in not provided");
            if (accessToken.date_last_updated == null)
                throw new Exception("Access token last updated not provided");
            return IsTokenExpired(accessToken.expires_in.Value, accessToken.date_last_updated.Value) ?
                new OauthService(clientToken).RefreshToken(accessToken) : accessToken;
        }

        private static bool IsTokenExpired(int expiresIn, DateTime dateCreated)
        {
            return DateTime.UtcNow.Subtract(dateCreated).TotalSeconds > expiresIn;
        }
    }
}
