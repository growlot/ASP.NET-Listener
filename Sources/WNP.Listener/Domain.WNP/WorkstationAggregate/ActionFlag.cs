// <copyright file="ActionFlag.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.WorkstationAggregate
{
    using System;
    using System.Linq;

    /// <summary>
    /// Action flag defines the possible actions for a field.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flag", Justification = "Reviewed")]
    public class ActionFlag : ValueObject<ActionFlag>
    {
        private readonly string flagName;
        private readonly string[] validFlags = { "D", "E", "R", "C" };

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionFlag" /> class.
        /// </summary>
        /// <param name="flagName">Name of the action flag.Possible values are:
        ///  'D' = Disabled
        ///  'E' = Enabled
        ///  'R' = Required
        ///  'C' = Clear
        ///  </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "flag", Justification ="Reviewed")]
        public ActionFlag(string flagName)
        {
            if (string.IsNullOrWhiteSpace(flagName))
            {
                this.flagName = "D";
            }

            if (!this.validFlags.Contains(flagName))
            {
                throw new ArgumentOutOfRangeException(nameof(flagName), "Invalid action flag value provided. It must be 'D', 'E', 'R' or 'C'.");
            }

            this.flagName = flagName;
        }

        /// <summary>
        /// Gets the flag for the action.
        /// </summary>
        /// <value>
        /// The name of action flag.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flag", Justification = "Reviewed")]
        public string FlagName
        {
            get
            {
                return this.flagName;
            }
        }
    }
}
