using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyCdr.Models
{
    public class BaseModel
    {
        public decimal ToPrecision(decimal input)
        {
            return Math.Round(input, Constants.PRECISION_FOR_DECIMALS);
        }

        public double ToPrecision(double input)
        {
            return Math.Round(input, Constants.PRECISION_FOR_DECIMALS);
        }
    }
}
