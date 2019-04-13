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
    }
}