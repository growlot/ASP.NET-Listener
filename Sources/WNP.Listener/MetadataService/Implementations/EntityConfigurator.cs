﻿// <copyright file="EntityConfigurator.cs" company="Advanced Metering Services LLC">
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
            var meterTestsConfiguration =
                new EntityConfiguration(MeterTestResults.FullTableName).OwnerSpecific()
                    .HasKey(
                        MeterTestResults.Owner,
                        MeterTestResults.EqpNo,
                        MeterTestResults.TestDateStart,
                        MeterTestResults.StepNo)
                    .HasRequired(
                        EqpMeter.FullTableName,
                        new ColumnMatch(MeterTestResults.Owner, EqpMeter.Owner),
                        new ColumnMatch(MeterTestResults.EqpNo, EqpMeter.EqpNo))
                    .HasMany(
                        Reading.FullTableName,
                        new ColumnMatch(MeterTestResults.Owner, Reading.Owner),
                        new ColumnMatch(MeterTestResults.EqpNo, Reading.EqpNo),
                        new ColumnMatch(MeterTestResults.TestDateStart, Reading.ReadDate))
                    .HasMany(
                        Comment.FullTableName,
                        new ColumnMatch(MeterTestResults.Owner, Comment.Owner),
                        new ColumnMatch(MeterTestResults.EqpNo, Comment.EqpNo),
                        new ColumnMatch(MeterTestResults.TestDateStart, Comment.CreateDate));

            this.configurations.Add(MeterTestResults.FullTableName, meterTestsConfiguration);
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