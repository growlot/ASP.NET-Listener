// <copyright file="ListenerRequestHeaderDictionary.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client
{
    using System.Collections.Generic;
    using System.Configuration;

    public class ListenerRequestHeaderDictionary : Dictionary<string, string>
    {
        public ListenerRequestHeaderDictionary()
        {
            this.Add("AMS-Company", ConfigurationManager.AppSettings["ams:company"]);
            this.Add("AMS-Application", ConfigurationManager.AppSettings["ams:application"]);
        }
    }
}
