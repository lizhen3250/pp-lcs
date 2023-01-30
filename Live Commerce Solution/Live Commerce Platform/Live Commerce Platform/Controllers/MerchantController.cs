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
    public class MerchantController : Controller
    {
        // GET: Merchant
        public ActionResult Index()
        {
            PayPalClient.GetAccessToken();
            var response = PayPalIdentity.GetUserInfo();
            var serialize = new JavaScriptSerializer();
            var json = serialize.Deserialize<PayPalMerchantInfo>(response.Content);
            return View(json);
        }

        public string GetMerchantEmail()
        {
            PayPalClient.GetAccessToken();
            var response = PayPalIdentity.GetUserInfo();
            return response.Content;
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