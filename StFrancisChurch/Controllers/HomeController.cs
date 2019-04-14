using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessObject.DataModel;
using DataAccessObject.IRepository;
using StFrancisChurch.Models;
using StFrancisChurch.Models.Utility;
using StFrancisChurch.Utility;

namespace StFrancisChurch.Controllers
{
    public class HomeController : Controller
    {

        private readonly IMemberRepository _memberRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IStationRepository _stationRepository;

        public HomeController(IMemberRepository memberRepository, IEventRepository eventRepository, IStationRepository stationRepository)
        {
            _memberRepository = memberRepository;
            _eventRepository = eventRepository;
            _stationRepository = stationRepository;
        }

        public ActionResult Index()
        {
            GetRecentEvents();
            GetSundayMassSchedules();
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

        public ActionResult Events()
        {
            GetRecentEvents();
            return View();
        }
         
        public ActionResult Event(int id)
        {
            GetEvent(id);
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

        public ActionResult News()
        {
            return View();
        }

        public ActionResult Reflections()
        {
            return View();
        }

        public ActionResult Station(int id)
        {
            ViewBag.StationName = ViewUtility.GetStationName(id);
            ViewBag.StationId = id;
            return View();
        }

        public ActionResult Stations(int station, string view)
        {
            ViewBag.StationName = ViewUtility.GetStationName(station);
            ViewBag.StationId = station;
            ViewResult viewResult = null;
            switch (view)
            {
                case "about":
                    viewResult = View("StationAbout");
                    break;
            }
            return viewResult;
        }

        /*Other Methods*/
        private void GetRecentEvents()
        {
            //get the last six event or the ones that are still within the time frame
            var parishEvents = _eventRepository.GetRecentEvents();
            var parishEventUis = new List<ParishEventUI>();

            foreach (var parishEvent in parishEvents)
            {
                var eventUi = new ParishEventUI
                {
                    Id = parishEvent.Id,
                    Name = parishEvent.EventName,
                    Description = parishEvent.EventDescription.Length < 65 ? parishEvent.EventDescription : parishEvent.EventDescription.Substring(0, 65) + "...",
                    CreateDate = parishEvent.CreateDate.ToString("D")
                };
                parishEventUis.Add(eventUi);
            }

            ViewBag.events = parishEventUis;
        }

        private void GetEvent(int id)
        {
            //get the last six event or the ones that are still within the time frame
            var parishEvent = _eventRepository.GetRecentEvents().FirstOrDefault(m => m.Id == id);
            var eventUi = new ParishEventUI
            {
                Id = parishEvent.Id,
                Name = parishEvent.EventName,
                Description = parishEvent.EventDescription,
                CreateDate = parishEvent.CreateDate.ToString("D")
            };
            ViewBag.parishEvent = eventUi;
        }

        private void GetSundayMassSchedules()
        {
            List<MassSchedules> massSchedules = new List<MassSchedules>();
            var schedules = _stationRepository.GetSundaySchedules();
            foreach (var _ in schedules)
            {
                massSchedules.Add(new MassSchedules
                {
                    StationName = _.Station1.Name,
                    DayOfTheWeek = (DaysOfTheWeek)_.DayOfTheWeek,
                    Time = _.Time
                });
            }

            ViewBag.MassSchedules = massSchedules;
        }
    }
}