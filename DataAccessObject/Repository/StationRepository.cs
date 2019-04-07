using System;
using System.Collections.Generic;
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
    }
}
