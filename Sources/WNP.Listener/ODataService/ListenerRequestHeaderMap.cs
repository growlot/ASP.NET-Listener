// <copyright file="ListenerRequestHeaderMap.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService
{
    using System;
    using System.Collections.Generic;

    public class ListenerRequestHeaderMap : Dictionary<string, Type>
    {
        private ListenerRequestHeaderMap()
        {
            this.Add("EntityCategory", typeof(string));
            this.Add("EntityKey", typeof(string));
            this.Add("BatchKey", typeof(string));
            this.Add("OperationKey", typeof(string));
            this.Add("RecordKey", typeof(Guid));
            this.Add("Details", typeof(string));
            this.Add("Message", typeof(string));
        }

        public static ListenerRequestHeaderMap Instance { get; } = new ListenerRequestHeaderMap();
    }
}
