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
        public String logo { get; set; }
        [Required]
        public DateTime foundationDate { get; set; }
        [Required]
        public String stadium { get; set; }

        public Club()
        {
            this.logo = null;
            this.foundationDate = DateTime.Now;
            this.stadium = null;
        }

        public Club(String name, String logo, DateTime foundationDate, String stadium)
        {
            this.logo = logo;
            this.foundationDate = foundationDate;
            this.stadium = stadium;
        }
    }


}