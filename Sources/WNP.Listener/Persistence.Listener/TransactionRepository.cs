// //-----------------------------------------------------------------------
// <copyright file="TransactionRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Persistence.Listener
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Communication;
    using Domain;
    using Domain.Listener.Transaction;
    using Utilities;
    using Repository;

    /// <summary>
    /// Implements <see cref="ITransactionRepository"/> for AsyncPoco
    /// </summary>
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IPersistenceAdapter persistence;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRepository"/> class.
        /// </summary>
        /// <param name="persistence">The persistence.</param>
        public TransactionRepository(IPersistenceAdapter persistence)
        {
            this.persistence = persistence;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="TransactionRepository" /> class.
        /// </summary>
        ~TransactionRepository()
        {
            // Finalizer calls Dispose(false)
            this.Dispose(false);
        }

        /// <inheritdoc/>
        public Task CreateAsync(TransactionExecution transaction)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task UpdateAsync(TransactionExecution transaction)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task UpdateHashAsync(int transactionId, string hash)
        {
            // Single update, transaction scope removed
            // await
            //    _persistence.UpdateAsync("TransactionRegistry", "TransactionId", new TransactionRegistryEntity()
            //    {
            //        TransactionHash = hash
            //    }, new[] { "TransactionHash" });
            await
                this.persistence.UpdateAsync(
                    new TransactionRegistryEntity { TransactionHash = hash, TransactionId = transactionId },
                    transactionId,
                    new[] { "TransactionHash" });
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<IMemento>> GetFieldConfigurationsAsync(string companyCode, string sourceApplicationKey, string operationKey)
        {
            var select = @"SELECT FCE.* FROM FieldConfigurationEntry FCE INNER JOIN FieldConfiguration FC ON FCE.FieldConfigurationId = FC.FieldConfigurationId INNER JOIN EnabledOperation EO ON EO.FieldConfigurationId = FC.FieldConfigurationId INNER JOIN Application A ON EO.ApplicationId = A.ApplicationId 
INNER JOIN Company C ON EO.CompanyId = C.CompanyId
INNER JOIN Operation O ON EO.OperationId = O.OperationId WHERE C.ExternalCode = @0 AND A.RecordKey = @1 AND O.Name = @2";

            var fieldConfigurationEntries = await this.persistence.GetListAsync<FieldConfigurationEntryEntity>(
                       select,
                       companyCode,
                       sourceApplicationKey,
                       operationKey);
            var valueMapEntries = await this.persistence.GetListAsync<ValueMapEntryEntity>("SELECT * FROM ValueMapEntry");
            var valueMaps = await this.persistence.GetListAsync<ValueMapEntity>("SELECT * FROM ValueMap");

           return PrepareFieldConfiguration(fieldConfigurationEntries, valueMapEntries, valueMaps);
        }

        /// <inheritdoc/>
        public async Task<IMemento> GetExecutionContextAsync(string recordKey)
        {
            TransactionExecutionMemento returnValue = null;
            var protocols = await this.persistence.GetListAsync<ProtocolTypeEntity>("SELECT * FROM ProtocolType");
            var valueMapEntries =
                await this.persistence.GetListAsync<ValueMapEntryEntity>("SELECT * FROM ValueMapEntry");
            var valueMaps = await this.persistence.GetListAsync<ValueMapEntity>("SELECT * FROM ValueMap");

            // var fieldConfigurationEntries = await _persistence.GetListAsync<FieldConfigurationEntryEntity>("SELECT * FROM FieldConfigurationEntry");
            var tr = await this.persistence.GetAsync<TransactionRegistryEntity>("SELECT * FROM TransactionRegistry WHERE RecordKey = @0", recordKey);

            Func<EndpointEntity, OperationEndpointEntity, EnabledOperationEntity, IntegrationEndpointConfigurationMemento> callback = (ee, oe, eo) => new IntegrationEndpointConfigurationMemento(
                    protocols.Single(s => s.ProtocolTypeId == ee.ProtocolTypeId).Name,
                    ee.ConnectionConfiguration,
                    ee.AdapterConfiguration,
                    (EndpointTriggerType)ee.EndpointTriggerTypeId);

            var select = @"
SELECT E.*, OE.*, EO.*
FROM 
	Endpoint E 
	INNER JOIN OperationEndpoint OE ON E.EndpointId = OE.EndpointId
	INNER JOIN EnabledOperation EO ON OE.EnabledOperationId = EO.EnabledOperationId
	INNER JOIN TransactionRegistry TR ON TR.EnabledOperationId = EO.EnabledOperationId
    LEFT JOIN FieldConfiguration FC ON EO.FieldConfigurationId = FC.FieldConfigurationId 
WHERE TR.RecordKey = @0";

            var endpoints = await this.persistence.ProjectionAsync(
                callback,
                select,
                recordKey);

            select = @"
SELECT FCE.* 
FROM FieldConfigurationEntry FCE 
    INNER JOIN FieldConfiguration FC ON FCE.FieldConfigurationId = FC.FieldConfigurationId 
    INNER JOIN EnabledOperation EO ON EO.FieldConfigurationId = FC.FieldConfigurationId 
WHERE EO.EnabledOperationId = @0";

            var fieldConfigurationEntries = await this.persistence.GetListAsync<FieldConfigurationEntryEntity>(
                        select,
                        tr.EnabledOperationId);

            if (endpoints != null)
            {
                returnValue = new TransactionExecutionMemento(
                    tr.TransactionId,
                    recordKey,
                    tr.EnabledOperationId,
                    endpoints,
                    PrepareFieldConfiguration(fieldConfigurationEntries, valueMapEntries, valueMaps));
            }

            return returnValue;
        }

        /// <inheritdoc/>
        public async Task CreateTransactionRegistryAsync(TransactionRegistry transactionRegistry)
        {
            var select = @"
SELECT EO.EnabledOperationId 
FROM EnabledOperation EO 
    INNER JOIN Application A ON A.ApplicationId = EO.ApplicationId 
    INNER JOIN Company C ON C.CompanyId = EO.CompanyId 
    INNER JOIN Operation O ON O.OperationId = EO.OperationId 
WHERE A.RecordKey = @0 AND C.ExternalCode = @1 AND O.Name = @2";

            var enabledOperationid =
                await
                    this.persistence.ExecuteScalarAsync<int>(
                        select,
                        transactionRegistry.ApplicationKey,
                        transactionRegistry.CompanyCode,
                        transactionRegistry.OperationKey);

            // Single insert, transaction scope removed
            await
                this.persistence.InsertAsync(new TransactionRegistryEntity // "TransactionRegistry", "TransactionId",
                {
                    CreatedDateTime = transactionRegistry.CreatedDateTime,
                    RecordKey = transactionRegistry.RecordKey,
                    TransactionStatusId = (int)transactionRegistry.Status,
                    EnabledOperationId = enabledOperationid,
                    Data = transactionRegistry.Data,
                    UpdatedDateTime = transactionRegistry.UpdatedDateTime,
                    AppUser = transactionRegistry.UserName,
                    Message = transactionRegistry.Message,
                    Details = transactionRegistry.Details,
                    TransactionKey = transactionRegistry.TransactionKey,
                    Summary = SerializationUtilities.DictionaryToXml(transactionRegistry.Summary)
                });
        }

        /// <inheritdoc/>
        public async Task<IMemento> GetRegistryEntry(string recordKey)
        {
            Func<TransactionRegistryEntity, ApplicationEntity, CompanyEntity, OperationEntity, TransactionRegistryMemento> callback = (tr, app, cmp, op) => new TransactionRegistryMemento(
                tr.TransactionId,
                tr.RecordKey,
                tr.TransactionKey,
                cmp.ExternalCode,
                app.RecordKey,
                op.Name,
                (TransactionStatusType)tr.TransactionStatusId,
                tr.AppUser,
                tr.CreatedDateTime,
                tr.UpdatedDateTime,
                tr.Data,
                tr.Message,
                tr.Details);

            var select = @"
SELECT TR.*, A.*, C.*, O.*
FROM TransactionRegistry TR 
    INNER JOIN EnabledOperation EO ON TR.EnabledOperationId = EO.EnabledOperationId
    INNER JOIN Application A ON EO.ApplicationId = A.ApplicationId 
    INNER JOIN Company C ON EO.CompanyId = C.CompanyId
    INNER JOIN Operation O ON EO.OperationId = O.OperationId
WHERE TR.RecordKey = @0";

            var registryEntities = await this.persistence.ProjectionAsync(
                callback,
                select,
                recordKey);

            return registryEntities.SingleOrDefault();
        }

        /// <inheritdoc/>
        public async Task UpdateTransactionRegistryAsync(TransactionRegistry transactionRegistry)
        {
            var entity = new TransactionRegistryEntity
            {
                TransactionId = transactionRegistry.Id,
                TransactionStatusId = (int)transactionRegistry.Status,
                UpdatedDateTime = transactionRegistry.UpdatedDateTime,
                AppUser = transactionRegistry.UserName,
                Message = transactionRegistry.Message,
                Details = transactionRegistry.Details
            };

            await this.persistence.UpdateAsync(
                entity,
                transactionRegistry.Id,
                new[] { "TransactionStatusId", "UpdatedDateTime", "AppUser", "Message", "Details" });
        }

        /// <inheritdoc/>
        public Task<string> GetTransactionDataAsync(string recordKey)
        {
            return this.persistence.ExecuteScalarAsync<string>("SELECT Data FROM TransactionRegistry WHERE RecordKey = @0", recordKey);
        }

        /// <inheritdoc/>
        public Task<int> GetHashCountAsync(int enabledOperationId, string hash)
        {
            return this.persistence.ExecuteScalarAsync<int>("SELECT COUNT(TransactionHash) FROM TransactionRegistry WHERE EnabledOperationId = @0 AND TransactionHash = @1", enabledOperationId, hash);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the specified disposing.
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        private static IEnumerable<FieldConfigurationMemento> PrepareFieldConfiguration(IEnumerable<FieldConfigurationEntryEntity> fieldConfigurationEntries, IEnumerable<ValueMapEntryEntity> valueMapEntries, IEnumerable<ValueMapEntity> valueMaps)
        {
            List<FieldConfigurationMemento> returnValue = new List<FieldConfigurationMemento>();

            foreach (var fieldConfigurationEntry in fieldConfigurationEntries)
            {
                Dictionary<string, object> valueMap = new Dictionary<string, object>();
                if (fieldConfigurationEntry.ValueMapId.HasValue)
                {
                    var map = valueMapEntries.Where(s => s.ValueMapId == fieldConfigurationEntry.ValueMapId);
                    if (!map.Any())
                    {
                        break;
                    }

                    var mapType = valueMaps.Single(m => m.ValueMapId == map.First().ValueMapId);
                    foreach (var valueMapEntry in map)
                    {
                        valueMap.Add(valueMapEntry.RecordKey ?? string.Empty, Converters.ConvertFromString(valueMapEntry.Value, mapType.ValueType));
                    }
                }

                returnValue.Add(new FieldConfigurationMemento(fieldConfigurationEntry.FieldName, fieldConfigurationEntry.MapToName, fieldConfigurationEntry.HashSequence, fieldConfigurationEntry.KeySequence, valueMap));
            }

            return returnValue;
        }
    }
}