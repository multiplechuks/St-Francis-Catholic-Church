using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StFrancisChurch.Models.Utility
{
    public class UtilModel
    {

    }

    public class SelectItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ReturnData
    {
        public bool HasValue { get; set; }
        public string Message { get; set; }
    }
}