using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Live_Commerce_Platform.PayPalApi
{
    public class PayPalMerchantInfo
    {
        public string name { get; set; }
        public string payer_id { get; set; }
        public bool verified_account { get; set; }

        public List<Email> emails { get; set; }
    }

    public class Email
    {
        public bool confirmed { get; set; }
        public bool primary { get; set; }
        public string value { get; set; }
    }
}