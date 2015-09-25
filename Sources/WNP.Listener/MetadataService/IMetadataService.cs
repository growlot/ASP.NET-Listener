using System.Collections.Generic;
using System.Reflection;
using AMSLLC.Listener.MetadataService.Impl;
using AMSLLC.Listener.MetadataService.Model;

namespace AMSLLC.Listener.MetadataService
{
    public interface IMetadataService
    {
        string ODataModelNamespace { get; }
        List<WNPMetadataEntry> RawMetadata { get; }
        Assembly ODataModelAssembly { get; }

        ODataModelMapping GetModelMapping(string clrModelName);
    }
}