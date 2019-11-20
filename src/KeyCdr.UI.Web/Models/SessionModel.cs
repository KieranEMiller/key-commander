using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyCdr.UI.Web.Models
{
    public class SessionModel
    {
        public Guid SessionId { get; set; }
        public Guid UserId { get; set; }
    }
}