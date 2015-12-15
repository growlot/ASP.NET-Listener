// <copyright file="BatchRequestMessage.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Model
{
    using System.Collections.Generic;

    public class BatchRequestMessage
    {
        public string BatchNumber { get; set; }
        public List<object> Body { get; set; } = new List<object>();
    }
}