// <copyright file="ListenerODataContext.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.DbContext
{
    using System;
    using System.Data.Entity;
    using System.Linq.Expressions;
    using System.Reflection;
    using Persistence.Listener;

    public class ListenerODataContext : DbContext
    {
        public ListenerODataContext()
            : base("name=ListenerContext")
        {
            Database.SetInitializer<ListenerODataContext>(null);
        }

        public ListenerODataContext(string connectionString)
            : base(connectionString)
        {
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




        /// <inheritdoc/>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.MapPetaPocoEntity<TransactionRegistryEntity, Guid>(modelBuilder, a => a.RecordKey);
            this.MapPetaPocoEntity<TransactionMessageDatumEntity, Guid>(modelBuilder, a => a.RecordKey);
            this.MapPetaPocoEntity<TransactionRegistryViewEntity, Guid>(modelBuilder, a => a.RecordKey);
            this.MapPetaPocoEntity<EndpointEntity, int>(modelBuilder, a => a.EndpointId);
            this.MapPetaPocoEntity<ProtocolTypeEntity, int>(modelBuilder, a => a.ProtocolTypeId);
            this.MapPetaPocoEntity<EndpointTriggerTypeEntity, int>(modelBuilder, a => a.EndpointTriggerTypeId);

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
