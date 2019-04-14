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

        public bool UpdateBaptism(Baptism baptism)
        {
            var existingBaptism = _entities.Baptism.FirstOrDefault(m => m.Id == baptism.Id);
            if (existingBaptism != null)
            {
                baptism.CreateDate = existingBaptism.CreateDate;
                _entities.Entry(existingBaptism).CurrentValues.SetValues(baptism);
                _entities.SaveChanges();
                return true;
            }
            return false;
        }

        public IQueryable<Baptism> GetBaptisedMembers()
        {
            return _entities.Baptism.AsQueryable();
        }

        public Baptism GetBaptism(int id)
        {
            return _entities.Baptism.FirstOrDefault(m => m.Id == id);
        }

        public bool AddMatrimony(Matrimony matrimony)
        {
            //var existingBaptism = _entities.Matrimonies.FirstOrDefault(m => m.BapitsmNumber == baptism.BapitsmNumber);
            _entities.Matrimonies.Add(matrimony);
            _entities.SaveChanges();
            return true;
        }

        public bool UpdateMatrimony(Matrimony matrimony)
        {
            var existingMatrimony = _entities.Matrimonies.FirstOrDefault(m => m.Id == matrimony.Id);
            if (existingMatrimony != null)
            {
                matrimony.CreateDate = existingMatrimony.CreateDate;
                _entities.Entry(existingMatrimony).CurrentValues.SetValues(matrimony);
                _entities.SaveChanges();
                return true;
            }
            return false;
        }

        public IQueryable<Matrimony> GetMatrimonyMembers()
        {
            return _entities.Matrimonies.AsQueryable();
        }

        public Matrimony GetMatrimony(int id)
        {
            return _entities.Matrimonies.FirstOrDefault(m => m.Id == id);
        }

        public bool AddCommunion(Communion communion)
        {
            _entities.Communions.Add(communion);
            _entities.SaveChanges();
            return true;
        }

        public bool UpdateCommunion(Communion communion)
        {
            var existingcommunion = _entities.Communions.FirstOrDefault(m => m.Id == communion.Id);
            if (existingcommunion != null)
            {
                communion.CreateDate = existingcommunion.CreateDate;
                _entities.Entry(existingcommunion).CurrentValues.SetValues(communion);
                _entities.SaveChanges();
                return true;
            }
            return false;
        }

        public IQueryable<Communion> GetCommunionMembers()
        {
            return _entities.Communions.AsQueryable();
        }

        public Communion GetCommunion(int id)
        {
            return _entities.Communions.FirstOrDefault(m => m.Id == id);
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

        public bool UpdateConfirmation(Confirmation confirmation)
        {
            var existingConfirmation = _entities.Confirmations.FirstOrDefault(m => m.Id == confirmation.Id);
            if (existingConfirmation != null)
            {
                confirmation.CreateDate = existingConfirmation.CreateDate;
                _entities.Entry(existingConfirmation).CurrentValues.SetValues(confirmation);
                _entities.SaveChanges();
                return true;
            }
            return false;
        }

        public IQueryable<Confirmation> GetConfirmedMembers()
        {
            return _entities.Confirmations.AsQueryable();
        }

        public Confirmation GetConfirmation(int id)
        {
            return _entities.Confirmations.FirstOrDefault(m => m.Id == id);
        }
    }
}
