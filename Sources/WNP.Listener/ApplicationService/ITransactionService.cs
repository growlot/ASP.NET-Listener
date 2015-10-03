// //-----------------------------------------------------------------------
// // <copyright file="ITransactionService.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService
{
    using System;
    using System.Threading.Tasks;
    using Communication;

    public interface ITransactionService
    {
        Task<string> Open(OpenTransactionRequestMessage requestMessage);
        Task Process<TMessageData>(ProcessTransactionRequestMessage requestMessage);
        Task Success(TransactionSuccessMessage transactionSuccessMessage);
        Task Failed(TransactionFailedMessage transactionFailedMessage);
    }
}