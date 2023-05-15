using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExamPortalApplicationUsingApis.Models;
using ExamPortalApplicationUsingApis.BL;
using System.IO;
namespace ExamPortalApplicationUsingApis.Areas.Master.Controllers
{
    public class MasterController : Controller
    {
        CIIT_CRMDBEntities db = new CIIT_CRMDBEntities();
        RepositoryBL<tbltopic_contents, int> contentrepo;
        RepositoryBL<tbltopic, int> topicrepo;
        RepositoryBL<tblrole, int> rolerepo;
        public MasterController()
        {
            contentrepo = new RepositoryBL<tbltopic_contents, int>(new CIIT_CRMDBEntities());
            topicrepo = new RepositoryBL<tbltopic, int>(new CIIT_CRMDBEntities());
            rolerepo = new RepositoryBL<tblrole, int>(new CIIT_CRMDBEntities());
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Topic()
        {
            return View();

        }
        public ActionResult Content()
        {
            ViewBag.topics = GetTopics();
            List<tbltopic_contents> lst = contentrepo.GetAll();
            ViewBag.topic_contents = lst;
            return View();
        }
        [HttpPost]
        public ActionResult Content(tbltopic_contents c,HttpPostedFileBase video,HttpPostedFileBase[]images)
        {

            string videopath = Server.MapPath("~/Tutorial/Videos/") + c.content_name;
            if (!Directory.Exists(videopath))
            {
                Directory.CreateDirectory(videopath);
            }
            string videoname = c.content_name + Path.GetExtension(video.FileName);
            string videourl = videopath + "/" + videoname;
            video.SaveAs(videourl);
            c.video_url = videoname;

            string imgpath = Server.MapPath("~/Tutorial/Images/") + c.content_name;
            if (!Directory.Exists(imgpath))
            {
                Directory.CreateDirectory(imgpath);
            }

            int i = 1;
            string imgnames="";
            foreach (HttpPostedFileBase h in images)
            {
                string imgname = i + Path.GetExtension(h.FileName);
                string path = imgpath + "/" + imgname;
                h.SaveAs(path);
                i++;
                imgnames =imgnames+ "," + imgname;
            }
            imgnames = imgnames.Substring(1, imgnames.Length - 1);
            c.ppts = imgnames;
            c.flag = 0;
            contentrepo.Create(c);

            ViewBag.topics = GetTopics();
            List<tbltopic_contents> lst = contentrepo.GetAll();
            ViewBag.topic_contents = lst;

            return View();
        }
        public List<SelectListItem> GetTopics()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            foreach(tbltopic t in topicrepo.GetAll())
            {
                SelectListItem s = new SelectListItem() { Text = t.topic_name, Value = t.topic_id.ToString() };
                lst.Add(s);
            }
            return lst;
        }
        public ActionResult Content_Question()
        {
            return View();
        }
        public ActionResult AllContent_Question()
        {
            return View();
        }

        public ActionResult Interview_Question()
        {
            return View();
        }
        public ActionResult AllInterview_Question()
        {
            return View();
        }
        public ActionResult Question_Level()
        {
            return View();
        }
        public ActionResult Role()
        {
            return View();
        }
        public List<RoleModel> GetAllRole()
        {
            List<RoleModel> lst = new List<RoleModel>();
            List<tblrole> r = db.tblroles.ToList();
            foreach(tblrole t in r)
            {
                RoleModel s = new RoleModel()
                {
                    RoleId = t.role_id,
                    RoleName = t.role_name
                };
                lst.Add(s);
            }
            return lst;
        }

        public JsonResult GetTheRoles()
        {
            return Json(GetAllRole(), JsonRequestBehavior.AllowGet);
        }
        
	}
}