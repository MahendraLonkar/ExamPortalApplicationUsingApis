//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExamPortalApplicationUsingApis.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblinterview_questions
    {
        public int question_id { get; set; }
        public Nullable<int> content_id { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public Nullable<int> flag { get; set; }
    
        public virtual tbltopic_contents tbltopic_contents { get; set; }
    }
}
