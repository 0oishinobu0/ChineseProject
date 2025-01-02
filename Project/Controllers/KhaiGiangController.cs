using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Dao;
using Models.EntityFramework;

namespace Project.Controllers
{
    public class KhaiGiangController : Controller
    {
        public ActionResult Index() { return View(); }
    }
}