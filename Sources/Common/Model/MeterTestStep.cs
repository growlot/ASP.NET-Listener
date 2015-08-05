//-----------------------------------------------------------------------
// <copyright file="MeterTestStep.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    /// <summary>
    /// Data model class representing meter test step
    /// </summary>
    public class MeterTestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeterTestStep"/> class.
        /// </summary>
        public MeterTestStep()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeterTestStep"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public MeterTestStep(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the test step.
        /// </summary>
        /// <value>
        /// The test step.
        /// </value>
        public int TestStep { get; set; }

        /// <summary>
        /// Gets or sets the element.
        /// </summary>
        /// <value>
        /// The element.
        /// </value>
        public char Element { get; set; }

        /// <summary>
        /// Gets or sets the type of the test.
        /// </summary>
        /// <value>
        /// The type of the test.
        /// </value>
        public string TestType { get; set; }

        /// <summary>
        /// Gets or sets the test standard.
        /// </summary>
        /// <value>
        /// The test standard.
        /// </value>
        public string TestStandard { get; set; }

        /// <summary>
        /// Gets or sets the upper limit for test to pass.
        /// </summary>
        /// <value>
        /// The upper limit.
        /// </value>
        public decimal UpperLimit { get; set; }

        /// <summary>
        /// Gets or sets the lower limit for test to pass.
        /// </summary>
        /// <value>
        /// The lower limit.
        /// </value>
        public decimal LowerLimit { get; set; }

        /// <summary>
        /// Gets or sets as found.
        /// </summary>
        /// <value>
        /// As found.
        /// </value>
        public decimal AsFound { get; set; }

        /// <summary>
        /// Gets or sets as left.
        /// </summary>
        /// <value>
        /// As left.
        /// </value>
        public decimal AsLeft { get; set; }

        /// <summary>
        /// Gets or sets the test amps.
        /// </summary>
        /// <value>
        /// The test amps.
        /// </value>
        public decimal TestAmps { get; set; }

        /// <summary>
        /// Gets or sets the test volts.
        /// </summary>
        /// <value>
        /// The test volts.
        /// </value>
        public decimal TestVolts { get; set; }

        /// <summary>
        /// Gets or sets the station identifier.
        /// </summary>
        /// <value>
        /// The station identifier.
        /// </value>
        public string StationId { get; set; }

        /// <summary>
        /// Gets or sets the WECO serial number.
        /// </summary>
        /// <value>
        /// The WECO serial number.
        /// </value>
        public string WecoSerialNumber { get; set; }

        /// <summary>
        /// Gets or sets the reverse power.
        /// </summary>
        /// <value>
        /// The reverse power.
        /// </value>
        public char ReversePower { get; set; }

        /// <summary>
        /// Gets or sets the test revisions.
        /// </summary>
        /// <value>
        /// The test revisions.
        /// </value>
        public int TestRevisions { get; set; }

        /// <summary>
        /// Gets or sets the type of the service.
        /// </summary>
        /// <value>
        /// The type of the service.
        /// </value>
        public string ServiceType { get; set; }

        /// <summary>
        /// Gets or sets the standard mode.
        /// </summary>
        /// <value>
        /// The standard mode.
        /// </value>
        public string StandardMode { get; set; }

        /// <summary>
        /// Gets or sets the phase angle.
        /// </summary>
        /// <value>
        /// The phase angle.
        /// </value>
        public decimal PhaseAngle { get; set; }

        /// <summary>
        /// Gets or sets the frequency.
        /// </summary>
        /// <value>
        /// The frequency.
        /// </value>
        public decimal Frequency { get; set; }

        /// <summary>
        /// Gets or sets the optics.
        /// </summary>
        /// <value>
        /// The optics.
        /// </value>
        public string Optics { get; set; }

        /// <summary>
        /// Gets or sets the desired accuracy.
        /// </summary>
        /// <value>
        /// The desired accuracy.
        /// </value>
        public decimal DesiredAccuracy { get; set; }

        /// <summary>
        /// Gets or sets the KH.
        /// </summary>
        /// <value>
        /// The KH.
        /// </value>
        public string KH { get; set; }
    }
}
