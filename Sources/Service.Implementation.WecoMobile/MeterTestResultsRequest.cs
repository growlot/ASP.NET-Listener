//-----------------------------------------------------------------------
// <copyright file="MeterTestResultsRequest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.WecoMobile
{
    using AMSLLC.Listener.Common.WNP.Model;

    /// <summary>
    /// Meter test results message for web service
    /// </summary>
    public class MeterTestResultsRequest
    {
        /// <summary>
        /// Gets or sets the meter test results.
        /// </summary>
        /// <value>
        /// The meter test results.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Needed for WCF implementation.")]
        public MeterTestResultRequest[] MeterTestResults { get; set; }
    }
}
