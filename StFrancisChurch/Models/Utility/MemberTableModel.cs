using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StFrancisChurch.Models.Utility
{
    public class MemberTableModel
    {
        public int Id { get; set; }
        public int Serial { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Othername { get; set; }
        public string Phone { get; set; }
        public string DateRegistered { get; set; }
        public string Gender { get; set; }
        public string HomeParish { get; set; }
        public string Town { get; set; }
        public string Nationality { get; set; }
        public string EmailAddress { get; set; }
        public string Phone2 { get; set; }
        public string EmpolymentAddress { get; set; }
        public string MaritalStatus { get; set; }
        public string NextOfKin { get; set; }
        public string NextOfKinMaritalStatus { get; set; }
        public string NextOfKinAddress { get; set; }
        public string SpouseName { get; set; }
        public string SpousePhone1 { get; set; }
        public string SpousePhone2 { get; set; }
        public int? SizeOfFamilyMale { get; set; }
        public int? SizeOfFamilyFemale { get; set; }
        public string StatutoryGroup { get; set; }
        public string PassportUrl { get; set; }
        public string Station { get; set; }
        public List<FamilyMembers> FamilyMembers { get; set; }
        public List<MembersSociety> MembersSociety { get; set; }
    }

    public class BaptismTableModel
    {
        public int Serial { get; set; }
        public string BaptismNumber { get; set; }
        public string BaptismPlace { get; set; }
        public string BaptismDate { get; set; }
        public string BaptismName { get; set; }
        public string Othername { get; set; }
        public string Surname { get; set; }
        public string DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string HomeTown { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public int BaptismType { get; set; }
        public string NameOfGodParent1 { get; set; }
        public string NameOfGodParent2 { get; set; }
        public string NameOfMinister { get; set; }
        public string Remarks { get; set; }
    }

    public class CommunionTableModel
    {
        public int Serial { get; set; }
        public string BaptismNumber { get; set; }
        public string BaptismPlace { get; set; }
        public string BaptismDate { get; set; }
        public string Othernames { get; set; }
        public string Surname { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public string NameOfMinister { get; set; }
        public string DateReceived { get; set; }
    }

    public class MatrimonyTableModel
    {
        public int Serial { get; set; }
        public string DateOfMarriage { get; set; }
        public string PlaceOfMarriage { get; set; }
        public string BrideFullName { get; set; }
        public string GroomFullName { get; set; }
        public string BrideAddress { get; set; }
        public string GroomAddress { get; set; }
        public int BrideAge { get; set; }
        public int GroomAge { get; set; }
        public string BrideBaptismPlace { get; set; }
        public string BrideBaptismDate { get; set; }
        public string BrideBaptismNo { get; set; }
        public string GroomBaptismPlace { get; set; }
        public string GroomBaptismDate { get; set; }
        public string GroomBaptismNo { get; set; }
        public string AssistingPriest { get; set; }
        public string BannDetails { get; set; }
        public string BrideParentName { get; set; }
        public string BrideParentHomeTown { get; set; }
        public string GroomParentName { get; set; }
        public string GroomParentHomeTown { get; set; }
        public string Witness1 { get; set; }
        public string Witness2 { get; set; }
        public string Remark { get; set; }
    }

    public class StationTableModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public int Serial { get; set; }
    }

    public class EventTableModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreateDate { get; set; }
    }
}