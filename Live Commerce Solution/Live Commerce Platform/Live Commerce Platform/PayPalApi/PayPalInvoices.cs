using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Live_Commerce_Platform.PayPalApi
{
    public class PayPalInvoices
    {
        public string total_count { get; set; }

        public List<Invoice> invoices { get; set; }

        public string invoice_data { get; set; }
    }

    public class Invoice
    {
        public string id { get; set; }
        public string status { get; set; }
        public MerchantInfo merchant_info { get; set; }

        public List<BillingInfo> billing_info { get; set; }

        public InvoiceShippingInfo shipping_info { get; set; }

        public TotalAmount total_amount { get; set; }

        public PaidAmount paid_amount { get; set; }

        public RefundedAmount refunded_amount { get; set; }
    }

    public class TotalAmount
    {
        public string currency { get; set; }
        public string value { get; set; }
    }

    public class PaidAmount
    {
        public PayPal paypal { get; set; }
    }

    public class RefundedAmount
    {
        public PayPal paypal { get; set; }
    }

    public class PayPal
    {
        public string currency { get; set; }
        public string value { get; set; }
    }
}