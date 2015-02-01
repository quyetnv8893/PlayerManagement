using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PlayerManagement.Models
{
    public class ClubRepository
    {
        private List<Club> allClubs;
        private XDocument clubData;

        public ClubRepository()
        {
            allClubs = new List<Club>();

            clubData = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
            var clubs = from club in clubData.Descendants("club")
                               select new Club(club.Element("name").Value, club.Element("logo").Value,
                                   (DateTime)club.Element("foundedDate"), club.Element("stadium").Value);

            allClubs.AddRange(clubs.ToList<Club>());

        }


        public IEnumerable<Club> GetClubs()
        {
            return allClubs;
        }

        public Club GetClubByName(string name)
        {
            return allClubs.Find(item => item.name.Equals(name));
        }

        public void InsertClub(Club club)
        {          

            clubData.Descendants("club").FirstOrDefault().Add(new XElement("club",
                new XElement("name", club.name), new XElement("logo"), club.logo), 
                new XElement("foundedDate", club.foundationDate), new XElement("stadium", club.stadium));

            clubData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }

        public void DeleteClub(string id)
        {
            clubData.Elements("club").Where(i => i.Element("name").Value.Equals(id)).Remove();

            clubData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }

        public void EditClub(Club club)
        {
            XElement node = clubData.Elements("club").
                Where(i => i.Element("name").Value.Equals(club.name)).FirstOrDefault();

            node.SetElementValue("name", club.name);
            node.SetElementValue("logo", club.logo);
            node.SetElementValue("foundedDate", club.foundationDate);
            node.SetElementValue("stadium", club.stadium);
            
            clubData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }
    }
}