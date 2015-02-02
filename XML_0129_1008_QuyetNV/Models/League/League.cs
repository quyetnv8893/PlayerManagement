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
        public String name { get; set; }
        [Required]
        public String logoLink { get; set; }
        public League() {
            this.name = null;
            this.logoLink = null;
        }
        public League(String name, String logoLink){
            this.name = name;
            this.logoLink = logoLink;
        }
    }
}