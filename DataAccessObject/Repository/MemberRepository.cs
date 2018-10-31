using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessObject.DataModel;
using DataAccessObject.IRepository;

namespace DataAccessObject.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly StFrancisCCEntities _entities = new StFrancisCCEntities();

        public IQueryable<Member> GetConfirmedMembers()
        {
            return _entities.Members.Where(m => m.Confirmed == 1 && m.Deleted == 0);
        }

        public IQueryable<Member> GetNonConfirmedMembers()
        {
            return _entities.Members.Where(m => m.Confirmed == 0 && m.Deleted == 0);
        }

        public IQueryable<Member> GetDeleteRegistrations()
        {
            return _entities.Members.Where(m => m.Deleted != 0);
        }

        public int AddMember(Member member)
        {
            _entities.Members.Add(member);
            _entities.SaveChanges();
            return member.Id;
        }

        public Member GetMember(int id)
        {
            return _entities.Members.FirstOrDefault(m => m.Id == id && m.Deleted == 0);
        }

        public bool ConfirmMember(int id, string userId)
        {
            var member = _entities.Members.FirstOrDefault(m => m.Id == id);
            if (member == null) return false;
            member.Confirmed = 1;
            member.ConfirmationDate = DateTime.Now;
            member.ConfirmedBy = userId;
            _entities.SaveChanges();
            return true;
        }

        public bool RejectMember(int id, string userId)
        {
            var member = _entities.Members.FirstOrDefault(m => m.Id == id);
            if (member == null) return false;
            member.Confirmed = 0;
            member.ConfirmationDate = DateTime.Now;
            member.ConfirmedBy = userId;
            _entities.SaveChanges();
            return true;
        }

        public bool DeleteMember(int id, string userId)
        {
            var member = _entities.Members.FirstOrDefault(m => m.Id == id);
            if (member == null) return false;
            member.Confirmed = 0;
            member.Deleted = 1;
            member.DateDeleted = DateTime.Now;
            member.DeletedBy = userId;
            _entities.SaveChanges();
            return true;
        }
    }
}
