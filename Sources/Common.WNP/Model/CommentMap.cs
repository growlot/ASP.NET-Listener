//-----------------------------------------------------------------------
// <copyright file="CommentMap.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;
    using NHibernate.Type;

    /// <summary>
    /// Mapping for <see cref="Comment"/> class
    /// </summary>
    public class CommentMap : ClassMapping<Comment>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommentMap"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "No possiblity to simplify mapping.")]
        public CommentMap()
        {
            this.Table("wndba.TCOMMENT");
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

            // mappings for Comment specific fields
            this.Property(
                x => x.EquipmentNumber,
                m =>
                {
                    m.Column("EQP_NO");
                    m.Type(TypeFactory.GetAnsiStringType(20));
                    m.Length(20);
                });
            this.Property(
                x => x.EquipmentType,
                m =>
                {
                    m.Column("EQP_TYPE");
                    m.Type(TypeFactory.GetAnsiStringType(2));
                    m.Length(2);
                });
            this.Property(
                x => x.Source,
                m =>
                {
                    m.Column("COMMENT_SRC");
                    m.Type(TypeFactory.GetAnsiStringType(3));
                    m.Length(3);
                });
            this.Property(
                x => x.Trouble1, 
                m => 
                {
                    m.Column("TROUBLE1");
                    m.Type(TypeFactory.GetAnsiStringType(3));
                    m.Length(3);
                });
            this.Property(
                x => x.Trouble2,
                m =>
                {
                    m.Column("TROUBLE2");
                    m.Type(TypeFactory.GetAnsiStringType(3));
                    m.Length(3);
                });
            this.Property(
                x => x.Trouble3,
                m =>
                {
                    m.Column("TROUBLE3");
                    m.Type(TypeFactory.GetAnsiStringType(3));
                    m.Length(3);
                });
            this.Property(
                x => x.Trouble4,
                m =>
                {
                    m.Column("TROUBLE4");
                    m.Type(TypeFactory.GetAnsiStringType(3));
                    m.Length(3);
                });
            this.Property(
                x => x.Trouble5,
                m =>
                {
                    m.Column("TROUBLE5");
                    m.Type(TypeFactory.GetAnsiStringType(3));
                    m.Length(3);
                });            
        }
    }
}