using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlayerManagement.Models
{
    public class Achievement
    {
        [Key]
        public String name { get; set; }

        [Required]
        public String imageLink { get; set; }

        public virtual ICollection<PlayerAchievement> playerAchievements { get; set; }

        public Achievement()
        {
            this.name = null;
            this.imageLink = null;
        }

        public Achievement(String name, String imageLink)
        {
            this.name = name;
            this.imageLink = imageLink;
        }
    }
}