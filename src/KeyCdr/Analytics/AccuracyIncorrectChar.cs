using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.Analytics
{
    public class AccuracyIncorrectChar
    {
        public AccuracyIncorrectChar()
        { }

        public char Expected { get; set; }
        public char Found { get; set; }
        public int Index { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
                return false;

            return this.Equals((AccuracyIncorrectChar)obj);
        }

        public bool Equals(AccuracyIncorrectChar obj)
        {
            return (this.Expected == obj.Expected
                && this.Found == obj.Found
                && this.Index == obj.Index);
        }

        public override int GetHashCode()
        {
            return new { this.Found, this.Expected, this.Index }.GetHashCode();
        }
    }
}
