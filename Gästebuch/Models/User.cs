using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Gästebuch.Models
{
    public class User
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Benutzername { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Password { get; set; }

        [NotMapped]
        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}