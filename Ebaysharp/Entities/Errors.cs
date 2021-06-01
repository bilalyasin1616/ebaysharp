using System.Collections.Generic;

namespace Ebaysharp.Entities
{
    public enum EbayResponseCodes
    {
        Invalid_Request = 20400,
        Missing_Field = 20401,
        Invalid_Input = 20402,
        System_Error = 20500,
        Service_Unavailable = 20501,
        Offer_Exits = 25002,
        AlreadyExists = 208,
        Success = 200
    }

    public enum ErrorCategories
    {
        APPLICATION, BUSINESS, REQUEST
    }

    public class Errors
    {
        public List<Error> errors { get; set; }
        public List<Error> warnings { get; set; }
        public string resource { get; set; }
    }

    public class Error
    {
        public int errorId { get; set; }
        public string domain { get; set; }
        public string category { get; set; }
        public string message { get; set; }
        public string longMessage { get; set; }
        public List<Parameter> parameters { get; set; }
    }

    public class Parameter
    {
        public string name { get; set; }
        public string value { get; set; }
    }
}
