using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlayerManagement.Models
{
    public class Player
    {
        [Key]
        public String ID { get; set; }

        [Required]
        [Display(Name = "Club")]
        public String ClubName { get; set; }        
        
        [Required]
        public int Number { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Position { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [Display(Name = "Place of Birth")]
        public string PlaceOfBirth { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public double Height { get; set; }
        [Required]
        public String Description { get; set; }
        [Required]
        [Display(Name = "Image")]
        public String ImageLink { get; set; }
        [Required]
        public Boolean Status { get; set; }
        
        public int Age
        {
            get
            {
                DateTime now = DateTime.Today;
                int age = now.Year - DateOfBirth.Year;
                if (DateOfBirth > now.AddYears(-age)) age--; ;
                return age;
            }

        }


        public virtual ICollection<PlayerAchievement> PlayerAchievements { get; set; }

        public Player(String clubName, String id, int number, String name, String position,
            DateTime dateOfBirth, String placeOfBirth, double weight, double height, String description, String imageLink,
            Boolean status)
        {
            this.ClubName = clubName;
            this.ID = id;
            this.Number = number;
            this.Name = name;
            this.Position = position;
            this.DateOfBirth = dateOfBirth;
            this.PlaceOfBirth = placeOfBirth;
            this.Weight = weight;
            this.Height = height;
            this.Description = description;
            this.ImageLink = imageLink;
            this.Status = status;
        }

        public Player()
        {
            this.ClubName = null;
            this.ID = null;
            this.Number = 0;
            this.Name = null;
            this.Position = null;
            this.DateOfBirth = DateTime.Now;
            this.PlaceOfBirth = null;
            this.Weight = 0.0;
            this.Height = 0.0;
            this.Description = null;
            this.ImageLink = null;
            this.Status = true;
        }
    }
}