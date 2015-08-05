//-----------------------------------------------------------------------
// <copyright file="FileQuoteMultilineMap.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;
    using NHibernate.Type;

    /// <summary>
    /// Mapping for <see cref="FileQuoteMultiline"/> class
    /// </summary>
    public class FileQuoteMultilineMap : ClassMapping<FileQuoteMultiline>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileQuoteMultilineMap"/> class.
        /// </summary>
        public FileQuoteMultilineMap()
        {
            this.Table("FileQuoteMultiline");
            this.Lazy(true);

            this.Id(
                x => x.Id,
                m =>
                {
                    m.Column("FileQuoteMultilineId");
                });
            this.Property(
                x => x.Description,
                x => x.Length(50));
        }
    }
}