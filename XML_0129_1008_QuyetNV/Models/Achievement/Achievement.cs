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
        public String Name { get; set; }

        [Required]
        [Display(Name = "Image")]
        public String ImageLink { get; set; }

        public virtual ICollection<PlayerAchievement> PlayerAchievements { get; set; }

        public Achievement()
        {
            this.Name = null;
            this.ImageLink = null;
        }

        public Achievement(String name, String imageLink)
        {
            this.Name = name;
            this.ImageLink = imageLink;
        }
    }
}