using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlayerManagement.Models
{
    public class Career
    {
        [Key]
        public String ID { get; set; }
        [Required]
        public DateTime From { get; set; }       

        public DateTime? To { get; set; }
        public int? NumberOfGoals { get; set; }

        [ForeignKey("Player")]
        public String PlayerID { get; set; }

        public virtual Player Player { get; set; }

        public Career()
        {
            this.ID = null;
            this.From = DateTime.Now;
            this.To = DateTime.Now;
            this.NumberOfGoals = 0;
            this.PlayerID = null;
        }

        public Career(String id, DateTime from, DateTime to, int numberOfGoals, String playerID)
        {
            this.ID = id;
            this.From = from;
            this.To = to;
            this.NumberOfGoals = numberOfGoals;
            this.PlayerID = playerID;
        }
    }
}