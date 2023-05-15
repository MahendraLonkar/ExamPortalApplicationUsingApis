using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;
using ExamPortalApplicationUsingApis.Models;
using ExamPortalApplicationUsingApis.BL;
using System.IO;
using System.Net.Mail;
using System.Net;
namespace ExamPortalApplicationUsingApis.Areas.Master.Controllers
{
    public class StudentController : Controller
    {
        RepositoryBL<tblstudent_details, int> studrepo;
        RepositoryBL<tblstudent_exam_details, int> examrepo;
        RepositoryBL<tblstudent_exam_questions, int> examquestionrepo;
        ExtraBL ebl;
        public StudentController()
        {
            ebl = new ExtraBL();
            studrepo = new RepositoryBL<tblstudent_details, int>(new CIIT_CRMDBEntities());
            examrepo = new RepositoryBL<tblstudent_exam_details, int>(new CIIT_CRMDBEntities());
            examquestionrepo = new RepositoryBL<tblstudent_exam_questions, int>(new CIIT_CRMDBEntities());
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllStudents()
        {
            List<tblstudent_details> lst = studrepo.GetAll();
            return View(lst);
        }
        public ActionResult AddStudent()
        {

            string studentcode = ExtraBL.GenerateNewStudentCode();
            int cnt = studrepo.GetAll().Count();
            ViewBag.count = cnt;
            ViewBag.msg = "";
            tblstudent_details st = new tblstudent_details() { student_code = studentcode };
            return View(st);
        }

        [HttpPost]
        public ActionResult AddStudent(tblstudent_details s , HttpPostedFileBase photo)
        {
            string password = ExtraBL.GetRandomPassword();
            s.password = password;
            if(photo==null)
            {

                ViewBag.msg = "Please select a photo";
                string studentcode = ExtraBL.GenerateNewStudentCode();
                tblstudent_details st = new tblstudent_details() { student_code = studentcode };
                return View(st);


            }
            else 
            {

                string photoname = s.student_name + Path.GetExtension(photo.FileName);
                string photopath = Server.MapPath("~/Student Photos/" + photoname);
                photo.SaveAs(photopath);
                s.photo = photoname;
                studrepo.Create(s);
                string studentcode = ExtraBL.GenerateNewStudentCode();

                ViewBag.msg = "Student Added Successfully";

                string subject="Registartion Confirmation";
                string body="<h2>Dear <br>"+s.student_name+",</h2> <p>Your Account has been created Successfully.</p><p>Your Login Id=<b>"+s.student_code+"</b> and password=<b>"+password+"</b> </p>";
                 ebl.Send_Gmail_Email(s.email_address,subject,body);
               // ebl.Send_Gmail_Email(s.email_address, subject, body);
                //tblstudent_details st = new tblstudent_details() { student_code = studentcode };
                return Redirect("/Master/Student/AddStudent");
            }

        }

        public ActionResult AllExams()
        {
            return View();
        }

       

    }
}