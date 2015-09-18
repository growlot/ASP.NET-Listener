using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WNP.Listener.Utilities
{
    /// <summary>
    /// Common utilities
    /// </summary>
    public static class CommonUtilities
    {
        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }


    }
}
