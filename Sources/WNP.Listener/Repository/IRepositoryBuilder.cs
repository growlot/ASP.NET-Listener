using System;
using System.Collections.Generic;

namespace AMSLLC.Listener.Repository
{
    public interface IRepositoryBuilder : IDisposable
    {
        TRepository Create<TRepository>() where TRepository : IRepository;

        IEnumerable<TRepository> Create<TRepository>(int sourceAppId, int destinationAppId,
            string operationKey) where TRepository : IRepository;
    }
}
