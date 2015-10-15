using System;

namespace AMSLLC.Listener.MetadataService
{
    public interface IEntityActionConfigurator
    {
        Type GetEntityActionContainer(string tableName);
        bool IsEntityActionsContainerAvailable(string tableName);
    }
}