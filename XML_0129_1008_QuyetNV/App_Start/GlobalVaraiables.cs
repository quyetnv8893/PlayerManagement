using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PlayerManagement.App_Start
{
    public class GlobalVariables
    {
        public static XDocument XmlData;
        public static  String XmlPath = "~/App_Data/player_management.xml";
        public static void Register()
        {
            XmlData = XDocument.Load(HttpContext.Current.Server.MapPath(XmlPath));
        }
        public static void Update()
        {
            Register();
        }
    }
}