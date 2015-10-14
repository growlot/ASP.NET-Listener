using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSLLC.Listener.ODataService
{
    using System.Threading;
    using System.Web.Http.Filters;
    using System.Web.OData;
    using Serilog;

    public class CustomEnableQueryAttribute : EnableQueryAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            try
            {
                base.OnActionExecuted(actionExecutedContext);
            }
            catch (Exception exc)
            {
                Log.Error(exc, "EnableQuery error");
                throw;
            }
        }
    }
}
