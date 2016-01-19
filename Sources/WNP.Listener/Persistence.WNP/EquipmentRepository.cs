// <copyright file="EquipmentRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNP
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;
    using Domain.WNP.ElectricMeterAggregate;
    using Metadata;
    using Repository.WNP;

    /// <summary>
    /// Repository implementation for working with SQL database.
    /// </summary>
    public class EquipmentRepository : IEquipmentRepository
    {
        private bool disposedValue = false; // To detect redundant calls
        private WNPDBContext dbContext;
        private int operatingCompany;

        /// <summary>
        /// Initializes a new instance of the <see cref="EquipmentRepository" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="operatingCompany">The operating company.</param>
        public EquipmentRepository(WNPDBContext dbContext, int operatingCompany)
        {
            this.dbContext = dbContext;
            this.operatingCompany = operatingCompany;
        }

        /// <inheritdoc/>
        public async Task<IMemento> GetElectricMeter(string equipmentNumber)
        {
            var electricMeterEntity = await this.dbContext.FirstOrDefaultAsync<EqpMeterEntity>(
                $@"
SELECT {DBMetadata.EqpMeter.EqpNo}
FROM {DBMetadata.EqpMeter.FullTableName} 
WHERE {DBMetadata.EqpMeter.Owner} = @0 and {DBMetadata.EqpMeter.EqpNo} = @1",
                this.operatingCompany,
                equipmentNumber);

            if (electricMeterEntity == null)
            {
                return null;
            }

            var meterMemento = new ElectricMeterMemento(
                equipmentNumber: electricMeterEntity.EqpNo,
                readings: await this.GetElectricMeterReadingsAsync(electricMeterEntity.EqpNo));

            return meterMemento;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                }

                this.disposedValue = true;
            }
        }

        private async Task<IEnumerable<IMemento>> GetElectricMeterReadingsAsync(string equipmentNumber)
        {
            var readings = await this.dbContext.FetchAsync<ReadingEntity>(
                $@"
SELECT *
FROM {DBMetadata.Reading.FullTableName}
WHERE {DBMetadata.Reading.Owner} = @0 and {DBMetadata.Reading.EqpNo} = @1",
                this.operatingCompany,
                equipmentNumber);

            var result = new List<ElectricMeterReadingMemento>();
            foreach (var reading in readings)
            {
                var readingMemento = new ElectricMeterReadingMemento(
                    annunciator: reading.Annunciator,
                    occasion: string.IsNullOrEmpty(reading.EventFlag) ? null : (char?)reading.EventFlag.ToCharArray()[0],
                    label: reading.ReadLabel,
                    value: reading.Reading,
                    source: reading.ReadSrc,
                    date: reading.ReadDate.Value,
                    user: reading.ReadBy);
                result.Add(readingMemento);
            }

            return result;
        }
    }
}
