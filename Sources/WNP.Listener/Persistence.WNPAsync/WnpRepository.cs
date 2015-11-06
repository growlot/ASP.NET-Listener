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
        public async Task<ICollection<Meter>> GetMeterTestBatchAsync(
            string batchNumber)
        {
            string sql = @"SELECT
	TR.*
FROM
	[wndba].[TMETER_TEST_RESULTS] TR
	INNER JOIN [wndba].[TEQP_METER] M ON TR.[EQP_NO] = M.[EQP_NO]
WHERE
	M.[NEW_BATCH_NO] = @0 AND TR.STEP_NO = 1";

            var tests = await this.persistenceAdapter.GetListAsync<TMETER_TEST_RESULTEntity>(sql, false, batchNumber);
            return Enumerable.GroupBy(
                tests,
                o => new
                {
                    o.EQP_NO,
                    o.OWNER
                }).Select(
                    s => new Meter(
                        s.Select(
                            ss => new MeterTest
                            {
                                StartDate = ss.TEST_DATE_START
                            }))
                    {
                        EquipmentNumber = s.Key.EQP_NO,
                        Owner = s.Key.OWNER
                    }).ToList();
        }
    }
}