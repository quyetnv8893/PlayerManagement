using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PlayerManagement.Models
{
    public class LeagueRepository : ILeagueRepository
    {
        private List<League> allLeagues;
        private XDocument leagueData;
        private String xml_path = "~/App_Data/player_management.xml";
        private String nodeName = "league";

        /// <summary>
        /// Constructor - get all leagues node from xml file
        /// </summary>
        public LeagueRepository()
        {
            allLeagues = new List<League>();
            leagueData = XDocument.Load(HttpContext.Current.Server.MapPath(xml_path));
            var leagues = from League in leagueData.Descendants(nodeName)
                          select new League(League.Element("name").Value, League.Element("logoLink").Value);
            allLeagues.AddRange(leagues.ToList<League>());
        }

        /// <summary>
        /// Get all leagues
        /// </summary>
        /// <returns></returns>
        public IEnumerable<League> GetLeagues()
        {
            return allLeagues;
        }

        /// <summary>
        /// Get league by league name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public League GetLeagueByName(String name)
        {            
            return allLeagues.Find(item => item.name.Equals(name));
        }
        /// <summary>
        /// Insert new league to xml file
        /// </summary>
        /// <param name="league"></param>
        public void InsertLeague(League league)
        {
            leagueData.Descendants("leagues").FirstOrDefault().Add(new XElement("league", new XElement("name", league.name), new XElement("logoLink", league.logoLink)));
            leagueData.Save(HttpContext.Current.Server.MapPath(xml_path));
        }
        /// <summary>
        /// Edit a league and save to xml file
        /// </summary>
        /// <param name="league"></param>
        public void EditLeague(League league)
        {
            XElement node = leagueData.Descendants("leagues").Elements(nodeName).Where(item => item.Element("name").Value.Equals(league.name)).FirstOrDefault();
            node.SetElementValue("name", league.name);
            node.SetElementValue("logoLink", league.logoLink);
            leagueData.Save(HttpContext.Current.Server.MapPath(xml_path));
        }

        /// <summary>
        /// Delete league which have league name equal name
        /// </summary>
        /// <param name="name"></param>
        public void DeleteLeague(String name)
        {
            leagueData.Descendants("leagues").Elements(nodeName).Where(item => item.Element("name").Value.Equals(name)).Remove();
            leagueData.Save(HttpContext.Current.Server.MapPath(xml_path));
        }
    }
}
