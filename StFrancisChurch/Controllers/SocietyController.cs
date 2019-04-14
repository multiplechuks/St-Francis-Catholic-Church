using System;
using System.Collections.Generic;
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
    public class SocietyController : Controller
    {
        public ISocietyRepository _societyRepository;

        public SocietyController(ISocietyRepository societyRepository)
        {
            _societyRepository = societyRepository;
        }

        // GET: Society
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

        [HttpGet]
        public ActionResult Position()
        {
            var returnData = (ReturnData)TempData["returnMessage"] ?? new ReturnData
            {
                HasValue = false
            };
            ViewBag.returns = returnData;
            return View();
        }

        [HttpPost]
        public ActionResult Position(PositionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var position = new Position
                {
                    PositionName = model.Name,
                    PositionDescription = model.Description
                };
                if (_societyRepository.AddPosition(position))
                {
                    var returnData = new ReturnData
                    {
                        HasValue = true,
                        Message = "Position was successfully created"
                    };
                    TempData["returnMessage"] = returnData;
                    return Redirect("Position");
                }
                ModelState.AddModelError(string.Empty, "There was an error completing the request, Please try again");
                return View(model);
            }
            catch (Exception e)
            {
                //error occured
                ModelState.AddModelError(string.Empty, "There was an error completing the request, Please try again later");
                ErrorUtil.LogError(e);
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult GetPosition(DatatableParam param)
        {
            var members = _societyRepository.GetPositions();
            var allItems = new List<PositionTableModel>();
            //Apply Searching 
            if (!string.IsNullOrEmpty(param.search.value))
            {
                var searchTerm = param.search.value;
                members = members.Where(m => m.PositionName.Contains(searchTerm) || m.PositionDescription.Contains(searchTerm));
            }

            var totalcount = members.Count();
            members = members.OrderBy(m => m.PositionName);

            //Apply Sorting/Ordering
            //IEnumerable<UserTableModel> entryTables = allItems;
            var orderColumnIndex = Convert.ToInt32(param.order[0].column);
            var orderDir = param.order[0].dir;
            if (orderDir.Equals("asc"))
            {
                if (orderColumnIndex == 1)
                {
                    members = members.OrderBy(m => m.PositionName);
                }
                if (orderColumnIndex == 2)
                {
                    members = members.OrderBy(m => m.PositionDescription);
                }
            }
            else
            {
                if (orderColumnIndex == 1)
                {
                    members = members.OrderByDescending(m => m.PositionName);
                }
                if (orderColumnIndex == 2)
                {
                    members = members.OrderByDescending(m => m.PositionDescription);
                }
            }

            //Apply Pagination
            //int.TryParse(param., out start);
            members = members.Skip(param.start).Take(param.length);

            var count = 0;

            foreach (var e in members)
            {
                count++;
                allItems.Add(new PositionTableModel
                {
                    Serial = count,
                    PositionName = e.PositionName,
                    PositionDescription = e.PositionDescription
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