using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessObject.DataModel;
using DataAccessObject.IRepository;

namespace DataAccessObject.Repository
{
    public class SocietyRepository : ISocietyRepository
    {
        private readonly StFrancisCCEntities _entities = new StFrancisCCEntities();

        public bool AddPosition(Position position)
        {
            _entities.Positions.Add(position);
            _entities.SaveChanges();
            return true;
        }

        public IQueryable<Position> GetPositions()
        {
            return _entities.Positions;
        }
    }
}
