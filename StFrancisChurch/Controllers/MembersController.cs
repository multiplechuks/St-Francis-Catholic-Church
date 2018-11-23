using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessObject.DataModel;
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
            var returnData = (ReturnData)TempData["returnMessage"] ?? new ReturnData
            {
                HasValue = false
            };
            ViewBag.returns = returnData;
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
                    Gender = member.LookUpTable_Gender.LookUpName,
                    Phone2 = member.Phone2,
                    HomeParish = member.HomeParish,
                    Town = member.Town,
                    Nationality = member.Nationality,
                    EmailAddress = member.Email,
                    EmpolymentAddress = member.EmploymentAddress,
                    MaritalStatus = member.MaritalStatus != null ? member.LookUpTable_MaritalStatus.LookUpName : "",
                    NextOfKin = member.NextOfKin,
                    NextOfKinMaritalStatus = member.NextOfKinMaritalStatus != null ? member.LookUpTableKinMaritalStatus.LookUpName : "",
                    NextOfKinAddress = member.NextOfKinAddress,
                    SpouseName = member.SpouseName,
                    SpousePhone1 = member.SpousePhone,
                    SpousePhone2 = member.SpousePhone2,
                    SizeOfFamilyMale = member.FamilyMaleSize,
                    SizeOfFamilyFemale = member.FamilyFemaleSize,
                    StatutoryGroup = member.StatutoryGroup != null ? member.LookUpTable_Statutory.LookUpName : "",
                    PassportUrl = member.PassportUrl
                };
            }
            else
            {
                ViewBag.Member = new MemberTableModel();
            }
            return View();
        }

        [HttpGet]
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
                    Phone2 = member.Phone2,
                    EmailAddress = member.Email,
                    Gender = member.Gender,
                    HomeParish = member.HomeParish,
                    Town = member.Town,
                    Nationality = member.Nationality,
                    EmploymentAddress = member.EmploymentAddress,
                    MaritalStatus = member.MaritalStatus,
                    NextOfKin = member.NextOfKin,
                    NextOfKinMaritalStatus = member.NextOfKinMaritalStatus,
                    NextOfKinAddress = member.NextOfKinAddress,
                    SpouseName = member.SpouseName,
                    SpousePhone1 = member.SpousePhone,
                    SpousePhone2 = member.SpousePhone2,
                    SizeOfFamilyFemale = member.FamilyFemaleSize ?? 0,
                    SizeOfFamilyMale = member.FamilyMaleSize ?? 0,
                    StatutoryGroup = member.StatutoryGroup,
                    PassportUrl = member.PassportUrl
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

        [HttpPost]
        public ActionResult Edit(MemberRegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (Request.Files.Count != 0 || !string.IsNullOrEmpty(Request.Files[0]?.FileName))
            {
                var directory = System.Web.Hosting.HostingEnvironment.MapPath("~/Images/Passports/");
                if (Directory.Exists(directory) == false)
                {
                    Directory.CreateDirectory(directory);
                }

                var fileName = $"{DateTime.Now:dd_MM_yyyy_hh_mm_ss}_{model.Surname}_" + Request.Files[0].FileName;
                var filePath = directory + "_" + fileName;
                Request.Files[0].SaveAs(filePath);
                model.PassportUrl = "/Images/Passports/_" + fileName;
            }
            try
            {
                var member = new Member
                {
                    Id = model.Id,
                    Surname = model.Surname,
                    Firstname = model.Firstname,
                    Othername = model.Othername,
                    Email = model.EmailAddress,
                    Phone = model.Phone,
                    Phone2 = model.Phone2,
                    Gender = model.Gender,
                    HomeParish = model.HomeParish,
                    Town = model.Town,
                    Nationality = model.Nationality,
                    EmploymentAddress = model.EmploymentAddress,
                    MaritalStatus = model.MaritalStatus,
                    NextOfKin = model.NextOfKin,
                    NextOfKinMaritalStatus = model.NextOfKinMaritalStatus,
                    NextOfKinAddress = model.NextOfKinAddress,
                    SpouseName = model.SpouseName,
                    SpousePhone = model.SpousePhone1,
                    SpousePhone2 = model.SpousePhone2,
                    FamilyFemaleSize = model.SizeOfFamilyFemale,
                    FamilyMaleSize = model.SizeOfFamilyMale,
                    StatutoryGroup = model.StatutoryGroup,
                    PassportUrl = model.PassportUrl
                };
                if (_memberRepository.UpdateMember(member) > 0)
                {
                    var returnData = new ReturnData
                    {
                        HasValue = true,
                        Message = model.Surname + " " + model.Firstname + " was successfully updated"
                    };
                    TempData["returnMessage"] = returnData;
                    return Redirect("/Members");
                }
                return View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(String.Empty, "There was an error completing the registration, Please try again later");
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Create(MemberRegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (Request.Files.Count == 0 || string.IsNullOrEmpty(Request.Files[0]?.FileName))
            {
                ModelState.AddModelError(String.Empty, "Please make sure a passport was chosen");
                return View(model);
            }
            var directory = System.Web.Hosting.HostingEnvironment.MapPath("~/Images/Passports/");
            if (Directory.Exists(directory) == false)
            {
                Directory.CreateDirectory(directory);
            }

            var fileName = $"{DateTime.Now:dd_MM_yyyy_hh_mm_ss}_{model.Surname}_" + Request.Files[0].FileName;
            var filePath = directory + "_" + fileName;
            Request.Files[0].SaveAs(filePath);
            try
            {
                var member = new Member
                {
                    Surname = model.Surname,
                    Firstname = model.Firstname,
                    Othername = model.Othername,
                    Email = model.EmailAddress,
                    Phone = model.Phone,
                    Phone2 = model.Phone2,
                    Gender = model.Gender,
                    HomeParish = model.HomeParish,
                    Town = model.Town,
                    Nationality = model.Nationality,
                    EmploymentAddress = model.EmploymentAddress,
                    MaritalStatus = model.MaritalStatus,
                    NextOfKin = model.NextOfKin,
                    NextOfKinMaritalStatus = model.NextOfKinMaritalStatus,
                    NextOfKinAddress = model.NextOfKinAddress,
                    SpouseName = model.SpouseName,
                    SpousePhone = model.SpousePhone1,
                    SpousePhone2 = model.SpousePhone2,
                    FamilyFemaleSize = model.SizeOfFamilyFemale,
                    FamilyMaleSize = model.SizeOfFamilyMale,
                    StatutoryGroup = model.StatutoryGroup,
                    Confirmed = 0,
                    Deleted = 0,
                    DateRegistered = DateTime.Now,
                    PassportUrl = "/Images/Passports/_" + fileName
                };
                if (_memberRepository.AddMember(member) > 0)
                {
                    return Redirect("Index");
                }
                return View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(String.Empty, "There was an error completing the registration, Please try again later");
                return View(model);
            }
        }
    }
}