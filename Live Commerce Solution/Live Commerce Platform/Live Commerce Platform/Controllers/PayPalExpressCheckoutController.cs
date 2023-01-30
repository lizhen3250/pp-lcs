using Live_Commerce_Platform.PayPalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace Live_Commerce_Platform.Controllers
{
    public class PayPalExpressCheckoutController : Controller
    {
        // GET: PayPalExpressCheckout
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ConfirmOrder()
        {
            return View();
        }

        public ActionResult AllTransactions()
        {
            var paypalTransactions = GetAllTransactions();
            return View(paypalTransactions);
        }

        [HttpPost]
        public string CreateOrder(PurchaseUnitRequest purchaseUnitRequest)
        {
            PayPalClient.GetAccessToken();
            var response = PayPalOrder.CreateOrder(purchaseUnitRequest);
            return response.Content;
        }

        [HttpGet]
        public string GetOrderDetails(string orderId)
        {
            PayPalClient.GetAccessToken();
            var response = PayPalOrder.GetOrderDetail(orderId);
            return response.Content;
        }

        [HttpPost]
        public string ConfirmOrder(string orderId)
        {
            var response = PayPalOrder.CaptureOrder(orderId);
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

        [HttpGet]
        public string GetTransactiondDetail(string transactionId)
        {
            PayPalClient.GetAccessToken();
            var response = PayPalOrder.GetTransactionDetail(transactionId);
            return response.Content;
        }

        [HttpPost]
        public string RefundTransaction(string transactionId)
        {
            PayPalClient.GetAccessToken();
            var response = PayPalOrder.RefundTransaction(transactionId);
            return response.Content;
        }
    }
}