using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PlayerManagement.Models
{
    public class PlayerRepository :IPlayerRepository
    {
        private List<Player> _allPlayers;
        private XDocument _playerData;

        public PlayerRepository()
        {
            _allPlayers = new List<Player>();

            _playerData = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
            var players = from player in _playerData.Descendants("player")
                          select new Player(player.Element("clubName").Value, player.Element("id").Value, (int)player.Element("number"),
                              player.Element("name").Value, player.Element("position").Value, (DateTime)player.Element("dateOfBirth"), player.Element("placeOfBirth").Value,
                              (double)player.Element("weight"), (double)player.Element("height"), player.Element("description").Value, player.Element("imageLink").Value, (Boolean)player.Element("status"));

            _allPlayers.AddRange(players.ToList<Player>());

        }


        //Return list of players
        public IEnumerable<Player> GetPlayers()
        {
            return _allPlayers;
        }

        public Player GetPlayerByID(String id)
        {
            return _allPlayers.Find(item => item.ID.Equals(id));
        }

        //Add new player

        public void InsertPlayer(Player player)
        {
            player.ID = (from p in _playerData.Descendants("player") orderby p.Element("id").Value descending select p.Element("id").Value).FirstOrDefault();

            //Change later
            _playerData.Descendants("players").FirstOrDefault().Add(new XElement("player", 
                new XElement("clubName", player.ClubName), 
                new XElement("id", player.ID),
                new XElement("number", player.Number), 
                new XElement("name", player.Name), 
                new XElement("position", player.Position),
                new XElement("dateOfBirth", player.DateOfBirth.Date), 
                new XElement("placeOfBirth", player.PlaceOfBirth),
                new XElement("weight", player.Weight), 
                new XElement("height", player.Height), 
                new XElement("description", player.Description),
                new XElement("imageLink", player.ImageLink), 
                new XElement("status", player.Status)));

            _playerData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }

        // Delete Record
        public void DeletePlayer(String id)
        {
            _playerData.Descendants("players").Elements("player").Where(i => i.Element("id").Value.Equals(id)).Remove();

            _playerData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }

        // Edit Record
        public void EditPlayer(Player player)
        {

            XElement node = _playerData.Descendants("players").Elements("player").Where(i => i.Element("id").Value.Equals(player.ID)).FirstOrDefault();

            node.SetElementValue("clubName", player.ClubName);
            node.SetElementValue("id", player.ID);
            node.SetElementValue("name", player.Name);
            node.SetElementValue("number", player.Number);
            node.SetElementValue("position", player.Position);
            node.SetElementValue("dateOfBirth", player.DateOfBirth.Date);
            node.SetElementValue("placeOfBirth", player.PlaceOfBirth);
            node.SetElementValue("weight", player.Weight);
            node.SetElementValue("height", player.Height);
            node.SetElementValue("description", player.Description);
            node.SetElementValue("imageLink", player.ImageLink);
            node.SetElementValue("status", player.Status);

            _playerData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }
    }
}