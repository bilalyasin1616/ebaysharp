using System;
using System.Collections.Generic;
using System.Text;

namespace Ebaysharp.Entities
{
    public class Privilege
    {
        public bool sellerRegistrationCompleted { get; set; }
        public SellingLimit sellingLimit { get; set; }
    }

    public class SellingLimit
    {
        public Amount amount { get; set; }
        public int quantity { get; set; }
    }
}
