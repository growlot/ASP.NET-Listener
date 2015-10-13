//-----------------------------------------------------------------------
// <copyright file="BillingAccount.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.WNP.SiteAggregate
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Billing Account object value type
    /// </summary>
    public sealed class BillingAccount : ValueObject<BillingAccount>
    {
        /// <summary>
        /// The account name
        /// </summary>
        private readonly string name;

        /// <summary>
        /// The account number
        /// </summary>
        private readonly string number;

        /// <summary>
        /// Initializes a new instance of the <see cref="BillingAccount"/> class.
        /// </summary>
        /// <param name="name">The account name.</param>
        /// <param name="number">The account number.</param>
        public BillingAccount(string name, string number)
        {
            this.name = name;
            this.number = number;
        }

        /// <summary>
        /// Gets the account name.
        /// </summary>
        /// <value>
        /// The account name.
        /// </value>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets the account number.
        /// </summary>
        /// <value>
        /// The account number.
        /// </value>
        public string Number
        {
            get
            {
                return this.number;
            }
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string result = string.Format(CultureInfo.InvariantCulture, "Account(name = {0}, number = {1})", this.name, this.number);
            return result;
        }
    }
}
