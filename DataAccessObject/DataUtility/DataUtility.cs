using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessObject.DataModel;

namespace DataAccessObject.DataUtility
{
    public static class DataUtility
    {
        private static readonly StFrancisCCEntities _entities = new StFrancisCCEntities();

        public static IQueryable<Sacrament> GetSacraments()
        {
            return _entities.Sacraments.AsQueryable();
        }

        public static IQueryable<Station> GetAllStations()
        {
            return _entities.Stations.AsQueryable();
        }

        public static IQueryable<Society> GetAllSocieties()
        {
            return _entities.Societies.AsQueryable();
        }

        public static IQueryable<LookUpTable> GeLookUpTables()
        {
            return _entities.LookUpTables.AsQueryable();
        }
    }
}
