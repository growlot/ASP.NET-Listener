using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public interface IRepositoryBuilder : IDisposable
    {
        TRepository Create<TRepository>() where TRepository : IRepository;

        IEnumerable<TRepository> Create<TRepository>(int sourceAppId, int destinationAppId,
            string operationKey) where TRepository : IRepository;
    }
}
