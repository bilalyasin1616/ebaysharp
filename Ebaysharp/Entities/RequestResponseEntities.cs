using System;
using System.Collections.Generic;
using System.Text;

namespace Ebaysharp.Entities
{
    public class DeletePolicyResponse
    {
        public List<Error> errors { get; set; }
        public int responseCode { get; set; }
    }
}
