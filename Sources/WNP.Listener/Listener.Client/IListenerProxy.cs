namespace AMSLLC.Listener.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IListenerProxy
    {
        Task<string> OpenAsync(
            Uri uri,
            object data,
            Dictionary<string, string> headers = null);
    }
}