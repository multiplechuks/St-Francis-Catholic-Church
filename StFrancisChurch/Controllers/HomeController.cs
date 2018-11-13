using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessObject.DataModel;
using DataAccessObject.IRepository;
using StFrancisChurch.Models;

namespace StFrancisChurch.Controllers
{
    public class HomeController : Controller
    {

        private readonly IMemberRepository _memberRepository;

        public HomeController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult MemberRegistration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MemberRegistration(MemberRegistrationViewModel model)
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
            var directory = System.Web.Hosting.HostingEnvironment.MapPath("~/Images/Passports");
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
                    return Redirect("MemberRegistrationSuccess");
                }
                return View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(String.Empty, "There was an error completing the registration, Please try again later");
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult MemberRegistrationSuccess()
        {
            return View();
        }        
    }
}