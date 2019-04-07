using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessObject.DataModel;
using DataAccessObject.IRepository;
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
    }
}