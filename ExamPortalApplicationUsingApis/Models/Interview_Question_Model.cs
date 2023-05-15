using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamPortalApplicationUsingApis.Models
{
    public class Interview_Question_Model
    {
        public int question_id { get; set; }
        public string question { get; set; }
        public int content_id { get; set; }
        public int topic_id { get; set; }
        public string content_name { get; set; }
        public string topic_name { get; set; }
        public string answer { get; set; }
    }
}