using Live_Commerce_Platform.Controllers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Live_Commerce_Platform.PayPalApi
{
    public class PayPalOrder
    {
        public static RestResponse CreateOrder(PurchaseUnitRequest purchaseUnitRequest)
        {
            OrderRequest orderRequest = new OrderRequest()
            {
                intent = "CAPTURE",
                payer = new Payer
                {
                    address = new Address
                    {
                        address_line_1 = purchaseUnitRequest.ContactInformation.ShippingAddress.Address,
                        address_line_2 = purchaseUnitRequest.ContactInformation.ShippingAddress.Apartment,
                        admin_area_1 = "CA",
                        admin_area_2 = purchaseUnitRequest.ContactInformation.ShippingAddress.City,
                        country_code = "US",
                        postal_code = purchaseUnitRequest.ContactInformation.ShippingAddress.ZipCode
                    },
                    name = new Name
                    {
                        first_name = purchaseUnitRequest.ContactInformation.ShippingAddress.FirstName,
                        last_name = purchaseUnitRequest.ContactInformation.ShippingAddress.LastName
                    },
                    email_address = purchaseUnitRequest.ContactInformation.Email,
                    phone = new Phone
                    {
                        phone_number = new PhoneNumber
                        {
                            national_number = purchaseUnitRequest.ContactInformation.ShippingAddress.PhoneNumber
                        }
                    }
                },

                application_context = new ApplicationContext
                {
                    return_url = "https://example.com",
                    cancel_url = "https://example.com"
                }

            };
            var purchaseUnit = new PurchaseUnit
            {
                description = purchaseUnitRequest.OrderDetail.Description,
                invoice_id = purchaseUnitRequest.OrderDetail.InvoiceNumber,
                amount = new Amount
                {
                    currency_code = "USD",
                    value = purchaseUnitRequest.OrderDetail.Total,
                    breakdown = new BreakDown
                    {
                        item_total = new ItemTotal
                        {
                            currency_code = "USD",
                            value = purchaseUnitRequest.OrderDetail.SubTotal
                        },
                        shipping = new ShippingFee
                        {
                            currency_code = "USD",
                            value = purchaseUnitRequest.OrderDetail.ShippingFee
                        }
                    }
                },

                shipping = new Shipping
                {
                    address = new Address
                    {
                        address_line_1 = purchaseUnitRequest.ContactInformation.ShippingAddress.Address,
                        address_line_2 = purchaseUnitRequest.ContactInformation.ShippingAddress.Apartment,
                        admin_area_1 = "CA",
                        admin_area_2 = purchaseUnitRequest.ContactInformation.ShippingAddress.City,
                        country_code = "US",
                        postal_code = purchaseUnitRequest.ContactInformation.ShippingAddress.ZipCode
                    },
                    name = new Name
                    {
                        full_name = purchaseUnitRequest.ContactInformation.ShippingAddress.FirstName + purchaseUnitRequest.ContactInformation.ShippingAddress.LastName
                    }
                }

            };

            if (purchaseUnitRequest.IsSPB == false)
            {
                orderRequest.application_context.return_url = "https://localhost:44398/PayPalExpressCheckout/ConfirmOrder";
            }

            List<Item> items = new List<Item>();
            for (var i = 0; i < purchaseUnitRequest.ItemDetails.Length; i++)
            {
                
                items.Add(new Item
                {
                    name = purchaseUnitRequest.ItemDetails[i].ItemName,
                    quantity = purchaseUnitRequest.ItemDetails[i].ItemQuantity,
                    description = purchaseUnitRequest.ItemDetails[i].ItemDescription,
                    unit_amount = new UniteAmount
                    {
                        currency_code = "USD",
                        value = purchaseUnitRequest.ItemDetails[i].ItemAmount
                    }
                });
                purchaseUnit.items = items;
            }            

            List<PurchaseUnit> purchaseUnits = new List<PurchaseUnit>();
            purchaseUnits.Add(purchaseUnit);
            orderRequest.purchase_units = purchaseUnits;

            var client = new RestClient(PayPalClient.endPoint + "v2/checkout/orders");
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", PayPalClient.accessToken);
            request.RequestFormat = DataFormat.Json;
            var body = orderRequest;
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = client.Execute(request);
            return response;
        }

        public static RestResponse CaptureOrder(string orderId)
        {
            var client = new RestClient(PayPalClient.endPoint + "v2/checkout/orders/"+orderId+"/capture");
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", PayPalClient.accessToken);
            var response = client.Execute(request);
            return response;
        }

        public static RestResponse GetOrderDetail(string orderId)
        {
            var url = PayPalClient.endPoint + "v2/checkout/orders/" + orderId;
            var client = new RestClient(url);
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddHeader("Authorization", PayPalClient.accessToken);
            var response = client.Execute(request);

            return response;
        }

        public static RestResponse GetAllTransactions()
        {
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.AddDays(-30);
            var startDateFormated = startDate.GetDateTimeFormats('s')[0].ToString() + "-0700";//2005-11-05T14:06:25
            var endDateFormated = endDate.GetDateTimeFormats('s')[0].ToString() + "-0700";
            var url = PayPalClient.endPoint + "v1/reporting/transactions/?start_date=" + startDateFormated + "&end_date=" + endDateFormated + "&fields=all";
            var client = new RestClient(url);
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddHeader("Authorization", PayPalClient.accessToken);
            var response = client.Execute(request);
            return response;
        }

        public static RestResponse GetTransactionDetail(string transactionId)
        {
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.AddDays(-30);
            var startDateFormated = startDate.GetDateTimeFormats('s')[0].ToString() + "-0700";//2005-11-05T14:06:25
            var endDateFormated = endDate.GetDateTimeFormats('s')[0].ToString() + "-0700";
            var url = PayPalClient.endPoint + "v1/reporting/transactions/?start_date=" + startDateFormated + "&end_date=" + endDateFormated + "&transaction_id=" + transactionId;
            var client = new RestClient(url);
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddHeader("Authorization", PayPalClient.accessToken);
            var response = client.Execute(request);
            return response;
        }

        public static RestResponse RefundTransaction(string transactionId)
        {
            var url = PayPalClient.endPoint + "v2/payments/captures/"+transactionId+"/refund";
            var client = new RestClient(url);
            var request = new RestRequest();
            request.Method = Method.Post;
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", PayPalClient.accessToken);
            var response = client.Execute(request);
            return response;
        }
    }
}