using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PlayerManagement.Models
{
    public class AchievementRepository :IAchievementRepository
    {
        private List<Achievement> allAchievements;
        private XDocument achievementData;

        public AchievementRepository()
        {
            allAchievements = new List<Achievement>();

            achievementData = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
            var achievements = from achievement in achievementData.Descendants("achievement")
                               select new Achievement(achievement.Element("name").Value, achievement.Element("imageLink").Value);

            allAchievements.AddRange(achievements.ToList<Achievement>());

        }


        public IEnumerable<Achievement> GetAchievements()
        {
            return allAchievements;
        }

        public Achievement GetAchievementByName(string name)
        {
            return allAchievements.Find(item => item.name.Equals(name));
        }

        public void InsertAchievement(Achievement achievement)
        {
            achievement.name = (from a in achievementData.Descendants("achievement") orderby a.Element("name").Value descending select a.Element("name").Value).FirstOrDefault();

            achievementData.Descendants("achievements").FirstOrDefault().Add(new XElement("achievement",
                new XElement("name", achievement.name), new XElement("imageLink"), achievement.imageLink));

            achievementData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }

        public void DeleteAchievement(string name)
        {
            achievementData.Descendants("achievements").Elements("achievement").Where(i => i.Element("name").Value.Equals(name)).Remove();

            achievementData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }

        public void EditAchievement(Achievement achievement)
        {
            XElement node = achievementData.Descendants("achievements").Elements("achievement").Where(i => i.Element("name").Value.Equals(achievement.name)).FirstOrDefault();

            node.SetElementValue("name", achievement.name);
            node.SetElementValue("imageLink", achievement.imageLink);
            achievementData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }
    }
}