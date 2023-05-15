using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExamPortalApplicationUsingApis.Models;
using ExamPortalApplicationUsingApis.BL;
namespace ExamPortalApplicationUsingApis.Controllers
{
    public class LoginController : Controller
    {
        RepositoryBL<tblstudent_details, int> studtepo;
        RepositoryBL<tbladmin_details, int> adminepo;
        public LoginController()
        {
            adminepo = new RepositoryBL<tbladmin_details, int>(new CIIT_CRMDBEntities());
            studtepo = new RepositoryBL<tblstudent_details, int>(new CIIT_CRMDBEntities());
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string scode = form["student_code"];
            string password = form["password"];
            tbladmin_details ad=null; 
            ad= adminepo.GetAll().FirstOrDefault(e => e.user_id.Equals(scode) & e.password.Equals(password));

            if (ad != null)
            {
                Session["student_code"] = scode;
                Session["student_id"] = ad.user_id;
                Session.Timeout = 10;
                return Redirect("/Master/Master/Index");

            }
            else
            {
                tblstudent_details st = null;
                st = studtepo.GetAll().FirstOrDefault(e => e.student_code.Equals(scode) & e.password.Equals(password));
                if (st != null)
                {
                    Session["student_code"] = scode;
                    Session["student_id"] = st.student_id;
                    Session.Timeout = 10;
                    return Redirect("/Student/Profile/Index");
                }
                else
                {
                    ViewBag.msg = "Invalid Student Code or Password";
                    return View();

                }
            }
        }
	}
}