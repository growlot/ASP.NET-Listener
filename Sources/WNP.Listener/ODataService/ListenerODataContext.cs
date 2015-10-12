namespace AMSLLC.Listener.ODataService
{
    using System;
    using System.Data.Entity;
    using System.Linq.Expressions;
    using System.Reflection;
    using Persistence.Listener;

    public class ListenerODataContext : DbContext
    {
        public ListenerODataContext() : base("name=ListenerContext")
        {
            Database.SetInitializer<ListenerODataContext>(null);
        }

        public DbSet<TransactionRegistryEntity> TransactionRegistry { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            MapPetaPocoEntity<TransactionRegistryEntity, int>(modelBuilder, a => a.TransactionId);

            base.OnModelCreating(modelBuilder);
        }

        private void MapPetaPocoEntity<T, TKey>(DbModelBuilder modelBuilder,
            Expression<Func<T, TKey>> primaryKeySelector) where T : class
        {
            var tableNameAttribute = typeof(T).GetCustomAttribute<AsyncPoco.TableNameAttribute>();
            var primaryKeyAttribute = typeof(T).GetCustomAttribute<AsyncPoco.PrimaryKeyAttribute>();
            var keyPropertyName = GetPropertyName(primaryKeySelector);
            if (string.Compare(primaryKeyAttribute.Value, keyPropertyName, StringComparison.InvariantCulture) != 0)
            {
                throw new InvalidOperationException(
                    $"Specified {keyPropertyName} as primary key, {primaryKeyAttribute.Value} expected");
            }
            modelBuilder.Entity<T>().HasKey(primaryKeySelector).ToTable(tableNameAttribute.Value);

            //tps.HasIdLink((ctxt) => ctxt.Url.CreateODataLink(new EntitySetPathSegment("Products"), new KeyValuePathSegment("id")), false);
        }

        private string GetPropertyName<T, TValue>(Expression<Func<T, TValue>> c)
        {
            Type paramType = c.Parameters[0].Type; // first parameter of expression
            var d = paramType.GetMember((c.Body as MemberExpression).Member.Name)[0];
            return d.Name;
        }
    }
}
