using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessObject.DataModel;
using DataAccessObject.IRepository;

namespace DataAccessObject.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly StFrancisCCEntities _entities = new StFrancisCCEntities();

        public int AddEvent(ParishEvent parishEvent)
        {
            _entities.ParishEvents.Add(parishEvent);
            _entities.SaveChanges();
            return parishEvent.Id;
        }

        public int UpdateEvent(ParishEvent parishEvent)
        {
            var existingEvent = _entities.ParishEvents.FirstOrDefault(m => m.Id == parishEvent.Id && m.Deleted == 0);
            if (existingEvent != null)
            {

                existingEvent.UpdateDate = DateTime.Now;

                _entities.Entry(existingEvent).State = EntityState.Modified;
            }
            _entities.SaveChanges();
            return parishEvent.Id;
        }

        public ParishEvent GetEvent(int id)
        {
            return _entities.ParishEvents.FirstOrDefault(m => m.Id == id && m.Deleted == 0);
        }

        public IQueryable<ParishEvent> GetAllActiveParishEvents()
        {
            return _entities.ParishEvents.Where(m => m.Active == 1 && m.Deleted == 0);
        }

        public IQueryable<ParishEvent> GetAllParishEvents()
        {
            return _entities.ParishEvents.Where(m => m.Deleted == 0);
        }

        public IQueryable<ParishEvent> GetRecentEvents()
        {
            return _entities.ParishEvents.OrderByDescending(m => m.Id).Take(6).Where(m => m.Active == 1 && m.Deleted == 0);
        }

        public int AddDailyVerse(DailyVerse dailyVerse)
        {
            _entities.DailyVerses.Add(dailyVerse);
            _entities.SaveChanges();
            return dailyVerse.Id;
        }

        public IQueryable<DailyVerse> GetDailyVerses()
        {
            return _entities.DailyVerses;
        }
    }
}
