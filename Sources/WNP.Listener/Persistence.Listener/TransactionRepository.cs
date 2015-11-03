// <copyright file="TransactionRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.Listener
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Dynamic;
    using System.Linq;
    using System.Threading.Tasks;
    using Communication;
    using Core;
    using Domain;
    using Domain.Listener.Transaction;
    using Models;
    using Newtonsoft.Json;
    using Repository;
    using Utilities;

    /// <summary>
    /// Implements <see cref="ITransactionRepository"/> for AsyncPoco
    /// </summary>
    [WithinListenerContext]
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

        /// <inheritdoc/>
        public Task CreateAsync(
            TransactionExecution transaction)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task UpdateAsync(
            TransactionExecution transaction)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task UpdateHashAsync(
            Dictionary<Guid, string> hashCodes)
        {
            // Single update, transaction scope removed
            // await
            //    _persistence.UpdateAsync("TransactionRegistry", "TransactionId", new TransactionRegistryEntity()
            //    {
            //        TransactionHash = hash
            //    }, new[] { "TransactionHash" });
            if (hashCodes.Any())
            {
                if (hashCodes.Count == 1)
                {
                    await this.UpdateHashCode(hashCodes.First());
                }
                else
                {
                    using (var tr = await this.persistence.BeginTransaction())
                    {
                        foreach (var hashCode in hashCodes)
                        {
                            await this.UpdateHashCode(hashCode);
                        }
                        tr.Commit();
                    }
                }
            }
        }

        /// <inheritdoc/>
        public async Task<Dictionary<string, IEnumerable<IMemento>>> GetFieldConfigurationsAsync(
            string companyCode,
            string sourceApplicationKey)
        {
            var valueMapEntries =
                await this.persistence.GetListAsync<ValueMapEntryEntity>("SELECT * FROM ValueMapEntry");
            var valueMaps = await this.persistence.GetListAsync<ValueMapEntity>("SELECT * FROM ValueMap");

            var select =
                @"SELECT FCE.*, O.*, ECO.* FROM FieldConfigurationEntry FCE 
INNER JOIN FieldConfiguration FC ON FCE.FieldConfigurationId = FC.FieldConfigurationId 
INNER JOIN EntityCategoryOperation ECO ON FC.FieldConfigurationId = ECO.FieldConfigurationId
INNER JOIN EnabledOperation EO ON EO.EnabledOperationId = ECO.EnabledOperationId 
INNER JOIN Application A ON EO.ApplicationId = A.ApplicationId 
INNER JOIN Company C ON EO.CompanyId = C.CompanyId
INNER JOIN Operation O ON EO.OperationId = O.OperationId WHERE C.ExternalCode = @0 AND A.RecordKey = @1";

            var fieldConfigurationEntries =
                await this.persistence.ProjectionAsync(
                    (FieldConfigurationEntryEntity ent,
                        OperationEntity operation,
                        EntityCategoryOperationEntity entityCategoryOperation) =>
                        PrepareFieldConfiguration(
                            ent,
                            valueMapEntries,
                            valueMaps,
                            entityCategoryOperation.EntityCategoryOperationId,
                            operation.Name),
                    select,
                    companyCode,
                    sourceApplicationKey); //.GetListAsync<FieldConfigurationEntryEntity>(
            //);

            return fieldConfigurationEntries.GroupBy(o => o.OperationKey)
                .ToDictionary(g => g.Key, g => g.ToList().Cast<IMemento>()); //;
        }

        /// <inheritdoc/>
        public async Task<IMemento> GetExecutionContextAsync(
            Guid recordKey)
        {
            TransactionExecutionMemento returnValue = null;
            var protocols = await this.persistence.GetListAsync<ProtocolTypeEntity>("SELECT * FROM ProtocolType");
            var valueMapEntries =
                await this.persistence.GetListAsync<ValueMapEntryEntity>("SELECT * FROM ValueMapEntry");
            var valueMaps = await this.persistence.GetListAsync<ValueMapEntity>("SELECT * FROM ValueMap");

            // var fieldConfigurationEntries = await _persistence.GetListAsync<FieldConfigurationEntryEntity>("SELECT * FROM FieldConfigurationEntry");

            var tr =
                await
                    this.persistence.GetAsync<TransactionRegistryEntity>(
                        "SELECT * FROM TransactionRegistry WHERE RecordKey = @0",
                        recordKey);

            var childTr =
                await
                    this.persistence.GetListAsync<TransactionRegistryEntity>(
                        "SELECT TR1.* FROM TransactionRegistry TR INNER JOIN TransactionRegistry TR1 ON TR.RecordKey = TR1.BatchKey WHERE TR.RecordKey = @0",
                        false,
                        recordKey);
            var entityCategoryOperations = childTr.Select(s => s.EntityCategoryOperationId).ToList();
            entityCategoryOperations.Add(tr.EntityCategoryOperationId);

            var recordKeys = childTr.Select(s => s.RecordKey).ToList();
            if (!recordKeys.Any())
            {
                recordKeys.Add(recordKey);
            }

            var transactionKeys = childTr.Select(s => s.IncomingHash).ToList();
            //if (!transactionKeys.Any())
            //{
            transactionKeys.Add(tr.IncomingHash);
            //}

            Func<EndpointEntity, OperationEndpointEntity, EntityCategoryOperationEntity, IECWrapper> callback = (ee,
                oe,
                eco) =>
                new IECWrapper(
                    new IntegrationEndpointConfigurationMemento(
                        protocols.Single(s => s.ProtocolTypeId == ee.ProtocolTypeId).Name,
                        ee.ConnectionConfiguration,
                        ee.AdapterConfiguration,
                        (EndpointTriggerType)ee.EndpointTriggerTypeId),
                    eco.EntityCategoryOperationId);

            var select = @"
SELECT DISTINCT E.*, OE.*, ECO.*
FROM 
	Endpoint E 
	INNER JOIN OperationEndpoint OE ON E.EndpointId = OE.EndpointId
	INNER JOIN EntityCategoryOperation ECO ON ECO.EntityCategoryOperationId = OE.EntityCategoryOperationId
	INNER JOIN TransactionRegistry TR ON TR.EntityCategoryOperationId = ECO.EntityCategoryOperationId
    LEFT JOIN FieldConfiguration FC ON ECO.FieldConfigurationId = FC.FieldConfigurationId 
WHERE TR.RecordKey IN (@records)";

            var endpoints = await this.persistence.ProjectionAsync(
                callback,
                select,
                new
                {
                    records = recordKeys.ToArray()
                });

            select = @"
SELECT DISTINCT FCE.*, ECO.*, O.*
FROM FieldConfigurationEntry FCE 
    INNER JOIN FieldConfiguration FC ON FCE.FieldConfigurationId = FC.FieldConfigurationId 
    INNER JOIN EntityCategoryOperation ECO ON FC.FieldConfigurationId = ECO.FieldConfigurationId
    INNER JOIN EnabledOperation EO ON EO.EnabledOperationId = ECO.EnabledOperationId 
    INNER JOIN Operation O ON EO.OperationId = O.OperationId
WHERE ECO.EntityCategoryOperationId IN (@operations)";

            //var fieldConfigurationEntries = await this.persistence.ProjectionAsync<FieldConfigurationEntryEntity>(select, new { operations = enabledOperations.Distinct().ToArray() });
            var fieldConfigurationEntries =
                await this.persistence.ProjectionAsync(
                    (FieldConfigurationEntryEntity ent,
                        EntityCategoryOperationEntity enabledOperation,
                        OperationEntity operation) =>
                        PrepareFieldConfiguration(
                            ent,
                            valueMapEntries,
                            valueMaps,
                            enabledOperation.EntityCategoryOperationId,
                            operation.Name),
                    select,
                    new
                    {
                        operations = entityCategoryOperations.Distinct().ToArray()
                    });

            if (endpoints != null)
            {
                select =
                    @"SELECT IncomingHash, RecordKey FROM TransactionRegistry WHERE IncomingHash IN (@transactionKeys) AND TransactionStatusId IN (@successStatus)";

                var duplicates = await this.persistence.GetListAsync<dynamic>(
                    select,
                    false,
                    new
                    {
                        transactionKeys = transactionKeys.ToArray(),
                        successStatus = new[]
                        {
                            (int)TransactionStatusType.Success
                        }
                    });

                returnValue = new TransactionExecutionMemento(
                    tr.TransactionId,
                    recordKey,
                    tr.EntityCategoryOperationId,
                    endpoints.Where(e => e.EntityCategoryOperationId == tr.EntityCategoryOperationId)
                        .Select(s => s.IntegrationEndpointConfigurationMemento),
                    fieldConfigurationEntries.Where(fc => fc.EntityCategoryOperationId == tr.EntityCategoryOperationId),
                    childTr.Select(
                        ctr =>
                            new TransactionExecutionMemento(
                                ctr.TransactionId,
                                ctr.RecordKey,
                                ctr.EntityCategoryOperationId,
                                endpoints.Where(e => e.EntityCategoryOperationId == ctr.EntityCategoryOperationId)
                                    .Select(s => s.IntegrationEndpointConfigurationMemento),
                                fieldConfigurationEntries.Where(
                                    fc => fc.EntityCategoryOperationId == ctr.EntityCategoryOperationId),
                                null,
                                string.IsNullOrWhiteSpace(ctr.Data)
                                    ? null
                                    : JsonConvert.DeserializeObject<ExpandoObject>(ctr.Data),
                                duplicates.Where(
                                    d =>
                                        string.Compare(
                                            (string)d.IncomingHash,
                                            ctr.IncomingHash,
                                            StringComparison.InvariantCulture) == 0 &&
                                        (Guid)d.RecordKey != ctr.RecordKey).Select(s => (Guid)s.RecordKey),
                                (TransactionStatusType)ctr.TransactionStatusId)),
                    string.IsNullOrWhiteSpace(tr.Data) ? null : JsonConvert.DeserializeObject<ExpandoObject>(tr.Data),
                    duplicates.Where(
                        d =>
                            string.Compare(
                                (string)d.IncomingHash,
                                tr.IncomingHash,
                                StringComparison.InvariantCulture) == 0 && (Guid)d.RecordKey != tr.RecordKey)
                        .Select(s => (Guid)s.RecordKey),
                    (TransactionStatusType)tr.TransactionStatusId);
            }

            return returnValue;
        }

        /// <inheritdoc/>
        public async Task CreateTransactionRegistryAsync(
            TransactionRegistry transactionRegistry)
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
                        await this.persistence.InsertAsync(
                            new // "TransactionRegistry", "TransactionId",
                            {
                                CreatedDateTime = childTransactionRegistryEntity.CreatedDateTime,
                                BatchKey = transactionRegistry.RecordKey,
                                RecordKey = childTransactionRegistryEntity.RecordKey,
                                TransactionStatusId = (int)childTransactionRegistryEntity.Status,
                                EntityCategoryOperationId = childTransactionRegistryEntity.EntityCategoryOperationId,
                                Data = childTransactionRegistryEntity.Data,
                                AppUser = transactionRegistry.UserName,
                                IncomingHash = childTransactionRegistryEntity.IncomingHash,
                                Summary =
                                    SerializationUtilities.DictionaryToXml(childTransactionRegistryEntity.Summary)
                            },
                            "TransactionRegistry",
                            "TransactionId");
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

        /// <inheritdoc/>
        public async Task<List<EntityOperationLookup>> GetEnabledEntityOperations()
        {
            var select = @"
SELECT ECO.*, EC.*, A.*, C.*, O.* 
FROM
    EntityCategoryOperation ECO  
    INNER JOIN EntityCategory EC ON ECO.EntityCategoryId = EC.EntityCategoryId
    INNER JOIN EnabledOperation EO ON ECO.EnabledOperationId = EO.EnabledOperationId
    INNER JOIN Application A ON A.ApplicationId = EO.ApplicationId 
    INNER JOIN Company C ON C.CompanyId = EO.CompanyId 
    INNER JOIN Operation O ON O.OperationId = EO.OperationId";
            //@select, transactionRegistry.ApplicationKey, transactionRegistry.CompanyCode, transactionRegistry.OperationKey
            return
                await
                    this.persistence
                        .ProjectionAsync
                        <EntityCategoryOperationEntity, EntityCategoryEntity, ApplicationEntity, CompanyEntity, OperationEntity,
                            EntityOperationLookup>(
                                (eco,
                                ec,
                                    a,
                                    c,
                                    o) =>
                                    new EntityOperationLookup(
                                        c.ExternalCode,
                                        a.RecordKey,
                                        o.Name,
                                        ec.Name,
                                        eco.EnabledOperationId),
                                select);
        }

        /// <inheritdoc/>
        public async Task<IMemento> GetRegistryEntry(
            Guid recordKey)
        {
            Func
                <TransactionRegistryEntity, ApplicationEntity, CompanyEntity, OperationEntity,
                    TransactionRegistryMemento> callback = CreateRegistryEntryProjectionCallback(null);

            var selectChildren = @"
SELECT TR.*, A.*, C.*, O.*
    FROM TransactionRegistry TR
    INNER JOIN EntityCategoryOperation ECO ON TR.EntityCategoryOperationId = ECO.EntityCategoryOperationId
    INNER JOIN EnabledOperation EO ON ECO.EnabledOperationId = EO.EnabledOperationId
    INNER JOIN Application A ON EO.ApplicationId = A.ApplicationId 
    INNER JOIN Company C ON EO.CompanyId = C.CompanyId
    INNER JOIN Operation O ON EO.OperationId = O.OperationId
WHERE TR.BatchKey = @0";

            var childTransactions = await this.persistence.ProjectionAsync(callback, selectChildren, recordKey);

            var select = @"
SELECT TR.*, A.*, C.*, O.*
FROM TransactionRegistry TR 
    INNER JOIN EntityCategoryOperation ECO ON TR.EntityCategoryOperationId = ECO.EntityCategoryOperationId
    INNER JOIN EnabledOperation EO ON ECO.EnabledOperationId = EO.EnabledOperationId
    INNER JOIN Application A ON EO.ApplicationId = A.ApplicationId 
    INNER JOIN Company C ON EO.CompanyId = C.CompanyId
    INNER JOIN Operation O ON EO.OperationId = O.OperationId
WHERE TR.RecordKey = @0";

            callback = CreateRegistryEntryProjectionCallback(childTransactions);

            var registryEntities = await this.persistence.ProjectionAsync(callback, select, recordKey);

            return registryEntities.SingleOrDefault();
        }

        /// <inheritdoc/>
        public async Task UpdateTransactionRegistryBulkAsync(
            Collection<TransactionRegistry> modifiedRegistries)
        {
            if (modifiedRegistries.Count == 1)
            {
                var transactionRegistry = modifiedRegistries.Single();
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
                    new[]
                    {
                        "TransactionStatusId",
                        "UpdatedDateTime",
                        "AppUser",
                        "Message",
                        "Details"
                    });
            }
            else
            {
                using (var tr = await this.persistence.BeginTransaction())
                {
                    foreach (var transactionRegistry in modifiedRegistries)
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
                            new[]
                            {
                                "TransactionStatusId",
                                "UpdatedDateTime",
                                "AppUser",
                                "Message",
                                "Details"
                            });
                    }

                    //await this.InsertRoot(transactionRegistry);

                    //foreach (var childTransactionRegistryEntity in transactionRegistry.ChildTransactions)
                    //{
                    //    await this.persistence.InsertAsync(new // "TransactionRegistry", "TransactionId",
                    //    {
                    //        CreatedDateTime = childTransactionRegistryEntity.CreatedDateTime,
                    //        BatchKey = transactionRegistry.RecordKey,
                    //        RecordKey = childTransactionRegistryEntity.RecordKey,
                    //        TransactionStatusId = (int)childTransactionRegistryEntity.Status,
                    //        EnabledOperationId = childTransactionRegistryEntity.EnabledOperationId,
                    //        Data = childTransactionRegistryEntity.Data,
                    //        AppUser = transactionRegistry.UserName,
                    //        TransactionKey = childTransactionRegistryEntity.TransactionKey,
                    //        Summary = SerializationUtilities.DictionaryToXml(childTransactionRegistryEntity.Summary)
                    //    }, "TransactionRegistry", "TransactionId");
                    //}

                    tr.Commit();
                }
            }
        }

        /// <inheritdoc/>
        public async Task UpdateTransactionRegistryAsync(
            TransactionRegistry transactionRegistry)
        {
            if (!transactionRegistry.ChildTransactions.Any())
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
                    new[]
                    {
                        "TransactionStatusId",
                        "UpdatedDateTime",
                        "AppUser",
                        "Message",
                        "Details"
                    });
            }
            else
            {
                using (var tr = await this.persistence.BeginTransaction())
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
                        new[]
                        {
                            "TransactionStatusId",
                            "UpdatedDateTime",
                            "AppUser",
                            "Message",
                            "Details"
                        });

                    foreach (var childTransactionRegistryEntity in transactionRegistry.ChildTransactions)
                    {
                        var centity = new TransactionRegistryEntity
                        {
                            TransactionId = childTransactionRegistryEntity.Id,
                            TransactionStatusId = (int)childTransactionRegistryEntity.Status,
                            UpdatedDateTime = childTransactionRegistryEntity.UpdatedDateTime,
                            Message = childTransactionRegistryEntity.Message,
                            Details = childTransactionRegistryEntity.Details
                        };

                        await this.persistence.UpdateAsync(
                            centity,
                            childTransactionRegistryEntity.RecordKey,
                            new[]
                            {
                                "TransactionStatusId",
                                "UpdatedDateTime",
                                "Message",
                                "Details"
                            });
                    }

                    tr.Commit();
                }
            }
        }

        /// <inheritdoc/>
        public Task<int> GetHashCountAsync(
            int entityCategoryOperationId,
            string hash)
        {
            return
                this.persistence.ExecuteScalarAsync<int>(
                    "SELECT COUNT(OutgoingHash) FROM TransactionRegistry WHERE EntityCategoryOperationId = @0 AND OutgoingHash = @1",
                    entityCategoryOperationId,
                    hash);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="TransactionRepository" /> class.
        /// </summary>
        ~TransactionRepository()
        {
            // Finalizer calls Dispose(false)
            this.Dispose(false);
        }

        private Task UpdateHashCode(
            KeyValuePair<Guid, string> hashCode)
        {
            return this.persistence.UpdateAsync(
                new TransactionRegistryEntity
                {
                    OutgoingHash = hashCode.Value,
                    RecordKey = hashCode.Key
                },
                hashCode.Key,
                new[]
                {
                    "OutgoingHash"
                });
        }

        private Task InsertRoot(
            TransactionRegistry transactionRegistry)
        {
            return this.persistence.InsertAsync(
                new // "TransactionRegistry", "TransactionId",
                {
                    CreatedDateTime = transactionRegistry.CreatedDateTime,
                    RecordKey = transactionRegistry.RecordKey,
                    TransactionStatusId = (int)transactionRegistry.Status,
                    EntityCategoryOperationId = transactionRegistry.EntityCategoryOperationId,
                    Data = transactionRegistry.Data,
                    UpdatedDateTime = transactionRegistry.UpdatedDateTime,
                    AppUser = transactionRegistry.UserName,
                    Message = transactionRegistry.Message,
                    Details = transactionRegistry.Details,
                    IncomingHash = transactionRegistry.IncomingHash,
                    Summary = SerializationUtilities.DictionaryToXml(transactionRegistry.Summary)
                },
                "TransactionRegistry",
                "TransactionId");
        }

        private static
            Func
                <TransactionRegistryEntity, ApplicationEntity, CompanyEntity, OperationEntity,
                    TransactionRegistryMemento> CreateRegistryEntryProjectionCallback(
            IEnumerable<TransactionRegistryMemento> childTransactions)
        {
            Func
                <TransactionRegistryEntity, ApplicationEntity, CompanyEntity, OperationEntity,
                    TransactionRegistryMemento> callback = (tr,
                        app,
                        cmp,
                        op) =>
                        new TransactionRegistryMemento(
                            tr.TransactionId,
                            tr.RecordKey,
                            tr.Priority,
                            tr.IncomingHash,
                            cmp.ExternalCode,
                            app.RecordKey,
                            op.Name,
                            (TransactionStatusType)tr.TransactionStatusId,
                            tr.AppUser,
                            tr.CreatedDateTime,
                            tr.UpdatedDateTime,
                            tr.Data,
                            tr.Message,
                            tr.Details,
                            tr.EntityCategoryOperationId,
                            childTransactions);
            return callback;
        }

        /// <inheritdoc/>
        public Task<string> GetTransactionDataAsync(
            Guid recordKey)
        {
            return
                this.persistence.ExecuteScalarAsync<string>(
                    "SELECT Data FROM TransactionRegistry WHERE RecordKey = @0",
                    recordKey);
        }

        /// <summary>
        /// Disposes the specified disposing.
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        protected virtual void Dispose(
            bool disposing)
        {
            if (disposing)
            {
            }
        }

        private static FieldConfigurationMemento PrepareFieldConfiguration(
            FieldConfigurationEntryEntity fieldConfigurationEntry,
            IEnumerable<ValueMapEntryEntity> valueMapEntries,
            IEnumerable<ValueMapEntity> valueMaps,
            int entityCategoryOperationId,
            string operationKey)
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
                        valueMap.Add(
                            valueMapEntry.RecordKey ?? string.Empty,
                            Converters.ConvertFromString(valueMapEntry.Value, mapType.ValueType));
                    }
                }
            }

            return new FieldConfigurationMemento(
                fieldConfigurationEntry.FieldName,
                fieldConfigurationEntry.MapToName,
                fieldConfigurationEntry.OutgoingSequence,
                fieldConfigurationEntry.IncomingSequence,
                valueMap,
                entityCategoryOperationId,
                operationKey,
                fieldConfigurationEntry.IncludeInSummary);
        }
    }
}