using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamPortalApplicationUsingApis.Controllers
{
    public class LogoutController : Controller
    {
        // GET: Logout
        public ActionResult Index()
        {
            Session["student_id"] = null;
            Session["student_code"] = null;
            return Redirect("/Login");
        }
    }
}