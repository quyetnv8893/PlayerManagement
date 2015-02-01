using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlayerManagement.Models
{
    public class Player
    {
        [Required]
        public String clubName { get; set; }
        [Required]
        public String id { get; set; }
        [Required]
        public int number { get; set; }
        [Required]
        public String name { get; set; }
        [Required]
        public String position { get; set; }
        [Required]
        public DateTime dateOfBirth { get; set; }
        [Required]
        public string placeOfBirth { get; set; }
        [Required]
        public double weight { get; set; }
        [Required]
        public double height { get; set; }
        [Required]
        public String description { get; set; }
        [Required]
        public String imageLink { get; set; }
        [Required]
        public Boolean status { get; set; }

        public virtual ICollection<PlayerAchievement> achievements { get; set; }

        public Player(String clubName, String id, int number, String name, String position,
            DateTime dateOfBirth, String placeOfBirth, double weight, double height, String description, String imageLink,
            Boolean status)
        {
            this.clubName = clubName;
            this.id = id;
            this.number = number;
            this.name = name;
            this.position = position;
            this.dateOfBirth = dateOfBirth;
            this.placeOfBirth = placeOfBirth;
            this.weight = weight;
            this.height = height;
            this.description = description;
            this.imageLink = imageLink;
            this.status = status;
        }

        public Player()
        {
            this.clubName = null;
            this.id = null;
            this.number = 0;
            this.name = null;
            this.position = null;
            this.dateOfBirth = DateTime.Now;
            this.placeOfBirth = null;
            this.weight = 0.0;
            this.height = 0.0;
            this.description = null;
            this.imageLink = null;
            this.status = true;
        }
    }
}