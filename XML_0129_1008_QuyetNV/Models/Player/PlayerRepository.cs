using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PlayerManagement.Models
{
    public class PlayerRepository :IPlayerRepository
    {
        private List<Player> allPlayers;
        private XDocument playerData;

        public PlayerRepository()
        {
            allPlayers = new List<Player>();

            playerData = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
            var players = from player in playerData.Descendants("player")
                          select new Player(player.Element("clubName").Value, player.Element("id").Value, (int)player.Element("number"),
                              player.Element("name").Value, player.Element("position").Value, (DateTime)player.Element("dateOfBirth"), player.Element("placeOfBirth").Value,
                              (double)player.Element("weight"), (double)player.Element("height"), player.Element("description").Value, player.Element("imageLink").Value, (Boolean)player.Element("status"));

            allPlayers.AddRange(players.ToList<Player>());

        }


        //Return list of players
        public IEnumerable<Player> GetPlayers()
        {
            return allPlayers;
        }

        public Player GetPlayerByID(String id)
        {
            return allPlayers.Find(item => item.id.Equals(id));
        }

        //Add new player

        public void InsertPlayer(Player player)
        {
            player.id = (from p in playerData.Descendants("player") orderby p.Element("id").Value descending select p.Element("id").Value).FirstOrDefault();

            //Change later
            playerData.Descendants("players").FirstOrDefault().Add(new XElement("player", new XElement("clubName", player.clubName), new XElement("id", player.id),
                new XElement("number", player.number), new XElement("name", player.name), new XElement("position", player.position),
                new XElement("dateOfBirth", player.dateOfBirth.Date), new XElement("placeOfBirth", player.placeOfBirth),
                new XElement("weight", player.weight), new XElement("height", player.height), new XElement("description", player.description),
                new XElement("imageLink", player.imageLink), new XElement("status", player.status)));

            playerData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }

        // Delete Record
        public void DeletePlayer(String id)
        {
            playerData.Descendants("players").Elements("player").Where(i => i.Element("id").Value.Equals(id)).Remove();

            playerData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }

        // Edit Record
        public void EditPlayer(Player player)
        {

            XElement node = playerData.Descendants("players").Elements("player").Where(i => i.Element("id").Value.Equals(player.id)).FirstOrDefault();

            node.SetElementValue("clubName", player.clubName);
            node.SetElementValue("id", player.id);
            node.SetElementValue("number", player.number);
            node.SetElementValue("position", player.position);
            node.SetElementValue("dateOfBirth", player.dateOfBirth.Date);
            node.SetElementValue("placeOfBirth", player.placeOfBirth);
            node.SetElementValue("weight", player.weight);
            node.SetElementValue("height", player.height);
            node.SetElementValue("description", player.description);
            node.SetElementValue("imageLink", player.imageLink);
            node.SetElementValue("status", player.status);

            playerData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }
    }
}