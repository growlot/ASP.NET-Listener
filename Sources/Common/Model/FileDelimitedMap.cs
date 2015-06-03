//-----------------------------------------------------------------------
// <copyright file="FileDelimitedMap.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;
    using NHibernate.Type;

    /// <summary>
    /// Mapping for <see cref="FileDelimited"/> class
    /// </summary>
    public class FileDelimitedMap : ClassMapping<FileDelimited>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileDelimitedMap"/> class.
        /// </summary>
        public FileDelimitedMap()
        {
            this.Table("File");
            this.Lazy(true);

            // Common fields for all flat file formats
            this.Id(
                x => x.Id,
                m =>
                {
                    m.Column("FileId");
                    m.Generator(Generators.Native);
                });
            this.Property(
                x => x.Name,
                x => x.Length(50));
            this.Property(x => x.System);

            this.ManyToOne(
                x => x.ExternalSystem,
                map =>
                {
                    map.Cascade(Cascade.None);
                    map.Class(typeof(ExternalSystem));
                    map.Column("ExternalSystemId");
                    map.Fetch(FetchKind.Join);
                });

            // Fieds specific to delimited file format
            this.Property(
                x => x.Delimiter,
                x => x.Length(2));

            this.Bag(
                x => x.Fields,
                m =>
                {
                    m.Cascade(Cascade.DeleteOrphans);
                    m.Fetch(CollectionFetchMode.Join);
                    m.Key(k => k.Column("FileId"));
                    m.Table("FileField");
                },
                map =>
                {
                    map.OneToMany(p => p.Class(typeof(FileFieldDelimited)));
                });
        }
    }
}