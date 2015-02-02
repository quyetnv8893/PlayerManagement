using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using PlayerManagement.Models;

namespace PlayerManagement.Models
{
    public class MatchRepository : IMatchRepository
    {
        private List<Match> _allMatches;
        private List<League> _allLeagues;
        private XDocument _matchData;
        private String _xmlPath = "~/App_Data/player_management.xml";
        
        /**
         * Contructor to get all matches from xml file and save them to allMatches List
         **/               
        public MatchRepository()
        {
            _allMatches = new List<Match>();
            _allLeagues = new List<League>();
            _matchData = XDocument.Load(HttpContext.Current.Server.MapPath(_xmlPath));
            var Matches = from Match in _matchData.Descendants("match")
                          select new Match(Match.Element("id").Value, (DateTime)Match.Element("time"), Match.Element("name").Value,
                              Match.Element("score").Value, Match.Element("leagueName").Value);
            var leagues = from League in _matchData.Descendants("league")
                          select new League(League.Element("name").Value, League.Element("logoLink").Value);
            _allLeagues.AddRange(leagues.ToList<League>());
            _allMatches.AddRange(Matches.ToList<Match>());
        }


        /**
         * Return list of Matches
         **/
        public IEnumerable<Match> GetMatches()
        {
            return _allMatches;
        }

        /**
         * Get Match by id
         **/
        public Match GetMatchByID(String id)
        {
            Match match = _allMatches.Find(item => item.ID.Equals(id));
            match.League = _allLeagues.Find(item => item.Name.Equals(match.LeagueName));
            return match;
        }

        /// <summary>
        /// Get Matches by  league name 
        /// </summary>
        /// <param name="leagueName"></param>
        /// <returns></returns>
        public IEnumerable<Match> GetMatchesByLeagueName(String leagueName)
        {
            return _allMatches.FindAll(item => item.LeagueName.Equals(leagueName));
        }

        /**
         * Insert new match
         **/
        public void InsertMatch(Match match)
        {
            if (match.ID == null)
            {
                match.ID = ((int)(DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
            }
            _matchData.Descendants("matches").FirstOrDefault().Add(new XElement("match", new XElement("id", match.ID),
                new XElement("time", match.Time), new XElement("name", match.Name), new XElement("score", match.Score),
                new XElement("leagueName", match.LeagueName)));
            _matchData.Save(HttpContext.Current.Server.MapPath(_xmlPath));
        }

        /**
         * Delete a match which it's id
         **/
        public void DeleteMatch(String id)
        {
            _matchData.Descendants("matches").Elements("match").Where(item => item.Element("id").Value.Equals(id)).Remove();
            _matchData.Save(HttpContext.Current.Server.MapPath(_xmlPath));
        }


        /**
         * Edit a match which it's id
         **/
        public void EditMatch(Match Match)
        {

            XElement node = _matchData.Descendants("matches").Elements("match").Where(item => item.Element("id").Value.Equals(Match.ID)).FirstOrDefault();
            node.SetElementValue("id", Match.ID);
            node.SetElementValue("time", Match.Time);
            node.SetElementValue("name", Match.Name);
            node.SetElementValue("score", Match.Score);
            node.SetElementValue("leagueName", Match.LeagueName);
            _matchData.Save(HttpContext.Current.Server.MapPath(_xmlPath));
        }

    }
}