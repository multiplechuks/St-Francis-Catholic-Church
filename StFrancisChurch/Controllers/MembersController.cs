using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessObject.IRepository;
using Microsoft.AspNet.Identity;
using StFrancisChurch.Models;
using StFrancisChurch.Models.Utility;

namespace StFrancisChurch.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        private readonly IMemberRepository _memberRepository;

        public MembersController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        // GET: Member
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetActiveMembers(DatatableParam param)
        {
            var members = _memberRepository.GetConfirmedMembers();
            var allItems = new List<MemberTableModel>();
            //Apply Searching 
            if (!string.IsNullOrEmpty(param.search.value))
            {
                var searchTerm = param.search.value;
                members = members.Where(m => m.Firstname.Contains(searchTerm) || m.Othername.Contains(searchTerm) || m.Surname.Contains(searchTerm));
            }

            var totalcount = members.Count();
            members = members.OrderBy(m => m.Firstname);

            //Apply Sorting/Ordering
            //IEnumerable<UserTableModel> entryTables = allItems;
            var orderColumnIndex = Convert.ToInt32(param.order[0].column);
            var orderDir = param.order[0].dir;
            if (orderDir.Equals("asc"))
            {
                if (orderColumnIndex == 1)
                {
                    members = members.OrderBy(m => m.Surname);
                }
                if (orderColumnIndex == 2)
                {
                    members = members.OrderBy(m => m.Firstname);
                }
                if (orderColumnIndex == 3)
                {
                    members = members.OrderBy(m => m.Othername);
                }
            }
            else
            {
                if (orderColumnIndex == 1)
                {
                    members = members.OrderByDescending(m => m.Surname);
                }
                if (orderColumnIndex == 2)
                {
                    members = members.OrderByDescending(m => m.Firstname);
                }
                if (orderColumnIndex == 3)
                {
                    members = members.OrderByDescending(m => m.Othername);
                }                
            }

            //Apply Pagination
            //int.TryParse(param., out start);
            members = members.Skip(param.start).Take(param.length);

            int count = 0;

            foreach (var e in members)
            {
                count++;
                allItems.Add(new MemberTableModel
                {
                    Id = e.Id,
                    Serial = count,
                    Surname = e.Surname,
                    Firstname = e.Firstname,
                    Othername = e.Othername
                });
            }

            return Json(new
            {
                param.draw,
                recordsFiltered = totalcount,
                recordsTotal = totalcount,
                data = allItems,
            }, JsonRequestBehavior.AllowGet);
        }

        // GET: Member
        public ActionResult Registering()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetRegisteringMembers(DatatableParam param)
        {
            var members = _memberRepository.GetNonConfirmedMembers();
            var allItems = new List<MemberTableModel>();
            //Apply Searching 
            if (!string.IsNullOrEmpty(param.search.value))
            {
                var searchTerm = param.search.value;
                members = members.Where(m => m.Firstname.Contains(searchTerm) || m.Othername.Contains(searchTerm) || m.Surname.Contains(searchTerm));
            }

            var totalcount = members.Count();
            members = members.OrderBy(m => m.Firstname);

            //Apply Sorting/Ordering
            //IEnumerable<UserTableModel> entryTables = allItems;
            var orderColumnIndex = Convert.ToInt32(param.order[0].column);
            var orderDir = param.order[0].dir;
            if (orderDir.Equals("asc"))
            {
                if (orderColumnIndex == 1)
                {
                    members = members.OrderBy(m => m.Surname);
                }
                if (orderColumnIndex == 2)
                {
                    members = members.OrderBy(m => m.Firstname);
                }
                if (orderColumnIndex == 3)
                {
                    members = members.OrderBy(m => m.Othername);
                }
            }
            else
            {
                if (orderColumnIndex == 1)
                {
                    members = members.OrderByDescending(m => m.Surname);
                }
                if (orderColumnIndex == 2)
                {
                    members = members.OrderByDescending(m => m.Firstname);
                }
                if (orderColumnIndex == 3)
                {
                    members = members.OrderByDescending(m => m.Othername);
                }
            }

            //Apply Pagination
            //int.TryParse(param., out start);
            members = members.Skip(param.start).Take(param.length);

            var count = 0;

            foreach (var e in members)
            {
                count++;
                allItems.Add(new MemberTableModel
                {
                    Id = e.Id,
                    Serial = count,
                    Surname = e.Surname,
                    Firstname = e.Firstname,
                    Othername = e.Othername
                });
            }

            return Json(new
            {
                param.draw,
                recordsFiltered = totalcount,
                recordsTotal = totalcount,
                data = allItems,
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult View(int id)
        {
            var member = _memberRepository.GetMember(id);
            if (member != null)
            {
                ViewBag.Member = new MemberTableModel
                {
                    Id = member.Id,
                    Surname = member.Surname,
                    Firstname = member.Firstname,
                    Othername = member.Othername,
                    Phone = member.Phone,
                    DateRegistered = member.DateRegistered.ToString("d"),
                    Gender = member.Gender,
                    Phone2 = member.Phone2,
                    HomeParish = member.HomeParish,
                    Town = member.Town,
                    Nationality = member.Nationality,
                    EmailAddress = member.Email,
                    EmpolymentAddress = member.EmploymentAddress,
                    MaritalStatus = member.MaritalStatus,
                    NextOfKin = member.NextOfKin,
                    NextOfKinMaritalStatus = member.NextOfKinMaritalStatus,
                    NextOfKinAddress = member.NextOfKinAddress,
                    SpouseName = member.SpouseName,
                    SpousePhone1 = member.SpousePhone,
                    SpousePhone2 = member.SpousePhone2,
                    SizeOfFamilyMale = (int)member.FamilyMaleSize,
                    SizeOfFamilyFemale = (int)member.FamilyFemaleSize,
                    StatutoryGroup = member.StatutoryGroup
                };
            }
            else
            {
                ViewBag.Member = new MemberTableModel();
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var member = _memberRepository.GetMember(id);
            if (member != null)
            {
                var model = new MemberRegistrationViewModel
                {
                    Id = member.Id,
                    Surname = member.Surname,
                    Firstname = member.Firstname,
                    Othername = member.Othername,
                    Phone = member.Phone,
                    Gender = member.Gender
                    //DateRegistered = member.DateRegistered.ToString("d")
                };
                ViewBag.Sex = new List<SelectListItem>
                {
                    new SelectListItem {Text="Select",Value=""},
                    new SelectListItem {Text="Male",Value="Male"},
                    new SelectListItem {Text="Female",Value="Female"}
                };
                return View(model);
            }
            else
            {
                var model = new MemberTableModel();
                return View(model);
            }
        }

        [HttpGet]
        public string ConfirmMember(int id)
        {
            return _memberRepository.ConfirmMember(id, User.Identity.GetUserId()) ? "Member was approved successfully" : "There was an error approving this member check the details correctly";
        }

        [HttpGet]
        public string RejectMember(int id)
        {
            return _memberRepository.ConfirmMember(id, User.Identity.GetUserId()) ? "Member was rejected successfully" : "There was an error rejecting this member check the details correctly";
        }

        [HttpGet]
        public string DeleteMember(int id)
        {
            return _memberRepository.ConfirmMember(id, User.Identity.GetUserId()) ? "Member was deleted successfully" : "There was an error deleting this member, try again.";
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
    }
}