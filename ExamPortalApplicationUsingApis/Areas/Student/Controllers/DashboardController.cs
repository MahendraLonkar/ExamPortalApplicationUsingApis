using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamPortalApplicationUsingApis.Areas.Student.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Student/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}