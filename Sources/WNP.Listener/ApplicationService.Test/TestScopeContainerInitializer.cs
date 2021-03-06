﻿// <copyright file="TestScopeContainerInitializer.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.Test
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Core;
    using Domain;
    using Domain.Listener.Transaction;
    using Ninject;
    using Repository;
    using Repository.Listener;

    public class TestScopeContainerInitializer : IDependencyInjectionModule
    {
        /// <summary>
        /// Initializes the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        public void Initialize(
            object container)
        {
            var kernel = (StandardKernel)container;
            kernel.Bind<IRepositoryManager>().To<RepositoryManager>();
            kernel.Bind<ITransactionRepository>().To<TestTransactionRepository>();
        }
    }

    public class AppServiceTestContainerInitializer : IDependencyInjectionModule
    {
        private ITransactionRepository rep;

        public AppServiceTestContainerInitializer(ITransactionRepository transactionRepository)
        {
            this.rep = transactionRepository;
        }

        public void Initialize(
            object container)
        {
            var kernel = (StandardKernel)container;
            kernel.Bind<IRepositoryManager>().To<RepositoryManager>();
            kernel.Bind<ITransactionRepository>().ToConstant(this.rep);
        }
    }

    public class TestTransactionRepository : ITransactionRepository
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public Task CreateAsync(
            TransactionExecution transaction)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(
            TransactionExecution transaction)
        {
            throw new NotImplementedException();
        }

        public Task<IMemento> GetExecutionContextAsync(
            Guid recordKey)
        {
            return Task.FromResult((IMemento)null);
            // throw new NotImplementedException();
        }

        public Task CreateTransactionRegistryAsync(
            TransactionRegistry transactionRegistry)
        {
            throw new NotImplementedException();
        }

        public Task<IMemento> GetRegistryEntry(
            Guid recordKey)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTransactionRegistryAsync(
            TransactionRegistry transactionRegistry)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetHashCountAsync(
            int enabledOperationId,
            string hash)
        {
            throw new NotImplementedException();
        }

        public Task UpdateHashAsync(
            Dictionary<Guid, string> hashCodes)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<string, IEnumerable<IMemento>>> GetFieldConfigurationsAsync(
            string companyCode,
            string sourceApplicationKey)
        {
            throw new NotImplementedException();
        }

        public Task<List<EntityOperationLookup>> GetEnabledEntityOperations()
        {
            throw new NotImplementedException();
        }

        public Task UpdateTransactionRegistryBulkAsync(
            Collection<TransactionRegistry> modifiedRegistries)
        {
            throw new NotImplementedException();
        }
    }
}
