using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace Live_Commerce_Platform.PayPalApi
{
    public class PayPalAccessToken
    {
        public string Access_Token { get; set; }
        public string Token_Type { get; set; }
    }
    public class PayPalClient
    {
        public static string accessToken { get; set; }
        public static string endPoint { get; set; }
        public static void GetAccessToken()
        {
            Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath);
            AppSettingsSection appSection = (AppSettingsSection)config.GetSection("appSettings");
            var clientId = appSection.Settings["ClientId"].Value;
            var secretId = appSection.Settings["SecretKey"].Value;
            var mode = appSection.Settings["Mode"].Value;
            if (mode == "sandbox")
            {
                endPoint = appSection.Settings["SandboxEndPoint"].Value;
            }
            else
            {
                endPoint = appSection.Settings["LiveEndPoint"].Value;
            }
            var client = new RestClient(endPoint + "v1/oauth2/token/");
            var request = new RestRequest("", Method.Post);
            byte[] bytes = Encoding.UTF8.GetBytes(clientId + ":" + secretId);
            string base64Str = Convert.ToBase64String(bytes);
            request.AddHeader("Authorization", "Basic " + base64Str);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            var response = client.Execute(request);
            var paypalAccessToken = JsonConvert.DeserializeObject<PayPalAccessToken>(response.Content);
            accessToken = "Bearer " + paypalAccessToken.Access_Token;           
        }

        public static bool ValidateAccessToken(string clientId, string secretKey, string mode)
        {
            if (mode == "sandbox")
            {
                endPoint = "https://api-m.sandbox.paypal.com/";
            }
            else
            {
                endPoint = "https://api-m.paypal.com/";
            }

            var client = new RestClient(endPoint + "v1/oauth2/token/");
            var request = new RestRequest("", Method.Post);
            byte[] bytes = Encoding.UTF8.GetBytes(clientId + ":" + secretKey);
            string base64Str = Convert.ToBase64String(bytes);
            request.AddHeader("Authorization", "Basic " + base64Str);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            var response = client.Execute(request);
            var paypalAccessToken = JsonConvert.DeserializeObject<PayPalAccessToken>(response.Content);
            if (paypalAccessToken != null)
            {
                return true;
            }

            return false;
        }
    }
}