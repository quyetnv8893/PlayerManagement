using PlayerManagement.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PlayerManagement.Models
{
    public class ClubRepository : IClubRepository
    {
        private List<Club> _allClubs;        

        public ClubRepository()
        {
            _allClubs = new List<Club>();            
            var clubs = from club in GlobalVariables.XmlData.Descendants("club")
                        select new Club(
                            club.Element("name").Value,
                            club.Element("logoLink").Value,
                            (DateTime)club.Element("foundedDate"),
                            club.Element("stadium").Value
                            );

            _allClubs.AddRange(clubs.ToList<Club>());

        }


        public IEnumerable<Club> GetClubs()
        {
            return _allClubs;
        }

        public Club GetClubByName(string id)
        {
            return _allClubs.Find(item => item.Name.Equals(id));
        }

        public void InsertClub(Club club)
        {

            GlobalVariables.XmlData.Descendants("playerManagement").FirstOrDefault().Add(new XElement("club",
                new XElement("name", club.Name),
                new XElement("logoLink", club.LogoLink),
                new XElement("foundedDate", club.FoundedDate),
                new XElement("stadium", club.Stadium)));

            GlobalVariables.XmlData.Save(HttpContext.Current.Server.MapPath(GlobalVariables.XmlPath));
            GlobalVariables.Update();
        }

        public void DeleteClub(string id)
        {
            GlobalVariables.XmlData.Elements("club").Where(i => i.Element("name").Value.Equals(id)).Remove();

            GlobalVariables.XmlData.Save(HttpContext.Current.Server.MapPath(GlobalVariables.XmlPath));
            GlobalVariables.Update();
        }

        public void EditClub(Club club)
        {
            XElement node = GlobalVariables.XmlData.Elements("club").
                Where(i => i.Element("name").Value.Equals(club.Name)).FirstOrDefault();

            node.SetElementValue("name", club.Name);
            node.SetElementValue("logoLink", club.LogoLink);
            node.SetElementValue("foundedDate", club.FoundedDate);
            node.SetElementValue("stadium", club.Stadium);

            GlobalVariables.XmlData.Save(HttpContext.Current.Server.MapPath(GlobalVariables.XmlPath));
            GlobalVariables.Update();
        }
    }
}