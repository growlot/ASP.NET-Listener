// <copyright file="ListenerRequestHeaderDictionary.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Runtime.Serialization;

    /// <summary>
    /// List of headers used in Listener request.
    /// </summary>
    [Serializable]
    public class ListenerRequestHeaderDictionary : Dictionary<string, string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerRequestHeaderDictionary"/> class.
        /// </summary>
        public ListenerRequestHeaderDictionary()
        {
            this.Add("AMS-Company", ConfigurationManager.AppSettings["ams:company"]);
            this.Add("AMS-Application", ConfigurationManager.AppSettings["ams:application"]);
            this.Add("AMS-CompanyId", ConfigurationManager.AppSettings["ams:company-id"]);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerRequestHeaderDictionary"/> class with serialized data.
        /// </summary>
        /// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Generic.Dictionary`2" />.</param>
        /// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.Dictionary`2" />.</param>
        protected ListenerRequestHeaderDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
