﻿using Ebaysharp.Entities;
using RestSharp;
using System;
using System.Net;

namespace Ebaysharp.Services
{
    public class RequestService : ApiUrls
    {
        protected RestClient RequestClient { get; set; }
        protected RestRequest Request { get; set; }
        protected ClientToken ClientToken { get; set; }
        public AccessToken AccessToken { get; set; }

        /// <summary>
        /// Creates request base service
        /// </summary>
        /// <param name="clientToken">Contains api clients information</param>
        /// <param name="accessToken">Contains current user's account api keys</param>
        public RequestService(ClientToken clientToken, AccessToken accessToken)
        {
            ClientToken = clientToken;
            AccessToken = accessToken;
        }

        /// <summary>
        /// Checks if token is refreshed after creating the sevice instance
        /// </summary>
        /// <param name="lastUpdated">last time the tokken was updated</param>
        /// <returns></returns>
        public bool IsAccessTokenRefreshed(DateTime lastUpdated)
        {
            return lastUpdated != AccessToken.date_last_updated;
        }

        protected void CreateRequest(string url, Method method)
        {
            RequestClient = new RestClient(url);
            Request = new RestRequest(method);
        }

        protected void CreateAuthorizedRequest(string url, Method method)
        {
            RefreshToken();
            CreateRequest(url, method);
            AddBearerToken();
        }

        protected void CreateAuthorizedRequestJson(string url, Method method)
        {
            RefreshToken();
            CreateRequest(url, method);
            AddBearerToken();
            AddJsonContentType();
        }

        protected void CreateRequestWithTokenAndContentLanguage(string url, Method method)
        {
            RefreshToken();
            CreateRequest(url, method);
            AddBearerToken();
            AddContentLanguage();
        }

        protected void CreateAuthorizedPagedRequest(EbayFilter filter, string url, Method method)
        {
            RefreshToken();
            if (filter.NextPage != null)
                CreateRequest(filter.NextPage, method);
            else
            {
                CreateRequest(url, method);
                AddLimitHeader(filter.Limit);
            }
            AddBearerToken();
        }

        /// <summary>
        /// Executes the request
        /// </summary>
        /// <typeparam name="T">Type to parse response to</typeparam>
        /// <returns>Returns data of T type</returns>
        protected T ExecuteRequest<T>() where T: new()
        {
            var response = RequestClient.Execute<T>(Request);
            ParseResponse(response);
            return response.Data;
        }

        /// <summary>
        /// Executes the request 
        /// </summary>
        /// <typeparam name="T">Type to parse response to</typeparam>
        /// <returns>Returns raw response</returns>
        protected IRestResponse ExecuteRequest()
        {
            var response = RequestClient.Execute(Request);
            ParseResponse(response);
            return response;
        }

        protected void ParseResponse(IRestResponse response)
        {
            if (response.StatusCode == HttpStatusCode.Created ||
                response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.NoContent ||
                response.StatusCode == HttpStatusCode.MultiStatus)
                return;
            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new NotFoundException("Resource that you are looking for is not found", response);
            else
                throw new EbayException("Ebay Api didn't respond with Okay, see exception for more details", response);
        }

        protected void AddLimitHeader(int limit)
        {
            Request.AddQueryParameter("limit", limit.ToString());
        }

        private void AddBearerToken()
        {
            Request.AddHeader("Authorization", "Bearer " + AccessToken.access_token);
        }

        private void AddContentLanguage()
        {
            Request.AddHeader("Content-Language", ContentLanguage);
        }

        private void AddJsonContentType()
        {
            Request.AddHeader("Content-Type", "application/json");
        }

        protected void RefreshToken()
        {
            AccessToken = OauthService.RefreshToken(ClientToken, AccessToken);
        }

    }

}
