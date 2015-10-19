using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.OData.Routing;
using System.Web.OData.Routing.Conventions;
using AMSLLC.Listener.MetadataService;
using Microsoft.OData.Edm;

namespace AMSLLC.Listener.ODataService
{
    public class WNPGenericRoutingConvention : IODataRoutingConvention
    {
        private readonly IMetadataService metadataService;

        public WNPGenericRoutingConvention(IMetadataService metadataService)
        {
            this.metadataService = metadataService;
        }

        public string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
        {
            if (controllerContext.Request.Method == HttpMethod.Get)
            {
                switch (odataPath.PathTemplate)
                {
                    case "~/entityset/key/navigation/key":
                        var navigationSegment = odataPath.Segments[2] as NavigationPathSegment;
                        var navigationProperty = navigationSegment.NavigationProperty.Partner;
                        var declaringType = navigationProperty.DeclaringType as IEdmEntityType;

                        var actionName = "Get" + declaringType.Name;
                        if (actionMap.Contains(actionName))
                        {
                            // Add keys to route data, so they will bind to action parameters.
                            var keyValueSegment = odataPath.Segments[1] as KeyValuePathSegment;
                            controllerContext.RouteData.Values[ODataRouteConstants.Key] = keyValueSegment.Value;

                            var relatedKeySegment = odataPath.Segments[3] as KeyValuePathSegment;
                            controllerContext.RouteData.Values[ODataRouteConstants.RelatedKey] = relatedKeySegment.Value;

                            return actionName;
                        }
                        break;
                    case "~/entityset/key":
                        break;
                }
            }
            else if (controllerContext.Request.Method == HttpMethod.Post)
            {
                switch (odataPath.PathTemplate)
                {
                    case "~/entityset/key/action":
                    case "~/entityset/action":
                        return "EntityActionHandler";
                    case "~/unboundaction":
                        return "UnboundActionHandler";
                }
            }

            // Not a match.
            return null;
        }

        public string SelectController(ODataPath odataPath, HttpRequestMessage request)
        {
            if (request.Method == HttpMethod.Get && odataPath.PathTemplate != "~" && odataPath.PathTemplate != "~/$metadata")
            {
                if (odataPath.EdmType?.TypeKind == EdmTypeKind.Collection)
                {
                    var entitySetSegment = odataPath.Segments[0] as EntitySetPathSegment;

                    // TODO: this method of getting type name is obnoxious, should be replaced with something better
                    var shortQualifiedName = ((IEdmCollectionType) odataPath.EdmType).ElementType.ShortQualifiedName();
                    var clrModelName = shortQualifiedName.Substring(shortQualifiedName.LastIndexOf('.') + 1);

                    var metadataModel = metadataService.GetModelMapping(clrModelName);
                    string controllerName;

                    if (metadataModel != null)
                    {
                        switch (metadataModel.TableName)
                        {
                            //case "TSECURITY_USERS":
                            //    controllerName = "Users";
                            //    break;
                            //case "TEQP_METER":
                            //    controllerName = "Meters";
                            //    break;
                            //case "TMETER_TEST_RESULTS":
                            //    controllerName = "MeterTests";
                            //    break;
                            default:
                                controllerName = "WNP";
                                break;
                        }
                    }
                    else
                    {
                        controllerName = entitySetSegment.EntitySetName;
                    }

                    return controllerName;
                }
                else
                {
                    return "WNP";
                }
            } else if (request.Method == HttpMethod.Post)
            {
                return "WNP";
            }

            return null;
        }
    }
}