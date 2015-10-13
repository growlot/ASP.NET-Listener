namespace AMSLLC.Listener.ODataService
{
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.OData.Routing;
    using System.Web.OData.Routing.Conventions;
    using Microsoft.OData.Edm;
    using MetadataService;

    public class WNPGenericRoutingConvention : IODataRoutingConvention
    {
        private readonly IMetadataService metadataService;

        public WNPGenericRoutingConvention(IMetadataService metadataService)
        {
            this.metadataService = metadataService;
        }

        public string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
        {
            if (controllerContext.Request.Method == HttpMethod.Get &&
                odataPath.PathTemplate == "~/entityset/key/navigation/key")
            {
                NavigationPathSegment navigationSegment = odataPath.Segments[2] as NavigationPathSegment;
                IEdmNavigationProperty navigationProperty = navigationSegment.NavigationProperty.Partner;
                IEdmEntityType declaringType = navigationProperty.DeclaringType as IEdmEntityType;

                string actionName = "Get" + declaringType.Name;
                if (actionMap.Contains(actionName))
                {
                    // Add keys to route data, so they will bind to action parameters.
                    KeyValuePathSegment keyValueSegment = odataPath.Segments[1] as KeyValuePathSegment;
                    controllerContext.RouteData.Values[ODataRouteConstants.Key] = keyValueSegment.Value;

                    KeyValuePathSegment relatedKeySegment = odataPath.Segments[3] as KeyValuePathSegment;
                    controllerContext.RouteData.Values[ODataRouteConstants.RelatedKey] = relatedKeySegment.Value;

                    return actionName;
                }
            }
            // Not a match.
            return null;
        }

        public string SelectController(ODataPath odataPath, HttpRequestMessage request)
        {
            if (request.Method == HttpMethod.Get && odataPath.PathTemplate == "~/entityset")
            {
                EntitySetPathSegment entitySetSegment = odataPath.Segments[0] as EntitySetPathSegment;
                MetadataModel metadataModel = metadataService.GetModelMapping(entitySetSegment.EntitySetName);
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

            return null;
        }
    }
}