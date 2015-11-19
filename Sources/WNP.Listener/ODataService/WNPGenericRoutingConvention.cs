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
    using Microsoft.OData.Edm;

    /// <summary>
    /// Custom OData routing convention for WNP.
    /// </summary>
    public class WNPGenericRoutingConvention : IODataRoutingConvention
    {
        private const string DefaultControllerName = "DEFAULT";
        private readonly IMetadataProvider metadataService;
        private readonly Dictionary<string, string> entityNameToController = new Dictionary<string, string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="WNPGenericRoutingConvention"/> class.
        /// </summary>
        /// <param name="metadataService">The metadata service.</param>
        public WNPGenericRoutingConvention(IMetadataProvider metadataService)
        {
            this.metadataService = metadataService;

            this.entityNameToController.Add(DefaultControllerName, "WNPEntity");

            // selects all controllers that are inheriting from WNPEntityController
            var entityControllers =
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => type != typeof(WNPEntityControllerBase))
                    .Where(type => typeof(WNPEntityControllerBase).IsAssignableFrom(type))
                    .ToList();

            // map table name to controller.
            entityControllers.Map(
                type =>
                {
                    var entityTableName = ((IWNPEntityController)FormatterServices.GetUninitializedObject(type)).GetEntityTableName();
                    var controllerName = type.Name.Substring(0, type.Name.IndexOf("Controller", StringComparison.Ordinal));

                    this.entityNameToController.Add(entityTableName, controllerName);
                });
        }

        /// <inheritdoc/>
        public string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
        {
            if (controllerContext.Request.Method == HttpMethod.Post)
            {
                switch (odataPath.PathTemplate)
                {
                    case "~/entityset/action":
                    case "~/entityset/key/action":
                    case "~/entityset/key/navigation/action":
                    case "~/entityset/key/navigation/key/action":
                        return "EntityActionHandler";
                    case "~/unboundaction":
                        return "UnboundActionHandler";
                    case "~/entityset/key/navigation":
                        return "Post";
                }
            }

            if (controllerContext.Request.Method == HttpMethod.Get)
            {
                switch (odataPath.PathTemplate)
                {
                    case "~/entityset":
                        return "Get";
                    case "~/entityset/key/navigation":
                        return "GetNavigation";
                    case "~/entityset/key":
                        return "GetSingleSimple";
                    case "~/entityset/key/navigation/key":
                        return "GetSingleByNavigation";
                }
            }

            if (controllerContext.Request.Method.Method == "PATCH")
            {
                switch (odataPath.PathTemplate)
                {
                    case "~/entityset/key/navigation/key":
                        return "Patch";
                }
            }

            return null;
        }

        /// <inheritdoc/>
        public string SelectController(ODataPath odataPath, HttpRequestMessage request)
        {
            if (odataPath.PathTemplate == "~" || odataPath.PathTemplate == "~/$metadata" || odataPath.PathTemplate == "~/$batch")
            {
                return null;
            }

            if (odataPath.PathTemplate == "~/unboundaction")
            {
                return "UnboundActions";
            }

            switch (odataPath.PathTemplate)
            {
                case "~/entityset":
                case "~/entityset/key":
                case "~/entityset/action":
                case "~/entityset/key/action":
                    var entitySetTableName = this.GetEntitySetTableName(odataPath);
                    if (entitySetTableName != null)
                    {
                        return this.GetControllerMapping(entitySetTableName);
                    }

                    break;
                case "~/entityset/key/navigation":
                case "~/entityset/key/navigation/key":
                case "~/entityset/key/navigation/action":
                case "~/entityset/key/navigation/key/action":
                    var navigationEntityTableName = this.GetNavigationEntityTableName(odataPath);
                    if (navigationEntityTableName != null)
                    {
                        return this.GetControllerMapping(navigationEntityTableName);
                    }

                    break;
            }

            // for models that are not WNP metadata based, use default routing logic
            return null;
        }

        private string GetEntitySetTableName(ODataPath odataPath)
        {
            var entitySetSegment = odataPath.Segments[0] as EntitySetPathSegment;
            Debug.Assert(entitySetSegment != null, "entitySetSegment != null");

            // remove "s" from the end
            var entitySetName = entitySetSegment.EntitySetName;
            var entityName = entitySetName.Remove(entitySetName.Length - 1);

            return this.metadataService.GetModelMapping(entityName)?.TableName;
        }

        private string GetNavigationEntityTableName(ODataPath odataPath)
        {
            var navigationSegment = odataPath.Segments[2] as NavigationPathSegment;
            Debug.Assert(navigationSegment != null, "navigationSegment != null");

            // remove "s" from the end
            var navigationPropertyName = navigationSegment.NavigationPropertyName;
            var entityName = navigationPropertyName.Remove(navigationPropertyName.Length - 1);

            return this.metadataService.GetModelMapping(entityName)?.TableName;
        }

        private string GetControllerMapping(string tableName)
        {
            // if there is entity specific controller defined, use it
            if (this.entityNameToController.ContainsKey(tableName))
            {
                return this.entityNameToController[tableName];
            }

            // if there is no entity specific controller, use base WNPEntityController
            return this.entityNameToController[DefaultControllerName];
        }
    }
}