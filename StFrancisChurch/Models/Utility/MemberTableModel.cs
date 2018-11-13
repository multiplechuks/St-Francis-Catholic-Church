﻿using System;
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
        public int Gender { get; set; }
        public string HomeParish { get; set; }
        public string Town { get; set; }
        public string Nationality { get; set; }
        public string EmailAddress { get; set; }
        public string Phone2 { get; set; }
        public string EmpolymentAddress { get; set; }
        public int? MaritalStatus { get; set; }
        public string NextOfKin { get; set; }
        public int? NextOfKinMaritalStatus { get; set; }
        public string NextOfKinAddress { get; set; }
        public string SpouseName { get; set; }
        public string SpousePhone1 { get; set; }
        public string SpousePhone2 { get; set; }
        public int? SizeOfFamilyMale { get; set; }
        public int? SizeOfFamilyFemale { get; set; }
        public int? StatutoryGroup { get; set; }
        public string PassportUrl { get; set; }
    }
    
}