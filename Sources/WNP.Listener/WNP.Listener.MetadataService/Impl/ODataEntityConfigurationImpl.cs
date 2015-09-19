using System.Collections.Generic;
using AMSLLC.Listener.Persistence.Metadata;

namespace AMSLLC.Listener.MetadataService.Impl
{
    public class ODataEntityConfigurationImpl : Dictionary<string, ExposedEntityConfiguration>, IODataEntityConfiguration
    {
        public ODataEntityConfigurationImpl()
        {
            this[DBMetadata.EqpMeter.RealTableName] = new ExposedEntityConfiguration() { DefaultEntityName = "Meter" };
        }
    }
}