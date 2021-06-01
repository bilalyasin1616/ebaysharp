using Ebaysharp.Entities;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Ebaysharp
{
    public static class ExtensionService
    {
        public static string GetDuplicateProfileName(this List<Error> errors)
        {
            string duplicateName = null;
            if (errors != null)
            {
                errors.ForEach(error =>
                {
                    if (error.parameters != null)
                        error.parameters.ForEach(param =>
                        {
                            if (param.name.Equals("DuplicateProfileName"))
                                duplicateName = param.value;
                        });
                });
            }
            return duplicateName;
        }

        public static Exception GetException(this IRestResponse response,string msg)
        {
            return GetException(msg, response);
        }

        public static Exception GetException(string msg, IRestResponse response=null)
        {
            //if(response!=null)
                //Log ebay response here
            return new Exception($"{msg}, eBay responded with an error");
        }

        public static List<string> GetErrors(this Errors errors)
        {
            var errsList = new List<string>();
            if (errors != null && errors.errors != null)
                errors.errors.ForEach(error =>
                {
                    if (error.category == ErrorCategories.BUSINESS.ToString() || error.category == ErrorCategories.REQUEST.ToString())
                    {
                        errsList.Add(error.message);
                        if (!string.IsNullOrEmpty(error.longMessage))
                            errsList.Add(error.longMessage);
                    }
                });
            if (errors != null && errors.warnings != null)
                errors.warnings.ForEach(warning =>
                {
                    if (warning.category == ErrorCategories.BUSINESS.ToString() || warning.category == ErrorCategories.REQUEST.ToString())
                        errsList.Add(warning.message);
                    if (!string.IsNullOrEmpty(warning.longMessage))
                        errsList.Add(warning.longMessage);
                });
            return errsList;
        }
    }
}
