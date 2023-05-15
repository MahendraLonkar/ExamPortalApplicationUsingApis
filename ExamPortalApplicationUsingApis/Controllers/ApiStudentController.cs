using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ExamPortalApplicationUsingApis.Models;
using ExamPortalApplicationUsingApis.BL;
namespace ExamPortalApplicationUsingApis.Controllers
{
    public class ApiStudentController : ApiController
    {
        RepositoryBL<tblstudent_details, int> studrepo;
        RepositoryBL<tblstudent_exam_details, int> examrepo;
        RepositoryBL<tblstudent_exam_questions, int> questionrepo;
        public ApiStudentController()
        {
            studrepo = new RepositoryBL<tblstudent_details, int>(new CIIT_CRMDBEntities());
            examrepo = new RepositoryBL<tblstudent_exam_details, int>(new CIIT_CRMDBEntities());
            questionrepo = new RepositoryBL<tblstudent_exam_questions, int>(new CIIT_CRMDBEntities());
        }


        //------------------------------Student Apis---------------------------//

        [HttpPost]
        [Route("api/student")]
        public string AddStudentDetails(tblstudent_details s)
        {

            studrepo.Create(s);
            return "Student Added Successfully";
        }

        [HttpGet]
        [Route("api/student")]
        public List<tblstudent_details> AllStudents()
        {
            List<tblstudent_details> lststudents = new List<tblstudent_details>();
            foreach (tblstudent_details s in studrepo.GetAll())
            {
                tblstudent_details st = new tblstudent_details() { student_id = s.student_id, student_name = s.student_name, student_code = s.student_code, password = s.password, photo = s.photo, address = s.address, email_address = s.email_address, mobile_number = s.mobile_number };
                lststudents.Add(st);
            }
            return lststudents;
        }
        [HttpGet]
        [Route("api/student/{id}")]
        public tblstudent_details GetStudentById(int id)
        {
            tblstudent_details s = studrepo.GetById(id);
            s.tblstudent_exam_details.Clear();


            return s;
        }



        //------------------------------Student Exam Apis---------------------------//

        [HttpPost]
        [Route("api/student_exam")]
        public string AddStudentExam(tblstudent_exam_details s)
        {
            s.flag = 0;
            examrepo.Create(s);
            return "Student Exam Added Successfully";
        }
        [HttpGet]
        [Route("api/student_exam")]
        public List<tblstudent_exam_details> AllStudentExams()
        {
            List<tblstudent_exam_details> lstexams = new List<tblstudent_exam_details>();
            foreach (tblstudent_exam_details s in examrepo.GetAll())
            {
                tblstudent_exam_details st = new tblstudent_exam_details() { exam_id = s.exam_id, student_id = s.student_id, exam_date = s.exam_date, end_time = s.end_time, start_time = s.start_time, status = s.status };
                lstexams.Add(st);
            }
            return lstexams;
        }
        [HttpGet]
        [Route("api/student_exam/{id}")]
        public tblstudent_exam_details GetStudentExamById(int id)
        {
            tblstudent_exam_details s = examrepo.GetById(id);
            s.tblstudent_details = null;
            s.tblstudent_exam_questions.Clear();


            return s;
        }


        //------------------------------Student Exam Apis---------------------------//

        //[HttpPost]
        //[Route("api/student_exam_question")]
        //public string AddStudentExamQuestion(tblstudent_exam_details s)
        //{
        //    examrepo.Create(s);
        //    return "Student Exam Added Successfully";
        //}

        [HttpGet]
        [Route("api/exam_wise_student_question/{id}")]
        public List<tblstudent_exam_questions> AllExamWiseStudentQuestions(int id)
        {
            List<tblstudent_exam_questions> lstexamsquestions = new List<tblstudent_exam_questions>();
            foreach (tblstudent_exam_questions s in questionrepo.GetAll().Where(e => e.exam_id.Equals(id)).ToList())
            {
                s.tblcontent_questions = null;
                s.tblstudent_exam_details = null;
                lstexamsquestions.Add(s);
            }
            return lstexamsquestions;
        }

    }
}
