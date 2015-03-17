using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlayerManagement.Models
{
    public class League
    {     
        [Key]
        [DataType(DataType.Text)]
        public String Name { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Logo")]
        public String LogoLink { get; set; }

        public League() {
            this.Name = null;
            this.LogoLink = null;
        }
        public League(String name, String logoLink){
            this.Name = name;
            this.LogoLink = logoLink;
        }
    }
}