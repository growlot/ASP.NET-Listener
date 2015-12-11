using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSLLC.Listener.ODataService.Model
{
    public class ProtocolMetadata
    {
        public List<ProtocolMetadataModel> Connection { get; } = new List<ProtocolMetadataModel>();
        public List<ProtocolMetadataModel> Configuration { get; } = new List<ProtocolMetadataModel>();
        public string ProtocolType { get; set; }
    }
}
