using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOIAcountingVersion2.ModelClasses
{
    public class TimePeriod
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public TimePeriod(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
    }
}
