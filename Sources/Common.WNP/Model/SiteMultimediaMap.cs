//-----------------------------------------------------------------------
// <copyright file="SiteMultimediaMap.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using NHibernate;
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;
    using NHibernate.Type;

    /// <summary>
    /// Mapping for <see cref="SiteMultimedia"/> class
    /// </summary>
    public class SiteMultimediaMap : ClassMapping<SiteMultimedia>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteMultimediaMap"/> class.
        /// </summary>
        public SiteMultimediaMap()
        {
            this.Table("wndba.TSITE_MULTIMEDIA");
            this.Lazy(false);

            // ID mapping
            this.Id(
                x => x.Id,
                m =>
                {
                    m.Column("ID");
                    m.Generator(Generators.Native);
                });

            // mappings for BaseMultimedia
            this.ManyToOne(
                x => x.Owner,
                map =>
                {
                    map.Cascade(Cascade.None);
                    map.Class(typeof(Owner));
                    map.Column("OWNER");
                    map.Fetch(FetchKind.Join);
                });

            this.Property(x => x.FileIndex, m => m.Column("FILE_INDEX"));
            this.Property(
                x => x.FileType,
                m =>
                {
                    m.Column("FILE_TYPE");
                    m.Type(TypeFactory.GetAnsiStringType(20));
                    m.Length(20);
                });
            this.Property(
                x => x.FileDescription,
                m =>
                {
                    m.Column("FILE_DESC");
                    m.Type(TypeFactory.GetAnsiStringType(250));
                    m.Length(250);
                });
            this.Property(
                x => x.FileContent,
                m =>
                {
                    m.Column("FILE_CONTENTS");
                    m.Type(NHibernateUtil.BinaryBlob);
                    m.Length(int.MaxValue);
                });
            this.Property(
                x => x.CreateDate,
                m =>
                {
                    m.Column("CREATE_DATE");
                    m.Type(new TimestampType());
                });
            this.Property(
                x => x.CreateUser,
                m =>
                {
                    m.Column("CREATE_BY");
                    m.Type(TypeFactory.GetAnsiStringType(32));
                    m.Length(32);
                });

            // mappings for SiteMultimedia specific fields
            this.ManyToOne(
                x => x.Site,
                map =>
                {
                    map.Cascade(Cascade.None);
                    map.Class(typeof(Site));
                    map.Column("SITE");
                    map.Fetch(FetchKind.Join);
                });
        }
    }
}