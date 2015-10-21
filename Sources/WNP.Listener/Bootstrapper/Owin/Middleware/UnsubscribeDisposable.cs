using System;

namespace AMSLLC.Listener.Bootstrapper.Owin.Middleware
{
    internal class UnsubscribeDisposable : IDisposable
    {
        IDisposable target;
        bool unsubscribe = false;

        public UnsubscribeDisposable(IDisposable target)
        {
            this.target = target;
        }

        public void CallTargetDispose()
        {
            if (!this.unsubscribe)
            {
                this.target.Dispose();
            }
        }

        public void Dispose()
        {
            this.unsubscribe = true;
        }
    }
}