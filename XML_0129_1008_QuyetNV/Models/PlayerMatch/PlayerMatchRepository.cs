using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PlayerManagement.Models.PlayerMatch
{
    public class PlayerMatchRepository : IPlayerMatchRepository
    {
        private List<PlayerMatch> allPlayerMatches;
        private XDocument XmlData;
        private String xml_path = "~/App_Data/player_management.xml";

        /// <summary>
        /// Contructor to get all matches from xml file and save them to allPlayerMatches List
        /// </summary>
        public PlayerMatchRepository()
        {
            allPlayerMatches = new List<PlayerMatch>();
            XmlData = XDocument.Load(HttpContext.Current.Server.MapPath(xml_path));
            var PlayerMatches = from PlayerMatch in XmlData.Descendants("player_match")
                                select new PlayerMatch(PlayerMatch.Element("playerId").Value, PlayerMatch.Element("matchId").Value, (int)PlayerMatch.Element("noOfGoals"),
                                    (int)PlayerMatch.Element("noOfYellows"), (int)PlayerMatch.Element("noOfReds"));
            allPlayerMatches.AddRange(PlayerMatches.ToList<PlayerMatch>());
        }

        /// <summary>
        /// Return list of PlayerMatches
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PlayerMatch> GetPlayerMatches()
        {
            return allPlayerMatches;
        }


        /// <summary>
        /// Get PlayerMatch by id of player and match
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>

        public PlayerMatch GetPlayerMatchByPlayerIdAndMatchId(String playerId, String matchId)
        {
            return allPlayerMatches.Find(item => (item.PlayerId.Equals(playerId) && item.MatchId.Equals(matchId)));
        }
        
        /// <summary>
        /// Get PlayerMatch by id of player
        /// </summary>
        /// <param name="playerId"></param>        
        /// <returns></returns> 
        public IEnumerable<PlayerMatch> GetPlayerMatchesByPlayerId(String playerId)
        {
            return allPlayerMatches.FindAll(item => item.PlayerId.Equals(playerId));
        }


        /// <summary>
        /// Get PlayerMatch by id of match
        /// </summary>
        /// <param name="matchId"></param>        
        /// <returns></returns> 
        public IEnumerable<PlayerMatch> GetPlayerMatchesByMatchId(String matchId)
        {
            return allPlayerMatches.FindAll(item => item.MatchId.Equals(matchId));
        }

        /// <summary>
        /// Insert new player_match
        /// </summary>
        /// <param name="playermatch"></param>
        public void InsertPlayerMatch(PlayerMatch playermatch)
        {
            XmlData.Descendants("players_matches").FirstOrDefault().Add(new XElement("player_match", new XElement("playerId", playermatch.PlayerId),
                new XElement("matchId", playermatch.MatchId), new XElement("noOfGoals", playermatch.NumberOfGoals), new XElement("noOfReds", playermatch.NumberOfReds),
                new XElement("noOfYellows", playermatch.NumberOfYellows)));
            XmlData.Save(HttpContext.Current.Server.MapPath(xml_path));
        }

        /// <summary>
        /// Delete a player match
        /// </summary>
        /// <param name="playerMatch"></param>
        public void DeletePlayerMatch(PlayerMatch playerMatch)
        {
            XmlData.Descendants("players_matches").Elements("player_match").Where(item => (item.Element("playerId").Value.Equals(playerMatch.PlayerId) && item.Element("matchId").Value.Equals(playerMatch.MatchId))).Remove();
            XmlData.Save(HttpContext.Current.Server.MapPath(xml_path));
        }


        /// <summary>
        /// Edit player match
        /// </summary>
        /// <param name="PlayerMatch"></param>
        public void EditPlayerMatch(PlayerMatch playerMatch)
        {
            XElement node = XmlData.Descendants("players_matches").Elements("player_match").Where(item => (item.Element("playerId").Value.Equals(playerMatch.PlayerId) && item.Element("matchId").Value.Equals(playerMatch.MatchId))).FirstOrDefault();
            node.SetElementValue("noOfGoals", playerMatch.NumberOfGoals);
            node.SetElementValue("noOfReds", playerMatch.NumberOfReds);
            node.SetElementValue("noOfYellows", playerMatch.NumberOfYellows);
            XmlData.Save(HttpContext.Current.Server.MapPath(xml_path));
        }

    }
}