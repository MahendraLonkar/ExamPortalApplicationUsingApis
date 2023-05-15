using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExamPortalApplicationUsingApis.Models;
using ExamPortalApplicationUsingApis.BL;
namespace ExamPortalApplicationUsingApis.Areas.Student.Controllers
{
    public class TutorialController : Controller
    {
        RepositoryBL<tbltopic_contents, int> contentrepo;
        public TutorialController()
        {
            contentrepo = new RepositoryBL<tbltopic_contents, int>(new CIIT_CRMDBEntities());
        }
        public ActionResult Videos()
        {
            List<tbltopic_contents> lst = contentrepo.GetAll().ToList();
            return View(lst);
        }
        [HttpPost]
        public ActionResult Videos(FormCollection form)
        {
             List<tbltopic_contents> lst=null;
            string txt = form["txtsearch"];
            if (txt == "" || txt == null)
            {
              lst  = contentrepo.GetAll().ToList();


            }
            else
            {
                  lst = contentrepo.GetAll().ToList().Where(e => e.content_name.ToLower().Contains(txt.ToLower())).ToList();
            }
            return View(lst);

        }
	}
}