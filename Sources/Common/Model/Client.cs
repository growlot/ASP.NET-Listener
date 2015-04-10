//-----------------------------------------------------------------------
// <copyright file="Client.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    /// <summary>
    /// Data model class representing Client entity
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Gets or sets the client account number.
        /// </summary>
        /// <value>
        /// The client account number.
        /// </value>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the client account.
        /// </summary>
        /// <value>
        /// The name of the client account.
        /// </value>
        public string AccountName { get; set; }
    }
}