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
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Asset", Scope = "member", Target = "AMSLLC.Listener.Service.Implementation.KCPL.ITransactionResponse.#AssetUpdate(System.ServiceModel.Channels.Message)", Justification = "Names provided with contract.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Asset", Scope = "member", Target = "AMSLLC.Listener.Service.Implementation.KCPL.ITransactionResponse.#AssetLoad(System.ServiceModel.Channels.Message)", Justification = "Names provided with contract.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Test", Scope = "member", Target = "AMSLLC.Listener.Service.Implementation.KCPL.ITransactionResponse.#TestResult(System.ServiceModel.Channels.Message)", Justification = "Names provided with contract.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "device", Scope = "member", Target = "AMSLLC.Listener.Service.Implementation.KCPL.CustomService.#ProcessMeter(AMSLLC.Listener.Common.Model.Device,AMSLLC.Listener.Common.WNP.Model.Meter,System.Int32)", Justification = "Temporary solution for testing.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Scope = "member", Target = "AMSLLC.Listener.Service.Implementation.KCPL.CustomService.#ProcessMetersBatch(AMSLLC.Listener.Common.WNP.Model.NewBatch)", Justification = "Need to refactor later")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1703:ResourceStringsShouldBeSpelledCorrectly", MessageId = "yyyy", Scope = "resource", Target = "AMSLLC.Listener.Service.Implementation.KCPL.Properties.Resources.resources", Justification = "This is a part of date format.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1703:ResourceStringsShouldBeSpelledCorrectly", MessageId = "dd", Scope = "resource", Target = "AMSLLC.Listener.Service.Implementation.KCPL.Properties.Resources.resources", Justification = "This is a part of date format.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Scope = "member", Target = "AMSLLC.Listener.Service.Implementation.KCPL.CustomService.#ProcessMetersBatch(AMSLLC.Listener.Common.WNP.Model.NewBatch,AMSLLC.Listener.Common.Model.DeviceBatch)", Justification = "Need to review.")]
