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

        public int StationId { get; set; }
    }

    public class MatrimonyViewModel
    {
        [Required]
        [Display(Name = "Date Of Marraige")]
        public string DateOfMarriage { get; set; }

        [Required]
        [Display(Name = "Place Of Marraige")]
        public string PlaceOfMarriage { get; set; }

        [Required]
        [Display(Name = "Bride Fullname")]
        public string BrideFullName { get; set; }

        [Required]
        [Display(Name = "Groom Fullname")]
        public string GroomFullName { get; set; }
        
        public string BrideAddress { get; set; }

        public string GroomAddress { get; set; }

        public int BrideAge { get; set; }

        public int GroomAge { get; set; }

        public string BrideBaptismPlace { get; set; }

        public string BrideBaptismDate { get; set; }

        [Required]
        [Display(Name = "Bride Baptism number")]
        public string BrideBaptismNo { get; set; }

        public string GroomBaptismPlace { get; set; }

        public string GroomBaptismDate { get; set; }

        [Required]
        [Display(Name = "Groom Baptism number")]
        public string GroomBaptismNo { get; set; }

        [Required]
        [Display(Name = "Assisting Priest")]
        public string AssistingPriest { get; set; }

        public string BannDetails { get; set; }

        public string BrideParentName { get; set; }
        public string BrideParentHomeTown { get; set; }
        public string GroomParentName { get; set; }
        public string GroomParentHomeTown { get; set; }

        [Required]
        [Display(Name = "Witness Name")]
        public string Witness1 { get; set; }
        public string Witness2 { get; set; }
        public string Remark { get; set; }
    }

    public class CommunionViewModel
    {
        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        public string Othernames { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public string Date { get; set; }
        public string Place { get; set; }
        public string Minister { get; set; }

        [Required]
        [Display(Name = "Baptism Number")]
        public string BaptismNumber { get; set; }
        public string BaptismPlace { get; set; }
        public string BaptismDate { get; set; }
    }
}