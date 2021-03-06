//-----------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the
// Code Analysis results, point to "Suppress Message", and click
// "In Suppression File".
// You do not need to add suppressions to this file manually.
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "AMSLLC.Listener.Service.Implementation.Alliant", Justification = "It's an assembly containing customer specific implementations and might contain fewer types")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "System.Boolean.TryParse(System.String,System.Boolean@)", Scope = "member", Target = "AMSLLC.Listener.Service.Implementation.Alliant.CustomService.#OnGetDevice(AMSLLC.Listener.Service.Contract.GetDeviceServiceRequest,AMSLLC.Listener.Common.Model.Device)", Justification = "It's a temporary feature needed for testing, and in case of failure to parse it returns expected value false")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "System.Boolean.TryParse(System.String,System.Boolean@)", Scope = "member", Target = "AMSLLC.Listener.Service.Implementation.Alliant.CustomService.#OnSendTestData(AMSLLC.Listener.Service.Contract.SendTestDataServiceRequest,AMSLLC.Listener.Common.Model.Device,AMSLLC.Listener.Common.Model.DeviceTest)", Justification = "It's a temporary feature needed for testing, and in case of failure to parse it returns expected value false")]