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

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Home Parish")]
        public string HomeParish { get; set; }

        [Display(Name = "Town")]
        public string Town { get; set; }

        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "EmailAddress")]
        public string EmailAddress { get; set; }

        [Display(Name = "Ist Phone Number")]
        public string Phone { get; set; }

        [Display(Name = "2nd Phone Number")]
        public string Phone2 { get; set; }

        [Display(Name = "Employment Address")]
        public string EmpolymentAddress { get; set; }

        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

        [Display(Name = "Next of kin")]
        public string NextOfKin { get; set; }

        [Display(Name = "Next of kin marital status")]
        public string NextOfKinMaritalStatus { get; set; }

        [Display(Name = "Next of kin address")]
        public string NextOfKinAddress { get; set; }

        [Display(Name = "Spouse Name")]
        public string SpouseName { get; set; }

        [Display(Name = "Spouse Ist Phone Number")]
        public string SpousePhone1 { get; set; }

        [Display(Name = "Spouse 2nd Phone Number")]
        public string SpousePhone2 { get; set; }

        [Display(Name = "Size of Male in Family")]
        public string SizeOfFamilyMale { get; set; }

        [Display(Name = "Size of Feale in Family")]
        public string SizeOfFamilyFemale { get; set; }

        [Display(Name = "Statutory Group")]
        public string StatutoryGroup { get; set; }
    }
}