//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class vw_student_details
    {
        public int student_id { get; set; }
        public string student_name { get; set; }
        public string email_address { get; set; }
        public string mobile_number { get; set; }
        public string student_code { get; set; }
        public string password { get; set; }
        public string photo { get; set; }
        public string address { get; set; }
        public int exam_id { get; set; }
        public Nullable<System.DateTime> exam_date { get; set; }
    }
}
