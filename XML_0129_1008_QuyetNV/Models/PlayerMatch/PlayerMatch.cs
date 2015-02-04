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
        public String PlayerId { get; set; }
        [Key][Column(Order = 1)]
        public String MatchId { get; set; }
        [Required]
        public int NumberOfGoals { get; set; }
        [Required]
        public int NumberOfYellows { get; set; }
        [Required]
        public int NumberOfReds { get; set; }
        public virtual Player Player { get; set; }
        public virtual Match Match { get; set; }
        public PlayerMatch() { }
        public PlayerMatch(String PlayerId, String MatchId, int NumberOfGoals, int NumberOfYellows, int NumberOfReds)
        {
            this.PlayerId = PlayerId;
            this.MatchId = MatchId;
            this.NumberOfGoals = NumberOfGoals;
            this.NumberOfReds = NumberOfReds;
            this.NumberOfYellows = NumberOfYellows;            
        }
    }
}