using System.Collections.Generic;

namespace AMSLLC.Listener.Client
{
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
