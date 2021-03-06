﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlayerManagement.Models
{
    public class Club
    {
        [Key]
        [DataType(DataType.Text)]
        public String Name { get; set; }

        [Required]
        [Display(Name = "Logo")]
        [DataType(DataType.ImageUrl)]
        public String LogoLink { get; set; }

        [Required]
        [Display(Name = "Founded Date")]

        public DateTime FoundedDate { get; set; }
        
        [Required]
        public String Stadium { get; set; }

        public Club()
        {
            this.Name = null;
            this.LogoLink = null;
            this.FoundedDate = DateTime.Now;
            this.Stadium = null;
        }

        public Club(String name, String logo, DateTime foundedDate, String stadium)
        {
            this.Name = name;
            this.LogoLink = logo;
            this.FoundedDate = foundedDate;
            this.Stadium = stadium;
        }
    }


}