namespace AMSLLC.Listener.Repository
{
    using System;

    public interface IRepositoryManager : IDisposable
    {
        TRepository Create<TRepository>() where TRepository : IRepository;
    }
}
