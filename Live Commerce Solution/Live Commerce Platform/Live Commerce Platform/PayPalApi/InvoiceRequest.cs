using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Live_Commerce_Platform.PayPalApi
{
    public class InvoiceRequest
    {
        public MerchantInfo merchant_info { get; set; }

        public List<BillingInfo> billing_info { get; set; }

        public InvoiceShippingInfo shipping_info { get; set; }

        public List<InvoiceItem> items { get; set; }

        public ShippingCost shipping_cost { get; set; }

        public string note { get; set; }
    }

    public class InvoiceItem
    {
        public string name { get; set; }
        public string description { get; set; }
        public string quantity { get; set; }
        public UnitPrice unit_price { get; set; }
    }

    public class UnitPrice
    {
        public string currency { get; set; }
        public string value { get; set; }
    }

    public class MerchantInfo
    {
        public string email { get; set; }
        public string business_name { get; set; }
    }

    public class BillingInfo
    {
        public string email { get; set; }
    }

    public class InvoiceShippingInfo
    {
        public string first_name { get; set; }
        public string last_name { get; set; }

        public TransactionAddress address { get; set; }
    }

    public class ShippingCost
    {
        public InvoiceShippingFee amount { get; set; }
    }

    public class InvoiceShippingFee
    {
        public string currency { get; set; }
        public string value { get; set; }
    }


}