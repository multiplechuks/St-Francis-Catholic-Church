using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StFrancisChurch.Models
{
    public class MemberRegistrationViewModel
    {
        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Firstname")]
        public string Firstname { get; set; }
        
        [Display(Name = "Othername")]
        public string Othername { get; set; }
    }
}