// <copyright file="ListenerODataContext.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

using Serilog;

namespace AMSLLC.Listener.ODataService
{
    using System;
    using System.Data.Entity;
    using System.Linq.Expressions;
    using System.Reflection;
    using Persistence.Listener;

    public class ListenerODataContext : DbContext
    {
        public ListenerODataContext()
            : this("name=ListenerContext")
        {
        }

        public ListenerODataContext(string connectionString)
            : base(connectionString)
        {
            this.Database.Log = (s) => Log.Debug(s);
            Database.SetInitializer<ListenerODataContext>(null);
        }

        [CLSCompliant(false)]
        public DbSet<TransactionRegistryEntity> TransactionRegistry { get; set; }

        [CLSCompliant(false)]
        public DbSet<TransactionMessageDatumEntity> TransactionMessageData { get; set; }

        [CLSCompliant(false)]
        public DbSet<TransactionRegistryViewEntity> TransactionRegistryDetails { get; set; }

        [CLSCompliant(false)]
        public DbSet<EndpointEntity> Endpoint { get; set; }

        [CLSCompliant(false)]
        public DbSet<ProtocolTypeEntity> ProtocolType { get; set; }

        [CLSCompliant(false)]
        public DbSet<EndpointTriggerTypeEntity> EndpointTriggerType { get; set; }

        [CLSCompliant(false)]
        public DbSet<ValueMapEntity> ValueMap { get; set; }

        [CLSCompliant(false)]
        public DbSet<ValueMapEntryEntity> ValueMapEntry { get; set; }

        [CLSCompliant(false)]
        public DbSet<FieldConfigurationEntity> FieldConfiguration { get; set; }

        [CLSCompliant(false)]
        public DbSet<FieldConfigurationEntryEntity> FieldConfigurationEntry { get; set; }

        [CLSCompliant(false)]
        public DbSet<EntityCategoryOperationEntity> EntityCategoryOperation { get; set; }

        [CLSCompliant(false)]
        public DbSet<EnabledOperationEntity> EnabledOperation { get; set; }

        [CLSCompliant(false)]
        public DbSet<EntityCategoryEntity> EntityCategory { get; set; }

        [CLSCompliant(false)]
        public DbSet<OperationEntity> Operation { get; set; }

        [CLSCompliant(false)]
        public DbSet<OperationEndpointEntity> OperationEndpoint { get; set; }


        /// <inheritdoc/>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.MapPetaPocoEntity<TransactionRegistryEntity, Guid>(modelBuilder, a => a.RecordKey);
            this.MapPetaPocoEntity<TransactionMessageDatumEntity, Guid>(modelBuilder, a => a.RecordKey);
            this.MapPetaPocoEntity<TransactionRegistryViewEntity, Guid>(modelBuilder, a => a.RecordKey);
            this.MapPetaPocoEntity<EndpointEntity, int>(modelBuilder, a => a.EndpointId);
            this.MapPetaPocoEntity<ProtocolTypeEntity, int>(modelBuilder, a => a.ProtocolTypeId);
            this.MapPetaPocoEntity<EndpointTriggerTypeEntity, int>(modelBuilder, a => a.EndpointTriggerTypeId);
            this.MapPetaPocoEntity<ValueMapEntity, int>(modelBuilder, a => a.ValueMapId);
            this.MapPetaPocoEntity<ValueMapEntryEntity, int>(modelBuilder, a => a.ValueMapEntryId);
            this.MapPetaPocoEntity<FieldConfigurationEntity, int>(modelBuilder, a => a.FieldConfigurationId);
            this.MapPetaPocoEntity<FieldConfigurationEntryEntity, int>(modelBuilder, a => a.FieldConfigurationEntryId);
            this.MapPetaPocoEntity<EntityCategoryOperationEntity, int>(modelBuilder, a => a.EntityCategoryOperationId);
            this.MapPetaPocoEntity<EntityCategoryEntity, int>(modelBuilder, a => a.EntityCategoryId);
            this.MapPetaPocoEntity<EnabledOperationEntity, int>(modelBuilder, a => a.EnabledOperationId);
            this.MapPetaPocoEntity<OperationEntity, int>(modelBuilder, a => a.OperationId);
            this.MapPetaPocoEntity<OperationEndpointEntity, int>(modelBuilder, a => a.OperationEndpointId);
            base.OnModelCreating(modelBuilder);
        }

        private System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<T> MapPetaPocoEntity<T, TKey>(
            DbModelBuilder modelBuilder,
            Expression<Func<T, TKey>> primaryKeySelector)
            where T : class
        {
            var tableNameAttribute = typeof(T).GetCustomAttribute<AsyncPoco.TableNameAttribute>();

            // var primaryKeyAttribute = typeof(T).GetCustomAttribute<AsyncPoco.PrimaryKeyAttribute>();
            // var keyPropertyName = GetPropertyName(primaryKeySelector);
            // if (string.Compare(primaryKeyAttribute.Value, keyPropertyName, StringComparison.InvariantCulture) != 0)
            // {
            //    throw new InvalidOperationException(
            //        $"Specified {keyPropertyName} as primary key, {primaryKeyAttribute.Value} expected");
            // }
            return modelBuilder.Entity<T>().HasKey(primaryKeySelector).ToTable(tableNameAttribute.Value);


            // tps.HasIdLink((ctxt) => ctxt.Url.CreateODataLink(new EntitySetPathSegment("Products"), new KeyValuePathSegment("id")), false);
        }

        private string GetPropertyName<T, TValue>(Expression<Func<T, TValue>> c)
        {
            Type paramType = c.Parameters[0].Type; // first parameter of expression
            var d = paramType.GetMember((c.Body as MemberExpression).Member.Name)[0];
            return d.Name;
        }
    }
}
