//-----------------------------------------------------------------------
// <copyright file="FileConverterMap.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;
    using NHibernate.Type;

    /// <summary>
    /// Mapping for <see cref="FileConverter"/> class
    /// </summary>
    public class FileConverterMap : ClassMapping<FileConverter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileConverterMap"/> class.
        /// </summary>
        public FileConverterMap()
        {
            this.Table("FileConverter");
            this.Lazy(true);

            this.Id(
                x => x.Id,
                m =>
                {
                    m.Column("FileConverterId");
                    m.Generator(Generators.Native);
                });
            this.Property(
                x => x.Argument1,
                x => x.Length(50));
            this.Property(
                x => x.Argument2,
                x => x.Length(50));
            this.Property(
                x => x.Argument3,
                x => x.Length(50));

            // many to one mappings
            this.ManyToOne(
                x => x.FileConverterKind,
                map =>
                {
                    map.Cascade(Cascade.None);
                    map.Class(typeof(FileConverterKind));
                    map.Column("FileConverterKindId");
                    map.Fetch(FetchKind.Join);
                });
        }
    }
}