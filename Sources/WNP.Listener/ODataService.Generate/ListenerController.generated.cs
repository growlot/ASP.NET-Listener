// <copyright file="ODataListenerServiceConfigurator.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.OData;
    using System.Web.OData.Builder;
    using AMSLLC.Listener.ODataService;
    using AMSLLC.Listener.ODataService.DbContext;
    using AMSLLC.Listener.Persistence.Listener;
    using System.Web.OData.Query;
    using Serilog;
    using System.CodeDom.Compiler;

    [GeneratedCode("Listener Controller Generator Template", "1.0.0.0")]
    public abstract partial class BaseListenerODataController : ODataController
    {
        protected string CompanyCode => this.Request.Headers.GetValues("AMS-Company").FirstOrDefault();

        protected string ApplicationKey => this.Request.Headers.GetValues("AMS-Application").FirstOrDefault();
    }

    [GeneratedCode("Listener Controller Generator Template", "1.0.0.0")]
    [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All, AllowedLogicalOperators = AllowedLogicalOperators.All)]
    public partial class TransactionRegistryController : BaseListenerODataController
    {
        private readonly ListenerODataContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRegistryController" /> class.
        /// </summary>
        /// <param name="dbctx">The db context.</param>
        public TransactionRegistryController(ListenerODataContext dbctx)
        {
            this._dbContext = dbctx;
        }

        /// <summary>
        /// Get the IQueryable of the served entity.
        /// </summary>
        /// <returns>System.Linq.IQueryable&lt;AMSLLC.Listener.Persistence.Listener.TransactionRegistryViewEntity&gt;.</returns>
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

        /// <summary>
        /// Get the single entity or null using primary key
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Web.Http.IHttpActionResult.</returns>
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

    [GeneratedCode("Listener Controller Generator Template", "1.0.0.0")]
    [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All, AllowedLogicalOperators = AllowedLogicalOperators.All)]
    public partial class TransactionMessageDataController : BaseListenerODataController
    {
        private readonly ListenerODataContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionMessageDataController" /> class.
        /// </summary>
        /// <param name="dbctx">The db context.</param>
        public TransactionMessageDataController(ListenerODataContext dbctx)
        {
            this._dbContext = dbctx;
        }

        /// <summary>
        /// Get the IQueryable of the served entity.
        /// </summary>
        /// <returns>System.Linq.IQueryable&lt;AMSLLC.Listener.Persistence.Listener.TransactionRegistryViewEntity&gt;.</returns>
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

        /// <summary>
        /// Get the single entity or null using primary key
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Web.Http.IHttpActionResult.</returns>
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

    [GeneratedCode("Listener Controller Generator Template", "1.0.0.0")]
    [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All, AllowedLogicalOperators = AllowedLogicalOperators.All)]
    public partial class TransactionRegistryDetailsController : BaseListenerODataController
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

        /// <summary>
        /// Get the IQueryable of the served entity.
        /// </summary>
        /// <returns>System.Linq.IQueryable&lt;AMSLLC.Listener.Persistence.Listener.TransactionRegistryViewEntity&gt;.</returns>
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

        /// <summary>
        /// Get the single entity or null using primary key
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Web.Http.IHttpActionResult.</returns>
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

    [GeneratedCode("Listener Controller Generator Template", "1.0.0.0")]
    [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All, AllowedLogicalOperators = AllowedLogicalOperators.All)]
    public partial class EndpointController : BaseListenerODataController
    {
        private readonly ListenerODataContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="EndpointController" /> class.
        /// </summary>
        /// <param name="dbctx">The db context.</param>
        public EndpointController(ListenerODataContext dbctx)
        {
            this._dbContext = dbctx;
        }

        /// <summary>
        /// Get the IQueryable of the served entity.
        /// </summary>
        /// <returns>System.Linq.IQueryable&lt;AMSLLC.Listener.Persistence.Listener.TransactionRegistryViewEntity&gt;.</returns>
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

        /// <summary>
        /// Get the single entity or null using primary key
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Web.Http.IHttpActionResult.</returns>
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

    [GeneratedCode("Listener Controller Generator Template", "1.0.0.0")]
    [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All, AllowedLogicalOperators = AllowedLogicalOperators.All)]
    public partial class ProtocolTypeController : BaseListenerODataController
    {
        private readonly ListenerODataContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProtocolTypeController" /> class.
        /// </summary>
        /// <param name="dbctx">The db context.</param>
        public ProtocolTypeController(ListenerODataContext dbctx)
        {
            this._dbContext = dbctx;
        }

        /// <summary>
        /// Get the IQueryable of the served entity.
        /// </summary>
        /// <returns>System.Linq.IQueryable&lt;AMSLLC.Listener.Persistence.Listener.TransactionRegistryViewEntity&gt;.</returns>
        public IQueryable<ProtocolTypeEntity> Get()
        {
            try
            {
                return this._dbContext.Set<ProtocolTypeEntity>().AsQueryable();
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }

        /// <summary>
        /// Get the single entity or null using primary key
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Web.Http.IHttpActionResult.</returns>
        public IHttpActionResult Get([FromODataUri] System.Int32 key)
        {
            try
            {
                var result = this._dbContext.Set<ProtocolTypeEntity>().SingleOrDefault(s => s.ProtocolTypeId == key);
                return this.Ok(result);
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }
    }

    [GeneratedCode("Listener Controller Generator Template", "1.0.0.0")]
    [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All, AllowedLogicalOperators = AllowedLogicalOperators.All)]
    public partial class EndpointTriggerTypeController : BaseListenerODataController
    {
        private readonly ListenerODataContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="EndpointTriggerTypeController" /> class.
        /// </summary>
        /// <param name="dbctx">The db context.</param>
        public EndpointTriggerTypeController(ListenerODataContext dbctx)
        {
            this._dbContext = dbctx;
        }

        /// <summary>
        /// Get the IQueryable of the served entity.
        /// </summary>
        /// <returns>System.Linq.IQueryable&lt;AMSLLC.Listener.Persistence.Listener.TransactionRegistryViewEntity&gt;.</returns>
        public IQueryable<EndpointTriggerTypeEntity> Get()
        {
            try
            {
                return this._dbContext.Set<EndpointTriggerTypeEntity>().AsQueryable();
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }

        /// <summary>
        /// Get the single entity or null using primary key
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Web.Http.IHttpActionResult.</returns>
        public IHttpActionResult Get([FromODataUri] System.Int32 key)
        {
            try
            {
                var result = this._dbContext.Set<EndpointTriggerTypeEntity>().SingleOrDefault(s => s.EndpointTriggerTypeId == key);
                return this.Ok(result);
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }
    }

    [GeneratedCode("Listener Controller Generator Template", "1.0.0.0")]
    public partial class ODataControllerConfigurator
    {

        /// <summary>
        /// Execute configurator to setup OData controllers
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Run(ODataModelBuilder builder)
        {

            this.SetupTransactionRegistryController(builder);
            this.SetupTransactionMessageDataController(builder);
            this.SetupTransactionRegistryDetailsController(builder);
            this.SetupEndpointController(builder);
            this.SetupProtocolTypeController(builder);
            this.SetupEndpointTriggerTypeController(builder);
        }

        /// <summary>
        /// Setup the controller.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="actionBuilder">The action builder.</param>
        protected virtual void SetupTransactionRegistryController(ODataModelBuilder builder, Action<ODataModelBuilder, EntityTypeConfiguration<TransactionRegistryEntity>> actionBuilder = null, string tableName = null)
        {
            this.PrepareODataController<TransactionRegistryEntity, System.Guid>(builder, a => a.RecordKey, actionBuilder, tableName);
        }

        /// <summary>
        /// Setup the controller.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="actionBuilder">The action builder.</param>
        protected virtual void SetupTransactionMessageDataController(ODataModelBuilder builder, Action<ODataModelBuilder, EntityTypeConfiguration<TransactionMessageDatumEntity>> actionBuilder = null, string tableName = null)
        {
            this.PrepareODataController<TransactionMessageDatumEntity, System.Guid>(builder, a => a.RecordKey, actionBuilder, tableName);
        }

        /// <summary>
        /// Setup the controller.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="actionBuilder">The action builder.</param>
        protected virtual void SetupTransactionRegistryDetailsController(ODataModelBuilder builder, Action<ODataModelBuilder, EntityTypeConfiguration<TransactionRegistryViewEntity>> actionBuilder = null, string tableName = null)
        {
            this.PrepareODataController<TransactionRegistryViewEntity, System.Guid>(builder, a => a.RecordKey, actionBuilder, tableName);
        }

        /// <summary>
        /// Setup the controller.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="actionBuilder">The action builder.</param>
        protected virtual void SetupEndpointController(ODataModelBuilder builder, Action<ODataModelBuilder, EntityTypeConfiguration<EndpointEntity>> actionBuilder = null, string tableName = null)
        {
            this.PrepareODataController<EndpointEntity, System.Int32>(builder, a => a.EndpointId, actionBuilder, tableName);
        }

        /// <summary>
        /// Setup the controller.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="actionBuilder">The action builder.</param>
        protected virtual void SetupProtocolTypeController(ODataModelBuilder builder, Action<ODataModelBuilder, EntityTypeConfiguration<ProtocolTypeEntity>> actionBuilder = null, string tableName = null)
        {
            this.PrepareODataController<ProtocolTypeEntity, System.Int32>(builder, a => a.ProtocolTypeId, actionBuilder, tableName);
        }

        /// <summary>
        /// Setup the controller.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="actionBuilder">The action builder.</param>
        protected virtual void SetupEndpointTriggerTypeController(ODataModelBuilder builder, Action<ODataModelBuilder, EntityTypeConfiguration<EndpointTriggerTypeEntity>> actionBuilder = null, string tableName = null)
        {
            this.PrepareODataController<EndpointTriggerTypeEntity, System.Int32>(builder, a => a.EndpointTriggerTypeId, actionBuilder, tableName);
        }


        private void PrepareODataController<TEntity, TKey>(
                ODataModelBuilder builder,
                Expression<Func<TEntity, TKey>> primaryKeySelector,
                Action<ODataModelBuilder, EntityTypeConfiguration<TEntity>> actionBuilder = null,
                string tableName = null) where TEntity : class
        {
            // separate OData endpoint for Listener API
            this.MapPetaPocoEntity(builder, primaryKeySelector, tableName);

            var entityType = builder.EntityType<TEntity>();

            actionBuilder?.Invoke(builder, entityType);
        }

        protected void MapPetaPocoEntity<T, TKey>(
            ODataModelBuilder modelBuilder,
            Expression<Func<T, TKey>> primaryKeySelector, string tableName = null)
            where T : class
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                var tableNameAttribute = typeof(T).GetCustomAttribute<AsyncPoco.TableNameAttribute>();
                modelBuilder.EntitySet<T>(tableNameAttribute.Value);
            }
            else
            {
                modelBuilder.EntitySet<T>(tableName);
            }
            modelBuilder.EntityType<T>().HasKey(primaryKeySelector);
        }

    }
}


