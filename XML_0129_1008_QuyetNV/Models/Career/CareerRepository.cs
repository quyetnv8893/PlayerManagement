﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PlayerManagement.Models
{
    public class CareerRepository : ICareerRepository
    {
        private List<Career> _allCareers;
        private XDocument _careerData;

        public CareerRepository()
        {
            _allCareers = new List<Career>();

            _careerData = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
            var careers = from career in _careerData.Descendants("career")
                        select new Career(
                            career.Element("id").Value,
                            (DateTime) career.Element("from"),
                            (DateTime) career.Element("to"),
                            (int) career.Element("noOfGoals"),
                            career.Element("playerID").Value
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
            return _allCareers.Find(career => career.ID.Equals(id));
        }

        public void InsertCareer(Career career)
        {
            _careerData.Descendants("careers").FirstOrDefault().Add(new XElement("career",
                new XElement("id", career.ID),
                new XElement("from", career.From),
                new XElement("to", career.To),
                new XElement("noOfGoals", career.NumberOfGoals),
                new XElement("playerId", career.PlayerID)
                ));

            _careerData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }

        public void DeleteCareer(string id)
        {
            _careerData.Elements("career").Where(i => i.Element("id").Value.Equals(id)).Remove();
            _careerData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));

        }

        public void EditCareer(Career career)
        {
            XElement node = _careerData.Elements("career").
                Where(i => i.Element("id").Value.Equals(career.ID)).FirstOrDefault();

            node.SetElementValue("id", career.ID);
            node.SetElementValue("from", career.From);
            node.SetElementValue("to", career.To);
            node.SetElementValue("noOfGoals", career.NumberOfGoals);
            node.SetElementValue("playerId", career.PlayerID);

            _careerData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }
    }
}