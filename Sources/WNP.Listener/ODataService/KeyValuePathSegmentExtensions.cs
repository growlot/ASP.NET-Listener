// <copyright file="KeyValuePathSegmentExtensions.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Web.OData.Routing;

    using Microsoft.OData.Core;
    using Microsoft.OData.Core.UriParser;

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
            var result = new Dictionary<string, object>();

            var keyRaw = keyValuePathSegment.Value;

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