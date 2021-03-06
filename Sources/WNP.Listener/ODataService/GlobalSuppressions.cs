// <copyright file="GlobalSuppressions.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the
// Code Analysis results, point to "Suppress Message", and click
// "In Suppression File".
// You do not need to add suppressions to this file manually.
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Scope = "member", Target = "AMSLLC.Listener.ODataService.Services.Implementations.ODataQueryHandler.ODataNavigationMultipleResultsQueryHandler.#Fetch()", Justification = "Need to review.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Scope = "type", Target = "AMSLLC.Listener.ODataService.Controllers.Base.WNPEntityController", Justification = "Need to review.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Scope = "member", Target = "AMSLLC.Listener.ODataService.Controllers.Base.WNPEntityController.#GetNavigation()", Justification = "Need to review.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "AMSLLC.Listener.ODataService.Controllers.Base", Justification = "More classes will be added later.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "AMSLLC.Listener.ODataService.HttpMessageHandlers", Justification = "More classes will be added later.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "AMSLLC.Listener.ODataService.Services", Justification = "More classes will be added later.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "AMSLLC.Listener.ODataService.Services.FilterTransformer", Justification = "More classes will be added later.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "AMSLLC.Listener.ODataService.Services.Implementations", Justification = "More classes will be added later.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "AMSLLC.Listener.ODataService.Services.Implementations.FilterTransformer", Justification = "More classes will be added later.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "System", Justification = "Namespace is used to match the extended ojbect type.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "System.Web.OData.Routing", Justification = "Namespace is used to match the extended ojbect type.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "AMSLLC.Listener.ODataService.Services.Filter", Justification = "More classes will be added later.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "AMSLLC.Listener.ODataService.Services.Implementations.Filter", Justification = "More classes will be added later.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Scope = "member", Target = "AMSLLC.Listener.ODataService.Services.Implementations.Query.ODataNavigationMultipleResultsQueryHandler.#Fetch()", Justification = "Need to review.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Scope = "member", Target = "AMSLLC.Listener.ODataService.Services.Filter.WhereClause.#PositionalParameters", Justification = "Need to review.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "AMSLLC.Listener.ODataService.ODataServiceConfigurator.#Configure(System.Web.Http.HttpConfiguration)", Justification = "If added to using statement, then service doesn't work, because handlers are disposed. Need to investigate how to dispose correctly.")]