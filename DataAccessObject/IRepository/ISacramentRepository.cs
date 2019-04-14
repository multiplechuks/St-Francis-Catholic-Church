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
        bool AddMatrimony(Matrimony matrimony);
        IQueryable<Matrimony> GetMatrimonyMembers();
        bool AddCommunion(Communion communion);
        IQueryable<Communion> GetCommunionMembers();
        bool AddConfirmation(Confirmation confirmation);
        IQueryable<Confirmation> GetConfirmedMembers();
        Baptism GetBaptism(int Id);
        bool UpdateBaptism(Baptism baptism);
        Communion GetCommunion(int id);
        bool UpdateCommunion(Communion communion);
        Confirmation GetConfirmation(int id);
        bool UpdateConfirmation(Confirmation confirmation);
        Matrimony GetMatrimony(int id);
        bool UpdateMatrimony(Matrimony matrimony);
    }
}
