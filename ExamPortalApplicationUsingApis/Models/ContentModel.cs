using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamPortalApplicationUsingApis.Models
{
    public class ContentModel
    {
        public int content_id { get; set; }
        public string content_name { get; set; }
        public int topic_id { get; set; }
        public string topic_name { get; set; }
        public string ppts { get; set; }
        public string description { get; set; }
        public string video_url { get; set; }
        
    }
}