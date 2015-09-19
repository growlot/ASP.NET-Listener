using System.Collections.Generic;

namespace AMSLLC.Listener.MetadataService
{
    public interface IODataEntityConfiguration : IDictionary<string, ExposedEntityConfiguration>
    {
        
    }

    public class ExposedEntityConfiguration
    {
        public string DefaultEntityName { get; set; }
    }
}