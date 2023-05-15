using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamPortalApplicationUsingApis.Areas.Master.Models
{
    public class AllExamModel
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
    }
}