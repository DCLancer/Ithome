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
        public ActionResult Index()
        {
            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
            ViewData["Runtime"] = isMono ? "Mono" : ".NET";

            //ViewData["Html"] 
            ArticleHelp.GetSingleArticle(
                ArticleHelp.GetArticleListNodeFromHtml(
                    ArticleHelp.GetHtml("http://quan.ithome.com")).ChildNodes.Where(a => !a.Name.Equals("#text")).FirstOrDefault());

            return View();
        }
    }
}
