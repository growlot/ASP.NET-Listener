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
    using AsyncPoco;
    using Communication;

    public class TransactionRepository : ITransactionRepository
    {
        private readonly ListenerDbContext _dbContext;

        public TransactionRepository(ListenerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Create(TransactionExecution transaction)
        {
            throw new NotImplementedException();
        }

        public Task Update(TransactionExecution transaction)
        {
            throw new NotImplementedException();
        }

        public async Task<IMemento> GetExecutionContext(string transactionKey)
        {
            TransactionExecutionMemento returnValue = null;
            var protocols = await _dbContext.PageAsync<ProtocolTypeEntity>(0, 1000, "SELECT * FROM ProtocolType");
            var valueMapEntries =
                await _dbContext.PageAsync<ValueMapEntryEntity>(0, int.MaxValue, "SELECT * FROM ValueMapEntry");
            var valueMaps = await _dbContext.PageAsync<ValueMapEntity>(0, int.MaxValue, "SELECT * FROM ValueMap");
            var fieldConfigurationEntries = await _dbContext.PageAsync<FieldConfigurationEntryEntity>(0, int.MaxValue, "SELECT * FROM FieldConfigurationEntry");

            var endpoints = await _dbContext.FetchAsync<EndpointEntity>(@"SELECT 
	E.*
FROM 
	[Endpoint] E 
	LEFT JOIN FieldConfiguration FC ON E.FieldConfigurationId = FC.FieldConfigurationId 
	INNER JOIN OperationEndpoint OE ON E.EndpointId = OE.EndpointId
	INNER JOIN EnabledOperation EO ON OE.EnabledOperationId = EO.EnabledOperationId
	INNER JOIN TransactionRegistry TR ON TR.CompanyId = EO.CompanyId AND TR.OperationId = EO.OperationId AND TR.ApplicationId = EO.ApplicationId
WHERE TR.[TransactionKey] = @0", transactionKey);
            if (endpoints != null)
            {
                List<IntegrationEndpointConfigurationMemento> configurations =
                    endpoints.Select(
                        endpoint =>
                            new IntegrationEndpointConfigurationMemento(protocols.Items.Single(s => s.ProtocolTypeId == endpoint.ProtocolTypeId).Key, endpoint.ConnectionCfgJson,
                                (Communication.EndpointTriggerType)endpoint.EndpointTriggerTypeId, PrepareFieldConfiguration(endpoint.FieldConfigurationId, fieldConfigurationEntries.Items, valueMapEntries.Items, valueMaps.Items))).ToList();

                returnValue = new TransactionExecutionMemento(transactionKey, configurations);
            }
            return returnValue;
        }

        public async Task Create(TransactionRegistry transactionRegistry)
        {
            var application =
                await _dbContext.SingleAsync<ApplicationEntity>("SELECT * FROM Application WHERE Key = @0", transactionRegistry.ApplicationKey);

            var operation = await _dbContext.SingleAsync<OperationEntity>("SELECT * FROM Operation WHERE Key = @0", transactionRegistry.OperationKey);

            var company = await _dbContext.SingleAsync<CompanyEntity>("SELECT * FROM Company WHERE ExternalCode = @0", transactionRegistry.CompanyCode);

            using (var tx = await _dbContext.GetTransactionAsync())
            {
                await
                    _dbContext.InsertAsync(new TransactionRegistryEntity
                    {
                        ApplicationId = application.ApplicationId,
                        CompanyId = company.CompanyId,
                        CreatedDateTime = transactionRegistry.CreatedDateTime,
                        Key = transactionRegistry.TransactionKey,
                        OperationId = operation.OperationId,
                        TransactionStatusId = (int)transactionRegistry.Status,
                        UpdatedDateTime = transactionRegistry.UpdatedDateTime,
                        User = transactionRegistry.UserName,
                        Message = transactionRegistry.Message,
                        Details = transactionRegistry.Details
                    });
                tx.Complete();
            }
        }

        public async Task<IMemento> GetRegistryEntry(string transactionKey)
        {
            var registryEntities =
                await
                    _dbContext.FetchAsync(
                        (TransactionRegistryEntity tr, ApplicationEntity app, CompanyEntity cmp, OperationEntity op) =>
                            new TransactionRegistryMemento(tr.Key, cmp.ExternalCode, app.Key, op.Key,
                                (TransactionStatusType)tr.TransactionStatusId, tr.User, tr.CreatedDateTime,
                                tr.UpdatedDateTime, tr.Data, tr.Message, tr.Details),
                        @"SELECT TR.*, A.*, C.*, O.*
FROM TransactionRegistry TR 
INNER JOIN Application A ON TR.ApplicationId = A.ApplicationId 
INNER JOIN Company C ON TR.CompanyId = C.CompanyId
INNER JOIN Operation O ON TR.OperationId = O.OperationId
WHERE TR.[Key] = @0", transactionKey);

            return registryEntities.SingleOrDefault();
        }

        public async Task Update(TransactionRegistry transactionRegistry)
        {
            using (var tx = await _dbContext.GetTransactionAsync())
            {
                await
                    _dbContext.UpdateAsync(new TransactionRegistryEntity
                    {
                        TransactionId = transactionRegistry.Id,
                        TransactionStatusId = (int)transactionRegistry.Status,
                        UpdatedDateTime = transactionRegistry.UpdatedDateTime,
                        User = transactionRegistry.UserName,
                        Message = transactionRegistry.Message,
                        Details = transactionRegistry.Details
                    });
                tx.Complete();
            }
        }

        public async Task<string> GetTransactionData(string transactionKey)
        {
            return await _dbContext.ExecuteScalarAsync<string>("SELECT Data FROM TransactionRegistry WHERE Key = @0", transactionKey);
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
                returnValue.Add(new FieldConfigurationMemento(fieldConfigurationEntry.FieldName, fieldConfigurationEntry.MapToName, valueMap));
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