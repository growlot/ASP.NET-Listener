
namespace AMSLLC.Listener.ODataService.Controllers
{
using System;
using System.Linq;
using System.Web.Http;
using System.Web.OData;
using AMSLLC.Listener.ODataService;
using AMSLLC.Listener.ODataService.DbContext;
using AMSLLC.Listener.Persistence.Listener;
using System.Web.OData.Query;
using Serilog;
[EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All, AllowedLogicalOperators = AllowedLogicalOperators.All)]
public partial class TransactionRegistryController : ODataController{
		private readonly ListenerODataContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRegistryController" /> class.
        /// </summary>
        /// <param name="dbctx">The db context.</param>
        public TransactionRegistryController(ListenerODataContext dbctx)
        {
            this._dbContext = dbctx;
        }

        public string CompanyCode => this.Request.Headers.GetValues("AMS-Company").FirstOrDefault();
        public string ApplicationKey => this.Request.Headers.GetValues("AMS-Application").FirstOrDefault();

        public IQueryable<TransactionRegistryEntity> Get()
        {
            try
            {
                return this._dbContext.Set<TransactionRegistryEntity>().AsQueryable();
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }

        public IHttpActionResult Get([FromODataUri] System.Guid key)
        {
            try
            {
                var result = this._dbContext.Set<TransactionRegistryEntity>().SingleOrDefault(s => s.RecordKey == key);
                return this.Ok(result);
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }
}

[EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All, AllowedLogicalOperators = AllowedLogicalOperators.All)]
public partial class TransactionMessageDataController : ODataController{
		private readonly ListenerODataContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionMessageDataController" /> class.
        /// </summary>
        /// <param name="dbctx">The db context.</param>
        public TransactionMessageDataController(ListenerODataContext dbctx)
        {
            this._dbContext = dbctx;
        }

        public string CompanyCode => this.Request.Headers.GetValues("AMS-Company").FirstOrDefault();
        public string ApplicationKey => this.Request.Headers.GetValues("AMS-Application").FirstOrDefault();

        public IQueryable<TransactionMessageDatumEntity> Get()
        {
            try
            {
                return this._dbContext.Set<TransactionMessageDatumEntity>().AsQueryable();
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }

        public IHttpActionResult Get([FromODataUri] System.Guid key)
        {
            try
            {
                var result = this._dbContext.Set<TransactionMessageDatumEntity>().SingleOrDefault(s => s.RecordKey == key);
                return this.Ok(result);
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }
}

[EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All, AllowedLogicalOperators = AllowedLogicalOperators.All)]
public partial class TransactionRegistryDetailsController : ODataController{
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
                return this._dbContext.Set<TransactionRegistryViewEntity>().AsQueryable();
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }

        public IHttpActionResult Get([FromODataUri] System.Guid key)
        {
            try
            {
                var result = this._dbContext.Set<TransactionRegistryViewEntity>().SingleOrDefault(s => s.RecordKey == key);
                return this.Ok(result);
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }
}

[EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All, AllowedLogicalOperators = AllowedLogicalOperators.All)]
public partial class EndpointController : ODataController{
		private readonly ListenerODataContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="EndpointController" /> class.
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
                return this._dbContext.Set<EndpointEntity>().AsQueryable();
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }

        public IHttpActionResult Get([FromODataUri] System.Int32 key)
        {
            try
            {
                var result = this._dbContext.Set<EndpointEntity>().SingleOrDefault(s => s.EndpointId == key);
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


