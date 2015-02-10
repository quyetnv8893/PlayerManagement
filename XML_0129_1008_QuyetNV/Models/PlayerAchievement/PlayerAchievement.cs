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
        [Key][Column(Order = 0)]
        [ForeignKey("Player")]
        [Display(Name = "Player ID")]
        public String PlayerID { get; set; }
        
        [Key][Column(Order = 1)]
        [ForeignKey("Achievement")]
        [Display(Name = "Achievement Name")]
        public String AchievementName { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public virtual Player Player { get; set; }

        [Required]
        public virtual Achievement Achievement { get; set; }

        public PlayerAchievement()
        {
            this.PlayerID = null;
            this.AchievementName = null;
            this.Number = 0;
        }

        public PlayerAchievement(int number, String playerId, String achievementName)
        {
            this.PlayerID = playerId;
            this.AchievementName = achievementName;
            this.Number = number;            
        }
    }
}