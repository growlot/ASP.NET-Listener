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



        public async Task<IMemento> GetExecutionContext(string transactionKey)
        {
            TransactionExecutionMemento returnValue = null;
            var protocols = await _persistence.GetListAsync<ProtocolTypeEntity>("SELECT * FROM ProtocolType");
            var valueMapEntries =
                await _persistence.GetListAsync<ValueMapEntryEntity>("SELECT * FROM ValueMapEntry");
            var valueMaps = await _persistence.GetListAsync<ValueMapEntity>("SELECT * FROM ValueMap");
            var fieldConfigurationEntries = await _persistence.GetListAsync<FieldConfigurationEntryEntity>("SELECT * FROM FieldConfigurationEntry");
            var tr = await _persistence.GetAsync<TransactionRegistryEntity>("SELECT * FROM TransactionRegistry WHERE [Key] = @0", transactionKey);
            var endpoints = await _persistence.ProjectionAsync<EndpointEntity, OperationEndpointEntity, IntegrationEndpointConfigurationMemento>((ee, oe) => new IntegrationEndpointConfigurationMemento(protocols.Single(s => s.ProtocolTypeId == ee.ProtocolTypeId).Key, ee.ConnectionCfgJson,
                                (EndpointTriggerType)ee.EndpointTriggerTypeId, PrepareFieldConfiguration(ee.FieldConfigurationId, fieldConfigurationEntries, valueMapEntries, valueMaps)), @"SELECT 
	E.*, OE.*
FROM 
	[Endpoint] E 
	LEFT JOIN FieldConfiguration FC ON E.FieldConfigurationId = FC.FieldConfigurationId 
	INNER JOIN OperationEndpoint OE ON E.EndpointId = OE.EndpointId
	INNER JOIN EnabledOperation EO ON OE.EnabledOperationId = EO.EnabledOperationId
	INNER JOIN TransactionRegistry TR ON TR.EnabledOperationId = EO.EnabledOperationId
WHERE TR.[Key] = @0", transactionKey);
            if (endpoints != null)
            {
                returnValue = new TransactionExecutionMemento(tr.TransactionId, transactionKey, tr.EnabledOperationId, endpoints);
            }
            return returnValue;
        }

        public async Task Create(TransactionRegistry transactionRegistry)
        {

            var enabledOperationid =
                await
                    _persistence.ExecuteScalarAsync<int>(
                        "SELECT EO.EnabledOperationId FROM EnabledOperation EO INNER JOIN [Application] A ON A.ApplicationId = EO.ApplicationId INNER JOIN Company C ON C.CompanyId = EO.CompanyId INNER JOIN Operation O ON O.OperationId = EO.OperationId WHERE A.[Key] = @0 AND C.[ExternalCode] = @1 AND O.[Key] = @2",
                        transactionRegistry.ApplicationKey, transactionRegistry.CompanyCode,
                        transactionRegistry.OperationKey);

            //Single insert, transaction scope removed
            await
                _persistence.InsertAsync(new TransactionRegistryEntity //"TransactionRegistry", "TransactionId", 
                {
                    CreatedDateTime = transactionRegistry.CreatedDateTime,
                    Key = transactionRegistry.TransactionKey,
                    TransactionStatusId = (int)transactionRegistry.Status,
                    EnabledOperationId = enabledOperationid,
                    Header = JsonConvert.SerializeObject(transactionRegistry.Header),
                    Data = transactionRegistry.Data,
                    UpdatedDateTime = transactionRegistry.UpdatedDateTime,
                    User = transactionRegistry.UserName,
                    Message = transactionRegistry.Message,
                    Details = transactionRegistry.Details,
                    Summary = SerializationUtilities.DictionaryToXml(transactionRegistry.Summary)
                });
        }

        public async Task<IMemento> GetRegistryEntry(string transactionKey)
        {
            Func<TransactionRegistryEntity, ApplicationEntity, CompanyEntity, OperationEntity, TransactionRegistryMemento> callback = (tr, app,
                 cmp, op) => new TransactionRegistryMemento(tr.TransactionId, tr.Key, cmp.ExternalCode, app.Key, op.Key,
                     (TransactionStatusType)tr.TransactionStatusId, tr.User, JsonConvert.DeserializeObject<Dictionary<string, object>>(tr.Header), tr.CreatedDateTime,
                     tr.UpdatedDateTime, tr.Data, tr.Message, tr.Details);

            var registryEntities = await
                    _persistence.ProjectionAsync(callback,
                        @"SELECT TR.*, A.*, C.*, O.*
FROM TransactionRegistry TR 
INNER JOIN EnabledOperation EO ON TR.EnabledOperationId = EO.EnabledOperationId
INNER JOIN Application A ON EO.ApplicationId = A.ApplicationId 
INNER JOIN Company C ON EO.CompanyId = C.CompanyId
INNER JOIN Operation O ON EO.OperationId = O.OperationId
WHERE TR.[Key] = @0", transactionKey);

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
                    User = transactionRegistry.UserName,
                    Message = transactionRegistry.Message,
                    Details = transactionRegistry.Details
                }, transactionRegistry.Id,
                    new[] { "TransactionStatusId", "UpdatedDateTime", "User", "Message", "Details" });
        }

        public Task<string> GetTransactionData(string transactionKey)
        {
            return _persistence.ExecuteScalarAsync<string>("SELECT Data FROM TransactionRegistry WHERE [Key] = @0", transactionKey);
        }

        public Task<string> GetTransactionHeader(string transactionKey)
        {
            return _persistence.ExecuteScalarAsync<string>("SELECT Header FROM TransactionRegistry WHERE [Key] = @0", transactionKey);
        }

        public Task<int> GetHashCount(int enabledOperationId, string hash)
        {
            return _persistence.ExecuteScalarAsync<int>("SELECT COUNT(TransactionHash) FROM TransactionRegistry WHERE [EnabledOperationId] = @0 AND [TransactionHash] = @1", enabledOperationId, hash);
        }

        private IEnumerable<FieldConfigurationMemento> PrepareFieldConfiguration(int? fieldConfigurationId, IEnumerable<FieldConfigurationEntryEntity> fieldConfigurationEntries, IEnumerable<ValueMapEntryEntity> valueMapEntries, IEnumerable<ValueMapEntity> valueMaps)
        {
            if (!fieldConfigurationId.HasValue)
                return null;

            List<FieldConfigurationMemento> returnValue = new List<FieldConfigurationMemento>();
            var fields = fieldConfigurationEntries.Where(s => s.FieldConfigurationId == fieldConfigurationId.Value);


            foreach (var fieldConfigurationEntry in fields)
            {
                Dictionary<string, object> valueMap = new Dictionary<string, object>();
                if (fieldConfigurationEntry.ValueMapId.HasValue)
                {
                    var map = valueMapEntries.Where(s => s.ValueMapId == fieldConfigurationId.Value);
                    if (!map.Any())
                    {
                        break;
                    }
                    var mapType = valueMaps.Single(m => m.ValueMapId == map.First().ValueMapId);
                    foreach (var valueMapEntry in map)
                    {
                        valueMap.Add(valueMapEntry.Key ?? string.Empty, ValueConverter.Convert(valueMapEntry.Value, mapType.ValueType));
                    }
                }
                returnValue.Add(new FieldConfigurationMemento(fieldConfigurationEntry.FieldName, fieldConfigurationEntry.MapToName, fieldConfigurationEntry.IncludeInHash, valueMap));
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