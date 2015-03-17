using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlayerManagement.Models.PlayerMatch
{
    public class PlayerMatch
    {
        [Key][Column(Order = 0)]
        [Display(Name = "Player ID")]
        public String PlayerID { get; set; }

        [Key][Column(Order = 1)]
        [Display(Name = "Match ID")]
        public String MatchID { get; set; }

        [Required]
        [Range(0, 99, ErrorMessage = "Can only be between 0 .. 99")]
        [Display(Name = "Number of Goals")]
        public int NumberOfGoals { get; set; }

        [Required]
        [Display(Name = "Number of Yellow cards")]
        [Range(0, 99, ErrorMessage = "Can only be between 0 .. 99")]
        public int NumberOfYellows { get; set; }

        [Required]
        [Range(0, 99, ErrorMessage = "Can only be between 0 .. 99")]
        [Display(Name = "Number of Red cards")]
        public int NumberOfReds { get; set; }

        public virtual Player Player { get; set; }

        public virtual Match Match { get; set; }

        public PlayerMatch() { }
        public PlayerMatch(String PlayerId, String MatchId, int NumberOfGoals, int NumberOfYellows, int NumberOfReds)
        {
            this.PlayerID = PlayerId;
            this.MatchID = MatchId;
            this.NumberOfGoals = NumberOfGoals;
            this.NumberOfReds = NumberOfReds;
            this.NumberOfYellows = NumberOfYellows;            
        }
    }
}