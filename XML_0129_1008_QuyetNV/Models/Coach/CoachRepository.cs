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
        private XDocument _coachData;

        public CoachRepository()
        {
            _allCoachs = new List<Coach>();

            _coachData = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
            var coaches = from coach in _coachData.Descendants("coach")
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
            coach.Name = (from a in _coachData.Descendants("coach")
                          orderby a.Element("name").Value
                              descending
                          select a.Element("name").Value).FirstOrDefault();

            _coachData.Descendants("coaches").FirstOrDefault().Add(new XElement("coach",
                new XElement("name", coach.Name), 
                new XElement("imageLink"), coach.ImageLink),
                new XElement("position", coach.Position), 
                new XElement("dateOfBirth", coach.DateOfBirth),
                new XElement("clubName", coach.ClubName));

            _coachData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }

        public void DeleteCoach(string name)
        {
            _coachData.Descendants("coaches").Elements("coach")
                .Where(i => i.Element("name").Value.Equals(name)).Remove();

            _coachData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }

        public void EditCoach(Coach coach)
        {
            XElement node = _coachData.Descendants("coaches").Elements("coach").
                Where(i => i.Element("name").Value.Equals(coach.Name)).FirstOrDefault();

            node.SetElementValue("name", coach.Name);
            node.SetElementValue("imageLink", coach.ImageLink);
            node.SetElementValue("position", coach.Position);
            node.SetElementValue("dateOfBirth", coach.DateOfBirth);
            node.SetElementValue("clubName", coach.ClubName);
            _coachData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }
    }
}