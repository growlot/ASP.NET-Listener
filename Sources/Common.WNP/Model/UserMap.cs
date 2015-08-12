//-----------------------------------------------------------------------
// <copyright file="UserMap.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;
    using NHibernate.Type;

    /// <summary>
    /// Mapping for <see cref="User"/> class
    /// </summary>
    public class UserMap : ClassMapping<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserMap"/> class.
        /// </summary>
        public UserMap()
        {
            this.Table("wndba.TSECURITY_USERS");
            this.Lazy(false);

            // ID mapping
            this.Id(
                x => x.UserName,
                m =>
                {
                    m.Column("USERNAME");
                });

            // many to one mappings
            this.ManyToOne(
                x => x.Owner,
                map =>
                {
                    map.Cascade(Cascade.None);
                    map.Class(typeof(Owner));
                    map.Column("DEFAULT_OWNER");
                    map.Fetch(FetchKind.Join);
                });

            // property mappings
            this.Property(
                x => x.FirstName,
                m =>
                {
                    m.Column("FIRST_NAME");
                    m.Type(TypeFactory.GetAnsiStringType(30));
                    m.Length(30);
                });

            this.Property(
                x => x.LastName,
                m =>
                {
                    m.Column("LAST_NAME");
                    m.Type(TypeFactory.GetAnsiStringType(30));
                    m.Length(30);
                });
        }
    }
}
