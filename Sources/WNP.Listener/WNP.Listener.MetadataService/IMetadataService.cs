using System.Collections.Generic;
using System.Reflection;
using WNP.Listener.MetadataService.Impl;
using WNP.Listener.MetadataService.Model;

namespace WNP.Listener.MetadataService
{
    public interface IMetadataService
    {
        string ODataModelNamespace { get; }
        Dictionary<string, MetadataServiceImpl.ODataModelMapping> ODataModelMappings { get; }
        List<WNPMetadataEntry> RawMetadata { get; }
        Assembly ODataModelAssembly { get; }
    }
}