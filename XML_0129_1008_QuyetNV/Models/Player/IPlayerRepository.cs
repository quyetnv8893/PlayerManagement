using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerManagement.Models
{
    public interface IPlayerRepository
    {
        IEnumerable<Player> GetPlayers();
        Player GetPlayerByID(String id);
        void InsertPlayer(Player player);
        void DeletePlayer(String id);
        void EditPlayer(Player player);
    }
}
