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
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Clubs", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "PlayerAchievement",
                url: "{controller}/{action}/{playerID}/{achievementName}",
                defaults: new { controller = "PlayerAchievements", action = "Index", playerID = "01", achievementName = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "PlayerMatches",
                url: "{controller}/{action}/{playerID}/{matchID}",
                defaults: new { controller = "PlayersMatches", action = "Edit", playerID = "01", matchID = UrlParameter.Optional }
            );


            

        }
    }
}
