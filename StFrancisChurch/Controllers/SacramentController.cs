using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessObject.IRepository;

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
            return View();
        }

        public ActionResult Communion()
        {
            return View();
        }

        public ActionResult Confirmation()
        {
            return View();
        }

        public ActionResult Baptism()
        {
            return View();
        }

        public ActionResult Matrimony()
        {
            return View();
        }
    }
}