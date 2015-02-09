using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PlayerManagement
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "PlayerAchievement",
                url: "{controller}/{action}/{id}/{name}",
                defaults: new { controller = "PlayerAchievements", action = "Index", id = "01", name = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Players", action = @"\d+", id = @"\d+" }
            );

        }
    }
}
