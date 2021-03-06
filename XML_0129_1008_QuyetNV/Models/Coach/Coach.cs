﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlayerManagement.Models
{
    public class Coach
    {
        [Key]
        [DataType(DataType.Text)]
        public String Name { get; set; }
        [Required]

        [Display(Name = "Image")]
        [DataType(DataType.ImageUrl)]
        public String ImageLink { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public String Position { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Club")]
        public String ClubName { get; set; }

        public Coach()
        {
            this.Name = null;
            this.ImageLink = null;
            this.Position = null;
            this.DateOfBirth = DateTime.Now;
            this.ClubName = null;
        }

        public Coach(String name, String imageLink, String position, DateTime dateOfBirth, String clubName)
        {
            this.Name = name;
            this.ImageLink = imageLink;
            this.Position = position;
            this.DateOfBirth = dateOfBirth;
            this.ClubName = clubName;
        }

        public Coach(string name, string position, DateTime dateOfBirth, string clubName)
        {
            // TODO: Complete member initialization
            this.Name = name;
            this.Position = position;
            this.DateOfBirth = dateOfBirth;
            this.ClubName = clubName;
        }
    }


}