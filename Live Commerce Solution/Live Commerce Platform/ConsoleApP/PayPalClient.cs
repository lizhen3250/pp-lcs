using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApP
{
    public class PayPalClient
    {
        public static string clientId = "Afi9aY1Q4NROu9qxuPNl3qY3UrAqacFEr5A2vQPoME7z-0t2yxKHFuIz0G9jE2IQhEm9W-_Yyn63eOQn";
        public static string secretId = "EIbkdi1pqOgZ9ZOIaL0Yl5pVJ6LSG9_uM7tY64geLi5TpIx9AoGxn8OqgkxLJX8zL4nhP0_SLaWYm6RO";
        public static string sandboxEndPoint = "https://api-m.sandbox.paypal.com/";

        public static PayPalAccessToken GetAccessToken()
        {
            var client = new RestClient(sandboxEndPoint + "v1/oauth2/token/");
            var request = new RestRequest("", Method.Post);
            byte[] bytes = Encoding.UTF8.GetBytes(clientId + ":" + secretId);
            string base64Str = Convert.ToBase64String(bytes);
            request.AddHeader("Authorization", "Basic " + base64Str);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<PayPalAccessToken>(response.Content);
        }
    }
}
