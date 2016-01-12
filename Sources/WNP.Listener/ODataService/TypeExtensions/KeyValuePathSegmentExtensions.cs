// <copyright file="KeyValuePathSegmentExtensions.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace System.Web.OData.Routing
{
    using System;
    using Collections.Generic;

    /// <summary>
    /// Extensions for KeyValuePathSegment.
    /// </summary>
    public static class KeyValuePathSegmentExtensions
    {
        /// <summary>
        /// Converts KeyValuePathSegment to Dictionary containing final key names/values.
        /// </summary>
        /// <param name="keyValuePathSegment">
        /// The path segment to be processed.
        /// </param>
        /// <returns>
        /// The dictionary, containing final key names/values.
        /// </returns>
        public static Dictionary<string, object> ToCompositeKeyDictionary(this KeyValuePathSegment keyValuePathSegment)
        {
            if (keyValuePathSegment == null)
            {
                throw new ArgumentNullException(nameof(keyValuePathSegment), "Can not execute action on null object.");
            }

            return keyValuePathSegment.Value.ToCompositeKeyDictionary();
        }
    }
}