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

        public PayPalTransaction GetAllTransactions()
        {
            PayPalClient.GetAccessToken();
            var response = PayPalOrder.GetAllTransactions();
            var serialize = new JavaScriptSerializer();
            var json = serialize.Deserialize<PayPalTransaction>(response.Content);
            return json;
        }

        public ActionResult AllInvoices()
        {
            PayPalClient.GetAccessToken();
            var response = PayPalInvoice.GetAllInvoices();
            var serialize = new JavaScriptSerializer();
            var paypalInvoices = serialize.Deserialize<PayPalInvoices>(response.Content);
            return View(paypalInvoices);
        }
    }
}