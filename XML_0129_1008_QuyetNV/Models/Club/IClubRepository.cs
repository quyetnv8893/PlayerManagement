using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerManagement.Models
{
    public interface IClubRepository
    {
        IEnumerable<Club> GetClubs();
        Club GetClubByName(String name);
        void InsertClub(Club club);
        void DeleteClub(String name);
        void EditClub(Club club);
    }
}
