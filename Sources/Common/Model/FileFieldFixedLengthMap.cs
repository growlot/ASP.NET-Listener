//-----------------------------------------------------------------------
// <copyright file="FileFieldFixedLengthMap.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;
    using NHibernate.Type;

    /// <summary>
    /// Mapping for <see cref="FileFieldFixedLength"/> class
    /// </summary>
    public class FileFieldFixedLengthMap : ClassMapping<FileFieldFixedLength>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileFieldFixedLengthMap"/> class.
        /// </summary>
        public FileFieldFixedLengthMap()
        {
            this.Table("FileField");
            this.Lazy(true);

            // Common fields for all flat file formats
            this.Id(
                x => x.Id,
                m =>
                {
                    m.Column("FileFieldId");
                    m.Generator(Generators.Native);
                });
            this.Property(
                x => x.Name,
                x => x.Length(100));
            this.Property(x => x.Index);
            this.Property(
                x => x.FieldType,
                x => x.Length(20));
            this.Property(
                x => x.NullValue,
                x => x.Length(50));
            this.Property(
                x => x.TrimChars,
                x => x.Length(10));
            this.Property(
                x => x.Description,
                x => x.Length(250));

            this.ManyToOne(
                x => x.Converter,
                map =>
                {
                    map.Cascade(Cascade.None);
                    map.Class(typeof(FileConverter));
                    map.Column("FileConverterId");
                    map.Fetch(FetchKind.Join);
                });

            this.ManyToOne(
                x => x.TrimMode,
                map =>
                {
                    map.Cascade(Cascade.None);
                    map.Class(typeof(FileTrimMode));
                    map.Column("FileTrimModeId");
                    map.Fetch(FetchKind.Join);
                });

            // Fieds specific to delimited file format
            this.Property(x => x.Length);
            this.Property(x => x.AlignChar);

            this.ManyToOne(
                x => x.File,
                map =>
                {
                    map.Cascade(Cascade.None);
                    map.Class(typeof(FileFixedLength));
                    map.Column("FileId");
                    map.Fetch(FetchKind.Join);
                });
            this.ManyToOne(
                x => x.AlignMode,
                map =>
                {
                    map.Cascade(Cascade.None);
                    map.Class(typeof(FileAlignMode));
                    map.Column("FileAlignModeId");
                    map.Fetch(FetchKind.Join);
                });
        }
    }
}