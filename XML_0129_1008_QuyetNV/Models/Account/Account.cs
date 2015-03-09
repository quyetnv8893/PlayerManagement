using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlayerManagement.Models
{
    public class Account
    {
        [Key]
        [Display(Name = "Username")]
        public String Username { get; set; }
        
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public String MD5Password { get; set; }

        public Account()
        {
            this.Username = null;
            this.MD5Password = null;
        }

        public Account(String username, String md5Password)
        {
            this.Username = username;
            this.MD5Password = md5Password;
        }
    }
}