using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerManagement.Models
{
    public interface ICareerRepository
    {
        IEnumerable<Career> GetCareers();
        IEnumerable<Career> GetCareersByPlayerID(String playerID);
        Career GetCareerByID(String id);
        void InsertCareer(Career career);
        void DeleteCareer(String id);
        void EditCareer(Career career);  
    }
}
