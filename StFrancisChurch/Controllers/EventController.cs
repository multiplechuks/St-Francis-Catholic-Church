using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessObject.DataModel;
using DataAccessObject.IRepository;
using StFrancisChurch.Models.Utility;

namespace StFrancisChurch.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository;

        public EventController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        // GET: Event
        [HttpGet]
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
        public ActionResult Index(ParishEventUI parishEvent)
        {
            if (Request.Files.Count > 0 || !string.IsNullOrEmpty(Request.Files[0]?.FileName))
            {
                var directory = System.Web.Hosting.HostingEnvironment.MapPath("~/Images/Events/");
                if (Directory.Exists(directory) == false)
                {
                    Directory.CreateDirectory(directory);
                }

                var fileName = $"{DateTime.Now:dd_MM_yyyy_hh_mm_ss}_{parishEvent.Name}_" + Request.Files[0].FileName;
                var filePath = directory + "_" + fileName;
                Request.Files[0].SaveAs(filePath);
                parishEvent.EventImage = filePath;
            }

            try
            {
                var pevent = new ParishEvent
                {
                    EventName = parishEvent.Name,
                    EventDescription = parishEvent.Description,
                    Image = parishEvent.EventImage,
                    Active = 1,
                    Deleted = 0,
                    CreateDate = DateTime.Now
                };
                
                if (_eventRepository.AddEvent(pevent) > 0)
                {
                    var returnData = new ReturnData
                    {
                        HasValue = true,
                        Message = "Event was successfully created"
                    };
                    TempData["returnMessage"] = returnData;
                    return Redirect("Event");
                }
            }
            catch (Exception e)
            {
                var returnData = new ReturnData
                {
                    HasValue = true,
                    Message = "There was an error creating the event"
                };
                TempData["returnMessage"] = returnData;
                return Redirect("Event");
            }

            return View();
        }

        //GetEvents
        [HttpPost]
        public JsonResult GetRecentEvents(DatatableParam param)
        {
            var events = _eventRepository.GetRecentEvents();
            var allItems = new List<EventTableModel>();
            //Apply Searching 
            if (!string.IsNullOrEmpty(param.search.value))
            {
                var searchTerm = param.search.value;
                events = events.Where(m => m.EventName.Contains(searchTerm));
            }

            var totalcount = events.Count();
            events = events.OrderBy(m => m.CreateDate);

            //Apply Sorting/Ordering
            //IEnumerable<UserTableModel> entryTables = allItems;
            var orderColumnIndex = Convert.ToInt32(param.order[0].column);
            var orderDir = param.order[0].dir;
            if (orderDir.Equals("asc"))
            {
                if (orderColumnIndex == 1)
                {
                    events = events.OrderBy(m => m.EventName);
                }
                if (orderColumnIndex == 2)
                {
                    events = events.OrderBy(m => m.EventDescription);
                }
                if (orderColumnIndex == 3)
                {
                    events = events.OrderBy(m => m.CreateDate);
                }
            }
            else
            {
                if (orderColumnIndex == 1)
                {
                    events = events.OrderByDescending(m => m.EventName);
                }
                if (orderColumnIndex == 2)
                {
                    events = events.OrderByDescending(m => m.EventDescription);
                }
                if (orderColumnIndex == 3)
                {
                    events = events.OrderByDescending(m => m.CreateDate);
                }
            }

            //Apply Pagination
            //int.TryParse(param., out start);
            events = events.Skip(param.start).Take(param.length);

            int count = 0;

            foreach (var e in events)
            {
                count++;
                allItems.Add(new EventTableModel
                {
                    Id = count,
                    Name = e.EventName,
                    Description = e.EventDescription.Length < 65 ? e.EventDescription : e.EventDescription.Substring(0, 65) + "...",
                    CreateDate = e.CreateDate.ToString("d")
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
        public ActionResult DailyVerse()
        {
            var returnData = (ReturnData)TempData["returnMessage"] ?? new ReturnData
            {
                HasValue = false
            };
            ViewBag.returns = returnData;
            return View();
        }

        [HttpPost]
        public ActionResult DailyVerse(DailyVerseModel model)
        {
            try
            {
                var dailyVerse = new DailyVerse
                {
                    Text = model.Text,
                    Quote = model.Quote,
                    Active = model.Active
                };

                if (_eventRepository.AddDailyVerse(dailyVerse) > 0)
                {
                    var returnData = new ReturnData
                    {
                        HasValue = true,
                        Message = "Daily verse was successfully added"
                    };
                    TempData["returnMessage"] = returnData;
                    return Redirect("DailyVerse");
                }
            }
            catch (Exception e)
            {
                var returnData = new ReturnData
                {
                    HasValue = true,
                    Message = "There was an error adding the verse"
                };
                TempData["returnMessage"] = returnData;
                return Redirect("DailyVerse");
            }
            return View();
        }

        [HttpPost]
        public JsonResult GetDailyVerses(DatatableParam param)
        {
            var dailyVerses = _eventRepository.GetDailyVerses();
            var allItems = new List<DailyVerseModel>();
            //Apply Searching 
            if (!string.IsNullOrEmpty(param.search.value))
            {
                var searchTerm = param.search.value;
                dailyVerses = dailyVerses.Where(m => m.Quote.Contains(searchTerm) || m.Text.Contains(searchTerm));
            }

            var totalcount = dailyVerses.Count();
            dailyVerses = dailyVerses.OrderBy(m => m.Active);

            //Apply Sorting/Ordering
            //IEnumerable<UserTableModel> entryTables = allItems;
            var orderColumnIndex = Convert.ToInt32(param.order[0].column);
            var orderDir = param.order[0].dir;
            if (orderDir.Equals("asc"))
            {
                if (orderColumnIndex == 1)
                {
                    dailyVerses = dailyVerses.OrderBy(m => m.Text);
                }
                if (orderColumnIndex == 2)
                {
                    dailyVerses = dailyVerses.OrderBy(m => m.Quote);
                }
                if (orderColumnIndex == 3)
                {
                    dailyVerses = dailyVerses.OrderBy(m => m.Active);
                }
            }
            else
            {
                if (orderColumnIndex == 1)
                {
                    dailyVerses = dailyVerses.OrderByDescending(m => m.Text);
                }
                if (orderColumnIndex == 2)
                {
                    dailyVerses = dailyVerses.OrderByDescending(m => m.Quote);
                }
                if (orderColumnIndex == 3)
                {
                    dailyVerses = dailyVerses.OrderByDescending(m => m.Active);
                }
            }

            //Apply Pagination
            //int.TryParse(param., out start);
            dailyVerses = dailyVerses.Skip(param.start).Take(param.length);

            int count = 0;

            foreach (var e in dailyVerses)
            {
                count++;
                allItems.Add(new DailyVerseModel
                {
                    Id = count,
                    Text = e.Text,
                    Quote = e.Quote,
                    Active = e.Active
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
    }
}