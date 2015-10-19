using System;

namespace AMSLLC.Listener.MetadataService
{
    public interface IActionConfigurator
    {
        Type GetEntityActionContainer(string tableName);
        bool IsEntityActionsContainerAvailable(string tableName);
        Type GetUnboundActionContainer(string containerName);
    }
}