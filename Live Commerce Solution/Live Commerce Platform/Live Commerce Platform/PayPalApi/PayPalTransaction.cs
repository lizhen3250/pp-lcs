using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Live_Commerce_Platform.PayPalApi
{
    public class PayPalTransaction
    {
        public List<TransactionDetails> transaction_details { get; set; }
    }

    public class TransactionDetails
    {
        public TransactionInfo transaction_info { get; set; }

        public PayerInfo payer_info { get; set; }

        public ShippingInfo shipping_info { get; set; }

    }

    public class TransactionInfo
    {
        public TransactionAmount transaction_amount { get; set; }

        public string transaction_status { get; set; }

        public string transaction_id { get; set; }

        public string invoice_id { get; set; }

        public string transaction_updated_date { get; set; }

        public string transaction_event_code { get; set; }
    }

    public class TransactionAmount
    {
        public string currency_code { get; set; }
        public string value { get; set; }
    }

    public class PayerInfo
    {
        public string email_address { get; set; }
    }

    public class ShippingInfo
    {
        public string name { get; set; }

        public TransactionAddress address { get; set; }
    }

    public class TransactionAddress
    {
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country_code { get; set; }
        public string postal_code { get; set; }
    }
}