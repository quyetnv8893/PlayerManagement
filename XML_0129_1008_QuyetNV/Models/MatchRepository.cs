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
        private List<Match> allMatches;
        private XDocument MatchData;
        private String xml_path = "~/App_Data/player_management.xml";

        /**
         * Contructor to get all matches from xml file and save them to allMatches List
         **/
        public MatchRepository()
        {
            allMatches = new List<Match>();
            MatchData = XDocument.Load(HttpContext.Current.Server.MapPath(xml_path));           
            var Matches = from Match in MatchData.Descendants("match")
                          select new Match(Match.Element("id").Value, (DateTime)Match.Element("time"),Match.Element("name").Value,
                              Match.Element("score").Value, Match.Element("leagueName").Value);
            allMatches.AddRange(Matches.ToList<Match>());            
        }


        /**
         * Return list of Matches
         **/
        public IEnumerable<Match> GetMatches()
        {
            return allMatches;
        }

        /**
         * Get Match by id
         **/ 
        public Match GetMatchByID(String id)
        {
            return allMatches.Find(item => item.id.Equals(id));
        }

        /**
         * Get Matches by  league name
         **/ 
        public IEnumerable<Match> GetMatchesByLeagueName(String leagueName)
        {
            return allMatches.FindAll(item => item.leagueName.Equals(leagueName));
        }
        
        /**
         * Insert new match
         **/
        public void InsertMatch(Match match)
        {            
            if (match.id == null)
            {
                match.id = ((int)(DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds).ToString(); 
            }
            MatchData.Descendants("matches").FirstOrDefault().Add(new XElement("match", new XElement("id", match.id),
                new XElement("time", match.time), new XElement("name", match.name), new XElement("score", match.score),
                new XElement("leagueName", match.leagueName)));
            MatchData.Save(HttpContext.Current.Server.MapPath(xml_path));
        }

        /**
         * Delete a match which it's id
         **/ 
        public void DeleteMatch(String id)
        {
            MatchData.Descendants("matches").Elements("match").Where(item => item.Element("id").Value.Equals(id)).Remove();
            MatchData.Save(HttpContext.Current.Server.MapPath(xml_path));
        }
       
        
        /**
         * Edit a match which it's id
         **/ 
        public void EditMatch(Match Match)
        {
            
            XElement node = MatchData.Descendants("matches").Elements("match").Where(item => item.Element("id").Value.Equals(Match.id)).FirstOrDefault();            
            node.SetElementValue("id", Match.id);
            node.SetElementValue("time", Match.time);            
            node.SetElementValue("name", Match.name);            
            node.SetElementValue("score", Match.score);
            node.SetElementValue("leagueName", Match.leagueName);
            MatchData.Save(HttpContext.Current.Server.MapPath(xml_path));
        }

    }
}