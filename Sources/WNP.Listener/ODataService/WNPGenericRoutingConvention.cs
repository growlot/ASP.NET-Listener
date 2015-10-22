using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http.Controllers;
using System.Web.OData.Routing;
using System.Web.OData.Routing.Conventions;
using AMSLLC.Core;
using AMSLLC.Listener.MetadataService;
using AMSLLC.Listener.ODataService.Actions;
using AMSLLC.Listener.ODataService.Controllers;
using AMSLLC.Listener.ODataService.Controllers.Base;

namespace AMSLLC.Listener.ODataService
{
    public class WNPGenericRoutingConvention : IODataRoutingConvention
    {
        private readonly IMetadataService metadataService;
        private readonly Dictionary<string, string> _entityNameToController = new Dictionary<string, string>();

        public WNPGenericRoutingConvention(IMetadataService metadataService)
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

                    _entityNameToController.Add(entityTableName, controllerName);
                });
        }

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

            var metadataModel = metadataService.GetModelMapping(entityName);
            if (_entityNameToController.ContainsKey(metadataModel.TableName))
                return _entityNameToController[metadataModel.TableName];

            return entitySetName;
        }
    }
}