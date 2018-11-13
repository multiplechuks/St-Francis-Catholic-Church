using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessObject.DataModel;

namespace DataAccessObject.IRepository
{
    public interface IMemberRepository
    {
        IQueryable<Member> GetConfirmedMembers();
        IQueryable<Member> GetNonConfirmedMembers();
        int AddMember(Member member);
        Member GetMember(int id);
        bool ConfirmMember(int id, string userId);
        bool DeleteMember(int id, string userId);
        bool RejectMember(int id, string userId);
        int UpdateMember(Member member);
    }
}
