﻿using System;
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

        Type GetEntityType(string typeName);
        Type GetEntityTypeBySetName(string entitySetName);

        MetadataModel GetModelMapping(string clrModelName);
        MetadataModel GetModelMapping(Type clrModel);
        MetadataModel GetModelMappingByTableName(string tableName);
    }
}