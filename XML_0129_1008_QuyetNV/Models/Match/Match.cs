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
        public String ID { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public String Name { get; set; }
        
        [Required]
        [RegularExpression("^[0-9]+-[0-9]+$", ErrorMessage ="Score format must be 0-0")]
        public String Score { get; set; }

        [Required]
        [Display(Name = "League")]
        [DataType(DataType.Text)]
        public String LeagueName { get; set; }

        public virtual IEnumerable<PlayerMatch.PlayerMatch> PlayerMatches { get; set; }
        public virtual League League { get; set; }
        public Match()
        {
            this.ID = null;
            this.Time = DateTime.Now;
            this.Name = null;
            this.Score = null;
            this.LeagueName = null;
        }
        public Match(String id, DateTime time, String name, String score, String leagueName)
        {
            this.ID = id;
            this.Time = time;
            this.Name = name;
            this.Score = score;
            this.LeagueName = leagueName;
        }

  
    }        
}