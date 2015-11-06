﻿// <copyright file="WNPGenericRoutingConvention.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Http;
    using System.Runtime.Serialization;
    using System.Web.Http.Controllers;
    using System.Web.OData.Routing;
    using System.Web.OData.Routing.Conventions;
    using Controllers.Base;
    using MetadataService;

    /// <summary>
    /// Custom OData routing convention for WNP.
    /// </summary>
    public class WNPGenericRoutingConvention : IODataRoutingConvention
    {
        private readonly IMetadataProvider metadataService;
        private readonly Dictionary<string, string> entityNameToController = new Dictionary<string, string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="WNPGenericRoutingConvention"/> class.
        /// </summary>
        /// <param name="metadataService">The metadata service.</param>
        public WNPGenericRoutingConvention(IMetadataProvider metadataService)
        {
            this.metadataService = metadataService;

            var entityControllers =
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => type != typeof(WNPEntityController))
                    .Where(type => typeof(WNPEntityController).IsAssignableFrom(type))
                    .ToList();

            entityControllers.Map(
                type =>
                {
                    var entityTableName = ((IBoundActionsContainer)FormatterServices.GetUninitializedObject(type)).GetEntityTableName();
                    var controllerName = type.Name.Substring(0, type.Name.IndexOf("Controller", StringComparison.Ordinal));

                    this.entityNameToController.Add(entityTableName, controllerName);
                });
        }

        /// <inheritdoc/>
        public string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
        {
            switch (odataPath.PathTemplate)
            {
                case "~/entityset/key/action":
                case "~/entityset/action":
                    return "EntityActionHandler";
                case "~/unboundaction":
                    return "UnboundActionHandler";
                case "~/entityset/key":
                    return "GetSingle";
            }

            return null;
        }

        /// <inheritdoc/>
        public string SelectController(ODataPath odataPath, HttpRequestMessage request)
        {
            if (odataPath.PathTemplate == "~" || odataPath.PathTemplate == "~/$metadata")
            {
                return null;
            }

            if (odataPath.PathTemplate == "~/unboundaction")
            {
                return "UnboundActions";
            }

            var entitySetSegment = odataPath.Segments[0] as EntitySetPathSegment;
            Debug.Assert(entitySetSegment != null, "entitySetSegment != null");

            // remove "s" from the end
            var entitySetName = entitySetSegment.EntitySetName;
            var entityName = entitySetName.Remove(entitySetName.Length - 1);

            var metadataModel = this.metadataService.GetModelMapping(entityName);
            if (this.entityNameToController.ContainsKey(metadataModel.TableName))
            {
                return this.entityNameToController[metadataModel.TableName];
            }

            return entitySetName;
        }
    }
}