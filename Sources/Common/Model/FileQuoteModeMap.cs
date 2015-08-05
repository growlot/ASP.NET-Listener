//-----------------------------------------------------------------------
// <copyright file="FileQuoteModeMap.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;
    using NHibernate.Type;

    /// <summary>
    /// Mapping for <see cref="FileQuoteMode"/> class
    /// </summary>
    public class FileQuoteModeMap : ClassMapping<FileQuoteMode>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileQuoteModeMap"/> class.
        /// </summary>
        public FileQuoteModeMap()
        {
            this.Table("FileQuoteMode");
            this.Lazy(true);

            this.Id(
                x => x.Id,
                m =>
                {
                    m.Column("FileQuoteModeId");
                });
            this.Property(
                x => x.Description,
                x => x.Length(50));
        }
    }
}