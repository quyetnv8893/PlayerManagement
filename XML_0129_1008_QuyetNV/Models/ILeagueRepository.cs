using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayerManagement.Models
{
    public interface ILeagueRepository
    {
        IEnumerable<League> GetLeagues();
        League GetLeagueByName(String name);
        void InsertLeague(League league);
        void DeleteLeague(String name);
        void EditLeague(League league);
    }
}