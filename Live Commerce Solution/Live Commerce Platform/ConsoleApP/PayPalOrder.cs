using RestSharp;
using System;
using System.Collections.Generic;


namespace ConsoleApP
{
    public class PayPalOrder
    {
        public static RestResponse CreateOrder(string accesstoken)
        {
            OrderRequest orderRequest = new OrderRequest()
            {
                intent = "CAPTURE",
                payer = new Payer
                {
                    address = new Address
                    {
                        address_line_1 = "11 philidelphia ave",
                        address_line_2 = "apartment",
                        admin_area_1 = "CA",
                        admin_area_2 = "Los Angeles",
                        country_code = "US",
                        postal_code = "90001"
                    },
                    name = new Name
                    {
                        first_name = "Zhen",
                        last_name = "Li"
                    },
                    email_address = "764482832@qq.com",
                    phone = new Phone
                    {
                        phone_number = new PhoneNumber
                        {
                            national_number = "5555555555"
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
                description = "description",
                invoice_id = "invoice_id",
                amount = new Amount
                {
                    currency_code = "USD",
                    value = "20.00",
                    breakdown = new BreakDown
                    {
                        item_total = new ItemTotal
                        {
                            currency_code = "USD",
                            value = "10"
                        },
                        shipping = new ShippingFee
                        {
                            currency_code = "USD",
                            value = "10"
                        }
                    }
                },

               shipping = new Shipping
               {
                   address = new Address
                   {
                       address_line_1 = "11 philidelphia ave",
                       address_line_2 = "apartment",
                       admin_area_1 = "CA",
                       admin_area_2 = "Los Angeles",
                       country_code = "US",
                       postal_code = "90001"
                   },
                   name = new Name
                   {
                       full_name = "Zhen Li"
                   }
               }
                
            };

            List<Item> items = new List<Item>();
            items.Add(new Item
            {
                name = "itemanme",
                quantity = "1",
                description = "item descripton",
                unit_amount = new UniteAmount
                {
                    currency_code = "USD",
                    value = "10"
                }
            });
            
            List<PurchaseUnit> purchaseUnits = new List<PurchaseUnit>();
            purchaseUnit.items = items;
            purchaseUnits.Add(purchaseUnit);
            orderRequest.purchase_units = purchaseUnits;

            var client = new RestClient(PayPalClient.sandboxEndPoint + "v2/checkout/orders");
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", accesstoken);
            request.RequestFormat = DataFormat.Json;
            var body = orderRequest;
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = client.Execute(request);
            return response;
        }

        public static RestResponse GetAllTransactions(string accesstoken, string startDate, string endDate)
        {

            var url = PayPalClient.sandboxEndPoint + "v1/reporting/transactions/?start_date=" + startDate + "&end_date="+endDate + "&fields=all";
            var client = new RestClient(url);
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddHeader("Authorization", accesstoken);
            var response = client.Execute(request);
            return response;
        }
    }
}
