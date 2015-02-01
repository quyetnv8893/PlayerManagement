using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlayerManagement.Models
{
    public class Coach
    {
        [Key]
        public String name { get; set; }
        [Required]
        public String imageLink { get; set; }
        [Required]
        public String position { get; set; }
        [Required]
        public DateTime dateOfBirth { get; set; }
        [Required]
        public String clubName { get; set; }

        public Coach()
        {
            this.name = null;
            this.imageLink = null;
            this.position = null;
            this.dateOfBirth = DateTime.Now;
            this.clubName = null;
        }

        public Coach(String name, String imageLink, String position, DateTime dateOfBirth, String clubName)
        {
            this.name = name;
            this.imageLink = imageLink;
            this.position = position;
            this.dateOfBirth = dateOfBirth;
            this.clubName = clubName;
        }
    }


}