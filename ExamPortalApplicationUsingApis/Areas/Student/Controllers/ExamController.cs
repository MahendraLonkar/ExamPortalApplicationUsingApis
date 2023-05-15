using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExamPortalApplicationUsingApis.Models;
using ExamPortalApplicationUsingApis.BL;
using ExamPortalApplicationUsingApis.Areas.Student.Models;
namespace ExamPortalApplicationUsingApis.Areas.Student.Controllers
{
    public class ExamController : Controller
    {
        CIIT_CRMDBEntities db = new CIIT_CRMDBEntities();

        static  tblstudent_exam_details st = null;
        RepositoryBL<tbltopic, int> topicrepo;
        RepositoryBL<tbltopic_contents,int> contentrepo;
        RepositoryBL<tblcontent_questions,int> questionrepo;
        RepositoryBL<tblstudent_exam_details,int> examrepo;

        public ExamController()
        {
            examrepo = new RepositoryBL<tblstudent_exam_details, int>(new CIIT_CRMDBEntities());
            contentrepo = new RepositoryBL<tbltopic_contents, int>(new CIIT_CRMDBEntities());
            topicrepo = new RepositoryBL<tbltopic, int>(new CIIT_CRMDBEntities());
            questionrepo = new RepositoryBL<tblcontent_questions, int>(new CIIT_CRMDBEntities());
        }

        public List<StudentExamModel> GetStudentWiseAttemptedExams()
        {

            int id = Convert.ToInt32(Session["student_id"].ToString());

            List<tblstudent_exam_details> lst = examrepo.GetAll().Where(e => e.student_id.Equals(id)).ToList();
            List<StudentExamModel> studentexams = new List<StudentExamModel>();
            foreach (tblstudent_exam_details s in lst)
            {
               int topic_id = (int)s.tblstudent_exam_questions.ToList().ToArray()[0].tblcontent_questions.tbltopic_contents.topic_id;
                tbltopic tp = topicrepo.GetById(topic_id);
                StudentExamModel sm = new StudentExamModel()
                {
                    student_id = (int)s.student_id,
                    student_name = s.tblstudent_details.student_name,
                    exam_id = s.exam_id,
                    topic_id=tp.topic_id,
                    topic_name=tp.topic_name,
 
                    start_time = (DateTime)s.start_time,
                    end_time = (DateTime)s.end_time,
                    exam_date = (DateTime)s.exam_date

                };

                List<StudentExam_AnswerModel> answers = new List<StudentExam_AnswerModel>();
                int total_questions = s.tblstudent_exam_questions.ToList().Count();
                int correct_questions = 0;
                foreach (tblstudent_exam_questions q in s.tblstudent_exam_questions.ToList())
                {
                    StudentExam_AnswerModel ans = new StudentExam_AnswerModel()
                    {
                        question_id = (int)q.question_id,
                        question = q.tblcontent_questions.question,

                        exam_id = s.exam_id,
                        student_submitted_option_number = (int)q.student_submitted_option,
                    };

                    tblcontent_questions qs = questionrepo.GetById((int)q.question_id);
                    ans.option1 = qs.option1;
                    ans.option2 = qs.option2;
                    ans.option3 = qs.option3;
                    ans.option4 = qs.option4;
                    ans.correct_option_number = (int)qs.correct_option_number;
                    if (q.student_submitted_option == qs.correct_option_number)
                    {
                        correct_questions++;
                        ans.status = "Correct";
                    }
                    else
                    {
                        ans.status = "Wrong";

                    }

                    answers.Add(ans);
                }
                sm.total_attempted_questions = total_questions;
                sm.total_correct_questions = correct_questions;
                sm.studentanswers = answers;
                studentexams.Add(sm);
            }
            return studentexams;
        }
        public ActionResult Index()
        {
            List<StudentExamModel> studentexams = GetStudentWiseAttemptedExams();
            return View(studentexams);
        }
        public ActionResult Exam_Details(int id)
        {
            StudentExamModel exam = GetStudentWiseAttemptedExams().FirstOrDefault(e => e.exam_id.Equals(id));
            return View(exam);
        }


        public ActionResult Select_Topic()
        {
            ViewBag.topics = GetTopics();
            return View();
        }

        public List<SelectListItem> GetTopics()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach(tbltopic topic in topicrepo.GetAll())
            {
                SelectListItem s = new SelectListItem() { Text=topic.topic_name, Value=topic.topic_id.ToString() };
                list.Add(s);
            }
            return list;
        }
        [HttpPost]
        public ActionResult StartExam(int topic_id)
        {


            var query = from c in contentrepo.GetAll()
                        join q in questionrepo.GetAll() on c.content_id equals q.content_id
                        join t in topicrepo.GetAll() on c.topic_id equals t.topic_id
                        where t.topic_id.Equals(topic_id)
                        select new
                        {
                            question_id = q.question_id,
                            content_id = c.content_id,
                            question = q.question,
                            option1 = q.option1,
                            option2 = q.option2,
                            option3 = q.option3,
                            option4 = q.option4,
                            correct_option_number = q.correct_option_number,
                            topic_id=t.topic_id
                        };
            List <tblcontent_questions> lst = new List<tblcontent_questions>();
            foreach (var q in query)
            {
                if(q.topic_id ==topic_id)
                {
                    lst.Add(new tblcontent_questions() { question_id=q.question_id, question=q.question, option1=q.option1, option2=q.option2, option3=q.option3, option4=q.option4 , correct_option_number=q.correct_option_number, content_id=q.content_id }) ;
                }
            }
           
            int size = lst.Count;
            Random r = new Random();
            int n = size - 1;

            int max_id = lst.Max(e => e.question_id);
            int min_id = lst.Min(e => e.question_id);

            List<tblcontent_questions> lstquestions = new List<tblcontent_questions>();
            while (lstquestions.Count !=5) 
            { 
                int id = r.Next(min_id, max_id);
                tblcontent_questions q = db.tblcontent_questions.Find(id);
                if (q != null)
                {
                    if (lstquestions.IndexOf(q) == -1)
                    {
                        lstquestions.Add(q);
                    }
                }
            }

            //for (int i = 1; i <= 7; i++)
            //{
            //    int index = r.Next(0, size - 1);
            //    tblcontent_questions t = lst.ToArray()[index];
            //    lstquestions.Add(t);
            //}

            st = new tblstudent_exam_details() {
                start_time = DateTime.Now
            };
            return View(lstquestions);

            //return View(lst);

        }

        [HttpPost]
        public string SubmitExam(List<tblstudent_exam_questions> lst)
        {
            int sid = Convert.ToInt32(Session["student_id"].ToString());
            st.student_id = sid;
             st.exam_date =DateTime.Now;
            st.flag =0;
            st.end_time =DateTime.Now;
            st.status ="Completed";

            List<tblstudent_exam_questions> lstquestions = new List<tblstudent_exam_questions>();
            foreach(tblstudent_exam_questions q in lst)
            {
                lstquestions.Add(q);

            }
            st.tblstudent_exam_questions = lstquestions;
            examrepo.Create(st);
            return "Exam Submitted Successfully";
        }
    }
}