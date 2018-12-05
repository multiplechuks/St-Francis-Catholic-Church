using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessObject.DataModel;
using DataAccessObject.IRepository;

namespace DataAccessObject.Repository
{
    public class SacramentRepository : ISacramentRepository
    {
        private readonly StFrancisCCEntities _entities = new StFrancisCCEntities();

        public bool AddBaptism(Baptism baptism)
        {
            var existingBaptism = _entities.Baptism.FirstOrDefault(m => m.BapitsmNumber == baptism.BapitsmNumber);
            if (existingBaptism == null)
            {
                _entities.Baptism.Add(baptism);
                _entities.SaveChanges();
                return true;
            }
            return false;
        }

        public IQueryable<Baptism> GetBaptisedMembers()
        {
            return _entities.Baptism.AsQueryable();
        }
    }
}
