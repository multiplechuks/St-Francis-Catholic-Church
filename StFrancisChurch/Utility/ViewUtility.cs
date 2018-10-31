using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessObject.DataUtility;

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
    }
}