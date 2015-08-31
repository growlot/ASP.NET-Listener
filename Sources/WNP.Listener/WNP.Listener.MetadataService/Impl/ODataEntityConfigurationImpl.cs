using System.Collections.Generic;

using static DBMetadata;

namespace WNP.Listener.MetadataService.Impl
{
    public class ODataEntityConfigurationImpl : Dictionary<string, ExposedEntityConfiguration>, IODataEntityConfiguration
    {
        public ODataEntityConfigurationImpl()
        {
            this[EqpMeter.RealTableName] = new ExposedEntityConfiguration() { DefaultEntityName = "Meter" };
        }
    }
}