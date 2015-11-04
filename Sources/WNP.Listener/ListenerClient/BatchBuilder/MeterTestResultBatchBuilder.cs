// <copyright file="MeterTestResultBatchBuilder.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ListenerClient.BatchBuilder
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Repository.WNP;

    public class MeterTestResultBatchBuilder : IBatchBuilder
    {
        private readonly IWnpRepository repository;

        public MeterTestResultBatchBuilder(IWnpRepository wnpRepository)
        {
            this.repository = wnpRepository;
        }

        public Task<Collection<object>> Create(string batchNumber)
        {
            return this.repository.GetMeterTestBatchAsync(batchNumber);
        }
    }
}