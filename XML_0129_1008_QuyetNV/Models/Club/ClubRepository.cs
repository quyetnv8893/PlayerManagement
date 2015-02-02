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
        private XDocument _clubData;

        public ClubRepository()
        {
            _allClubs = new List<Club>();

            _clubData = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
            var clubs = from club in _clubData.Descendants("club")
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

            _clubData.Descendants("playerManagement").FirstOrDefault().Add(new XElement("club",
                new XElement("name", club.Name), 
                new XElement("logoLink", club.LogoLink), 
                new XElement("foundedDate", club.FoundedDate), 
                new XElement("stadium", club.Stadium)));

            _clubData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }

        public void DeleteClub(string id)
        {
            _clubData.Elements("club").Where(i => i.Element("name").Value.Equals(id)).Remove();

            _clubData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }

        public void EditClub(Club club)
        {
            XElement node = _clubData.Elements("club").
                Where(i => i.Element("name").Value.Equals(club.Name)).FirstOrDefault();

            node.SetElementValue("name", club.Name);
            node.SetElementValue("logoLink", club.LogoLink);
            node.SetElementValue("foundedDate", club.FoundedDate);
            node.SetElementValue("stadium", club.Stadium);
            
            _clubData.Save(HttpContext.Current.Server.MapPath("~/App_Data/player_management.xml"));
        }
    }
}