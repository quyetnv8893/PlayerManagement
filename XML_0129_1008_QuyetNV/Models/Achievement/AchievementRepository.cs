using PlayerManagement.App_Start;
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
        

        public AchievementRepository()
        {
            _allAchievements = new List<Achievement>();           
            var Achievements = from achievement in GlobalVariables.XmlData.Descendants("achievement")
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
            Achievement achievement = _allAchievements.Find(item => item.Name.Equals(name));
            return achievement;
        }

        public void InsertAchievement(Achievement achievement)
        {

            GlobalVariables.XmlData.Descendants("achievements").FirstOrDefault().Add(new XElement("achievement",
                new XElement("name", achievement.Name),
                new XElement("imageLink", achievement.ImageLink)
                ));

            GlobalVariables.XmlData.Save(HttpContext.Current.Server.MapPath(GlobalVariables.XmlPath));
            GlobalVariables.Update();
        }

        public void DeleteAchievement(string name)
        {
            GlobalVariables.XmlData.Descendants("achievements").Elements("achievement")
                .Where(i => i.Element("name").Value.Equals(name)).Remove();

            GlobalVariables.XmlData.Save(HttpContext.Current.Server.MapPath(GlobalVariables.XmlPath));
            GlobalVariables.Update();
        }

        public void EditAchievement(Achievement achievement)
        {
            XElement node = GlobalVariables.XmlData.Descendants("achievements").Elements("achievement")
                .Where(i => i.Element("name").Value.Equals(achievement.Name)).FirstOrDefault();

            node.SetElementValue("name", achievement.Name);
            node.SetElementValue("imageLink", achievement.ImageLink);

            GlobalVariables.XmlData.Save(HttpContext.Current.Server.MapPath(GlobalVariables.XmlPath));
            GlobalVariables.Update();
        }
    }
}