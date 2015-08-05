//-----------------------------------------------------------------------
// <copyright file="GlobalConstants.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common
{
    /// <summary>
    /// Class that contains global constants for all projects.
    /// </summary>
    public static class GlobalConstants
    {
        /// <summary>
        /// The previous successful transaction not found
        /// </summary>
        private const string PreviousSuccessfulTransactionNotFoundText = "There are no previous successful transactions";

        /// <summary>
        /// Gets the previous successful transaction not found marker text.
        /// </summary>
        /// <value>
        /// The previous successful transaction not found.
        /// </value>
        public static string PreviousSuccessfulTransactionNotFound
        {
            get
            {
                return PreviousSuccessfulTransactionNotFoundText;
            }
        }
    }
}
