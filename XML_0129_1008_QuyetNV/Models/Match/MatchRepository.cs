using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using PlayerManagement.Models;
using PlayerManagement.App_Start;
using PlayerManagement.Models.PlayerMatch;

namespace PlayerManagement.Models
{
    public class MatchRepository : IMatchRepository
    {       
        private List<Match> _allMatches;
        private PlayerMatchRepository _playerMatchRes;
        /**
         * Contructor to get all matches from xml file and save them to allMatches List
         **/
        public MatchRepository()
        {
            _playerMatchRes = new PlayerMatchRepository();            
            _allMatches = new List<Match>();
          
            var Matches = from Match in GlobalVariables.XmlData.Descendants("match")
                          select new Match(Match.Element("id").Value, (DateTime)Match.Element("time"), Match.Element("name").Value,
                              "0-0", Match.Element("leagueName").Value);
            _allMatches.AddRange(Matches.ToList<Match>());
            foreach (var match in _allMatches)
            {
                int real=0;
                int guest=0;
                IEnumerable<PlayerMatch.PlayerMatch> listPlayerMatch = _playerMatchRes.GetPlayerMatchesByMatchId(match.ID);
                foreach (var playerMatch in listPlayerMatch)
                {
                    if (playerMatch.NumberOfGoals > 0)
                    {
                        if (playerMatch.PlayerID != "-1")
                        {
                            real = real + playerMatch.NumberOfGoals;
                        }
                        else
                        {
                            guest = guest + 1;
                        }
                    }
                }
                string[] words = match.Name.Split('-');
                if (words[0].Contains("Real Madrid")) {
                    match.Score = real + " - " + guest;
                } else{
                    match.Score = guest + " - " + real;
                }
            }
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
            return match;
        }

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
            GlobalVariables.XmlData.Descendants("matches").FirstOrDefault().Add(new XElement("match", new XElement("id", match.ID),
                new XElement("time", match.Time), new XElement("name", match.Name), new XElement("score", match.Score),
                new XElement("leagueName", match.LeagueName)));
            GlobalVariables.XmlData.Save(HttpContext.Current.Server.MapPath(GlobalVariables.XmlPath));
            GlobalVariables.Update();
        }

        /**
         * Delete a match which it's id
         **/
        public void DeleteMatch(String id)
        {
            GlobalVariables.XmlData.Descendants("matches").Elements("match").Where(item => item.Element("id").Value.Equals(id)).Remove();
            GlobalVariables.XmlData.Save(HttpContext.Current.Server.MapPath(GlobalVariables.XmlPath));
            GlobalVariables.Update();
        }


        /**
         * Edit a match which it's id
         **/
        public void EditMatch(Match Match)
        {

            XElement node = GlobalVariables.XmlData.Descendants("matches").Elements("match").Where(item => item.Element("id").Value.Equals(Match.ID)).FirstOrDefault();
            node.SetElementValue("id", Match.ID);
            node.SetElementValue("time", Match.Time);
            node.SetElementValue("name", Match.Name);
            node.SetElementValue("score", Match.Score);
            node.SetElementValue("leagueName", Match.LeagueName);
            GlobalVariables.XmlData.Save(HttpContext.Current.Server.MapPath(GlobalVariables.XmlPath));
            GlobalVariables.Update();
        }

    }
}