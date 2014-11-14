//-----------------------------------------------------------------------
// <copyright file="TransactionStateLookup.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Lookup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Lists possible transaction states.
    /// </summary>
    public enum TransactionStateLookup
    {
        /// <summary>
        /// No value, should never be used
        /// </summary>
        None = 0,

        /// <summary>
        /// The listener client has started processing request
        /// </summary>
        ClientStart = 1,

        /// <summary>
        /// The listener client has called Listener service
        /// </summary>
        ClientSendMessage = 2,

        /// <summary>
        /// The listener client has finished processing request
        /// </summary>
        ClientEnd = 5,

        /// <summary>
        /// The listener service has started processing request
        /// </summary>
        ServiceStart = 3,

        /// <summary>
        /// The listener service has called 3rdParty service
        /// </summary>
        ServiceSendMessage = 4,

        /// <summary>
        /// The listener service has finished processing request
        /// </summary>
        ServiceEnd = 6
    }
}
