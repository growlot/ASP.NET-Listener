﻿// //-----------------------------------------------------------------------
// <copyright file="TransactionRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Persistence.Listener
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Resources;
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
        public async Task UpdateHashAsync(Guid recordKey, string hash)
        {
            // Single update, transaction scope removed
            // await
            //    _persistence.UpdateAsync("TransactionRegistry", "TransactionId", new TransactionRegistryEntity()
            //    {
            //        TransactionHash = hash
            //    }, new[] { "TransactionHash" });
            await
                this.persistence.UpdateAsync(
                    new TransactionRegistryEntity { TransactionHash = hash, RecordKey = recordKey },
                    recordKey,
                    new[] { "TransactionHash" });
        }

        /// <inheritdoc/>
        public async Task<Dictionary<string, IEnumerable<IMemento>>> GetFieldConfigurationsAsync(string companyCode, string sourceApplicationKey)
        {
            var valueMapEntries = await this.persistence.GetListAsync<ValueMapEntryEntity>("SELECT * FROM ValueMapEntry");
            var valueMaps = await this.persistence.GetListAsync<ValueMapEntity>("SELECT * FROM ValueMap");

            var select = @"SELECT FCE.*, O.*, EO.* FROM FieldConfigurationEntry FCE INNER JOIN FieldConfiguration FC ON FCE.FieldConfigurationId = FC.FieldConfigurationId INNER JOIN EnabledOperation EO ON EO.FieldConfigurationId = FC.FieldConfigurationId INNER JOIN Application A ON EO.ApplicationId = A.ApplicationId 
INNER JOIN Company C ON EO.CompanyId = C.CompanyId
INNER JOIN Operation O ON EO.OperationId = O.OperationId WHERE C.ExternalCode = @0 AND A.RecordKey = @1";

            var fieldConfigurationEntries = await this.persistence.ProjectionAsync((FieldConfigurationEntryEntity ent, OperationEntity operation, EnabledOperationEntity enabledOperation) => PrepareFieldConfiguration(ent, valueMapEntries, valueMaps, enabledOperation.EnabledOperationId, operation.Name), select,
                       companyCode,
                       sourceApplicationKey);//.GetListAsync<FieldConfigurationEntryEntity>(
                                             //);

            return fieldConfigurationEntries.GroupBy(o => o.OperationKey).ToDictionary(g => g.Key, g => g.ToList().Cast<IMemento>()); //;
        }

        /// <inheritdoc/>
        public async Task<IMemento> GetExecutionContextAsync(Guid recordKey)
        {
            TransactionExecutionMemento returnValue = null;
            var protocols = await this.persistence.GetListAsync<ProtocolTypeEntity>("SELECT * FROM ProtocolType");
            var valueMapEntries =
                await this.persistence.GetListAsync<ValueMapEntryEntity>("SELECT * FROM ValueMapEntry");
            var valueMaps = await this.persistence.GetListAsync<ValueMapEntity>("SELECT * FROM ValueMap");

            // var fieldConfigurationEntries = await _persistence.GetListAsync<FieldConfigurationEntryEntity>("SELECT * FROM FieldConfigurationEntry");

            var tr = await this.persistence.GetAsync<TransactionRegistryEntity>("SELECT * FROM TransactionRegistry WHERE RecordKey = @0", recordKey);

            var childTr = await this.persistence.GetListAsync<TransactionRegistryEntity>("SELECT TR1.* FROM TransactionRegistry TR INNER JOIN TransactionRegistry TR1 ON TR.RecordKey = TR1.BatchKey WHERE TR.RecordKey = @0", false, recordKey);
            var enabledOperations = childTr.Select(s => s.EnabledOperationId).ToList();
            enabledOperations.Add(tr.EnabledOperationId);

            var recordKeys = childTr.Select(s => s.RecordKey).ToList();
            if (!recordKeys.Any())
            {
                recordKeys.Add(recordKey);
            }

            Func<EndpointEntity, OperationEndpointEntity, EnabledOperationEntity, IntegrationEndpointConfigurationMemento> callback = (ee, oe, eo) => new IntegrationEndpointConfigurationMemento(protocols.Single(s => s.ProtocolTypeId == ee.ProtocolTypeId).Name, ee.ConnectionConfiguration, ee.AdapterConfiguration, (EndpointTriggerType)ee.EndpointTriggerTypeId);

            var select = @"
SELECT E.*, OE.*, EO.*
FROM 
	Endpoint E 
	INNER JOIN OperationEndpoint OE ON E.EndpointId = OE.EndpointId
	INNER JOIN EnabledOperation EO ON OE.EnabledOperationId = EO.EnabledOperationId
	INNER JOIN TransactionRegistry TR ON TR.EnabledOperationId = EO.EnabledOperationId
    LEFT JOIN FieldConfiguration FC ON EO.FieldConfigurationId = FC.FieldConfigurationId 
WHERE TR.RecordKey IN (@records)";

            var endpoints = await this.persistence.ProjectionAsync(callback, select, new { records = recordKeys.ToArray() });

            select = @"
SELECT FCE.*, EO.*, O.*
FROM FieldConfigurationEntry FCE 
    INNER JOIN FieldConfiguration FC ON FCE.FieldConfigurationId = FC.FieldConfigurationId 
    INNER JOIN EnabledOperation EO ON EO.FieldConfigurationId = FC.FieldConfigurationId 
    INNER JOIN Operation O ON EO.OperationId = O.OperationId
WHERE EO.EnabledOperationId IN (@operations)";

            //var fieldConfigurationEntries = await this.persistence.ProjectionAsync<FieldConfigurationEntryEntity>(select, new { operations = enabledOperations.Distinct().ToArray() });
            var fieldConfigurationEntries = await this.persistence.ProjectionAsync((FieldConfigurationEntryEntity ent, EnabledOperationEntity enabledOperation, OperationEntity operation) => PrepareFieldConfiguration(ent, valueMapEntries, valueMaps, enabledOperation.EnabledOperationId, operation.Name), select,
                        new { operations = enabledOperations.Distinct().ToArray() });

            if (endpoints != null)
            {
                returnValue = new TransactionExecutionMemento(tr.TransactionId, recordKey, tr.EnabledOperationId, endpoints, fieldConfigurationEntries.Where(fc => fc.EnabledOperationId == tr.EnabledOperationId), childTr.Select(ctr => new TransactionExecutionMemento(ctr.TransactionId, ctr.RecordKey, ctr.EnabledOperationId, new List<IntegrationEndpointConfigurationMemento>(), new List<FieldConfigurationMemento>(), null)));
            }


            return returnValue;
        }

        /// <inheritdoc/>
        public async Task CreateTransactionRegistryAsync(TransactionRegistry transactionRegistry)
        {
            //await this.GetEnabledOperations();
            bool transactional = transactionRegistry.ChildTransactions.Any();

            if (transactional)
            {
                using (var tr = await this.persistence.BeginTransaction())
                {
                    await this.InsertRoot(transactionRegistry);

                    foreach (var childTransactionRegistryEntity in transactionRegistry.ChildTransactions)
                    {
                        await this.persistence.InsertAsync(new TransactionRegistryEntity // "TransactionRegistry", "TransactionId",
                        {
                            CreatedDateTime = childTransactionRegistryEntity.CreatedDateTime,
                            BatchKey = transactionRegistry.RecordKey,
                            RecordKey = childTransactionRegistryEntity.RecordKey,
                            TransactionStatusId = (int)childTransactionRegistryEntity.Status,
                            EnabledOperationId = childTransactionRegistryEntity.EnabledOperationId,
                            Data = childTransactionRegistryEntity.Data,
                            UpdatedDateTime = null,
                            AppUser = transactionRegistry.UserName,
                            Message = null,
                            Details = null,
                            TransactionKey = childTransactionRegistryEntity.TransactionKey,
                            Summary = SerializationUtilities.DictionaryToXml(childTransactionRegistryEntity.Summary)
                        });
                    }

                    tr.Commit();
                }
            }
            else
            {
                await this.InsertRoot(transactionRegistry);
            }

            // Single insert, transaction scope removed





        }

        private async Task InsertRoot(TransactionRegistry transactionRegistry)
        {
            await this.persistence.InsertAsync(new  // "TransactionRegistry", "TransactionId",
            {
                CreatedDateTime = transactionRegistry.CreatedDateTime,
                RecordKey = transactionRegistry.RecordKey,
                TransactionStatusId = (int)transactionRegistry.Status,
                EnabledOperationId = transactionRegistry.EnabledOperationId,
                Data = transactionRegistry.Data,
                UpdatedDateTime = transactionRegistry.UpdatedDateTime,
                AppUser = transactionRegistry.UserName,
                Message = transactionRegistry.Message,
                Details = transactionRegistry.Details,
                TransactionKey = transactionRegistry.TransactionKey,
                Summary = SerializationUtilities.DictionaryToXml(transactionRegistry.Summary)
            }, "TransactionRegistry", "TransactionId");
        }

        /// <inheritdoc/>
        public async Task<List<EnabledOperationLookup>> GetEnabledOperations()
        {
            var select = @"
SELECT EO.*, A.*, C.*, O.* 
FROM EnabledOperation EO 
    INNER JOIN Application A ON A.ApplicationId = EO.ApplicationId 
    INNER JOIN Company C ON C.CompanyId = EO.CompanyId 
    INNER JOIN Operation O ON O.OperationId = EO.OperationId";
            //@select, transactionRegistry.ApplicationKey, transactionRegistry.CompanyCode, transactionRegistry.OperationKey
            return await this.persistence.ProjectionAsync<EnabledOperationEntity, ApplicationEntity, CompanyEntity, OperationEntity, EnabledOperationLookup>((eo, a, c, o) => new EnabledOperationLookup(c.ExternalCode, a.RecordKey, o.Name, eo.EnabledOperationId), select);
        }

        /// <inheritdoc/>
        public async Task<IMemento> GetRegistryEntry(Guid recordKey)
        {


            Func<TransactionRegistryEntity, ApplicationEntity, CompanyEntity, OperationEntity, TransactionRegistryMemento> callback = CreateRegistryEntryProjectionCallback(null);

            var selectChildren = @"
SELECT TR1.*, A.*, C.*, O.*
FROM TransactionRegistry TR INNER JOIN TransactionRegistry TR1 ON TR.RecordKey = TR1.BatchKey
    INNER JOIN EnabledOperation EO ON TR1.EnabledOperationId = EO.EnabledOperationId
    INNER JOIN Application A ON EO.ApplicationId = A.ApplicationId 
    INNER JOIN Company C ON EO.CompanyId = C.CompanyId
    INNER JOIN Operation O ON EO.OperationId = O.OperationId
WHERE TR.RecordKey = @0";

            var childTransactions = await this.persistence.ProjectionAsync(
                 callback,
                 selectChildren,
                 recordKey);

            var select = @"
SELECT TR.*, A.*, C.*, O.*
FROM TransactionRegistry TR 
    INNER JOIN EnabledOperation EO ON TR.EnabledOperationId = EO.EnabledOperationId
    INNER JOIN Application A ON EO.ApplicationId = A.ApplicationId 
    INNER JOIN Company C ON EO.CompanyId = C.CompanyId
    INNER JOIN Operation O ON EO.OperationId = O.OperationId
WHERE TR.RecordKey = @0";

            callback = CreateRegistryEntryProjectionCallback(childTransactions);

            var registryEntities = await this.persistence.ProjectionAsync(
                callback,
                select,
                recordKey);

            return registryEntities.SingleOrDefault();
        }

        private static Func<TransactionRegistryEntity, ApplicationEntity, CompanyEntity, OperationEntity, TransactionRegistryMemento> CreateRegistryEntryProjectionCallback(IEnumerable<TransactionRegistryMemento> childTransactions)
        {
            Func<TransactionRegistryEntity, ApplicationEntity, CompanyEntity, OperationEntity, TransactionRegistryMemento> callback = (tr, app, cmp, op) => new TransactionRegistryMemento(tr.TransactionId, tr.RecordKey, tr.TransactionKey, cmp.ExternalCode, app.RecordKey, op.Name, (TransactionStatusType)tr.TransactionStatusId, tr.AppUser, tr.CreatedDateTime, tr.UpdatedDateTime, tr.Data, tr.Message, tr.Details, tr.EnabledOperationId, childTransactions);
            return callback;
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
                transactionRegistry.RecordKey,
                new[] { "TransactionStatusId", "UpdatedDateTime", "AppUser", "Message", "Details" });
        }

        /// <inheritdoc/>
        public Task<string> GetTransactionDataAsync(Guid recordKey)
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

        private static FieldConfigurationMemento PrepareFieldConfiguration(FieldConfigurationEntryEntity fieldConfigurationEntry, IEnumerable<ValueMapEntryEntity> valueMapEntries, IEnumerable<ValueMapEntity> valueMaps, int enabledOperationId, string operationKey)
        {
            Dictionary<string, object> valueMap = new Dictionary<string, object>();
            if (fieldConfigurationEntry.ValueMapId.HasValue)
            {
                var map = valueMapEntries.Where(s => s.ValueMapId == fieldConfigurationEntry.ValueMapId);
                if (map.Any())
                {
                    var mapType = valueMaps.Single(m => m.ValueMapId == map.First().ValueMapId);
                    foreach (var valueMapEntry in map)
                    {
                        valueMap.Add(valueMapEntry.RecordKey ?? string.Empty, Converters.ConvertFromString(valueMapEntry.Value, mapType.ValueType));
                    }
                }
            }

            return new FieldConfigurationMemento(fieldConfigurationEntry.FieldName, fieldConfigurationEntry.MapToName, fieldConfigurationEntry.HashSequence, fieldConfigurationEntry.KeySequence, valueMap, enabledOperationId, operationKey);
        }
    }
}