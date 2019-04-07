using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public int UpdateMember(Member member)
        {
            var existingMember = _entities.Members.FirstOrDefault(m => m.Id == member.Id && m.Deleted == 0);
            if (existingMember != null)
            {
                existingMember.Firstname = member.Firstname;
                existingMember.Surname = member.Surname;
                existingMember.Firstname = member.Firstname;
                existingMember.Othername = member.Othername;
                existingMember.Email = member.Email;
                existingMember.Phone = member.Phone;
                existingMember.Phone2 = member.Phone2;
                existingMember.Gender = member.Gender;
                existingMember.HomeParish = member.HomeParish;
                existingMember.Town = member.Town;
                existingMember.Nationality = member.Nationality;
                existingMember.EmploymentAddress = member.EmploymentAddress;
                existingMember.MaritalStatus = member.MaritalStatus;
                existingMember.NextOfKin = member.NextOfKin;
                existingMember.NextOfKinMaritalStatus = member.NextOfKinMaritalStatus;
                existingMember.NextOfKinAddress = member.NextOfKinAddress;
                existingMember.SpouseName = member.SpouseName;
                existingMember.SpousePhone = member.SpousePhone;
                existingMember.SpousePhone2 = member.SpousePhone2;
                existingMember.FamilyFemaleSize = member.FamilyFemaleSize;
                existingMember.FamilyMaleSize = member.FamilyMaleSize;
                existingMember.StatutoryGroup = member.StatutoryGroup;
                existingMember.PassportUrl = string.IsNullOrEmpty(member.PassportUrl) ? existingMember.PassportUrl : member.PassportUrl;
                existingMember.Station = member.Station;

                existingMember.DateUpdated = DateTime.Now;
                
                _entities.Entry(existingMember).State = EntityState.Modified;
            }
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

        public bool UpdateFamilyMembers(MemberFamilyMetaData family)
        {
            _entities.MemberFamilyMetaDatas.Add(family);
            var inserted = _entities.SaveChanges();
            return inserted > 0;
        }

        public bool UpdateMembersSociety(SocietyMemberLink society)
        {
            _entities.SocietyMemberLinks.Add(society);
            var inserted = _entities.SaveChanges();
            return inserted > 0;
        }

        public void UpdateSacramentReceived(int sacramentId, int memberId, string userId)
        {
            //check if sacrament has been added for user
            var existingLink =
                _entities.SacramentMemberLinks.FirstOrDefault(m => m.MemberId == memberId && m.SacramentId == sacramentId);
            if (existingLink == null)
            {
                existingLink = new SacramentMemberLink
                {
                    MemberId = memberId,
                    SacramentId = sacramentId,
                    RecordedBy = userId,
                    DateRecorded = DateTime.Now,
                    Deleted = 0
                };
                _entities.SacramentMemberLinks.Add(existingLink);
                _entities.SaveChanges();
            }

            var updateLog = new SacramentMemberUpdateLog
            {
                UpdateDate = DateTime.Now,
                UpdateBy = userId,
                SacramentMemberLinkId = existingLink.Id
            };
            _entities.SacramentMemberUpdateLogs.Add(updateLog);
            _entities.SaveChanges();
        }
    }
}
