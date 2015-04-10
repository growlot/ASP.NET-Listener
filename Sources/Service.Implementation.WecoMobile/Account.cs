//-----------------------------------------------------------------------
// <copyright file="Account.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.WecoMobile
{
    using System;

    /// <summary>
    /// Data model class representing Account entity
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Gets or sets the account number.
        /// </summary>
        /// <value>
        /// The account number.
        /// </value>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the account.
        /// </summary>
        /// <value>
        /// The name of the account.
        /// </value>
        public string AccountName { get; set; }

        /// <summary>
        /// Gets or sets the premise number.
        /// </summary>
        /// <value>
        /// The premise number.
        /// </value>
        public string PremiseNumber { get; set; }
    }
}
