using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr
{
    public class Constants
    {
        public const int PRECISION_FOR_DECIMALS = 2;

        public class StringSplits
        {
            public static char[] SEPARATOR_WORD = new char[] { ' ' };
            public static char[] SEPARATOR_SENTENACE = new char[] { '.', ';' };
        }

        public class Wikipedia
        {
            public const string REGEX_MOBILE_URL = "^https?://en.m";

            public class ArticleTitle
            {
                public const string TITLE_DESKTOP = "div#content h1#firstHeading";
                public const string TITLE_MOBILE = "main#content div.page-heading h1";
            }

            public class Paragraphs
            {
                public const string PARAGRAPHS = "div#mw-content-text p";
            }
        }
    }
}
