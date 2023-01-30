using Live_Commerce_Platform.PayPalApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Live_Commerce_Platform.Controllers
{
    public class ApiCredentails
    {
        public string Email { get; set; }
        public string MerchantId { get; set; }
        public string ClientId { get; set; }
        public string SecretKey { get; set; }
        public string Mode { get; set; }
    }
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!AppSettingsHasValue())
                return Redirect("/Home/Apicredentials");

            return View();
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Apicredentials()
        {
            if (AppSettingsHasValue())
                return Redirect("/Home/Index");

            return View();
        }

        public string ValidateApiCredentails(ApiCredentails apiCredentails)
        {
            var validate = PayPalClient.ValidateAccessToken(apiCredentails.ClientId, apiCredentails.SecretKey, apiCredentails.Mode);

            if (validate)
            {
                // save the client id and secret id on webconfig
                SetAppKey("ClientId", apiCredentails.ClientId);
                SetAppKey("SecretKey", apiCredentails.SecretKey);
                SetAppKey("Mode", apiCredentails.Mode);
                return "OK";
            }

            return "Failue";
        }

        public string Identity()
        {
            return "";
        }

        private bool AppSettingsHasValue()
        {
            Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath);
            AppSettingsSection appSection = (AppSettingsSection)config.GetSection("appSettings");
            if (appSection.Settings["ClientId"].Value == string.Empty)
                return false;
            return true;
        }

        private void SetAppKey(string keyName, string keyValue)
        {
            Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath);
            AppSettingsSection appSection = (AppSettingsSection)config.GetSection("appSettings");
            if (appSection.Settings[keyName] != null)
            {
                appSection.Settings[keyName].Value = keyValue;
                config.Save();
            }

        }
    }
}