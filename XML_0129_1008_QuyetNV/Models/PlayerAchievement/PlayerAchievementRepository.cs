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
        private XDocument _playerAchievementData;
        private IAchievementRepository _achievementRepository;
        private IPlayerRepository _playerRepository;

        public PlayerAchievementRepository()
        {
            _allPlayerAchievements = new List<PlayerAchievement>();

            _playerAchievementData = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
            var playerAchievements = from playerAchievement in _playerAchievementData.Descendants("player_achievement")
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
            var playerAchievement = _allPlayerAchievements.Find(item => (item.AchievementName.Equals(achievementName)) &&
                                                        (item.PlayerID.Equals(playerId)));
            _achievementRepository = new AchievementRepository();
            playerAchievement.Achievement = _achievementRepository.GetAchievementByName(achievementName);

            _playerRepository = new PlayerRepository();
            playerAchievement.Player = _playerRepository.GetPlayerByID(playerId);

            return playerAchievement;
        }

        public void InsertPlayerAchievement(PlayerAchievement playerAchievement)
        {

            _playerAchievementData.Descendants("players_achievements").FirstOrDefault().Add(new XElement("player_achievement",
                new XElement("number", playerAchievement.Number), 
                new XElement("playerId", playerAchievement.PlayerID),
                new XElement("achievementName", playerAchievement.AchievementName)));

            _playerAchievementData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }


        public void DeletePlayerAchievement(String playerID, String achievementName)
        {
            _playerAchievementData.Descendants("players_achievements").Elements("player_achievement").
                Where(i => i.Element("playerId").Value.Equals(playerID)).
                Where(i => i.Element("achievementName").Value.Equals(achievementName)).
                Remove();

            _playerAchievementData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));

        }

        public void EditPlayerAchievement(PlayerAchievement playerAchievement)
        {
            XElement node = _playerAchievementData.Descendants("players_achievements").Elements("player_achievement")
                .Where(i => i.Element("playerId").Value.Equals(playerAchievement.PlayerID))
                .Where(i => i.Element("achievementName").Value.Equals(playerAchievement.AchievementName))
                .FirstOrDefault();

            node.SetElementValue("number", playerAchievement.Number);
            node.SetElementValue("playerId", playerAchievement.PlayerID);
            node.SetElementValue("achievementName", playerAchievement.AchievementName);

            _playerAchievementData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }
    }
}