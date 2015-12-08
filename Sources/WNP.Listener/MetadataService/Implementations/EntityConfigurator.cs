// <copyright file="EntityConfigurator.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.MetadataService.Implementations
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using static Persistence.WNP.Metadata.DBMetadata;

    /// <summary>
    ///     The entity configurator.
    /// </summary>
    public class EntityConfigurator : IEntityConfigurator
    {
        /// <summary>
        ///     The configurations.
        /// </summary>
        private readonly Dictionary<string, EntityConfiguration> configurations =
            new Dictionary<string, EntityConfiguration>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="EntityConfigurator" /> class.
        /// </summary>
        [SuppressMessage(
            "Readability.Rules",
            "SA1118:ParameterMustNotSpanMultipleLines",
            Justification = "In the constructor the multiple lines span does not make code less readable, however all the fields in one line hurts readability.")]
        public EntityConfigurator()
        {
            var meterTestsConfiguration =
                new EntityConfiguration(MeterTestResults.FullTableName).OwnerSpecific()
                    .HasVirtualRelation(
                        RelationType.OneToMany,
                        "TestStep",
                        new[] { MeterTestResults.StepNo },
                        new[]
                            {
                                MeterTestResults.AccuracyStatus, MeterTestResults.Af, MeterTestResults.Al,
                                MeterTestResults.DesiredAccuracy, MeterTestResults.Element, MeterTestResults.Frequency,
                                MeterTestResults.HarmonicConfiguration, MeterTestResults.HarmonicRevision,
                                MeterTestResults.LowerLimit, MeterTestResults.Optics, MeterTestResults.PhaseAngle,
                                MeterTestResults.ReversePower, MeterTestResults.ServiceType, MeterTestResults.StepDuration,
                                MeterTestResults.StepNo, MeterTestResults.TestVolts, MeterTestResults.TestAmps,
                                MeterTestResults.UpperLimit, MeterTestResults.TestType, MeterTestResults.TestRevs,
                                MeterTestResults.StandardMode
                            })
                    .HasRequired(
                        EqpMeter.FullTableName,
                        false,
                        new ColumnMatch(EqpMeter.Owner, MeterTestResults.Owner),
                        new ColumnMatch(EqpMeter.EqpNo, MeterTestResults.EqpNo))
                    .HasMany(
                        Reading.FullTableName,
                        true,
                        new ColumnMatch(Reading.Owner, MeterTestResults.Owner),
                        new ColumnMatch(Reading.EqpNo, MeterTestResults.EqpNo),
                        new ColumnMatch(Reading.ReadDate, MeterTestResults.TestDateStart))
                    .HasMany(
                        Comment.FullTableName,
                        true,
                        new ColumnValueMatch(Comment.EqpType, "EM"),
                        new ColumnMatch(Comment.Owner, MeterTestResults.Owner),
                        new ColumnMatch(Comment.EqpNo, MeterTestResults.EqpNo),
                        new ColumnMatch(Comment.CreateDate, MeterTestResults.TestDateStart));

            var meterConfiguration =
                new EntityConfiguration(EqpMeter.FullTableName).OwnerSpecific()
                    .HasMany(
                        Reading.FullTableName,
                        true,
                        new ColumnMatch(Reading.Owner, EqpMeter.Owner),
                        new ColumnMatch(Reading.EqpNo, EqpMeter.EqpNo))
                    .HasMany(
                        Comment.FullTableName,
                        true,
                        new ColumnValueMatch(Comment.EqpType, "EM"),
                        new ColumnMatch(Comment.Owner, EqpMeter.Owner),
                        new ColumnMatch(Comment.EqpNo, EqpMeter.EqpNo));

            var meterReadingConfiguration = new EntityConfiguration(Reading.FullTableName).Contained(
                Reading.Owner,
                Reading.EqpNo);

            var commentsConfiguration = new EntityConfiguration(Comment.FullTableName).Contained(
                Comment.Owner,
                Comment.EqpType,
                Comment.EqpNo);

            this.configurations.Add(EqpMeter.FullTableName, meterConfiguration);
            this.configurations.Add(MeterTestResults.FullTableName, meterTestsConfiguration);
            this.configurations.Add(Reading.FullTableName, meterReadingConfiguration);
            this.configurations.Add(Comment.FullTableName, commentsConfiguration);

            var siteConfiguration =
                new EntityConfiguration(Site.FullTableName).OwnerSpecific()
                    .HasMany(
                        Circuit.FullTableName,
                        true,
                        new ColumnMatch(Circuit.Owner, Site.Owner),
                        new ColumnMatch(Circuit.Site, Site.Site));

            var circuitConfiguration = new EntityConfiguration(Circuit.FullTableName).Contained(
                Circuit.Owner,
                Circuit.Site);

            this.configurations.Add(Site.FullTableName, siteConfiguration);
            this.configurations.Add(Circuit.FullTableName, circuitConfiguration);

            var workstationConfiguration = new EntityConfiguration(Workstation.FullTableName)
                .OwnerSpecific()
                .HasMany(
                    TrackingOut.FullTableName,
                    true,
                    new ColumnMatch(Workstation.Owner, TrackingOut.Owner),
                    new ColumnMatch(Workstation.Workstation, TrackingOut.Workstation));

            var trackingOutConfiguration = new EntityConfiguration(TrackingOut.FullTableName)
                .Contained(TrackingOut.Owner, TrackingOut.Workstation);

            this.configurations.Add(Workstation.FullTableName, workstationConfiguration);
            this.configurations.Add(TrackingOut.FullTableName, trackingOutConfiguration);
        }

        /// <inheritdoc />
        public EntityConfiguration GetEntityConfiguration(string tableName)
        {
            if (this.configurations.ContainsKey(tableName))
            {
                return this.configurations[tableName];
            }

            return null;
        }
    }
}