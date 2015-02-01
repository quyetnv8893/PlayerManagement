using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PlayerManagement.Models
{
    public class CoachRepository : ICoachRepository
    {
        private List<Coach> allCoachs;
        private XDocument coachData;

        public CoachRepository()
        {
            allCoachs = new List<Coach>();

            coachData = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
            var coachs = from coach in coachData.Descendants("coach")
                               select new Coach(coach.Element("name").Value, coach.Element("imageLink").Value, coach.Element("position").Value,
                                   (DateTime)coach.Element("dateOfBirth"), coach.Element("clubname").Value);

            allCoachs.AddRange(coachs.ToList<Coach>());

        }


        public IEnumerable<Coach> GetCoachs()
        {
            return allCoachs;
        }

        public Coach GetCoachByName(string id)
        {
            return allCoachs.Find(item => item.name.Equals(id));
        }

        public void InsertCoach(Coach coach)
        {
            coach.name = (from a in coachData.Descendants("coach") orderby a.Element("name").Value 
                              descending select a.Element("name").Value).FirstOrDefault();

            coachData.Descendants("coachs").FirstOrDefault().Add(new XElement("coach",
                new XElement("name", coach.name), new XElement("imageLink"), coach.imageLink), 
                new XElement("position", coach.position), new XElement("dateOfBirth", coach.dateOfBirth),
                new XElement("clubName", coach.clubName));

            coachData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }

        public void DeleteCoach(string name)
        {
            coachData.Descendants("coachs").Elements("coach").Where(i => i.Element("name").Value.Equals(name)).Remove();

            coachData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }

        public void EditCoach(Coach coach)
        {
            XElement node = coachData.Descendants("coachs").Elements("coach").
                Where(i => i.Element("name").Value.Equals(coach.name)).FirstOrDefault();

            node.SetElementValue("name", coach.name);
            node.SetElementValue("imageLink", coach.imageLink);
            coachData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }
    }
}