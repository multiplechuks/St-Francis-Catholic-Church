using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessObject.DataUtility;
using StFrancisChurch.Models.Utility;

namespace StFrancisChurch.Utility
{
    public static class ViewUtility
    {
        public static IQueryable<dynamic> GetAllSacrament()
        {
            var users = DataUtility.GetSacraments();
            List<dynamic> formatted = new List<dynamic>();
            foreach (var f in users)
            {
                formatted.Add(new
                {
                    f.Id,
                    Name = f.Sacrament1
                });
            }
            return formatted.AsQueryable();
        }

        public static IEnumerable<SelectItem> GetGender()
        {
            var gender = DataUtility.GeLookUpTables().Where(m => m.Category == "Gender");
            List<SelectItem> formatted = new List<SelectItem>();
            foreach (var g in gender)
            {
                formatted.Add(new SelectItem
                {
                    Id = g.Id,
                    Name = g.LookUpName
                });
            }
            return formatted.AsQueryable();
        }

        public static IEnumerable<SelectItem> GetMaritalStatus()
        {
            var status = DataUtility.GeLookUpTables().Where(m => m.Category == "Marital Status");
            List<SelectItem> formatted = new List<SelectItem>();
            foreach (var g in status)
            {
                formatted.Add(new SelectItem
                {
                    Id = g.Id,
                    Name = g.LookUpName
                });
            }
            return formatted.AsQueryable();
        }

        public static IEnumerable<SelectItem> GetStatutoryBody()
        {
            var body = DataUtility.GeLookUpTables().Where(m => m.Category == "Statutory Body");
            List<SelectItem> formatted = new List<SelectItem>();
            foreach (var g in body)
            {
                formatted.Add(new SelectItem
                {
                    Id = g.Id,
                    Name = g.LookUpName
                });
            }
            return formatted.AsQueryable();
        }

        public static IEnumerable<SelectItem> GetBaptismType()
        {
            var type = DataUtility.GeLookUpTables().Where(m => m.Category == "BaptismType");
            List<SelectItem> formatted = new List<SelectItem>();
            foreach (var g in type)
            {
                formatted.Add(new SelectItem
                {
                    Id = g.Id,
                    Name = g.LookUpName
                });
            }
            return formatted.AsQueryable();
        }

        public static SacramentIndex GetSacramentsCount()
        {
            var communion = DataUtility.GetCommunion();
            var matrimony = DataUtility.GetMatrimony();
            var baptism = DataUtility.GetBaptisms();
            var confirmation = DataUtility.GetConfirmation();

            return new SacramentIndex
            {
                Communion = communion,
                Baptism = baptism,
                Matrimony = matrimony,
                Confirmation = confirmation
            };
        }

        public static IEnumerable<SelectItem> GetSocieties()
        {
            var body = DataUtility.GetAllSocieties();
            List<SelectItem> formatted = new List<SelectItem>();
            foreach (var g in body)
            {
                formatted.Add(new SelectItem
                {
                    Id = g.Id,
                    Name = g.Name
                });
            }
            return formatted.AsQueryable();
        }

        public static IEnumerable<SelectItem> GetStations()
        {
            var body = DataUtility.GetAllStations();
            List<SelectItem> formatted = new List<SelectItem>();
            foreach (var g in body)
            {
                formatted.Add(new SelectItem
                {
                    Id = g.Id,
                    Name = g.Name
                });
            }
            return formatted.AsQueryable();
        }

        public static string GetStationName(int id)
        {
            return DataUtility.GetAllStations().FirstOrDefault(m => m.Id == id)?.Name;
        }

        public static List<FamilyMembers> GetFamilyMembers(int memberId)
        {
            List<FamilyMembers> familyMembers = new List<FamilyMembers>();
            var families = DataUtility.GetFamilyMembers(memberId);
            var count = 0;
            foreach (var family in families)
            {
                count++;
                familyMembers.Add(new FamilyMembers
                {
                    Id = count,
                    Name = family.Name,
                    DateOfBirth = family.DateOfBirth,
                    Gender = family.LookUpTable.LookUpName,
                    Relationship = family.Relationship
                });
            }
            return familyMembers;
        }

        public static List<MembersSociety> GetMembersSociety(int memberId)
        {
            List<MembersSociety> societyMembers = new List<MembersSociety>();
            var societies = DataUtility.GetMembersSocieties(memberId);
            var count = 0;
            foreach (var society in societies)
            {
                count++;
                societyMembers.Add(new MembersSociety
                {
                    Id = count,
                    Society = society.Society.Name,
                    Position = society.Position.PositionName
                });
            }
            return societyMembers;
        }

        public static IEnumerable<SelectItem> GetPositions()
        {
            var body = DataUtility.GetAllPositons();
            List<SelectItem> formatted = new List<SelectItem>();
            foreach (var g in body)
            {
                formatted.Add(new SelectItem
                {
                    Id = g.Id,
                    Name = g.PositionName
                });
            }
            return formatted.AsQueryable();
        }
    }
}