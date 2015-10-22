using AMSLLC.Listener.MetadataService;
using AMSLLC.Listener.ODataService.Actions;
using AMSLLC.Listener.ODataService.Actions.Attributes;
using AMSLLC.Listener.ODataService.Controllers.Base;
using AMSLLC.Listener.ODataService.Services;
using AMSLLC.Listener.ODataService.Services.FilterTransformer;
using AMSLLC.Listener.Persistence;

namespace AMSLLC.Listener.ODataService.Controllers
{
    [ActionPrefix("Unbound")]
    public class UnboundActionsController : WNPController, IUnboundActionsContainer
    {
        public UnboundActionsController(IMetadataService metadataService, WNPDBContext dbContext,
            IFilterTransformer filterTransformer, IAutoConvertor convertor, IActionConfigurator actionConfigurator)
            : base(metadataService, dbContext, filterTransformer, convertor, actionConfigurator) { }

        [UnboundAction]
        public string Test(string mystr)
        {
            return mystr;
        }
    }
}