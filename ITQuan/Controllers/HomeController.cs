using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace ITQuan.Controllers
{
    public class HomeController : Controller
    {
        static string _URL = "http://quan.ithome.com/thread/";


        public ActionResult Index(int id=1)
        {
            if (id <= 0)
                id = 1;
            return View(ArticleHelp.GetArticleList(string.Concat(_URL, id)));
        }
    }
}
