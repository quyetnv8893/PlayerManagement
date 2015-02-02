﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PlayerManagement.Models
{
    public class LeagueRepository : ILeagueRepository
    {
        private List<League> _allLeagues;
        private XDocument _leagueData;
        private String _xmlPath = "~/App_Data/player_management.xml";
        private String _nodeName = "league";

        /// <summary>
        /// Constructor - get all leagues node from xml file
        /// </summary>
        public LeagueRepository()
        {
            _allLeagues = new List<League>();
            _leagueData = XDocument.Load(HttpContext.Current.Server.MapPath(_xmlPath));
            var leagues = from League in _leagueData.Descendants(_nodeName)
                          select new League(League.Element("name").Value, League.Element("logoLink").Value);
            _allLeagues.AddRange(leagues.ToList<League>());
        }

        /// <summary>
        /// Get all leagues
        /// </summary>
        /// <returns></returns>
        public IEnumerable<League> GetLeagues()
        {
            return _allLeagues;
        }

        /// <summary>
        /// Get league by league name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public League GetLeagueByName(String name)
        {            
            return _allLeagues.Find(item => item.Name.Equals(name));
        }
        /// <summary>
        /// Insert new league to xml file
        /// </summary>
        /// <param name="league"></param>
        public void InsertLeague(League league)
        {
            _leagueData.Descendants("leagues").FirstOrDefault().Add(new XElement("league", new XElement("name", league.Name), new XElement("logoLink", league.LogoLink)));
            _leagueData.Save(HttpContext.Current.Server.MapPath(_xmlPath));
        }
        /// <summary>
        /// Edit a league and save to xml file
        /// </summary>
        /// <param name="league"></param>
        public void EditLeague(League league)
        {
            XElement node = _leagueData.Descendants("leagues").Elements(_nodeName).Where(item => item.Element("name").Value.Equals(league.Name)).FirstOrDefault();
            node.SetElementValue("name", league.Name);
            node.SetElementValue("logoLink", league.LogoLink);
            _leagueData.Save(HttpContext.Current.Server.MapPath(_xmlPath));
        }

        /// <summary>
        /// Delete league which have league name equal name
        /// </summary>
        /// <param name="name"></param>
        public void DeleteLeague(String name)
        {
            _leagueData.Descendants("leagues").Elements(_nodeName).Where(item => item.Element("name").Value.Equals(name)).Remove();
            _leagueData.Save(HttpContext.Current.Server.MapPath(_xmlPath));
        }
    }
}
