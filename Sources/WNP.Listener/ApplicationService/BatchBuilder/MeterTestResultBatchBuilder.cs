// <copyright file="MeterTestResultBatchBuilder.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.BatchBuilder
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Commands;
    using Model;
    using Newtonsoft.Json;
    using Repository.WNP;
    using Repository.WNP.Model;

    /// <summary>
    /// Meter batch builder
    /// </summary>
    public class MeterTestResultBatchBuilder : IBatchBuilder
    {
        private readonly IWnpBatchRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeterTestResultBatchBuilder"/> class.
        /// </summary>
        /// <param name="wnpRepository">The WNP repository.</param>
        public MeterTestResultBatchBuilder(IWnpBatchRepository wnpRepository)
        {
            this.repository = wnpRepository;
        }

        /// <inheritdoc/>
        public async Task<OpenBatchTransactionCommand> Create(
            string batchNumber, string companyCode, string applicationKey, string userName)
        {
            var records = await this.repository.GetMeterTestBatchAsync(batchNumber);
            var returnValue = new OpenBatchTransactionCommand()
            {
                CompanyCode = companyCode,
                SourceApplicationKey = applicationKey,
                User = userName,
                BatchNumber = batchNumber
            };
            foreach (var record in records)
            {
                returnValue.Batch.Add(
                    new BatchTransactionEntry
                    {
                        OperationKey = "Add",
                        EntityCategory = "ElectricMeters",
                        Priority = 1,
                        Data = JsonConvert.SerializeObject(
                            new Meter
                            {
                                EquipmentNumber = record.EquipmentNumber,
                                Owner = record.Owner
                            })
                    });

                foreach (var test in record.Tests)
                {
                    returnValue.Batch.Add(
                    new BatchTransactionEntry
                    {
                        OperationKey = "Test",
                        EntityCategory = "ElectricMeters",
                        Priority = 2,
                        Data = JsonConvert.SerializeObject(
                            new MeterTest
                            {
                                StartDate = test.StartDate
                            })
                    });
                }
            }

            return returnValue;
        }
    }
}