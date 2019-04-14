using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessObject.DataModel;
using DataAccessObject.IRepository;
using Newtonsoft.Json;
using StFrancisChurch.Models.Utility;

namespace StFrancisChurch.Controllers
{
    [Authorize]
    public class StationController : Controller
    {
        private readonly IStationRepository _stationRepository;

        public StationController(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }

        // GET: Station
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
        public ActionResult Index(OutStation outStation)
        {
            
            try
            {
                var station = new Station
                {
                    Name = outStation.Name,
                    Location = outStation.Location,
                    Contact = outStation.Contact,
                    DisplayName = outStation.DisplayName,
                    Deleted = 0,
                    CreateDate = outStation.CreateDate
                };

                if (_stationRepository.AddStation(station) > 0)
                {
                    var returnData = new ReturnData
                    {
                        HasValue = true,
                        Message = "Station was successfully created"
                    };
                    TempData["returnMessage"] = returnData;
                    return Redirect("Station");
                }
            }
            catch (Exception e)
            {
                var returnData = new ReturnData
                {
                    HasValue = true,
                    Message = "There was an error saving the station"
                };
                TempData["returnMessage"] = returnData;
                return Redirect("Station");
            }

            return View();
        }

        [HttpPost]
        public JsonResult GetStations(DatatableParam param)
        {
            var stations = _stationRepository.GetAllStations();
            var allItems = new List<StationTableModel>();
            //Apply Searching 
            if (!string.IsNullOrEmpty(param.search.value))
            {
                var searchTerm = param.search.value;
                stations = stations.Where(m => m.Name.Contains(searchTerm));
            }

            var totalcount = stations.Count();
            stations = stations.OrderBy(m => m.Name);

            //Apply Sorting/Ordering
            //IEnumerable<UserTableModel> entryTables = allItems;
            var orderColumnIndex = Convert.ToInt32(param.order[0].column);
            var orderDir = param.order[0].dir;
            if (orderDir.Equals("asc"))
            {
                if (orderColumnIndex == 1)
                {
                    stations = stations.OrderBy(m => m.Name);
                }
            }
            else
            {
                if (orderColumnIndex == 1)
                {
                    stations = stations.OrderByDescending(m => m.Name);
                }
            }

            //Apply Pagination
            //int.TryParse(param., out start);
            stations = stations.Skip(param.start).Take(param.length);

            var count = 0;

            foreach (var e in stations)
            {
                count++;
                allItems.Add(new StationTableModel
                {
                    Id = e.Id,
                    Serial = count,
                    Name = e.Name,
                    Location = e.Location,
                    Contact = e.Contact
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

        [HttpGet]
        public ActionResult Schedule()
        {
            var schedules = _stationRepository.GetSchedules().GroupBy(m => m.Station);
            ViewBag.Schedules = schedules.ToList();
            var returnData = (ReturnData)TempData["returnMessage"] ?? new ReturnData
            {
                HasValue = false
            };
            ViewBag.returns = returnData;
            return View();
        }

        [HttpPost]
        public ActionResult Schedule(ScheduleModel model)
        {
            var returnData = new ReturnData();
            List<WeeklySchedule> weeklySchedules = new List<WeeklySchedule>();
            if (!string.IsNullOrEmpty(model.Sunday))
            {
                weeklySchedules.Add(new WeeklySchedule
                {
                    DayOfTheWeek = (int)DaysOfTheWeek.Sunday,
                    Time = model.Sunday,
                    Station = model.Station,
                    Description = "mass schedule",
                    ScheduleType = 1
                });
            }
            if (!string.IsNullOrEmpty(model.Monday))
            {
                weeklySchedules.Add(new WeeklySchedule
                {
                    DayOfTheWeek = (int)DaysOfTheWeek.Monday,
                    Time = model.Monday,
                    Station = model.Station,
                    Description = "mass schedule",
                    ScheduleType = 1
                });
            }
            if (!string.IsNullOrEmpty(model.Tuesday))
            {
                weeklySchedules.Add(new WeeklySchedule
                {
                    DayOfTheWeek = (int)DaysOfTheWeek.Tuesday,
                    Time = model.Tuesday,
                    Station = model.Station,
                    Description = "mass schedule",
                    ScheduleType = 1
                });
            }
            if (!string.IsNullOrEmpty(model.Wednesday))
            {
                weeklySchedules.Add(new WeeklySchedule
                {
                    DayOfTheWeek = (int)DaysOfTheWeek.Wednesday,
                    Time = model.Wednesday,
                    Station = model.Station,
                    Description = "mass schedule",
                    ScheduleType = 1
                });
            }
            if (!string.IsNullOrEmpty(model.Thursday))
            {
                weeklySchedules.Add(new WeeklySchedule
                {
                    DayOfTheWeek = (int)DaysOfTheWeek.Thursday,
                    Time = model.Thursday,
                    Station = model.Station,
                    Description = "mass schedule",
                    ScheduleType = 1
                });
            }
            if (!string.IsNullOrEmpty(model.Friday))
            {
                weeklySchedules.Add(new WeeklySchedule
                {
                    DayOfTheWeek = (int)DaysOfTheWeek.Friday,
                    Time = model.Friday,
                    Station = model.Station,
                    Description = "mass schedule",
                    ScheduleType = 1
                });
            }
            if (!string.IsNullOrEmpty(model.Saturday))
            {
                weeklySchedules.Add(new WeeklySchedule
                {
                    DayOfTheWeek = (int)DaysOfTheWeek.Saturday,
                    Time = model.Saturday,
                    Station = model.Station,
                    Description = "mass schedule",
                    ScheduleType = 1
                });
            }

            if (_stationRepository.AddSchedules(weeklySchedules))
            {
                returnData = new ReturnData
                {
                    HasValue = true,
                    Message = "Mass schedule was sucessfully updated"
                };
                TempData["returnMessage"] = returnData;
                return RedirectToAction("Schedule");
            }
            returnData = new ReturnData
            {
                HasValue = true,
                Message = "There was an error updateing the changes try again"
            };
            TempData["returnMessage"] = returnData;
            return RedirectToAction("Schedule");
        }

        [HttpGet]
        public string GetStationSchedule(int id)
        {
            var schedules = _stationRepository.GetStationSchedules(id);
            ScheduleModel schedule = new ScheduleModel();

            schedule.Station = id;
            schedule.Sunday = schedules.FirstOrDefault(m => m.DayOfTheWeek == (int)DaysOfTheWeek.Sunday)?.Time;
            schedule.Monday = schedules.FirstOrDefault(m => m.DayOfTheWeek == (int)DaysOfTheWeek.Monday)?.Time;
            schedule.Tuesday = schedules.FirstOrDefault(m => m.DayOfTheWeek == (int)DaysOfTheWeek.Tuesday)?.Time;
            schedule.Wednesday = schedules.FirstOrDefault(m => m.DayOfTheWeek == (int)DaysOfTheWeek.Wednesday)?.Time;
            schedule.Thursday = schedules.FirstOrDefault(m => m.DayOfTheWeek == (int)DaysOfTheWeek.Thursday)?.Time;
            schedule.Friday = schedules.FirstOrDefault(m => m.DayOfTheWeek == (int)DaysOfTheWeek.Friday)?.Time;
            schedule.Saturday = schedules.FirstOrDefault(m => m.DayOfTheWeek == (int)DaysOfTheWeek.Saturday)?.Time;

            return JsonConvert.SerializeObject(schedule);
        }
    }
}