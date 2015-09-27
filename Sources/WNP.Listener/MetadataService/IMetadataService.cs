using System.Collections.Generic;
using System.Reflection;
using AMSLLC.Listener.MetadataService.Impl;
using AMSLLC.Listener.MetadataService.Model;

namespace AMSLLC.Listener.MetadataService
{
    public interface IMetadataService
    {
        string ODataModelNamespace { get; }
        Assembly ODataModelAssembly { get; }

        MetadataModel GetModelMapping(string clrModelName);
    }
}