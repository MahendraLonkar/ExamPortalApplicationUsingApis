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
    
    public partial class tblemployee_details
    {
        public int employee_id { get; set; }
        public string employee_name { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string mobile_number { get; set; }
        public string email_address { get; set; }
        public Nullable<int> role_id { get; set; }
    
        public virtual tblrole tblrole { get; set; }
    }
}
