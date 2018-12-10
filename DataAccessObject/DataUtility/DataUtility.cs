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

        public static int GetBaptisms()
        {
            return _entities.Baptism.Count();
        }

        public static int GetCommunion()
        {
            return _entities.Communions.Count();
        }

        public static int GetMatrimony()
        {
            return _entities.Matrimonies.Count();
        }

        public static int GetConfirmation()
        {
            return _entities.Confirmations.Count();
        }

        public static IQueryable<MemberFamilyMetaData> GetFamilyMembers(int memberId)
        {
            return _entities.MemberFamilyMetaDatas.Where(m => m.MemberId == memberId);
        }

        public static IQueryable<SocietyMemberLink> GetMembersSocieties(int memberId)
        {
            return _entities.SocietyMemberLinks.Where(m => m.MemberId == memberId);
        }
        
        public static IQueryable<Position> GetAllPositons()
        {
            return _entities.Positions.AsQueryable();
        }
    }
}
