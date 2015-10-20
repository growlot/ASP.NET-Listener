using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.OData.Routing;
using System.Web.OData.Routing.Conventions;
using AMSLLC.Listener.MetadataService;

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
            return null;
        }

        public string SelectController(ODataPath odataPath, HttpRequestMessage request)
        {
            EntitySetPathSegment entitySetSegment = odataPath.Segments[0] as EntitySetPathSegment;
                
            // remove "s" from the end
            string entityName = entitySetSegment.EntitySetName.Remove(entitySetSegment.EntitySetName.Length - 1);
            MetadataModel metadataModel = metadataService.GetModelMapping(entityName);
            string controllerName;

            if (metadataModel != null)
            {
                switch (metadataModel.TableName)
                {
                    case "wndba.tsite":
                        controllerName = "Sites";
                        break;
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
    }
}