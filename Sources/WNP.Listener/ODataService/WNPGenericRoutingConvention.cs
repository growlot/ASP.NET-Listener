// <copyright file="WNPGenericRoutingConvention.cs" company="Advanced Metering Services LLC">
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

    public class WNPGenericRoutingConvention : IODataRoutingConvention
    {
        private readonly IMetadataProvider metadataService;
        private readonly Dictionary<string, string> _entityNameToController = new Dictionary<string, string>();

        public WNPGenericRoutingConvention(IMetadataProvider metadataService)
        {
            this.metadataService = metadataService;

            var entityControllers =
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => type != typeof (WNPEntityController))
                    .Where(type => typeof (WNPEntityController).IsAssignableFrom(type))
                    .ToList();

            entityControllers.Map(
                type =>
                {
                    var entityTableName = ((IBoundActionsContainer)FormatterServices.GetUninitializedObject(type)).GetEntityTableName();
                    var controllerName = type.Name.Substring(0, type.Name.IndexOf("Controller", StringComparison.Ordinal));

                    this._entityNameToController.Add(entityTableName, controllerName);
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
            }

            return null;
        }

        /// <inheritdoc/>
        public string SelectController(ODataPath odataPath, HttpRequestMessage request)
        {
            if (odataPath.PathTemplate == "~" || odataPath.PathTemplate == "~/$metadata")
                return null;

            if (odataPath.PathTemplate == "~/unboundaction")
                return "UnboundActions";

            var entitySetSegment = odataPath.Segments[0] as EntitySetPathSegment;
            Debug.Assert(entitySetSegment != null, "entitySetSegment != null");

            // remove "s" from the end
            var entitySetName = entitySetSegment.EntitySetName;
            var entityName = entitySetName.Remove(entitySetName.Length - 1);

            var metadataModel = this.metadataService.GetModelMapping(entityName);
            if (this._entityNameToController.ContainsKey(metadataModel.TableName))
                return this._entityNameToController[metadataModel.TableName];

            return entitySetName;
        }
    }
}