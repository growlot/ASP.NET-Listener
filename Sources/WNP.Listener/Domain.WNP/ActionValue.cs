// <copyright file="ActionValue.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Value object for action values.
    /// </summary>
    public class ActionValue : ValueObject<ActionValue>
    {
        private static List<string> supportedActions = new List<string>(new[] { "D", "E", "R", "C" });
        private readonly string valueCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionValue"/> class.
        /// </summary>
        /// <param name="actionValue">The value of action to take place.</param>
        public ActionValue(string actionValue)
        {
            if (string.IsNullOrWhiteSpace(actionValue))
            {
                actionValue = "D";
            }

            if (!supportedActions.Contains(actionValue))
            {
                throw new ArgumentOutOfRangeException(nameof(actionValue), "Action value is not recognized. Supported values are: {0}".FormatWith(string.Join(",", supportedActions.ToArray())));
            }

            this.valueCode = actionValue;
        }

        /// <summary>
        /// Gets the value for disabled action
        /// </summary>
        public static ActionValue Disabled
        {
            get { return new ActionValue("D"); }
        }

        /// <summary>
        /// Gets the value for enabled action
        /// </summary>
        public static ActionValue Enabled
        {
            get { return new ActionValue("E"); }
        }

        /// <summary>
        /// Gets the value for required action
        /// </summary>
        public static ActionValue Required
        {
            get { return new ActionValue("R"); }
        }

        /// <summary>
        /// Gets the value for clear action
        /// </summary>
        public static ActionValue Clear
        {
            get { return new ActionValue("C"); }
        }

        /// <summary>
        /// Gets the action value code.
        /// </summary>
        /// <value>
        /// The action value code.
        /// </value>
        public string Code
        {
            get
            {
                return this.valueCode;
            }
        }
    }
}