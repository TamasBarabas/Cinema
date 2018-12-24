using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Helpers
{
    public class RoundRate
    {
        public double Round(double rate)
        {
            double res = Math.Floor(rate);
            double dec = rate % 1.0;

            if (dec >= 0.75) res += 1;
            else if (dec >= 0.25) res += 0.5;
            return res;
        }
    }
}
