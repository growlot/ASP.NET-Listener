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
    public class TransactionRegistryDetailsController : ODataController
    {
        private readonly ListenerODataContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRegistryDetailsController" /> class.
        /// </summary>
        /// <param name="dbctx">The db context.</param>
        public TransactionRegistryDetailsController(ListenerODataContext dbctx)
        {
            this._dbContext = dbctx;
        }

        public string CompanyCode => this.Request.Headers.GetValues("AMS-Company").FirstOrDefault();
        public string ApplicationKey => this.Request.Headers.GetValues("AMS-Application").FirstOrDefault();

        public IQueryable<TransactionRegistryViewEntity> Get()
        {
            try
            {
                return this._dbContext.TransactionRegistryDetails.Where(s => s.CompanyCode == this.CompanyCode).AsQueryable();
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }

        public IHttpActionResult Get([FromODataUri] Guid key)
        {
            try
            {
                var result = this._dbContext.TransactionRegistryDetails.Where(s => s.RecordKey == key && s.CompanyCode == this.CompanyCode);
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
