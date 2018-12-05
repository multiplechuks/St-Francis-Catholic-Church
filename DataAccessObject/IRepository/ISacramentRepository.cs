using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessObject.DataModel;

namespace DataAccessObject.IRepository
{
    public interface ISacramentRepository
    {
        bool AddBaptism(Baptism baptism);
        IQueryable<Baptism> GetBaptisedMembers();
    }
}
