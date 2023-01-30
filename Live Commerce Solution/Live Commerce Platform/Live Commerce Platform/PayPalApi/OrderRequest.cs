using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Live_Commerce_Platform.PayPalApi
{
    public class OrderRequest
    {
        public string intent { get; set; }

        public List<PurchaseUnit> purchase_units { get; set; }

        public ApplicationContext application_context { get; set; }

        public Payer payer { get; set; }
    }

    public class PurchaseUnit
    {
        public string description { get; set; }

        public string invoice_id { get; set; }
        public Amount amount { get; set; }

        public Shipping shipping { get; set; }

        public List<Item> items { get; set; }
    }

    public class Amount
    {
        public string currency_code { get; set; }

        public string value { get; set; }

        public BreakDown breakdown { get; set; }
    }

    public class ApplicationContext
    {
        public string cancel_url { get; set; }

        public string return_url { get; set; }

        public string shipping_preference { get; set; }
    }

    public class Shipping
    {
        public Name name { get; set; }
        public Address address { get; set; }
    }

    public class Payer
    {
        public Name name { get; set; }
        public string email_address { get; set; }

        public Address address { get; set; }

        public Phone phone { get; set; }
    }

    public class Phone
    {
        public PhoneNumber phone_number { get; set; }
    }

    public class PhoneNumber
    {
        public string national_number { get; set; }
    }


    public class Name
    {
        public string full_name { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }
    }

    public class Address
    {
        public string country_code { get; set; }
        public string address_line_1 { get; set; }
        public string address_line_2 { get; set; }
        public string admin_area_1 { get; set; }
        public string admin_area_2 { get; set; }
        public string postal_code { get; set; }
    }

    public class BreakDown
    {
        public ItemTotal item_total { get; set; }

        public ShippingFee shipping { get; set; }
    }

    public class ItemTotal
    {
        public string currency_code { get; set; }
        public string value { get; set; }
    }

    public class ShippingFee
    {
        public string currency_code { get; set; }
        public string value { get; set; }
    }

    public class Item
    {
        public string name { get; set; }
        public string description { get; set; }
        public string quantity { get; set; }
        public UniteAmount unit_amount { get; set; }
    }

    public class UniteAmount
    {
        public string currency_code { get; set; }

        public string value { get; set; }
    }
}