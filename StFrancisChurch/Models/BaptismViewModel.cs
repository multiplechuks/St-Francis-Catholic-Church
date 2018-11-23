using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StFrancisChurch.Models
{
    public class BaptismViewModel
    {
        [Required]
        [Display(Name = "Baptism Number")]
        public string BaptismNumber { get; set; }

        [Required]
        [Display(Name = "Baptism Place")]
        public string BaptismPlace { get; set; }

        [Required]
        [Display(Name = "Baptism Date")]
        public string BaptismDate { get; set; }

        [Required]
        [Display(Name = "Baptismal Name")]
        public string BaptismName { get; set; }
        
        [Display(Name = "Othername")]
        public string Othername { get; set; }
        
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Date of Birth")]
        public string DateOfBirth { get; set; }
        
        [Display(Name = "Place of Birth")]
        public string PlaceOfBirth { get; set; }
        
        [Display(Name = "Home town")]
        public string HomeTown { get; set; }
        
        [Display(Name = "Father's name")]
        public string FathersName { get; set; }
        
        [Display(Name = "Mother's name")]
        public string MothersName { get; set; }
        
        [Display(Name = "Baptism Type")]
        public int BaptismType { get; set; }
        
        [Required]
        [Display(Name = "God parent name")]
        public string NameOfGodParent1 { get; set; }
        
        [Display(Name = "God parent name")]
        public string NameOfGodParent2 { get; set; }

        [Display(Name = "Minister's name")]
        public string NameOfMinister { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }
        
        public int MemberId { get; set; }
    }
}