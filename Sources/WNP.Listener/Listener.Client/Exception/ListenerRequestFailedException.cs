﻿namespace AMSLLC.Listener.Client.Exception
{
    using System.Net;
    using Exception = System.Exception;

    public class ListenerRequestFailedException : Exception
    {
        public ListenerRequestFailedException(HttpStatusCode statusCode) : base($"Listener request failed with status code: {statusCode}")
        {
        }

    }
}
