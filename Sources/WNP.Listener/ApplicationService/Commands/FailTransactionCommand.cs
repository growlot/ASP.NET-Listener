// //-----------------------------------------------------------------------
// <copyright file="FailTransactionCommand.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------
namespace AMSLLC.Listener.ApplicationService.Commands
{
    using Domain;

    public class FailTransactionCommand : ICommand
    {
        public string RecordKey { get; set; }

        public string Message { get; set; }

        public string Details { get; set; }
    }
}