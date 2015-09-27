// //-----------------------------------------------------------------------
// // <copyright file="EndpointTriggerType.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Communication
{
    public enum EndpointTriggerType
    {
        Undefined = 0,
        Always = 1,
        Changed = 2,
        Unchanged = 3
    }
}