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
    }
    
}