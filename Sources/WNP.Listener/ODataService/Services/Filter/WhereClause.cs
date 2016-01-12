// <copyright file="WhereClause.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Filter
{
    using System;

    /// <summary>
    /// Container object for SQL WHERE clause and positional arguments bound to it.
    /// </summary>
    public class WhereClause
    {
        /// <summary>
        /// Gets or sets the SQL WHERE clause.
        /// </summary>
        public string Clause { get; set; }

        /// <summary>
        /// Gets or sets positional arguments for the SQL WHERE clause.
        /// </summary>
        public object[] PositionalParameters { get; set; }

        /// <summary>
        /// Converts the UTC parameters to local time.
        /// </summary>
        public void ConvertUtcParametersToLocalTime()
        {
            for (int i = 0; i < this.PositionalParameters.Length; i++)
            {
                DateTimeOffset? parameter = this.PositionalParameters[i] as DateTimeOffset?;

                if (parameter != null)
                {
                    DateTime localTime = new DateTime(parameter.Value.ToLocalTime().Ticks);
                    DateTime localTimeAsUtc = DateTime.SpecifyKind(localTime, DateTimeKind.Utc);
                    this.PositionalParameters[i] = (DateTimeOffset)localTimeAsUtc;
                }
            }
        }
    }
}