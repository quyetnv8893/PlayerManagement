using PlayerManagement.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace PlayerManagement.Models
{
    public class CareerRepository : ICareerRepository
    {
        private List<Career> _allCareers;
        private IPlayerRepository _playerRepository = new PlayerRepository();
        public CareerRepository()
        {
            _allCareers = new List<Career>();
            IEnumerable<Career> careers = null;

            careers = from career in GlobalVariables.XmlData.Descendants("career")
                      select new Career(
                          career.Element("id").Value,                          
                          (DateTime)career.Element("from"),
                          (DateTime?)career.Element("to"),
                          career.Element("clubName").Value,
                          (int)career.Element("noOfGoals"),
                          career.Element("playerId").Value
                          );
            _allCareers.AddRange(careers.ToList<Career>());

        }

        public IEnumerable<Career> GetCareers()
        {
            return _allCareers;
        }

        public IEnumerable<Career> GetCareersByPlayerID(string playerID)
        {
            IEnumerable<Career> careers = _allCareers.FindAll(career => career.PlayerID.Equals(playerID));
            foreach (var career in careers)
            {
                DateTime d = new DateTime(2100, 1, 1, 0, 0, 0);
                if(DateTime.Compare((DateTime)career.To, d) == 0){
                    career.To = DateTime.Now;
                }
            }
            return careers;

        }

        public Career GetCareerByID(String id)
        {
            Career career = _allCareers.Find(c => c.ID.Equals(id));
            career.Player = _playerRepository.GetPlayerByID(career.PlayerID);
            return career;
        }

        public void InsertCareer(Career career)
        {
            GlobalVariables.XmlData.Descendants("careers").FirstOrDefault().Add(new XElement("career",
                new XElement("id", career.ID),
                new XElement("from", career.From.ToString("yyyy-MM-dd")),
                new XElement("to", career.To),
                new XElement("clubName", career.ClubName),
                new XElement("noOfGoals", career.NumberOfGoals),
                new XElement("playerId", career.PlayerID)
                ));

            GlobalVariables.XmlData.Save(HttpContext.Current.Server.MapPath(GlobalVariables.XmlPath));
            GlobalVariables.Update();
        }

        public void DeleteCareer(string id)
        {
            GlobalVariables.XmlData.Descendants("careers").Elements("career").Where(i => i.Element("id").Value.Equals(id)).Remove();
            GlobalVariables.XmlData.Save(HttpContext.Current.Server.MapPath(GlobalVariables.XmlPath));
            GlobalVariables.Update();
        }

        public void EditCareer(Career career)
        {
            if (GlobalVariables.XmlData != null)
            {
                XElement node = GlobalVariables.XmlData.Descendants("careers").Elements("career")
                .Where(i => i.Element("id").Value.Equals(career.ID)).FirstOrDefault();

                node.SetElementValue("id", career.ID);
                node.SetElementValue("from", career.From.ToString("yyyy-MM-dd"));
                node.SetElementValue("to", career.To);
                node.SetElementValue("clubName", career.ClubName);
                node.SetElementValue("noOfGoals", career.NumberOfGoals);
                node.SetElementValue("playerId", career.PlayerID);

            }

            GlobalVariables.XmlData.Save(HttpContext.Current.Server.MapPath(GlobalVariables.XmlPath));
            GlobalVariables.Update();
        }
    }
}