using Ebaysharp.Entities;
using RestSharp;
using System;
using System.Net;

namespace Ebaysharp
{
    public class EbayException : Exception
    {
        public ExceptionResponse Response { get; set; }
        public Errors Errors { get; set; }

        public EbayException(string msg, IRestResponse response = null) : base(msg)
        {
            if (response != null)
            {
                Response = new ExceptionResponse();
                Response.Content = response.Content;
                Response.ResponseCode = response.StatusCode;
                Errors = ParseResponse();
            }
        }

        private Errors ParseResponse()
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Errors>(Response.Content);
            }
            catch (Exception)
            {
                return default;
            }
        }

    }

    public class NotFoundException : EbayException
    {
        public NotFoundException(string msg, IRestResponse response = null) : base(msg, response)
        {

        }
    }

    public class ExceptionResponse
    {
        public string Content { get; set; }
        public HttpStatusCode? ResponseCode { get; set; }
    }
}
