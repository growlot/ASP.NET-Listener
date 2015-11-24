// <copyright file="WnpRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNPAsync
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using Repository;
    using Repository.WNP;
    using Repository.WNP.Model;
    using Serilog;
    using WNP;
    using WNP.Metadata;

    /// <summary>
    /// Class WnpRepository.
    /// </summary>
    [WithinWnpContext]
    public class WnpRepository : IWnpBatchRepository
    {
        private readonly IPersistenceAdapter persistenceAdapter;

        /// <summary>
        /// Initializes a new instance of the <see cref="WnpRepository"/> class.
        /// </summary>
        /// <param name="persistenceAdapter">The persistence adapter.</param>
        public WnpRepository(IPersistenceAdapter persistenceAdapter)
        {
            this.persistenceAdapter = persistenceAdapter;
        }

        /// <summary>
        /// Gets the meter test batch asynchronous.
        /// </summary>
        /// <param name="batchNumber">The batch number.</param>
        /// <returns>Task&lt;Collection&lt;Meter&gt;&gt;.</returns>
        public async Task<ICollection<Meter>> GetMeterTestBatchAsync(string batchNumber)
        {
            string sql = $@"
SELECT
	TR.*
FROM
	{DBMetadata.MeterTestResults.FullTableName} TR
	INNER JOIN {DBMetadata.EqpMeter.FullTableName} M ON TR.{DBMetadata.MeterTestResults.EqpNo} = M.{DBMetadata.EqpMeter.EqpNo}
WHERE
	M.{DBMetadata.EqpMeter.NewBatchNo} = @0 AND TR.{DBMetadata.MeterTestResults.StepNo} = 1";

            Log.Logger.Information("Querying WNP for batch information: {0} (batch number: {1})", sql, batchNumber);

            var tests = await this.persistenceAdapter.GetListAsync<MeterTestResultsEntity>(sql, false, batchNumber);
            return Enumerable.GroupBy(
                tests,
                o => new
                {
                    o.EqpNo,
                    o.Owner
                }).Select(
                    s => new Meter(
                        s.Select(
                            ss => new MeterTest
                            {
                                StartDate = ss.TestDateStart
                            }))
                    {
                        EquipmentNumber = s.Key.EqpNo,
                        Owner = s.Key.Owner
                    }).ToList();
        }
    }
}