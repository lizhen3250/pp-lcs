using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApP
{
    class Program
    {
        static void Main(string[] args)
        {
            //var response = PayPalClient.GetAccessToken();
            //string accesstoken = response.Token_Type + " " + response.Access_Token;
            //DateTime endDate = DateTime.Now;
            //DateTime startDate = endDate.AddDays(-30);
            //var a = startDate.GetDateTimeFormats('s')[0].ToString() + "-0700";//2005-11-05T14:06:25
            //var b = endDate.GetDateTimeFormats('s')[0].ToString() + "-0700";
            //var url = PayPalClient.sandboxEndPoint + "v1/reporting/transactions/?start_date=" + a + "&end_date=" + b + "&fields=all";
            //Console.WriteLine(url);
            //var aa = PayPalOrder.GetAllTransactions(accesstoken,a,b);
            //Console.WriteLine(aa.Content);
            List<int> list = new List<int>() { 1, 2, 3, 4 };
            list.Reverse();
            foreach(var li in list)
            {
                Console.WriteLine(li);
            }
            Console.ReadLine();
        }
    }
}
