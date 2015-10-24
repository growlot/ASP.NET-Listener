// <copyright file="UnsubscribeDisposable.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Bootstrapper.Owin.Middleware
{
    using System;

    internal class UnsubscribeDisposable : IDisposable
    {
        private IDisposable target;
        private bool unsubscribe = false;

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