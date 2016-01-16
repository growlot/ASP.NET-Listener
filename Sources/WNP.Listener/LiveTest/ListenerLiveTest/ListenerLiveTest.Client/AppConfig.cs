using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListenerLiveTest.Client
{
    using System.Configuration;

    public static class AppConfig
    {
        private static readonly Lazy<string> _companyCodeInitializer = new Lazy<string>(() => ConfigurationManager.AppSettings["ams:company"]);
        public static string CompanyCode { get { return _companyCodeInitializer.Value; } }

        private static readonly Lazy<string> _companyIdInitializer = new Lazy<string>(() => ConfigurationManager.AppSettings["ams:company-id"]);
        public static string CompanyId { get { return _companyIdInitializer.Value; } }

        private static readonly Lazy<string> _applicationCodeInitializer = new Lazy<string>(() => ConfigurationManager.AppSettings["ams:application"]);
        public static string ApplicationCode { get { return _applicationCodeInitializer.Value; } }

        private static readonly Lazy<string> _listenerUrl = new Lazy<string>(() => ConfigurationManager.AppSettings["listener-uri"]);
        public static string ListenerUrl { get { return _listenerUrl.Value; } }


    }
}
