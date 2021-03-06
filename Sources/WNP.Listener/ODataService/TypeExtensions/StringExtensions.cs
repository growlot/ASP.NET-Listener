﻿// <copyright file="StringExtensions.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace System
{
    using Collections.Generic;
    using Linq;

    using Microsoft.OData.Core;
    using Microsoft.OData.Core.UriParser;

    /// <summary>
    /// Extensions for KeyValuePathSegment.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts KeyValuePathSegment to Dictionary containing final key names/values.
        /// </summary>
        /// <param name="keyRaw">
        /// The raw json key in string format.
        /// </param>
        /// <returns>
        /// The dictionary, containing final key names/values.
        /// </returns>
        public static Dictionary<string, object> ToCompositeKeyDictionary(this string keyRaw)
        {
            if (keyRaw == null)
            {
                throw new ArgumentNullException(nameof(keyRaw), "Can not execute action on null object.");
            }

            var result = new Dictionary<string, object>();

            var compoundKeyPairs = keyRaw.Split(',');
            if (!compoundKeyPairs.Any())
            {
                return new Dictionary<string, object>();
            }

            foreach (var compoundKeyPair in compoundKeyPairs)
            {
                var pair = compoundKeyPair.Split('=');
                if (pair.Length != 2)
                {
                    continue;
                }

                var keyName = pair[0].Trim();
                var keyValue = pair[1].Trim();

                result.Add(keyName, ODataUriUtils.ConvertFromUriLiteral(keyValue, ODataVersion.V4));
            }

            return result;
        }
    }
}