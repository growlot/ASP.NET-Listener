//-----------------------------------------------------------------------
// <copyright file="TransformerTestResult.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    /// <summary>
    /// Data model class extending test results with fields common for all transformers
    /// </summary>
    public class TransformerTestResult : EquipmentTestResult
    {
        /// <summary>
        /// Gets or sets the primary test reason.
        /// </summary>
        /// <value>
        /// The primary test reason.
        /// </value>
        public string PrimaryTestReason { get; set; }

        /// <summary>
        /// Gets or sets selected ratio.
        /// </summary>
        /// <value>
        /// The selected ratio.
        /// </value>
        public string SelectedRatio { get; set; }

        /// <summary>
        /// Gets or sets ratio correction factor.
        /// </summary>
        /// <value>
        /// The ratio correction factor.
        /// </value>
        public decimal RatioCorrection { get; set; }

        /// <summary>
        /// Gets or sets phase error.
        /// </summary>
        /// <value>
        /// The phase error.
        /// </value>
        public decimal PhaseError { get; set; }

        /// <summary>
        /// Gets or sets the accuracy class.
        /// </summary>
        /// <value>
        /// The accuracy class.
        /// </value>
        public decimal AccuracyClass { get; set; }

        /// <summary>
        /// Gets or sets the load label.
        /// </summary>
        /// <value>
        /// The load label.
        /// </value>
        public string LoadLabel { get; set; }
    }
}
