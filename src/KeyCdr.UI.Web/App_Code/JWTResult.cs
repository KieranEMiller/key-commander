using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyCdr.UI.Web
{
    public class JWTResult
    {
        public bool isvalid { get; set; }
        public string username { get; set; }
        public string jwt { get; set; }
    }
}