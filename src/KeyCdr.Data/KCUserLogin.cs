//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KeyCdr.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class KCUserLogin
    {
        public System.Guid KCUserLoginId { get; set; }
        public System.Guid UserId { get; set; }
        public System.DateTime Created { get; set; }
        public string UserAgent { get; set; }
        public string IpAddress { get; set; }
    
        public virtual KCUser KCUser { get; set; }
    }
}
