using Live_Commerce_Platform.PayPalApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Live_Commerce_Platform.Controllers
{
    public class MerchantInfo
    {
        public string name { get; set; }
        public string payer_id { get; set; }
        public bool verified_account { get; set; }

        public Email email { get; set; }
    }

    public class Email
    {
        public string value { get; set; }
        public bool confirmed { get; set; }
        public bool primary { get; set; }
    }
    public class MerchantController : Controller
    {
        // GET: Merchant
        public MerchantInfo Index()
        {
            PayPalClient.GetAccessToken();
            var response = PayPalIdentity.GetUserInfo();
            var serialize = new JavaScriptSerializer();
            var json = serialize.Deserialize<MerchantInfo>(response.Content);
            return json;
        }

        public ActionResult Disconnect()
        {

            Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath);
            AppSettingsSection appSection = (AppSettingsSection)config.GetSection("appSettings");
            if (appSection.Settings["ClientId"] != null)
                appSection.Settings["ClientId"].Value = "";
            if (appSection.Settings["SecretKey"] != null)
                appSection.Settings["SecretKey"].Value = "";
            if (appSection.Settings["Mode"] != null)
                appSection.Settings["Mode"].Value = "";

            config.Save();

            return Redirect("/Home/Apicredentials");

        }
    }
}