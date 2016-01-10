// <copyright file="ListenerODataContext.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq.Expressions;
    using System.Reflection;
    using Persistence.Listener;
    using Serilog;

    /// <summary>
    /// Entity Framework Listener database context.
    /// </summary>
    public class ListenerODataContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerODataContext"/> class.
        /// </summary>
        public ListenerODataContext()
            : this("name=ListenerContext")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerODataContext"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public ListenerODataContext(string connectionString)
            : base(connectionString)
        {
            this.Database.Log = (s) => Log.Debug(s);
            Database.SetInitializer<ListenerODataContext>(null);
        }

        /// <summary>
        /// Gets or sets the transaction registry.
        /// </summary>
        /// <value>
        /// The transaction registry.
        /// </value>
        [CLSCompliant(false)]
        public DbSet<TransactionRegistryEntity> TransactionRegistry { get; set; }

        /// <summary>
        /// Gets or sets the transaction message data.
        /// </summary>
        /// <value>
        /// The transaction message data.
        /// </value>
        [CLSCompliant(false)]
        public DbSet<TransactionMessageDatumEntity> TransactionMessageData { get; set; }

        /// <summary>
        /// Gets or sets the transaction registry details.
        /// </summary>
        /// <value>
        /// The transaction registry details.
        /// </value>
        [CLSCompliant(false)]
        public DbSet<TransactionRegistryViewEntity> TransactionRegistryDetails { get; set; }

        /// <summary>
        /// Gets or sets the endpoint.
        /// </summary>
        /// <value>
        /// The endpoint.
        /// </value>
        [CLSCompliant(false)]
        public DbSet<EndpointEntity> Endpoint { get; set; }

        /// <summary>
        /// Gets or sets the type of the protocol.
        /// </summary>
        /// <value>
        /// The type of the protocol.
        /// </value>
        [CLSCompliant(false)]
        public DbSet<ProtocolTypeEntity> ProtocolType { get; set; }

        /// <summary>
        /// Gets or sets the type of the endpoint trigger.
        /// </summary>
        /// <value>
        /// The type of the endpoint trigger.
        /// </value>
        [CLSCompliant(false)]
        public DbSet<EndpointTriggerTypeEntity> EndpointTriggerType { get; set; }

        /// <summary>
        /// Gets or sets the value map.
        /// </summary>
        /// <value>
        /// The value map.
        /// </value>
        [CLSCompliant(false)]
        public DbSet<ValueMapEntity> ValueMap { get; set; }

        /// <summary>
        /// Gets or sets the value map entry.
        /// </summary>
        /// <value>
        /// The value map entry.
        /// </value>
        [CLSCompliant(false)]
        public DbSet<ValueMapEntryEntity> ValueMapEntry { get; set; }

        /// <summary>
        /// Gets or sets the field configuration.
        /// </summary>
        /// <value>
        /// The field configuration.
        /// </value>
        [CLSCompliant(false)]
        public DbSet<FieldConfigurationEntity> FieldConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the field configuration entry.
        /// </summary>
        /// <value>
        /// The field configuration entry.
        /// </value>
        [CLSCompliant(false)]
        public DbSet<FieldConfigurationEntryEntity> FieldConfigurationEntry { get; set; }

        /// <summary>
        /// Gets or sets the entity category operation.
        /// </summary>
        /// <value>
        /// The entity category operation.
        /// </value>
        [CLSCompliant(false)]
        public DbSet<EntityCategoryOperationEntity> EntityCategoryOperation { get; set; }

        /// <summary>
        /// Gets or sets the enabled operation.
        /// </summary>
        /// <value>
        /// The enabled operation.
        /// </value>
        [CLSCompliant(false)]
        public DbSet<EnabledOperationEntity> EnabledOperation { get; set; }

        /// <summary>
        /// Gets or sets the entity category.
        /// </summary>
        /// <value>
        /// The entity category.
        /// </value>
        [CLSCompliant(false)]
        public DbSet<EntityCategoryEntity> EntityCategory { get; set; }

        /// <summary>
        /// Gets or sets the operation.
        /// </summary>
        /// <value>
        /// The operation.
        /// </value>
        [CLSCompliant(false)]
        public DbSet<OperationEntity> Operation { get; set; }

        /// <summary>
        /// Gets or sets the operation endpoint.
        /// </summary>
        /// <value>
        /// The operation endpoint.
        /// </value>
        [CLSCompliant(false)]
        public DbSet<OperationEndpointEntity> OperationEndpoint { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            MapPetaPocoEntity<TransactionRegistryEntity, Guid>(modelBuilder, a => a.RecordKey);
            MapPetaPocoEntity<TransactionMessageDatumEntity, Guid>(modelBuilder, a => a.RecordKey);
            MapPetaPocoEntity<TransactionRegistryViewEntity, Guid>(modelBuilder, a => a.RecordKey);
            MapPetaPocoEntity<EndpointEntity, int>(modelBuilder, a => a.EndpointId);
            MapPetaPocoEntity<ProtocolTypeEntity, int>(modelBuilder, a => a.ProtocolTypeId);
            MapPetaPocoEntity<EndpointTriggerTypeEntity, int>(modelBuilder, a => a.EndpointTriggerTypeId);
            MapPetaPocoEntity<ValueMapEntity, int>(modelBuilder, a => a.ValueMapId);
            MapPetaPocoEntity<ValueMapEntryEntity, int>(modelBuilder, a => a.ValueMapEntryId);
            MapPetaPocoEntity<FieldConfigurationEntity, int>(modelBuilder, a => a.FieldConfigurationId);
            MapPetaPocoEntity<FieldConfigurationEntryEntity, int>(modelBuilder, a => a.FieldConfigurationEntryId);
            MapPetaPocoEntity<EntityCategoryOperationEntity, int>(modelBuilder, a => a.EntityCategoryOperationId);
            MapPetaPocoEntity<EntityCategoryEntity, int>(modelBuilder, a => a.EntityCategoryId);
            MapPetaPocoEntity<EnabledOperationEntity, int>(modelBuilder, a => a.EnabledOperationId);
            MapPetaPocoEntity<OperationEntity, int>(modelBuilder, a => a.OperationId);
            MapPetaPocoEntity<OperationEndpointEntity, int>(modelBuilder, a => a.OperationEndpointId);
            base.OnModelCreating(modelBuilder);
        }

        private static EntityTypeConfiguration<T> MapPetaPocoEntity<T, TKey>(
            DbModelBuilder modelBuilder,
            Expression<Func<T, TKey>> primaryKeySelector)
            where T : class
        {
            var tableNameAttribute = typeof(T).GetCustomAttribute<Persistence.Poco.TableNameAttribute>();

            return modelBuilder.Entity<T>().HasKey(primaryKeySelector).ToTable(tableNameAttribute.Value);
        }

        // private string GetPropertyName<T, TValue>(Expression<Func<T, TValue>> c)
        // {
        //     Type paramType = c.Parameters[0].Type; // first parameter of expression
        //     var d = paramType.GetMember((c.Body as MemberExpression).Member.Name)[0];
        //     return d.Name;
        // }
    }
}
