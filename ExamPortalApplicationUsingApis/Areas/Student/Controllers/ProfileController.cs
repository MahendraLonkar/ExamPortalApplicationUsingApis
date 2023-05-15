using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExamPortalApplicationUsingApis.Models;
using ExamPortalApplicationUsingApis.BL;
namespace ExamPortalApplicationUsingApis.Areas.Student.Controllers
{
    public class ProfileController : Controller
    {

        RepositoryBL<tblstudent_details, int> studrepo;
        public ProfileController()
        {
            studrepo = new RepositoryBL<tblstudent_details, int>(new CIIT_CRMDBEntities());
        }
       
        public ActionResult Index()
        {
            if(Session["student_id"]!=null)
            {
                int sid=Convert.ToInt32(Session["student_id"].ToString());
                tblstudent_details st = studrepo.GetById(sid);
                return View(st);
            }
            else
            {
                return Redirect("/Login/Index");
            }
           

        }
	}
}