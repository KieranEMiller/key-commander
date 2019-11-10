using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyCdr.UI.Web
{
    public class JWTToken
    {
        public string UserName { get; set; }
        public string UserId { get; set; }

        public bool IsValid { get; set; }

        public string JWTValue { get; set; }
    }
}