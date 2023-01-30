using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Live_Commerce_Platform.PayPalApi
{
    public class PayPalIdentity
    {
        public static RestResponse GetUserInfo()
        {
            var client = new RestClient(PayPalClient.endPoint + "v1/identity/oauth2/userinfo?schema=paypalv1.1");
            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddHeader("Authorization", PayPalClient.accessToken);
            var response = client.Execute(request);
            return response;
        }
    }
}