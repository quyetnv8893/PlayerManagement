using PlayerManagement.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PlayerManagement.Models
{
    public class CoachRepository : ICoachRepository
    {
        private List<Coach> _allCoachs;        
        public CoachRepository()
        {
            _allCoachs = new List<Coach>();            
            var coaches = from coach in GlobalVariables.XmlData.Descendants("coach")
                          select new Coach(
                              coach.Element("name").Value, 
                              coach.Element("imageLink").Value, 
                              coach.Element("position").Value,
                              (DateTime)coach.Element("dateOfBirth"), 
                              coach.Element("clubName").Value);

            _allCoachs.AddRange(coaches.ToList<Coach>());

        }


        public IEnumerable<Coach> GetCoaches()
        {
            return _allCoachs;
        }

        public Coach GetCoachByName(string name)
        {
            return _allCoachs.Find(item => item.Name.Equals(name));
        }

        public void InsertCoach(Coach coach)
        {
            coach.Name = (from a in GlobalVariables.XmlData.Descendants("coach")
                          orderby a.Element("name").Value
                              descending
                          select a.Element("name").Value).FirstOrDefault();

            GlobalVariables.XmlData.Descendants("coaches").FirstOrDefault().Add(new XElement("coach",
                new XElement("name", coach.Name), 
                new XElement("imageLink"), coach.ImageLink),
                new XElement("position", coach.Position), 
                new XElement("dateOfBirth", coach.DateOfBirth),
                new XElement("clubName", coach.ClubName));

            GlobalVariables.XmlData.Save(HttpContext.Current.Server.MapPath(GlobalVariables.XmlPath));
            GlobalVariables.Update();
        }

        public void DeleteCoach(string name)
        {
            GlobalVariables.XmlData.Descendants("coaches").Elements("coach")
                .Where(i => i.Element("name").Value.Equals(name)).Remove();

            GlobalVariables.XmlData.Save(HttpContext.Current.Server.MapPath(GlobalVariables.XmlPath));
            GlobalVariables.Update();
        }

        public void EditCoach(Coach coach)
        {
            XElement node = GlobalVariables.XmlData.Descendants("coaches").Elements("coach").
                Where(i => i.Element("name").Value.Equals(coach.Name)).FirstOrDefault();

            node.SetElementValue("name", coach.Name);
            node.SetElementValue("imageLink", coach.ImageLink);
            node.SetElementValue("position", coach.Position);
            node.SetElementValue("dateOfBirth", coach.DateOfBirth);
            node.SetElementValue("clubName", coach.ClubName);
            GlobalVariables.XmlData.Save(HttpContext.Current.Server.MapPath(GlobalVariables.XmlPath));
            GlobalVariables.Update();
        }
    }
}