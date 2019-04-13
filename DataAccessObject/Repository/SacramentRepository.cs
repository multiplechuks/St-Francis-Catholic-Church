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

        public bool AddMatrimony(Matrimony matrimony)
        {
            //var existingBaptism = _entities.Matrimonies.FirstOrDefault(m => m.BapitsmNumber == baptism.BapitsmNumber);
            _entities.Matrimonies.Add(matrimony);
            _entities.SaveChanges();
            return true;
        }

        public IQueryable<Matrimony> GetMatrimonyMembers()
        {
            return _entities.Matrimonies.AsQueryable();
        }

        public bool AddCommunion(Communion communion)
        {
            _entities.Communions.Add(communion);
            _entities.SaveChanges();
            return true;
        }

        public IQueryable<Communion> GetCommunionMembers()
        {
            return _entities.Communions.AsQueryable();
        }

        public bool AddConfirmation(Confirmation confirmation)
        {
            var existingConfirmation = _entities.Confirmations.FirstOrDefault(m => m.Number == confirmation.Number);
            if (existingConfirmation == null)
            {
                _entities.Confirmations.Add(confirmation);
                _entities.SaveChanges();
                return true;
            }
            return false;
        }

        public IQueryable<Confirmation> GetConfirmedMembers()
        {
            return _entities.Confirmations.AsQueryable();
        }
    }
}
