//-----------------------------------------------------------------------
// <copyright file="ServiceFaultDetails.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Contract
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [DataContract]
    public class ServiceFaultDetails
    {
        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string DebugInfo { get; set; }
    }
}
