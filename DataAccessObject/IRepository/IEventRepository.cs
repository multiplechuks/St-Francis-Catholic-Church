using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessObject.DataModel;

namespace DataAccessObject.IRepository
{
    public interface IEventRepository
    {
        int AddEvent(ParishEvent parishEvent);
        int UpdateEvent(ParishEvent parishEvent);
        ParishEvent GetEvent(int id);
        IQueryable<ParishEvent> GetAllActiveParishEvents();
        IQueryable<ParishEvent> GetAllParishEvents();
        IQueryable<ParishEvent> GetRecentEvents();
    }
}
