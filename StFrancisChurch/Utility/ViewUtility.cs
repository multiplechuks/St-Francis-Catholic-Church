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
    }
}