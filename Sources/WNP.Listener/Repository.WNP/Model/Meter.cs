// <copyright file="Meter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Repository.WNP.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Meter tests
    /// </summary>
    public class Meter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Meter"/> class.
        /// </summary>
        public Meter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Meter" /> class.
        /// </summary>
        /// <param name="tests">The tests.</param>
        public Meter(IEnumerable<MeterTest> tests)
        {
            this.Tests.AddRange(tests);
        }

        /// <summary>
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>The equipment number.</value>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>The owner.</value>
        public int? Owner { get; set; }

        /// <summary>
        /// Gets the tests.
        /// </summary>
        /// <value>The tests.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "As designed")]
        public MeterTestCollection Tests { get; } = new MeterTestCollection();
    }
}