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
    public class StationRepository : IStationRepository
    {
        private readonly StFrancisCCEntities _entities = new StFrancisCCEntities();

        public IQueryable<Station> GetAllStations()
        {
            return _entities.Stations.AsQueryable();
        }

        public int AddStation(Station station)
        {
            _entities.Stations.Add(station);
            _entities.SaveChanges();
            return station.Id;
        }

        public IQueryable<WeeklySchedule> GetSchedules()
        {
            return _entities.WeeklySchedules.OrderBy(m => m.DayOfTheWeek);
        }

        public IQueryable<WeeklySchedule> GetSundaySchedules()
        {
            return _entities.WeeklySchedules.Where(m=>m.DayOfTheWeek == 1);
        }

        public IQueryable<WeeklySchedule> GetStationSchedules(int id)
        {
            return _entities.WeeklySchedules.Where(m => m.Station == id);
        }

        public bool AddSchedules(List<WeeklySchedule> schedules)
        {
            try
            {
                foreach (var schedule in schedules)
                {
                    var existingRecord = _entities.WeeklySchedules.FirstOrDefault(m =>
                        m.DayOfTheWeek == schedule.DayOfTheWeek
                        && m.Station == schedule.Station);
                    if (existingRecord != null)
                    {
                        //Update
                        existingRecord.Time = schedule.Time;
                        _entities.Entry(existingRecord).State = EntityState.Modified;
                    }
                    else
                    {
                        _entities.WeeklySchedules.Add(schedule);
                    }
                }

                _entities.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
