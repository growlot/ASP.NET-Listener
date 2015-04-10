//-----------------------------------------------------------------------
// <copyright file="SiteCommentMap.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;
    using NHibernate.Type;

    /// <summary>
    /// Mapping for <see cref="SiteComment"/> class
    /// </summary>
    public class SiteCommentMap : ClassMapping<SiteComment>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteCommentMap"/> class.
        /// </summary>
        public SiteCommentMap()
        {
            this.Table("wndba.TSITE_COMMENTS");
            this.Lazy(false);

            // ID mapping
            this.Id(
                x => x.Id,
                m =>
                {
                    m.Column("ID");
                    m.Generator(Generators.Native);
                });

            // mappings for BaseComment
            this.ManyToOne(
                x => x.Owner,
                map =>
                {
                    map.Cascade(Cascade.None);
                    map.Class(typeof(Owner));
                    map.Column("OWNER");
                    map.Fetch(FetchKind.Join);
                });

            this.Property(x => x.CommentIndex, m => m.Column("COMMENT_INDEX"));
            this.Property(
                x => x.CommentText,
                m =>
                {
                    m.Column("COMMENTS");
                    m.Type(TypeFactory.GetAnsiStringType(250));
                    m.Length(250);
                });
            this.Property(x => x.CommentType, m => m.Column("COMMENT_TYPE"));
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

            // mappings for SiteComment specific fields
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