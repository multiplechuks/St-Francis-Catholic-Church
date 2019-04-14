using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessObject.DataModel;

namespace DataAccessObject.IRepository
{
    public interface IStationRepository
    {
        IQueryable<Station> GetAllStations();
        int AddStation(Station station);
        IQueryable<WeeklySchedule> GetSchedules();
        IQueryable<WeeklySchedule> GetSundaySchedules();
        IQueryable<WeeklySchedule> GetStationSchedules(int id);
        bool AddSchedules(List<WeeklySchedule> schedules);
    }
}
