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
        [DataType(DataType.Date)]
        public DateTime From { get; set; }

        [DataType(DataType.Date)]
        public DateTime? To { get; set; }

        [Required]
        [Display(Name = "Club")]
        [DataType(DataType.Text)]
        public String ClubName { get; set; }

        [Display(Name = "Number of Goals")]
        [Required]
        [Range(0, 999, ErrorMessage = "Can only be between 0 .. 999")]
        [StringLength(3, ErrorMessage = "Max 3 digits")]
        public int? NumberOfGoals { get; set; }

        [ForeignKey("Player")]
        public String PlayerID { get; set; }

        public Player Player { get; set; }

        public Career()
        {
            this.ID = null;
            this.From = DateTime.Now;
            this.To = DateTime.Now;
            this.ClubName = null;
            this.NumberOfGoals = 0;
            this.PlayerID = null;
        }

        public Career(String id, DateTime from, DateTime? to, String clubName, int numberOfGoals, String playerID)
        {
            this.ID = id;
            this.From = from;
            if (to.HasValue)
            {
                this.To = to;
            }
            else
            {
                this.To = DateTime.Now;
            }
            this.ClubName = clubName;
            this.NumberOfGoals = numberOfGoals;
            this.PlayerID = playerID;
        }
    }
}