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
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "AMSLLC.Listener.Service.Implementation", Justification = "More types will be added later during development")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "AMSLLC.Listener.Service.Implementation.FlatFile", Justification = "More types will be added later during development")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Scope = "member", Target = "AMSLLC.Listener.Service.Implementation.TransactionHelper.#ProcessMetersBatch(AMSLLC.Listener.Common.WNP.Model.NewBatch,AMSLLC.Listener.Common.Model.DeviceBatch,AMSLLC.Listener.Common.Model.Company,System.Action`4<AMSLLC.Listener.Common.Model.Device,AMSLLC.Listener.Common.WNP.Model.Meter,System.Int32,System.Boolean>,System.Action`4<AMSLLC.Listener.Common.Model.Device,AMSLLC.Listener.Common.Model.DeviceTest,AMSLLC.Listener.Common.WNP.Model.Meter,System.Int32>)", Justification = "Need to review later")]
