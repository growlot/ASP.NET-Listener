//-----------------------------------------------------------------------
// <copyright file="TransactionLogMap.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using NHibernate;
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    /// <summary>
    /// Mapping for <see cref="TransactionLog"/> class
    /// </summary>
    public class TransactionLogMap : ClassMapping<TransactionLog>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionLogMap"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Can't simplify mapping."), 
         System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Can't simplify mapping.")]
        public TransactionLogMap()
        {
            this.Table("TransactionLog");
            this.Lazy(false);

            // ID mapping
            this.Id(
                x => x.Id,
                m =>
                {
                    m.Column("TransactionLogId");
                    m.Generator(Generators.Native);
                });

            // many to one mappings
            this.ManyToOne(
                x => x.Batch,
                map =>
                {
                    map.Cascade(Cascade.None);
                    map.Class(typeof(Batch));
                    map.Column("BatchId");
                    map.Fetch(FetchKind.Join);
                });

            this.ManyToOne(
                x => x.Device,
                map =>
                {
                    map.Cascade(Cascade.None);
                    map.Class(typeof(Device));
                    map.Column("DeviceId");
                    map.Fetch(FetchKind.Join);
                });

            this.ManyToOne(
                x => x.DeviceBatch,
                map =>
                {
                    map.Cascade(Cascade.None);
                    map.Class(typeof(DeviceBatch));
                    map.Column("DeviceBatchId");
                    map.Fetch(FetchKind.Join);
                });

            this.ManyToOne(
                x => x.DeviceTest,
                map =>
                {
                    map.Cascade(Cascade.None);
                    map.Class(typeof(DeviceTest));
                    map.Column("DeviceTestId");
                    map.Fetch(FetchKind.Join);
                });

            this.ManyToOne(
                x => x.TransactionStatus,
                map =>
                {
                    map.Cascade(Cascade.None);
                    map.Class(typeof(TransactionStatus));
                    map.Column("TransactionStatusId");
                    map.Fetch(FetchKind.Join);
                });

            this.ManyToOne(
                x => x.TransactionType,
                map =>
                {
                    map.Cascade(Cascade.None);
                    map.Class(typeof(TransactionType));
                    map.Column("TransactionTypeId");
                    map.Fetch(FetchKind.Join);
                });

            this.Bag(
                x => x.TransactionLogStates,
                m => 
                {
                    m.Cascade(Cascade.DeleteOrphans);
                    m.Fetch(CollectionFetchMode.Join);
                    m.Key(k => k.Column("TransactionLogId"));
                    m.Table("TransactionLogState");
                },
                map =>
                {
                    map.OneToMany(p => p.Class(typeof(TransactionLogState)));
                });

            this.Property(x => x.DataHash, x => x.Length(40));
            this.Property(x => x.DebugInfo, x => x.Type(NHibernateUtil.StringClob));
            this.Property(x => x.Message, x => x.Length(1000));
            this.Property(x => x.TransactionEnd);
            this.Property(x => x.TransactionStart);
        }
    }
}