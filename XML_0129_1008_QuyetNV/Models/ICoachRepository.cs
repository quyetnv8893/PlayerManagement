using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerManagement.Models
{
    public interface ICoachRepository
    {
        IEnumerable<Coach> GetCoachs();
        Coach GetCoachByName(String name);
        void InsertCoach(Coach coach);
        void DeleteCoach(String name);
        void EditCoach(Coach coach);
    }
}
