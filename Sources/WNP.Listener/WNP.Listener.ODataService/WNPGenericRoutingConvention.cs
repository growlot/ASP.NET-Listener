using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.OData.Routing;
using System.Web.OData.Routing.Conventions;
using Microsoft.OData.Edm;
using WNP.Listener.MetadataService;

namespace WNP.Listener.ODataService
{
    public class WNPGenericRoutingConvention : IODataRoutingConvention
    {
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
            if (request.Method != HttpMethod.Get || odataPath.PathTemplate != "~/entityset")
                return null;

            return "WNP";
        }
    }
}