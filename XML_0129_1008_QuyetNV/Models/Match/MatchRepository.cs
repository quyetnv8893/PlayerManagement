﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using PlayerManagement.Models;
using PlayerManagement.App_Start;

namespace PlayerManagement.Models
{
    public class MatchRepository : IMatchRepository
    {
        private List<Match> _allMatches;
        //private List<Player> _allPlayers;
        //private List<PlayerMatch.PlayerMatch> _allPlayerMatches;        

        /**
         * Contructor to get all matches from xml file and save them to allMatches List
         **/
        public MatchRepository()
        {
            _allMatches = new List<Match>();
          //  _allPlayers = new List<Player>();
            //_allPlayerMatches = new List<PlayerMatch.PlayerMatch>();            
            var Matches = from Match in GlobalVariables.XmlData.Descendants("match")
                          select new Match(Match.Element("id").Value, (DateTime)Match.Element("time"), Match.Element("name").Value,
                              Match.Element("score").Value, Match.Element("leagueName").Value);
            _allMatches.AddRange(Matches.ToList<Match>());
            /*var PlayerMatches = from PlayerMatch in GlobalVaraiables.XmlData.Descendants("player_match")
                                select new PlayerMatch.PlayerMatch(PlayerMatch.Element("playerId").Value, PlayerMatch.Element("matchId").Value, (int)PlayerMatch.Element("noOfGoals"),
                                    (int)PlayerMatch.Element("noOfYellows"), (int)PlayerMatch.Element("noOfReds"));
            _allPlayerMatches.AddRange(PlayerMatches.ToList<PlayerMatch.PlayerMatch>());
            var players = from player in GlobalVaraiables.XmlData.Descendants("player")
                          select new Player(
                              player.Element("clubName").Value,
                              player.Element("id").Value,
                              (int)player.Element("number"),
                              player.Element("name").Value,
                              player.Element("position").Value,
                              (DateTime)player.Element("dateOfBirth"),
                              player.Element("placeOfBirth").Value,
                              (double)player.Element("weight"),
                              (double)player.Element("height"),
                              player.Element("description").Value,
                              player.Element("imageLink").Value,
                              (Boolean)player.Element("status"));
            _allPlayers.AddRange(players.ToList<Player>()); */
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
           /* IEnumerable<PlayerMatch.PlayerMatch> temp = _allPlayerMatches.FindAll(item => item.MatchId.Equals(id));
            foreach (var item in temp)
            {
                item.Player = _allPlayers.Find(i => i.ID.Equals(item.PlayerId));
            }
            if (temp != null && match != null)
            {
                match.PlayerMatches = temp;
            } */
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