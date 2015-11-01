// <copyright file="IBatchBuilder.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ListenerClient
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    public interface IBatchBuilder
    {
        Task<Collection<object>> Create(
            string batchNumber);
    }
}