using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PlayerManagement.Models
{
    public class PlayerManagementContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public PlayerManagementContext() : base("name=PlayerManagementContext")
        {
        }

        public System.Data.Entity.DbSet<PlayerManagement.Models.Player> Players { get; set; }

        public System.Data.Entity.DbSet<PlayerManagement.Models.Achievement> Achievements { get; set; }

        public System.Data.Entity.DbSet<PlayerManagement.Models.PlayerAchievement> PlayerAchievements { get; set; }
    
    }
}
