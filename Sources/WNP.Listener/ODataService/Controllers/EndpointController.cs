using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSLLC.Listener.ODataService.Controllers
{
    using System.Web.Http;
    using System.Web.OData;
    using System.Web.OData.Query;
    using Persistence.Listener;
    using Serilog;

    [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All, AllowedLogicalOperators = AllowedLogicalOperators.All)]
    public class EndpointController : ODataController
    {
        private readonly ListenerODataContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionMessageDataController" /> class.
        /// </summary>
        /// <param name="dbctx">The db context.</param>
        public EndpointController(ListenerODataContext dbctx)
        {
            this._dbContext = dbctx;
        }

        public string CompanyCode => this.Request.Headers.GetValues("AMS-Company").FirstOrDefault();
        public string ApplicationKey => this.Request.Headers.GetValues("AMS-Application").FirstOrDefault();

        public IQueryable<EndpointEntity> Get()
        {
            try
            {
                return this._dbContext.Endpoint.AsQueryable();
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }

        public IHttpActionResult Get([FromODataUri] int key)
        {
            try
            {
                var result = this._dbContext.Endpoint.Where(s => s.EndpointId == key);
                return this.Ok(result);
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }
    }
}
