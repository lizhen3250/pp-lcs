using Live_Commerce_Platform.PayPalApi;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;

namespace Live_Commerce_Platform.Controllers
{
    public class PurchaseUnitRequest
    {
        public ItemDetail[] ItemDetails { get; set; }
        public OrderDetail OrderDetail { get; set; }
        public ContactInformation ContactInformation { get; set; }

        public bool IsSPB { get; set; }
    }

    public class ItemDetail
    {
        public string ItemName { get; set; }
        public string ItemQuantity { get; set; }
        public string ItemAmount { get; set; }
        public string ItemDescription { get; set; }
    }

    public class OrderDetail
    {
        public string Description { get; set; }
        public string InvoiceNumber { get; set; }

        public string ShippingFee { get; set; }

        public string SubTotal { get; set; }

        public string Total { get; set; }
    }

    public class ContactInformation
    {
        public string Email { get; set; }
        public ShippingAddress ShippingAddress { get; set; }

    }

    public class ShippingAddress
    {
        public string Country { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public string PhoneNumber { get; set; }
        public string Apartment { get; set; }

    }
    public class PayPalSmartPaymentController : Controller
    {
        // GET: PayPalSmartPayment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PageReview()
        {
            return View();
        }

        public ActionResult CheckoutPage()
        {
            return View();
        }

        [HttpPost]
        public string CreateOrder(PurchaseUnitRequest purchaseUnitRequest)
        {
            PayPalClient.GetAccessToken();
            var response = PayPalOrder.CreateOrder(purchaseUnitRequest);
            return response.Content;
        }

        [HttpPost]
        public string CaptureOrder(string orderId)
        {
            var response = PayPalOrder.CaptureOrder(orderId);
            return response.Content;
        }
    }
}