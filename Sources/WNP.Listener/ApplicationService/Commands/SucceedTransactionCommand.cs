// //-----------------------------------------------------------------------
// <copyright file="SucceedTransactionCommand.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------
namespace AMSLLC.Listener.ApplicationService.Commands
{
    using Domain;

    public class SucceedTransactionCommand : ICommand
    {
        public string RecordKey { get; set; }
    }
}