using Live_Commerce_Platform.PayPalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Live_Commerce_Platform.Controllers
{
    public class PayPalInvoiceController : Controller
    {
        // GET: PayPalInvoice
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string CreateDraftInvoice(InvoiceRequest invoiceRequest)
        {
            PayPalClient.GetAccessToken();
            var response = PayPalInvoice.CreateDraftInvoice(invoiceRequest);
            return response.Content;
        }

        public ActionResult AllInvoices()
        {
            PayPalClient.GetAccessToken();
            var response = PayPalInvoice.GetAllInvoices();
            var serialize = new JavaScriptSerializer();
            var paypalInvoices = serialize.Deserialize<PayPalInvoices>(response.Content);
            return View(paypalInvoices);
        }

        [HttpGet]
        public string GetInvoiceDetails(string invoiceId)
        {
            PayPalClient.GetAccessToken();
            var response = PayPalInvoice.GetInvoiceDetail(invoiceId);
            return response.Content;
        }

        [HttpPost]
        public string SendInvoice(string invoiceId)
        {
            PayPalClient.GetAccessToken();
            var response = PayPalInvoice.SendInvoice(invoiceId);
            return response.StatusCode.ToString();
        }

        [HttpPost]
        public string DeleteInvoice(string invoiceId)
        {
            PayPalClient.GetAccessToken();
            var response = PayPalInvoice.DeleteInvoice(invoiceId);
            return response.StatusCode.ToString();
        }

        [HttpPost]
        public string RemindInvoice(string invoiceId)
        {
            PayPalClient.GetAccessToken();
            var response = PayPalInvoice.RemindInvoice(invoiceId);
            return response.StatusCode.ToString();
        }

        [HttpPost]
        public string CancelInvoice(string invoiceId)
        {
            PayPalClient.GetAccessToken();
            var response = PayPalInvoice.CancelInvoice(invoiceId);
            return response.StatusCode.ToString();
        }
    }
}