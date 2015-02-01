using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlayerManagement.Models
{    
    public class Match
    {
        [Key]
        public String id { get; set; }
        [Required]
        public DateTime time { get; set; }
        [Required]
        public String name { get; set; }
        [Required]
        public String score { get; set; }
        [Required]
        public String leagueName { get; set; }
        public virtual League league { get; set; }
        public Match()
        {
            this.id = null;
            this.time = DateTime.Now;
            this.name = null;
            this.score = null;
            this.leagueName = null;
        }
        public Match(String id, DateTime time, String name, String score, String leagueName)
        {
            this.id = id;
            this.time = time;
            this.name = name;
            this.score = score;
            this.leagueName = leagueName;
        }

  
    }        
}