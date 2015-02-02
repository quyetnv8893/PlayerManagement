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
        public String Name { get; set; }
        [Required]
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