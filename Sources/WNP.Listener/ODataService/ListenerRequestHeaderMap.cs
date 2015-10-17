﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSLLC.Listener.ODataService
{
    public class ListenerRequestHeaderMap : Dictionary<string, Type>
    {
        private ListenerRequestHeaderMap()
        {
            Add("EntityCategory", typeof(string));
            Add("EntityKey", typeof(string));
            Add("OperationKey", typeof(string));
            Add("RecordKey", typeof(string));
            Add("Details", typeof(string));
            Add("Message", typeof(string));
        }

        public static ListenerRequestHeaderMap Instance { get; } = new ListenerRequestHeaderMap();
    }
}
