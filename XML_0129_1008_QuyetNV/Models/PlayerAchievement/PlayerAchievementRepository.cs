using PlayerManagement.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PlayerManagement.Models
{
    public class PlayerAchievementRepository : IPlayerAchievementRepository
    {
        public List<PlayerAchievement> _allPlayerAchievements;        
        private IAchievementRepository _achievementRepository;
        private IPlayerRepository _playerRepository;
        public PlayerAchievementRepository()
        {
            _allPlayerAchievements = new List<PlayerAchievement>();            
            var playerAchievements = from playerAchievement in GlobalVaraiables.XmlData.Descendants("player_achievement")
                                     select new PlayerAchievement(
                                         (int)playerAchievement.Element("number"),
                                         playerAchievement.Element("playerId").Value,
                                         playerAchievement.Element("achievementName").Value);

            _allPlayerAchievements.AddRange(playerAchievements.ToList<PlayerAchievement>());

        }

        public IEnumerable<PlayerAchievement> GetPlayerAchievementsByPlayerID(String id)
        {
            return _allPlayerAchievements.FindAll(item => item.PlayerID.Equals(id));
        }

        public PlayerAchievement GetPlayerAchievement(String playerId, String achievementName)
        {
            PlayerAchievement playerAchievement = null;

            playerAchievement = _allPlayerAchievements.Find(item => (item.AchievementName.Equals(achievementName)) &&
                                                        (item.PlayerID.Equals(playerId)));
             _achievementRepository = new AchievementRepository();
                
            if (playerAchievement != null)
            {
                Achievement achievement = _achievementRepository.GetAchievementByName(achievementName);
               
                playerAchievement.Achievement = achievement;

                _playerRepository = new PlayerRepository();
                playerAchievement.Player = _playerRepository.GetPlayerByID(playerId);

                return playerAchievement;
            }
            else
            {
                return null;
            }
            
        }

        public void InsertPlayerAchievement(PlayerAchievement playerAchievement)
        {

            GlobalVaraiables.XmlData.Descendants("players_achievements").FirstOrDefault().Add(new XElement("player_achievement",
                new XElement("number", playerAchievement.Number),
                new XElement("playerId", playerAchievement.PlayerID),
                new XElement("achievementName", playerAchievement.AchievementName)));

            GlobalVaraiables.XmlData.Save(HttpContext.Current.Server.MapPath(GlobalVaraiables.XmlPath));
            GlobalVaraiables.Update();
        }


        public void DeletePlayerAchievement(String playerID, String achievementName)
        {
            GlobalVaraiables.XmlData.Descendants("players_achievements").Elements("player_achievement").
                Where(i => i.Element("playerId").Value.Equals(playerID)).
                Where(i => i.Element("achievementName").Value.Equals(achievementName)).
                Remove();
            GlobalVaraiables.XmlData.Save(HttpContext.Current.Server.MapPath(GlobalVaraiables.XmlPath));
            GlobalVaraiables.Update();
        }

        public void EditPlayerAchievement(PlayerAchievement playerAchievement)
        {
            XElement node = GlobalVaraiables.XmlData.Descendants("players_achievements").Elements("player_achievement")
                .Where(i => i.Element("playerId").Value.Equals(playerAchievement.PlayerID))
                .Where(i => i.Element("achievementName").Value.Equals(playerAchievement.AchievementName))
                .FirstOrDefault();

            node.SetElementValue("number", playerAchievement.Number);
            node.SetElementValue("playerId", playerAchievement.PlayerID);
            node.SetElementValue("achievementName", playerAchievement.AchievementName);
            GlobalVaraiables.XmlData.Save(HttpContext.Current.Server.MapPath(GlobalVaraiables.XmlPath));
            GlobalVaraiables.Update();
        }
    }
}