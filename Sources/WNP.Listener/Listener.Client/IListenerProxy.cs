namespace AMSLLC.Listener.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IListenerProxy : IDisposable
    {
        Task<string> OpenAsync(Uri uri, object data);

        Task<string> GetAsync(
            Uri uri);
    }
}