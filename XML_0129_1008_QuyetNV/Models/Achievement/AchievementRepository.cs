using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PlayerManagement.Models
{
    public class AchievementRepository : IAchievementRepository
    {
        private List<Achievement> _allAchievements;
        private XDocument _achievementData;

        public AchievementRepository()
        {
            _allAchievements = new List<Achievement>();

            _achievementData = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
            var Achievements = from achievement in _achievementData.Descendants("achievement")
                               select new Achievement(
                                   achievement.Element("name").Value,
                                   achievement.Element("imageLink").Value);

            _allAchievements.AddRange(Achievements.ToList<Achievement>());

        }

        public IEnumerable<Achievement> GetAchievements()
        {
            return _allAchievements;
        }

        public IEnumerable<String> GetAchievementNames() { 
            List<String> achievementNames = new List<String>();
            foreach (var a in _allAchievements)
	        {
                achievementNames.Add(a.Name);
	        }

            return achievementNames;
        }

        public Achievement GetAchievementByName(string name)
        {
            return _allAchievements.Find(item => item.Name.Equals(name));
        }

        public void InsertAchievement(Achievement achievement)
        {

            _achievementData.Descendants("achievements").FirstOrDefault().Add(new XElement("achievement",
                new XElement("name", achievement.Name),
                new XElement("imageLink", achievement.ImageLink)
                ));

            _achievementData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }

        public void DeleteAchievement(string name)
        {
            _achievementData.Descendants("achievements").Elements("achievement")
                .Where(i => i.Element("name").Value.Equals(name)).Remove();

            _achievementData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }

        public void EditAchievement(Achievement achievement)
        {
            XElement node = _achievementData.Descendants("achievements").Elements("achievement")
                .Where(i => i.Element("name").Value.Equals(achievement.Name)).FirstOrDefault();

            node.SetElementValue("name", achievement.Name);
            node.SetElementValue("imageLink", achievement.ImageLink);

            _achievementData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }
    }
}