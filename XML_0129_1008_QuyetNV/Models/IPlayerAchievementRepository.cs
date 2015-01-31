using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerManagement.Models
{
    public interface IPlayerAchievementRepository
    {
        IEnumerable<PlayerAchievement> GetPlayerAchievementsByPlayerID(String playerID);
        PlayerAchievement GetPlayerAchievementByAchievementName(String playerID, String achievementName);
        void InsertPlayerAchievement(PlayerAchievement playerAchievement);
        void DeletePlayerAchievement(String playerID, String achievementName);
        void EditPlayerAchievement(PlayerAchievement playerAchievement);        
    }
}
