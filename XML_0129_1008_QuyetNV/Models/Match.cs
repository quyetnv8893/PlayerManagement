using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayerManagement.Models
{    
    public class Match
    {
        public String id { get; set; }
        public DateTime time { get; set; }
        public String name { get; set; }
        public String score { get; set; }
        public String leagueName { get; set; }
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