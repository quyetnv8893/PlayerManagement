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
            IEnumerable<Career> careers = from career in GlobalVaraiables.XmlData.Descendants("career")
                      select new Career(
                          career.Element("id").Value,
                          //XmlConvert.ToDateTime(career.Element("from").Value, XmlDateTimeSerializationMode.Local),
                          //XmlConvert.ToDateTime(career.Element("to").Value, XmlDateTimeSerializationMode.Local),
                          (DateTime)career.Element("from"),
                          (DateTime)career.Element("to"),
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
            return _allCareers.FindAll(career => career.PlayerID.Equals(playerID));
            
        }

        public Career GetCareerByID(String id)
        {
            Career career = _allCareers.Find(c => c.ID.Equals(id));
            career.Player = _playerRepository.GetPlayerByID(career.PlayerID);
            return career;
        }

        public void InsertCareer(Career career)
        {
            GlobalVaraiables.XmlData.Descendants("careers").FirstOrDefault().Add(new XElement("career",
                new XElement("id", career.ID),
                new XElement("from", career.From),
                new XElement("to", career.To),
                new XElement("clubName", career.ClubName),
                new XElement("noOfGoals", career.NumberOfGoals),
                new XElement("playerId", career.PlayerID)
                ));

            GlobalVaraiables.XmlData.Save(HttpContext.Current.Server.MapPath(GlobalVaraiables.XmlPath));
            GlobalVaraiables.Update();
        }

        public void DeleteCareer(string id)
        {
            GlobalVaraiables.XmlData.Descendants("careers").Elements("career").Where(i => i.Element("id").Value.Equals(id)).Remove();
            GlobalVaraiables.XmlData.Save(HttpContext.Current.Server.MapPath(GlobalVaraiables.XmlPath));
            GlobalVaraiables.Update();
        }

        public void EditCareer(Career career)
        {
            if (GlobalVaraiables.XmlData != null)
            {
                XElement node = GlobalVaraiables.XmlData.Descendants("careers").Elements("career")
                .Where(i => i.Element("id").Value.Equals(career.ID)).FirstOrDefault();

                node.SetElementValue("id", career.ID);
                node.SetElementValue("from", career.From);
                node.SetElementValue("to", career.To);
                node.SetElementValue("clubName", career.ClubName);
                node.SetElementValue("noOfGoals", career.NumberOfGoals);
                node.SetElementValue("playerId", career.PlayerID);

            }
            
            GlobalVaraiables.XmlData.Save(HttpContext.Current.Server.MapPath(GlobalVaraiables.XmlPath));
            GlobalVaraiables.Update();
        }
    }
}