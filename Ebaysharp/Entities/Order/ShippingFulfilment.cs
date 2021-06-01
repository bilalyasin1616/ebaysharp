using System;
using System.Collections.Generic;

namespace Ebaysharp.Entities.Order
{
    public class ShippingFulfilment
    {
        public List<ShipmentLineItem> lineItems { get; set; }
        public string shippedDate { get; set; }
        public string shippingCarrierCode { get; set; }
        public string trackingNumber { get; set; }
    }

    public class ShipmentLineItem
    {
        public string lineItemId { get; set; }
        public int quantity { get; set; }
    }
}
