using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ITQuan
{
    public class Global : HttpApplication
    {
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}


		/*public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

			routes.MapRoute(
				"ArticleList",                                              
                "{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }  
			);

		}*/

		protected void Application_Start()
		{
            //RegisterGlobalFilters(GlobalFilters.Filters);
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
		}



	}
}
