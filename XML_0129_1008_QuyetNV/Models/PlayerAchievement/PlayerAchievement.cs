using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlayerManagement.Models
{
    public class PlayerAchievement
    {
        [ForeignKey("Player")]
        public String playerId { get; set; }
        
        [ForeignKey("Achievement")]
        public String achievementName { get; set; }

        [Required]
        public int number { get; set; }

        [Required]
        public virtual Player player { get; set; }

        [Required]
        public virtual Achievement achievement { get; set; }

        public PlayerAchievement()
        {
            this.playerId = null;
            this.achievementName = null;
            this.number = 0;
        }

        public PlayerAchievement(int number, String playerId, String achievementName)
        {
            this.playerId = playerId;
            this.achievementName = achievementName;
            this.number = number;            
        }
    }
}