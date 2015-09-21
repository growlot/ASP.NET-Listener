using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSLLC.Core
{
    public class UtcDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now()
        {
            return DateTime.UtcNow;
        }
    }
}
