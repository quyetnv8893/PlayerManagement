using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlayerManagement.Models
{
    public class Club
    {
        [Key]
        public String name { get; set; }
        [Required]
        public String logoLink { get; set; }
        [Required]
        public DateTime foundedDate { get; set; }
        [Required]
        public String stadium { get; set; }

        public Club()
        {
            this.name = name;
            this.logoLink = null;
            this.foundedDate = DateTime.Now;
            this.stadium = null;
        }

        public Club(String name, String logo, DateTime foundedDate, String stadium)
        {
            this.name = name;
            this.logoLink = logo;
            this.foundedDate = foundedDate;
            this.stadium = stadium;
        }
    }


}