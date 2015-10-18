// //-----------------------------------------------------------------------
// // <copyright file="TransactionRepository.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Repository.Listener
{
    using System;
    using System.Threading.Tasks;
    using Domain;
    using Domain.Listener.Transaction;
    using Persistence.Listener;
    using System.Collections.Generic;
    using System.Linq;
    using Communication;
    using Core;
    using Newtonsoft.Json;
    using Utilities;

    public class TransactionRepository : ITransactionRepository
    {
        private readonly IPersistenceAdapter _persistence;

        public TransactionRepository(IPersistenceAdapter persistence)
        {
            _persistence = persistence;
        }

        public Task Create(TransactionExecution transaction)
        {
            throw new NotImplementedException();
        }

        public Task Update(TransactionExecution transaction)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateHash(int transactionId, string hash)
        {
            //Single update, transaction scope removed
            //await
            //    _persistence.UpdateAsync("TransactionRegistry", "TransactionId", new TransactionRegistryEntity()
            //    {
            //        TransactionHash = hash
            //    }, new[] { "TransactionHash" });

            await
                _persistence.UpdateAsync(new TransactionRegistryEntity { TransactionHash = hash, TransactionId = transactionId }, transactionId,
                    new[] { "TransactionHash" });
        }

        /// <summary>
        /// Gets the field configurations.
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="sourceApplicationKey">The source application key.</param>
        /// <param name="operationKey">The operation key.</param>
        /// <returns>Task&lt;IEnumerable&lt;IMemento&gt;&gt;.</returns>
        public async Task<IEnumerable<IMemento>> GetFieldConfigurations(string companyCode, string sourceApplicationKey, string operationKey)
        {
            var fieldConfigurationEntries = await _persistence.GetListAsync<FieldConfigurationEntryEntity>(
                       @"SELECT FCE.* FROM FieldConfigurationEntry FCE INNER JOIN FieldConfiguration FC ON FCE.FieldConfigurationId = FC.FieldConfigurationId INNER JOIN EnabledOperation EO ON EO.FieldConfigurationId = FC.FieldConfigurationId INNER JOIN Application A ON EO.ApplicationId = A.ApplicationId 
INNER JOIN Company C ON EO.CompanyId = C.CompanyId
INNER JOIN Operation O ON EO.OperationId = O.OperationId WHERE C.ExternalCode = @0 AND A.RecordKey = @1 AND O.Name = @2", companyCode, sourceApplicationKey, operationKey);
            var valueMapEntries = await _persistence.GetListAsync<ValueMapEntryEntity>("SELECT * FROM ValueMapEntry");
            var valueMaps = await _persistence.GetListAsync<ValueMapEntity>("SELECT * FROM ValueMap");

           return PrepareFieldConfiguration(fieldConfigurationEntries, valueMapEntries, valueMaps);
        }

        //        public Task<int> GetEnabledOperation(string companyCode, string sourceApplicationKey, string operationKey)
        //        {
        //            return _persistence.ExecuteScalarAsync<int>(@"SELECT EO.EnabledOperationId FROM EnabledOperation EO
        //INNER JOIN Application A ON EO.ApplicationId = A.ApplicationId
        //INNER JOIN Company C ON EO.CompanyId = C.CompanyId
        //INNER JOIN Operation O ON EO.OperationId = O.OperationId WHERE C.ExternalCode = @0 AND A.RecordKey = @1 AND O.Name = @2", companyCode, sourceApplicationKey, operationKey);
        //        }


        public async Task<IMemento> GetExecutionContext(string recordKey)
        {
            TransactionExecutionMemento returnValue = null;
            var protocols = await _persistence.GetListAsync<ProtocolTypeEntity>("SELECT * FROM ProtocolType");
            var valueMapEntries =
                await _persistence.GetListAsync<ValueMapEntryEntity>("SELECT * FROM ValueMapEntry");
            var valueMaps = await _persistence.GetListAsync<ValueMapEntity>("SELECT * FROM ValueMap");
            //var fieldConfigurationEntries = await _persistence.GetListAsync<FieldConfigurationEntryEntity>("SELECT * FROM FieldConfigurationEntry");
            var tr = await _persistence.GetAsync<TransactionRegistryEntity>("SELECT * FROM TransactionRegistry WHERE RecordKey = @0", recordKey);
            //
            var endpoints = await _persistence.ProjectionAsync<EndpointEntity, OperationEndpointEntity, EnabledOperationEntity, IntegrationEndpointConfigurationMemento>((ee, oe, eo) => new IntegrationEndpointConfigurationMemento(protocols.Single(s => s.ProtocolTypeId == ee.ProtocolTypeId).Name, ee.ConnectionConfiguration,
                                 (EndpointTriggerType)ee.EndpointTriggerTypeId), @"SELECT 
	E.*, OE.*, EO.*
FROM 
	Endpoint E 
	INNER JOIN OperationEndpoint OE ON E.EndpointId = OE.EndpointId
	INNER JOIN EnabledOperation EO ON OE.EnabledOperationId = EO.EnabledOperationId
	INNER JOIN TransactionRegistry TR ON TR.EnabledOperationId = EO.EnabledOperationId
    LEFT JOIN FieldConfiguration FC ON EO.FieldConfigurationId = FC.FieldConfigurationId 
WHERE TR.RecordKey = @0", recordKey);


            var fieldConfigurationEntries =
                await
                    _persistence.GetListAsync<FieldConfigurationEntryEntity>(
                        "SELECT FCE.* FROM FieldConfigurationEntry FCE INNER JOIN FieldConfiguration FC ON FCE.FieldConfigurationId = FC.FieldConfigurationId INNER JOIN EnabledOperation EO ON EO.FieldConfigurationId = FC.FieldConfigurationId WHERE EO.EnabledOperationId = @0", tr.EnabledOperationId);

            if (endpoints != null)
            {
                returnValue = new TransactionExecutionMemento(tr.TransactionId, recordKey, tr.EnabledOperationId, endpoints, PrepareFieldConfiguration(fieldConfigurationEntries, valueMapEntries, valueMaps));
            }
            return returnValue;
        }

        public async Task Create(TransactionRegistry transactionRegistry)
        {

            var enabledOperationid =
                await
                    _persistence.ExecuteScalarAsync<int>(
                        "SELECT EO.EnabledOperationId FROM EnabledOperation EO INNER JOIN Application A ON A.ApplicationId = EO.ApplicationId INNER JOIN Company C ON C.CompanyId = EO.CompanyId INNER JOIN Operation O ON O.OperationId = EO.OperationId WHERE A.RecordKey = @0 AND C.ExternalCode = @1 AND O.Name = @2",
                        transactionRegistry.ApplicationKey, transactionRegistry.CompanyCode,
                        transactionRegistry.OperationKey);

            //Single insert, transaction scope removed
            await
                _persistence.InsertAsync(new TransactionRegistryEntity //"TransactionRegistry", "TransactionId", 
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

        public async Task<IMemento> GetRegistryEntry(string recordKey)
        {
            Func<TransactionRegistryEntity, ApplicationEntity, CompanyEntity, OperationEntity, TransactionRegistryMemento> callback = (tr, app,
                 cmp, op) => new TransactionRegistryMemento(tr.TransactionId, tr.RecordKey, tr.TransactionKey, cmp.ExternalCode, app.RecordKey, op.Name,
                     (TransactionStatusType)tr.TransactionStatusId, tr.AppUser, tr.CreatedDateTime,
                     tr.UpdatedDateTime, tr.Data, tr.Message, tr.Details);

            var registryEntities = await
                    _persistence.ProjectionAsync(callback,
                        @"SELECT TR.*, A.*, C.*, O.*
FROM TransactionRegistry TR 
INNER JOIN EnabledOperation EO ON TR.EnabledOperationId = EO.EnabledOperationId
INNER JOIN Application A ON EO.ApplicationId = A.ApplicationId 
INNER JOIN Company C ON EO.CompanyId = C.CompanyId
INNER JOIN Operation O ON EO.OperationId = O.OperationId
WHERE TR.RecordKey = @0", recordKey);

            return registryEntities.SingleOrDefault();
        }

        public async Task Update(TransactionRegistry transactionRegistry)
        {
            await
                _persistence.UpdateAsync(new TransactionRegistryEntity
                {
                    TransactionId = transactionRegistry.Id,
                    TransactionStatusId = (int)transactionRegistry.Status,
                    UpdatedDateTime = transactionRegistry.UpdatedDateTime,
                    AppUser = transactionRegistry.UserName,
                    Message = transactionRegistry.Message,
                    Details = transactionRegistry.Details
                }, transactionRegistry.Id,
                    new[] { "TransactionStatusId", "UpdatedDateTime", "AppUser", "Message", "Details" });
        }

        public Task<string> GetTransactionData(string recordKey)
        {
            return _persistence.ExecuteScalarAsync<string>("SELECT Data FROM TransactionRegistry WHERE RecordKey = @0", recordKey);
        }

        public Task<int> GetHashCount(int enabledOperationId, string hash)
        {
            return _persistence.ExecuteScalarAsync<int>("SELECT COUNT(TransactionHash) FROM TransactionRegistry WHERE EnabledOperationId = @0 AND TransactionHash = @1", enabledOperationId, hash);
        }

        private IEnumerable<FieldConfigurationMemento> PrepareFieldConfiguration(IEnumerable<FieldConfigurationEntryEntity> fieldConfigurationEntries, IEnumerable<ValueMapEntryEntity> valueMapEntries, IEnumerable<ValueMapEntity> valueMaps)
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
                        valueMap.Add(valueMapEntry.RecordKey ?? string.Empty, ValueConverter.Convert(valueMapEntry.Value, mapType.ValueType));
                    }
                }
                returnValue.Add(new FieldConfigurationMemento(fieldConfigurationEntry.FieldName, fieldConfigurationEntry.MapToName, fieldConfigurationEntry.HashSequence, fieldConfigurationEntry.KeySequence, valueMap));
            }

            return returnValue;
        }




        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Finalizes an instance of the <see cref="TransactionRepository" /> class.
        /// </summary>
        ~TransactionRepository()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
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
    }
}