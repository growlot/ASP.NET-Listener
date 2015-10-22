// //-----------------------------------------------------------------------
// <copyright file="DomainValidatorDictionary.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Domain validator registry
    /// </summary>
    public class DomainValidatorDictionary : Dictionary<Type, IDomainValidator>
    {
        /// <summary>
        /// Check, if all validators are valid
        /// </summary>
        /// <returns><c>true</c> if valid, <c>false</c> otherwise.</returns>
        public bool Valid()
        {
            return this.Values.Aggregate(true, (current, value) => current && value.Valid);
        }
    }
}