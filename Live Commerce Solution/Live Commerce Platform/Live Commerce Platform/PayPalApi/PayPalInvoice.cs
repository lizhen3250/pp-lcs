using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Live_Commerce_Platform.PayPalApi
{
    public class PayPalInvoice
    {
        public static RestResponse CreateDraftInvoice(InvoiceRequest invoiceRequest)
        {
            var client = new RestClient(PayPalClient.endPoint + "v1/invoicing/invoices/");
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", PayPalClient.accessToken);
            request.RequestFormat = DataFormat.Json;
            var body = invoiceRequest;
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = client.Execute(request);
            return response;
        }

        public static RestResponse GetAllInvoices()
        {
            var url = PayPalClient.endPoint + "v1/invoicing/invoices?total_count_required=true";
            var client = new RestClient(url);
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddHeader("Authorization", PayPalClient.accessToken);
            var response = client.Execute(request);
            return response;
        }

        public static RestResponse GetInvoiceDetail(string invoiceId)
        {
            var url = PayPalClient.endPoint + "v1/invoicing/invoices/" + invoiceId;
            var client = new RestClient(url);
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddHeader("Authorization", PayPalClient.accessToken);
            var response = client.Execute(request);
            return response;
        }

        public static RestResponse SendInvoice(string invoiceId)
        {
            var client = new RestClient(PayPalClient.endPoint + "v1/invoicing/invoices/"+invoiceId+"/send?notify_merchant=true");
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", PayPalClient.accessToken);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute(request);
            return response;
        }

        public static RestResponse DeleteInvoice(string invoiceId)
        {
            var client = new RestClient(PayPalClient.endPoint + "v1/invoicing/invoices/" + invoiceId);
            var request = new RestRequest("", Method.Delete);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", PayPalClient.accessToken);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute(request);
            return response;
        }
    }
}