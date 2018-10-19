﻿using System;
using System.Collections.Generic;
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
            try
            {
                var member = new Member
                {
                    Surname = model.Surname,
                    Firstname = model.Firstname,
                    Othername = model.Othername,
                    Confirmed = 0,
                    Deleted = 0,
                    DateRegistered = DateTime.Now
                };
                if (_memberRepository.AddMember(member) > 0)
                {
                    return Redirect("MemberRegistrationSuccess");
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult MemberRegistrationSuccess()
        {
            return View();
        }        
    }
}