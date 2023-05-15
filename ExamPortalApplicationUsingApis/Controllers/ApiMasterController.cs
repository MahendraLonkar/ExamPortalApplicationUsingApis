using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ExamPortalApplicationUsingApis.Models;
using ExamPortalApplicationUsingApis.BL;
using System.Web;
namespace ExamPortalApplicationUsingApis.Controllers
{
    public class ApiMasterController : ApiController
    {
        IRepositoryBL<tbltopic, int> topicrepo;
        IRepositoryBL<tbltopic_contents, int> contentrepo;
        IRepositoryBL<vw_topic_contents, int> vwcontentrepo;
        IRepositoryBL<tblcontent_questions, int> questionrepo;
        IRepositoryBL<vw_content_Questions, int> vwquestionrepo;
        IRepositoryBL<tbllevel, int> levelrepo;
        IRepositoryBL<tblrole, int> rolerepo;
        IRepositoryBL<tblinterview_questions, int> interviewrepo;

        public ApiMasterController()
        {
            topicrepo = new RepositoryBL<tbltopic, int>(new CIIT_CRMDBEntities());
            contentrepo = new RepositoryBL<tbltopic_contents, int>(new CIIT_CRMDBEntities());
            questionrepo = new RepositoryBL<tblcontent_questions, int>(new CIIT_CRMDBEntities());
            levelrepo = new RepositoryBL<tbllevel, int>(new CIIT_CRMDBEntities());
            rolerepo = new RepositoryBL<tblrole, int>(new CIIT_CRMDBEntities());
            vwcontentrepo = new RepositoryBL<vw_topic_contents, int>(new CIIT_CRMDBEntities());
            vwquestionrepo = new RepositoryBL<vw_content_Questions, int>(new CIIT_CRMDBEntities());
            interviewrepo = new RepositoryBL<tblinterview_questions, int>(new CIIT_CRMDBEntities());
        }


        //-------------------------Topic APIS------------------------------//


        [HttpPost]
        [Route("api/topic")]
        public string AddTopic(tbltopic t)
        {
            t.flag = 0;
            topicrepo.Create(t);
            return "Topic Added Successfully";

        }
        [HttpGet]
        [Route("api/topic")]
        public List<tbltopic> GetAllTopics()
        {
            List<tbltopic> lsttopics = new List<tbltopic>();
            foreach (tbltopic t in topicrepo.GetAll().Where(e => e.flag.Equals(0)).ToList())
            {
                t.tbltopic_contents.Clear();
                lsttopics.Add(t);
            }
            return lsttopics;
        }
        [HttpGet]
        [Route("api/topic/{id}")]
        public tbltopic GetTopicById(int id)
        {
            tbltopic t = topicrepo.GetById(id);
            t.tbltopic_contents.Clear();
            return t;
        }
        [HttpPut]
        [Route("api/topic")]
        public string UpdateTopic(tbltopic t)
        {
            topicrepo.Update(t);
            return "Topic Updated Successfully";
        }

        [HttpDelete]
        [Route("api/deletetopic/{id}")]
        public string DeleteTopic(int id)
        {
            tbltopic t = topicrepo.GetById(id);
            t.flag = 1;
            topicrepo.Delete(t);
            return "Topic Deleted Successfully";
        }





        //-------------------------Content APIS------------------------------//

        [HttpPost]
        [Route("api/content")]
        public string AddContent(tbltopic_contents t)
        {

            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    docfiles.Add(filePath);
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }


            t.flag = 0;
            contentrepo.Create(t);
            return "Content Added Successfully";

        }




        //[AcceptVerbs(HttpVerbs.Post)]
        //public JsonResult UploadFile()
        //{
        //    string _imgname = string.Empty;
        //    if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
        //    {
        //        var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
        //        if (pic.ContentLength > 0)
        //        {
        //            var fileName = Path.GetFileName(pic.FileName);
        //            var _ext = Path.GetExtension(pic.FileName);

        //            _imgname = Guid.NewGuid().ToString();
        //            var _comPath = Server.MapPath("/Upload/MVC_") + _imgname + _ext;
        //            _imgname = "MVC_" + _imgname + _ext;

        //            ViewBag.Msg = _comPath;
        //            var path = _comPath;

        //            // Saving Image in Original Mode
        //            pic.SaveAs(path);

        //            // resizing image
        //            MemoryStream ms = new MemoryStream();
        //            WebImage img = new WebImage(_comPath);

        //            if (img.Width > 200)
        //                img.Resize(200, 200);
        //            img.Save(_comPath);
        //            // end resize
        //        }
        //    }
        //    return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
        //}
        //[HttpGet]
        //[Route("api/content")]
        //public List<tbltopic_contents> GetAllContents()
        //{
        //    List<tbltopic_contents> lstcontents = new List<tbltopic_contents>();
        //    foreach (tbltopic_contents t in contentrepo.GetAll())
        //    {
        //        tbltopic_contents tp = new tbltopic_contents() { content_id=t.content_id, content_name=t.content_name , topic_id=t.topic_id };
        //        lstcontents.Add(tp);
        //    }
        //    return lstcontents;
        //}
        public List<ContentModel> GetContentData()
        {
            List<ContentModel> lst = new List<ContentModel>();
            foreach (tbltopic_contents t in contentrepo.GetAll())
            {
                ContentModel cm = new ContentModel() { content_id = t.content_id, content_name = t.content_name, description = t.description, ppts = t.ppts, video_url = t.video_url, topic_id = (int)t.topic_id, topic_name = t.tbltopic.topic_name };
                lst.Add(cm);
            }
            return lst;
        }

        [HttpGet]
        [Route("api/content")]
        public List<ContentModel> GetAllContents()
        {

            return GetContentData();
        }
        [HttpGet]
        [Route("api/topic_wise_content/{id}")]
        public List<ContentModel> GetTopicWiseContents(int id)
        {
            return GetContentData().Where(e => e.topic_id.Equals(id)).ToList();
        }

        [HttpGet]
        [Route("api/content/{id}")]
        public ContentModel GetTopicContentById(int id)
        {
            //tbltopic_contents t = contentrepo.GetById(id);
            //tbltopic_contents tp = new tbltopic_contents() { content_id=t.content_id , topic_id=t.topic_id, content_name=t.content_name };


            return GetContentData().FirstOrDefault(e => e.content_id.Equals(id));
        }


        //-------------------------Content Question APIS------------------------------//

        [HttpPost]
        [Route("api/content_question")]
        public string AddQuestion(List<tblcontent_questions> qlist)
        {

            foreach (tblcontent_questions q in qlist)
            {
                q.flag = 0;
                questionrepo.Create(q);
            }
            return "Questions Added Successfully";

        }

        [HttpGet]
        [Route("api/content_question")]
        public List<tblcontent_questions> GetAllContentQuestions()
        {
            List<tblcontent_questions> lstcontentquestions = new List<tblcontent_questions>();
            foreach (tblcontent_questions t in questionrepo.GetAll())
            {
                tblcontent_questions q = new tblcontent_questions() { content_id = t.content_id, question_id = t.question_id, question = t.question, option1 = t.option1, option2 = t.option2, option3 = t.option3, option4 = t.option4, level_id = t.level_id, correct_option_number = t.correct_option_number };
                lstcontentquestions.Add(q);
            }
            return lstcontentquestions;
        }

        [HttpGet]
        [Route("api/content_wise_questions/{id}")]
        public List<vw_content_Questions> GetContentWiseQuestions(int id)
        {

            return vwquestionrepo.GetAll().Where(e => e.content_id.Equals(id)).ToList();
        }

        [HttpGet]
        [Route("api/topic_wise_questions/{id}")]
        public List<vw_content_Questions> GetTopicWiseQuestions(int id)
        {

            return vwquestionrepo.GetAll().Where(e => e.topic_id.Equals(id)).ToList();
        }

        [HttpGet]
        [Route("api/level_wise_questions/{id}")]
        public List<vw_content_Questions> GetLevelWiseQuestions(int id)
        {

            return vwquestionrepo.GetAll().Where(e => e.level_id.Equals(id)).ToList();
        }
        [HttpGet]
        [Route("api/content_question/{id}")]
        public tblcontent_questions GetContentQuestionById(int id)
        {
            tblcontent_questions t = questionrepo.GetById(id);
            tblcontent_questions q = new tblcontent_questions() { content_id = t.content_id, question_id = t.question_id, question = t.question, option1 = t.option1, option2 = t.option2, option3 = t.option3, option4 = t.option4, level_id = t.level_id, correct_option_number = t.correct_option_number };

            return q;
        }


        /*----------------------------------------------Interview Questions----------------------------------------------*/

        [HttpPost]
        [Route("api/interview_question")]
        public string AddInterviewQuestion(List<tblinterview_questions> qlist)
        {

            foreach (tblinterview_questions q in qlist)
            {

                interviewrepo.Create(q);
            }
            return "Interview Questions Added Successfully";

        }

        [HttpGet]
        [Route("api/interview_question")]
        public List<Interview_Question_Model> GetAllInterviewQuestions()
        {
            List<Interview_Question_Model> lstcontentquestions = new List<Interview_Question_Model>();
            foreach (tblinterview_questions t in interviewrepo.GetAll())
            {
                tbltopic tp = topicrepo.GetById((int)t.tbltopic_contents.topic_id);
                Interview_Question_Model q = new Interview_Question_Model() { content_id = (int)t.content_id, question_id = t.question_id, question = t.question, answer = t.answer, content_name = t.tbltopic_contents.content_name };
                q.topic_id = tp.topic_id;
                q.topic_name = tp.topic_name;

                lstcontentquestions.Add(q);
            }
            return lstcontentquestions;
        }

        [HttpGet]
        [Route("api/contentwise_interview_question/{id}")]
        public List<Interview_Question_Model> GetAllInterviewQuestions(int id)
        {
            List<Interview_Question_Model> lstcontentquestions = new List<Interview_Question_Model>();
            foreach (tblinterview_questions t in interviewrepo.GetAll().Where(e => e.content_id.Equals(id)).ToList())
            {
                tbltopic tp = topicrepo.GetById((int)t.tbltopic_contents.topic_id);
                Interview_Question_Model q = new Interview_Question_Model() { content_id = (int)t.content_id, question_id = t.question_id, question = t.question, answer = t.answer, content_name = t.tbltopic_contents.content_name };
                q.topic_id = tp.topic_id;
                q.topic_name = tp.topic_name;

                lstcontentquestions.Add(q);
            }
            return lstcontentquestions;
        }



        //[HttpGet]
        //[Route("api/content_wise_questions/{id}")]
        //public List<tblcontent_questions> GetContentWiseQuestions(int id)
        //{
        //    List<tblcontent_questions> lstcontentquestions = new List<tblcontent_questions>();
        //    foreach (tblcontent_questions t in questionrepo.GetAll().Where(e=>e.content_id.Equals(id)).ToList())
        //    {

        //        tblcontent_questions q = new tblcontent_questions() { content_id = t.content_id, correct_option_number = t.correct_option_number, level_id = t.level_id, option4 = t.option4, option3 = t.option3, option2 = t.option2, option1 = t.option1, question = t.question, question_id = t.question_id };
        //        lstcontentquestions.Add(q);
        //    }
        //    return lstcontentquestions;
        //}


        //-------------------------Exam Level APIS------------------------------//

        [HttpPost]
        [Route("api/exam_level")]
        public string AddLevel(tbllevel l)
        {

            levelrepo.Create(l);
            return "Exam Level Added Successfully";
        }

        [HttpGet]
        [Route("api/exam_level")]
        public List<tbllevel> GetAllLevels()
        {
            List<tbllevel> lstlevels = new List<tbllevel>();
            foreach (tbllevel t in levelrepo.GetAll())
            {
                tbllevel l = new tbllevel() { level_id = t.level_id, question_level = t.question_level };
                lstlevels.Add(l);
            }
            return lstlevels;
        }
        [HttpGet]
        [Route("api/exam_level/{id}")]
        public tbllevel GetLevelById(int id)
        {
            tbllevel t = levelrepo.GetById(id);
            tbllevel l = new tbllevel() { level_id = t.level_id, question_level = t.question_level };


            return l;
        }



        //-------------------------Exam Level APIS------------------------------//

        [HttpPost]
        [Route("api/role")]
        public string AddRole(tblrole l)
        {
            l.flag = 0;
            rolerepo.Create(l);
            return "Role Added Successfully";
        }

        [HttpGet]
        [Route("api/role")]
        public List<tblrole> GetAllRoles()
        {
            List<tblrole> lstroles = new List<tblrole>();
            foreach (tblrole t in rolerepo.GetAll())
            {
                tblrole r = new tblrole() { role_id = t.role_id, role_name = t.role_name };
                lstroles.Add(r);
            }
            return lstroles;
        }
        [HttpGet]
        [Route("api/role/{id}")]
        public tblrole GetRoleById(int id)
        {
            tblrole t = rolerepo.GetById(id);
            tblrole r = new tblrole() { role_id = t.role_id, role_name = t.role_name };

            return r;
        }
    }
}
