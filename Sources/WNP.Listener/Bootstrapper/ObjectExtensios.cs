// <copyright file="ObjectExtensios.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace System
{
    using System.Web.Script.Serialization;

    /// <summary>
    /// Cusotm extesions for <see cref="object"/>
    /// </summary>
    internal static class ObjectExtensios
    {
        /// <summary>
        /// Converts object to Json string.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns>The object represented as Json string.</returns>
        internal static string ToJson(this object o)
        {
            if (o != null)
            {
                return new JavaScriptSerializer().Serialize(o);
            }

            return null;
        }
    }
}