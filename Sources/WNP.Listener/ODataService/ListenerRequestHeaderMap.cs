using System;
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
            this.Add("EntityCategory", typeof(string));
            this.Add("EntityKey", typeof(string));
            this.Add("OperationKey", typeof(string));
            this.Add("RecordKey", typeof(string));
            this.Add("Details", typeof(string));
            this.Add("Message", typeof(string));
        }

        public static ListenerRequestHeaderMap Instance { get; } = new ListenerRequestHeaderMap();
    }
}
