// <copyright file="EntityConfigurator.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.MetadataService.Implementations
{
    using System.Collections.Generic;
    using static Persistence.WNP.Metadata.DBMetadata;

    /// <summary>
    /// The entity configurator.
    /// </summary>
    public class EntityConfigurator : IEntityConfigurator
    {
        /// <summary>
        /// The configurations.
        /// </summary>
        private Dictionary<string, EntityConfiguration> configurations = new Dictionary<string, EntityConfiguration>();

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityConfigurator"/> class.
        /// </summary>
        public EntityConfigurator()
        {
            var meterTestsConfiguration = new EntityConfiguration(MeterTestResults.FullTableName)
                .OwnerSpecific()
                    .HasKey(
                        MeterTestResults.Owner,
                        MeterTestResults.EqpNo,
                        MeterTestResults.TestDateStart,
                        MeterTestResults.StepNo)
                    .HasRequired(
                        EqpMeter.FullTableName,
                        false,
                        new ColumnMatch(MeterTestResults.Owner, EqpMeter.Owner),
                        new ColumnMatch(MeterTestResults.EqpNo, EqpMeter.EqpNo))
                    .HasMany(
                        Reading.FullTableName,
                        true,
                        new ColumnMatch(MeterTestResults.Owner, Reading.Owner),
                        new ColumnMatch(MeterTestResults.EqpNo, Reading.EqpNo),
                        new ColumnMatch(MeterTestResults.TestDateStart, Reading.ReadDate))
                    .HasMany(
                        Comment.FullTableName,
                        true,
                        new ColumnMatch(MeterTestResults.Owner, Comment.Owner),
                        new ColumnMatch(MeterTestResults.EqpNo, Comment.EqpNo),
                        new ColumnMatch(MeterTestResults.TestDateStart, Comment.CreateDate));

            var meterConfiguration = new EntityConfiguration(EqpMeter.FullTableName)
                .OwnerSpecific()
                .HasKey(
                    EqpMeter.Owner,
                    EqpMeter.EqpNo)
                .HasMany(
                    Reading.FullTableName,
                    true,
                    new ColumnMatch(EqpMeter.Owner, Reading.Owner),
                    new ColumnMatch(EqpMeter.EqpNo, Reading.EqpNo));

            var meterReadingConfiguration = new EntityConfiguration(Reading.FullTableName)
                    .Contained()
                    .HasKey(Reading.ReadIndex);

            var commentsConfiguration = new EntityConfiguration(Comment.FullTableName)
                    .Contained()
                    .HasKey(Comment.CommentIndex);

            this.configurations.Add(EqpMeter.FullTableName, meterConfiguration);
            this.configurations.Add(MeterTestResults.FullTableName, meterTestsConfiguration);
            this.configurations.Add(Reading.FullTableName, meterReadingConfiguration);
            this.configurations.Add(Comment.FullTableName, commentsConfiguration);

            var siteConfiguration = new EntityConfiguration(Site.FullTableName).OwnerSpecific()
                .HasKey(
                    Site.Owner,
                    Site.PremiseNo)
                .HasMany(
                    Circuit.FullTableName,
                    true,
                    new ColumnMatch(Site.Owner, Circuit.Owner),
                    new ColumnMatch(Site.Site, Circuit.Site));

            var circuitConfiguration = new EntityConfiguration(Circuit.FullTableName)
                .Contained()
                .HasKey(Circuit.MeterPoint);

            this.configurations.Add(Site.FullTableName, siteConfiguration);
            this.configurations.Add(Circuit.FullTableName, circuitConfiguration);
        }

        /// <inheritdoc/>
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