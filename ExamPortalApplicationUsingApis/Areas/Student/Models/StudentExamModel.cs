using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamPortalApplicationUsingApis.Areas.Student.Models
{
    public class StudentExamModel
    {

        public int student_id { get; set; }
        public string student_name { get; set; }
        public int exam_id { get; set; }
        public DateTime exam_date { get; set; }
        public int topic_id { get; set; }
        public string topic_name { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public int total_attempted_questions { get; set; }
        public int total_correct_questions { get; set; }
        public string status { get; set; }

        public List<StudentExam_AnswerModel> studentanswers { get; set; }
    }

    public class StudentExam_AnswerModel
    {
        public int answer_id { get; set; }
        public int exam_id { get; set; }
        public int question_id { get; set; }
        public string question { get; set; }
        public int correct_option_number { get; set; }
        public int student_submitted_option_number { get; set; }
        public string  option1 { get; set; }
        public string option2 { get; set; }
        public string option3 { get; set; }
        public string  option4 { get; set; }
        public string status { get; set; }
    }
}