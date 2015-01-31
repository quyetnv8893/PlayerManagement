using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PlayerManagement.Models
{
    public class PlayerAchievementRepository : IPlayerAchievementRepository
    {
        public List<PlayerAchievement> allPlayerAchievements;
        private XDocument playerAchievementData;

        public PlayerAchievementRepository()
        {
            allPlayerAchievements = new List<PlayerAchievement>();

            playerAchievementData = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
            var playerAchievements = from playerAchievement in playerAchievementData.Descendants("player_achievement")
                                     select new PlayerAchievement((int)playerAchievement.Element("number"), playerAchievement.Element("playerId").Value,
                                         playerAchievement.Element("achievementName").Value);

            allPlayerAchievements.AddRange(playerAchievements.ToList<PlayerAchievement>());

        }

        public IEnumerable<PlayerAchievement> GetPlayerAchievementsByPlayerID(String playerId)
        {
            return allPlayerAchievements.FindAll(item => item.playerId.Equals(playerId));
        }

        public PlayerAchievement GetPlayerAchievementByAchievementName(String playerId, String achievementName)
        {

            return allPlayerAchievements.Find(item => (item.achievementName.Equals(achievementName)) &&
                                                        (item.achievementName.Equals(playerId)));
        }

        public void InsertPlayerAchievement(PlayerAchievement playerAchievement)
        {


            playerAchievementData.Descendants("player_achievement").FirstOrDefault().Add(new XElement("player_achievement",
                new XElement("number", playerAchievement.number), new XElement("playerID"), playerAchievement.playerId),
                new XElement("achievementName"), playerAchievement.achievementName);

            playerAchievementData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }


        public void DeletePlayerAchievement(String playerID, String achievementName)
        {
            playerAchievementData.Descendants("players_achievements").Elements("player_achievement").
                Where(i => i.Element("playerID").Value.Equals(playerID)).
                Where(i => i.Element("achievementName").Value.Equals(achievementName)).Remove();

            playerAchievementData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));

        }

        public void EditPlayerAchievement(PlayerAchievement playerAchievement)
        {
            XElement node = playerAchievementData.Descendants("players_achievements").Elements("player_achievement")
                .Where(i => i.Element("playerID").Value.Equals(playerAchievement.playerId))
                .Where(i => i.Element("achievementName").Value.Equals(playerAchievement.achievementName))
                .FirstOrDefault();

            node.SetElementValue("number", playerAchievement.number);
            node.SetElementValue("playerID", playerAchievement.playerId);
            node.SetElementValue("achievementName", playerAchievement.achievementName);

            playerAchievementData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }
    }
}