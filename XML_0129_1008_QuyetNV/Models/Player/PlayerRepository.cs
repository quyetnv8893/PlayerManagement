using PlayerManagement.App_Start;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PlayerManagement.Models
{
    public class PlayerRepository :IPlayerRepository
    {
        private List<Player> _allPlayers;        

        public PlayerRepository()
        {
            _allPlayers = new List<Player>();
            
            var players = from player in GlobalVariables.XmlData.Descendants("player")
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

            _allPlayers.AddRange(players.ToList<Player>());

        }


        //Return list of players
        public IEnumerable<Player> GetPlayers()
        {
            return _allPlayers;
        }

        public IEnumerable<String> GetPlayerNames()
        {
            List<String> playerNames = new List<String>();
            foreach (var player in _allPlayers)
            {
                playerNames.Add(player.Name);
            }

            return playerNames;
        }

        public Player GetPlayerByID(String id)
        {
            return _allPlayers.Find(item => item.ID.Equals(id));
        }

        //Add new player

        public void InsertPlayer(Player player)
        {           
            //Change later

            
            GlobalVariables.XmlData.Descendants("players").FirstOrDefault().Add(new XElement("player", 
                new XElement("clubName", player.ClubName), 
                new XElement("id", player.ID),
                new XElement("number", player.Number), 
                new XElement("name", player.Name), 
                new XElement("position", player.Position),
                new XElement("dateOfBirth", player.DateOfBirth.ToString("yyyy-MM-dd")), 
                new XElement("placeOfBirth", player.PlaceOfBirth),
                new XElement("weight", player.Weight), 
                new XElement("height", player.Height), 
                new XElement("description", player.Description),
                new XElement("imageLink", player.ImageLink), 
                new XElement("status", player.Status)
                ));

            GlobalVariables.XmlData.Save(HttpContext.Current.Server.MapPath(GlobalVariables.XmlPath));
            GlobalVariables.Update();
        }

        // Delete Record
        public void DeletePlayer(String id)
        {
            GlobalVariables.XmlData.Descendants("players").Elements("player")
                .Where(i => i.Element("id").Value.Equals(id)).Remove();

            GlobalVariables.XmlData.Save(HttpContext.Current.Server.MapPath(GlobalVariables.XmlPath));
            GlobalVariables.Update();
        }

        // Edit Record
        public void EditPlayer(Player player)
        {

            XElement node = GlobalVariables.XmlData.Descendants("players").Elements("player")
                .Where(i => i.Element("id").Value.Equals(player.ID)).FirstOrDefault();

            node.SetElementValue("clubName", player.ClubName);
            node.SetElementValue("id", player.ID);
            node.SetElementValue("name", player.Name);
            node.SetElementValue("number", player.Number);
            node.SetElementValue("position", player.Position);
            node.SetElementValue("dateOfBirth", player.DateOfBirth.Date.ToString("yyyy-MM-dd"));
            node.SetElementValue("placeOfBirth", player.PlaceOfBirth);
            node.SetElementValue("weight", player.Weight);
            node.SetElementValue("height", player.Height);
            node.SetElementValue("description", player.Description);
            node.SetElementValue("imageLink", player.ImageLink);
            node.SetElementValue("status", player.Status);

            GlobalVariables.XmlData.Save(HttpContext.Current.Server.MapPath(GlobalVariables.XmlPath));
            GlobalVariables.Update();
        }
    }
}