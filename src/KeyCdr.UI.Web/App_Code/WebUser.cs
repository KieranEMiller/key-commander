using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyCdr.UI.Web
{
    public class WebUser 
    {
        public string username { get; set; }
        public string password { get; set; }

        //used in registration is a username is already in use
        public bool IsInUse { get; set; }
    }
}