// //-----------------------------------------------------------------------
// // <copyright file="ITransactionService.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService
{
    using System.Threading.Tasks;
    using Communication;

    public interface ITransactionService
    {
        Task Open(OpenTransactionRequestMessage requestMessage);
        Task Process(ProcessTransactionRequestMessage requestMessage);
        Task Success(TransactionSuccessMessage transactionSuccessMessage);
        Task Failed(TransactionFailedMessage transactionFailedMessage);
    }
}