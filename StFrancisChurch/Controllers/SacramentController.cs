﻿using System;
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
    [Authorize]
    public class SacramentController : Controller
    {
        private ISacramentRepository _sacramentRepository;

        public SacramentController(ISacramentRepository sacramentRepository)
        {
            _sacramentRepository = sacramentRepository;
        }

        // GET: Sacrament
        public ActionResult Index()
        {
            ViewBag.SacramentCounts = ViewUtility.GetSacramentsCount();
            return View();
        }

        public ActionResult Communion()
        {
            var returnData = (ReturnData)TempData["returnMessage"] ?? new ReturnData
            {
                HasValue = false
            };
            ViewBag.returns = returnData;
            return View();
        }

        public ActionResult Confirmation()
        {
            var returnData = (ReturnData)TempData["returnMessage"] ?? new ReturnData
            {
                HasValue = false
            };
            ViewBag.returns = returnData;
            return View();
        }

        public ActionResult Baptism()
        {
            var returnData = (ReturnData)TempData["returnMessage"] ?? new ReturnData
            {
                HasValue = false
            };
            ViewBag.returns = returnData;
            return View();
        }

        public ActionResult Matrimony()
        {
            var returnData = (ReturnData)TempData["returnMessage"] ?? new ReturnData
            {
                HasValue = false
            };
            ViewBag.returns = returnData;
            return View();
        }

        [HttpGet]
        public ActionResult CreateBaptism()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBaptism(BaptismViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var baptism = new Baptism
                {
                    BapitsmNumber = model.BaptismNumber,
                    BaptismPlace = model.BaptismPlace,
                    BaptismDate = DateTime.Parse(model.BaptismDate),
                    BaptismName = model.BaptismName,
                    BaptismType = model.BaptismType,
                    Othername = model.Othername,
                    Surname = model.Surname,
                    DateOfBirth = DateTime.Parse(model.DateOfBirth),
                    PlaceOfBirth = model.PlaceOfBirth,
                    HomeTown = model.HomeTown,
                    FathersName = model.FathersName,
                    MothersName = model.MothersName,
                    NameOfGodParent1 = model.NameOfGodParent1,
                    NameOfGodParent2 = model.NameOfGodParent2,
                    NameOfMinister = model.NameOfMinister,
                    Remarks = model.Remarks,
                    Deleted = 0,
                    CreateDate = DateTime.Now
                };
                if (_sacramentRepository.AddBaptism(baptism))
                {
                    var returnData = new ReturnData
                    {
                        HasValue = true,
                        Message = "Baptismal record was successfully created"
                    };
                    TempData["returnMessage"] = returnData;
                    return Redirect("Baptism");
                }
                ModelState.AddModelError(string.Empty, "There was an error completing the registration, Please check if the bapismal number is correct");
                return View(model);
            }
            catch (Exception e)
            {
                //error occured
                ModelState.AddModelError(string.Empty, "There was an error completing the registration, Please try again later");
                ErrorUtil.LogError(e);
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult GetBaptisedMembers(DatatableParam param)
        {
            var members = _sacramentRepository.GetBaptisedMembers();
            var allItems = new List<BaptismTableModel>();
            //Apply Searching 
            if (!string.IsNullOrEmpty(param.search.value))
            {
                var searchTerm = param.search.value;
                members = members.Where(m => m.BaptismName.Contains(searchTerm) || m.BapitsmNumber.Contains(searchTerm) || m.Surname.Contains(searchTerm));
            }

            var totalcount = members.Count();
            members = members.OrderBy(m => m.BaptismName);

            //Apply Sorting/Ordering
            //IEnumerable<UserTableModel> entryTables = allItems;
            var orderColumnIndex = Convert.ToInt32(param.order[0].column);
            var orderDir = param.order[0].dir;
            if (orderDir.Equals("asc"))
            {
                if (orderColumnIndex == 1)
                {
                    members = members.OrderBy(m => m.BaptismName);
                }
                if (orderColumnIndex == 2)
                {
                    members = members.OrderBy(m => m.Surname);
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
                    members = members.OrderByDescending(m => m.BaptismName);
                }
                if (orderColumnIndex == 2)
                {
                    members = members.OrderByDescending(m => m.Surname);
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
                allItems.Add(new BaptismTableModel
                {
                    Serial = count,
                    BaptismName = e.BaptismName,
                    Surname = e.Surname,
                    Othername = e.Othername,
                    BaptismDate = e.BaptismDate.ToString("d")
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
        public ActionResult CreateCommunion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCommunion(CommunionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var communion = new Communion()
                {
                    BapitsmNumber = model.BaptismNumber,
                    BaptismPlace = model.BaptismPlace,
                    BaptismDate = DateTime.Parse(model.BaptismDate),
                    Othernames = model.Othernames,
                    Surname = model.Surname,
                    DateReceived = DateTime.Parse(model.Date),
                    Place = model.Place,
                    FathersName = model.FathersName,
                    MothersName = model.MothersName,
                    NameOfMinister = model.Minister,
                    Deleted = 0,
                    CreateDate = DateTime.Now
                };
                if (_sacramentRepository.AddCommunion(communion))
                {
                    var returnData = new ReturnData
                    {
                        HasValue = true,
                        Message = "Communion record was successfully created"
                    };
                    TempData["returnMessage"] = returnData;
                    return Redirect("Communion");
                }
                ModelState.AddModelError(string.Empty, "There was an error completing the registration, Please check the entries and try again");
                return View(model);
            }
            catch (Exception e)
            {
                //error occured
                ModelState.AddModelError(string.Empty, "There was an error completing the registration, Please try again later");
                ErrorUtil.LogError(e);
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult GetCommunionMembers(DatatableParam param)
        {
            var members = _sacramentRepository.GetCommunionMembers();
            var allItems = new List<CommunionTableModel>();
            //Apply Searching 
            if (!string.IsNullOrEmpty(param.search.value))
            {
                var searchTerm = param.search.value;
                members = members.Where(m => m.BapitsmNumber.Contains(searchTerm) || m.Othernames.Contains(searchTerm) || m.Surname.Contains(searchTerm));
            }

            var totalcount = members.Count();
            members = members.OrderBy(m => m.DateReceived);

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
                    members = members.OrderBy(m => m.Othernames);
                }
                if (orderColumnIndex == 3)
                {
                    members = members.OrderBy(m => m.DateReceived);
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
                    members = members.OrderByDescending(m => m.Othernames);
                }
                if (orderColumnIndex == 3)
                {
                    members = members.OrderByDescending(m => m.DateReceived);
                }
            }

            //Apply Pagination
            //int.TryParse(param., out start);
            members = members.Skip(param.start).Take(param.length);

            var count = 0;

            foreach (var e in members)
            {
                count++;
                allItems.Add(new CommunionTableModel
                {
                    Serial = count,
                    Surname = e.Surname,
                    Othernames = e.Othernames,
                    DateReceived = e.BaptismDate.ToString("d")
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
        public ActionResult CreateMatrimony()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateMatrimony(MatrimonyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var matrimony = new Matrimony
                {
                    DateOfMarriage = model.DateOfMarriage,
                    PlaceOfMarriage = model.PlaceOfMarriage,
                    BrideFullName = model.BrideFullName,
                    GroomFullName = model.GroomFullName,
                    BrideAddress = model.BrideAddress,
                    GroomAddress = model.GroomAddress,
                    BrideAge = model.BrideAge,
                    GroomAge = model.GroomAge,
                    BrideBaptismPlace = model.BrideBaptismPlace,
                    BrideBaptismDate = model.BrideBaptismDate,
                    BrideBaptismNo = model.BrideBaptismNo,
                    GroomBaptismPlace = model.GroomBaptismPlace,
                    GroomBaptismDate = model.GroomBaptismDate,
                    GroomBaptismNo = model.GroomBaptismNo,
                    AssistingPriest = model.AssistingPriest,
                    BannDetails = model.BannDetails,
                    BrideParentName = model.BrideParentName,
                    BrideParentHomeTown = model.BrideParentHomeTown,
                    GroomParentName = model.GroomParentName,
                    GroomParentHomeTown = model.GroomParentHomeTown,
                    Witness1 = model.Witness1,
                    Witness2 = model.Witness2,
                    Remark = model.Remark,
                    Deleted = 0,
                    CreateDate = DateTime.Now
                };
                if (_sacramentRepository.AddMatrimony(matrimony))
                {
                    var returnData = new ReturnData
                    {
                        HasValue = true,
                        Message = "Marriage record was successfully created"
                    };
                    TempData["returnMessage"] = returnData;
                    return Redirect("Matrimony");
                }
                ModelState.AddModelError(string.Empty, "There was an error completing the registration, Please check if the bapismal number is correct");
                return View(model);
            }
            catch (Exception e)
            {
                //error occured
                ModelState.AddModelError(string.Empty, "There was an error completing the registration, Please try again later");
                ErrorUtil.LogError(e);
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult GetMatrimonyMembers(DatatableParam param)
        {
            var members = _sacramentRepository.GetMatrimonyMembers();
            var allItems = new List<MatrimonyTableModel>();
            //Apply Searching 
            if (!string.IsNullOrEmpty(param.search.value))
            {
                var searchTerm = param.search.value;
                members = members.Where(m => m.BrideFullName.Contains(searchTerm) || m.GroomFullName.Contains(searchTerm));
            }

            var totalcount = members.Count();
            members = members.OrderBy(m => m.Id);

            //Apply Sorting/Ordering
            //IEnumerable<UserTableModel> entryTables = allItems;
            var orderColumnIndex = Convert.ToInt32(param.order[0].column);
            var orderDir = param.order[0].dir;
            if (orderDir.Equals("asc"))
            {
                /*if (orderColumnIndex == 1)
                {
                    members = members.OrderBy(m => m.BaptismName);
                }
                if (orderColumnIndex == 2)
                {
                    members = members.OrderBy(m => m.Surname);
                }
                if (orderColumnIndex == 3)
                {
                    members = members.OrderBy(m => m.Othername);
                }*/
            }
            else
            {
                /*if (orderColumnIndex == 1)
                {
                    members = members.OrderByDescending(m => m.BaptismName);
                }
                if (orderColumnIndex == 2)
                {
                    members = members.OrderByDescending(m => m.Surname);
                }
                if (orderColumnIndex == 3)
                {
                    members = members.OrderByDescending(m => m.Othername);
                }*/
            }

            //Apply Pagination
            //int.TryParse(param., out start);
            members = members.Skip(param.start).Take(param.length);

            var count = 0;

            foreach (var e in members)
            {
                count++;
                allItems.Add(new MatrimonyTableModel
                {
                    Serial = count,
                    BrideFullName = e.BrideFullName,
                    GroomFullName = e.GroomFullName,
                    DateOfMarriage = e.DateOfMarriage,
                    PlaceOfMarriage = e.PlaceOfMarriage
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
        public ActionResult CreateConfirmation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateConfirmation(ConfirmationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var confirmation = new Confirmation
                {
                    BapitsmNumber = model.BaptismNumber,
                    BaptismPlace = model.BaptismPlace,
                    BaptismDate = DateTime.Parse(model.BaptismDate),
                    ConfirmationName = model.ConfirmationName,
                    DateReceived = DateTime.Parse(model.Date),
                    Place = model.Place,
                    Number = model.Number,
                    Othernames = model.Othernames,
                    Surname = model.Surname,
                    FathersName = model.FathersName,
                    MothersName = model.MothersName,
                    Sponsor = model.Sponsor,
                    NameOfMinister = model.Minister,
                    Deleted = 0,
                    CreateDate = DateTime.Now
                };
                if (_sacramentRepository.AddConfirmation(confirmation))
                {
                    var returnData = new ReturnData
                    {
                        HasValue = true,
                        Message = "Confirmation record was successfully created"
                    };
                    TempData["returnMessage"] = returnData;
                    return Redirect("Confirmation");
                }
                ModelState.AddModelError(string.Empty, "There was an error completing the registration, Please check if the confirmation number is correct");
                return View(model);
            }
            catch (Exception e)
            {
                //error occured
                ModelState.AddModelError(string.Empty, "There was an error completing the registration, Please try again later");
                ErrorUtil.LogError(e);
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult GetConfirmationMembers(DatatableParam param)
        {
            var members = _sacramentRepository.GetConfirmedMembers();
            var allItems = new List<ConfirmationTableModel>();
            //Apply Searching 
            if (!string.IsNullOrEmpty(param.search.value))
            {
                var searchTerm = param.search.value;
                members = members.Where(m => m.ConfirmationName.Contains(searchTerm) || m.Number.Contains(searchTerm) || m.Surname.Contains(searchTerm));
            }

            var totalcount = members.Count();
            members = members.OrderBy(m => m.DateReceived);

            //Apply Sorting/Ordering
            //IEnumerable<UserTableModel> entryTables = allItems;
            var orderColumnIndex = Convert.ToInt32(param.order[0].column);
            var orderDir = param.order[0].dir;
            if (orderDir.Equals("asc"))
            {
                if (orderColumnIndex == 1)
                {
                    members = members.OrderBy(m => m.Number);
                }
                if (orderColumnIndex == 2)
                {
                    members = members.OrderBy(m => m.Surname);
                }
                if (orderColumnIndex == 3)
                {
                    members = members.OrderBy(m => m.ConfirmationName);
                }
            }
            else
            {
                if (orderColumnIndex == 1)
                {
                    members = members.OrderByDescending(m => m.Number);
                }
                if (orderColumnIndex == 2)
                {
                    members = members.OrderByDescending(m => m.Surname);
                }
                if (orderColumnIndex == 3)
                {
                    members = members.OrderByDescending(m => m.ConfirmationName);
                }
            }

            //Apply Pagination
            //int.TryParse(param., out start);
            members = members.Skip(param.start).Take(param.length);

            var count = 0;

            foreach (var e in members)
            {
                count++;
                allItems.Add(new ConfirmationTableModel()
                {
                    Serial = count,
                    Number = e.Number,
                    Surname = e.Surname,
                    ConfirmationName = e.ConfirmationName,
                    DateReceived = e.DateReceived.ToString("d")
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