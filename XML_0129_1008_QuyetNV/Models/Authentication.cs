using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlayerManagement.Models
{
    public class Authentication
    {
        [Key]
        [Display(Name = "Username")]
        public String Username { get; set; }
        [Required]
        [Display(Name = "Password")]
        public String MD5Password { get; set; }

        public Authentication()
        {
            this.Username = null;
            this.MD5Password = null;
        }

        public Authentication(String username, String md5Password)
        {
            this.Username = username;
            this.MD5Password = md5Password;
        }
    }
}