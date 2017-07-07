using System;
using Tiexue.UI.Common;
using System.Net;

namespace IthomeRSS
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(GetRssContent());
            Console.ReadKey();
        }



        private static string GetRssContent()
        {
            string strRssContent=string.Empty;

            using(WebClient wb=new WebClient())
            {
                strRssContent=wb.DownloadString("https://www.ithome.com/rss/");
            }

            return strRssContent;
        }

    }
}
