//-----------------------------------------------------------------------
// <copyright file="ElectricMetersController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using AMSLLC.Listener.ODataService.Actions.Attributes;
using AMSLLC.Listener.ODataService.Controllers.Base;
using AMSLLC.Listener.Persistence.Metadata;

namespace AMSLLC.Listener.ODataService.Controllers
{
    using MetadataService;
    using Persistence;
    using Services;
    using Services.FilterTransformer;

    [ActionPrefix("ElectricMeter")]
    public class ElectricMetersController : WNPEntityController
    {
        public override string GetEntityTableName() => DBMetadata.EqpMeter.FullTableName;

        public ElectricMetersController(IMetadataProvider metadataService, WNPDBContext dbContext, IFilterTransformer filterTransformer, IAutoConvertor convertor, IActionConfigurator actionConfigurator)
            : base(metadataService, dbContext, filterTransformer, convertor, actionConfigurator)
        {
        }

        /// <summary>
        /// Example URI: ~/ElectricMeters('1')/AMSLLC.Listener.ElectricMeter_Test
        /// POST data:
        /// {mystr: "user"}
        /// </summary>
        /// <param name="mystr"></param>
        /// <returns></returns>
        [BoundAction]
        public string Test(string mystr)
        {
            return mystr;
        }

        /// <summary>
        /// Installs meter.
        /// Example URI: ~/ElectricMeters('1')/AMSLLC.Listener.Listener.ElectricMeter_Install
        /// POST data:
        /// {siteId: 1, circuitIndex: 1, userName: "user", installationDate: "2015-07-07"}
        /// </summary>
        /// <param name="equipmentNumber">Electric meter equipment number used as a key parameter to select specific meter</param>
        /// <param name="siteId">Site where equipment will be installed</param>
        /// <param name="circuitIndex">Circuit index in the site </param>
        /// <param name="userName">User name of the users who did the installation</param>
        /// <param name="installationDate">Time when installation was performed</param>
        [BoundAction]
        public void Install([BoundEntityKey] string equipmentNumber, int siteId, int circuitIndex, string userName, DateTime installationDate)
        {
            ////    if (!ModelState.IsValid)
            ////    {
            ////        return BadRequest();
            ////    }

            ////    int siteId = (int)parameters[this.metadataService.GetModelMappingByTableName("teqp_meter").ColumnToModelMappings["site"]];
            ////    int circuitId = (int)parameters[this.metadataService.GetModelMappingByTableName("teqp_meter").ColumnToModelMappings["circuit"]];
            ////    string installerId = (string)parameters[this.metadataService.GetModelMappingByTableName("tsite_install_history").ColumnToModelMappings["install_by"]];
            ////    DateTime installDate = (DateTime)parameters[this.metadataService.GetModelMappingByTableName("tsite_install_history").ColumnToModelMappings["install_date"]];
            ////    //DateTime installServiceOrderIssueDate = (DateTime)parameters[this.metadataService.GetModelMappingByTableName("tsite_install_history").ColumnToModelMappings["install_service_order_start"]];
            ////    //DateTime installServiceOrderCompleteDate = (DateTime)parameters[this.metadataService.GetModelMappingByTableName("tsite_install_history").ColumnToModelMappings["install_service_order_complete"]];

            ////    User selectedUser = Init.Users.FirstOrDefault(item => item.UserName == key);
            ////    selectedUser.DefaultLocation = location;

            ////    return CreateOkResponse(oDataModelType, result);
            ////    return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Example URI: ~/ElectricMeters/AMSLLC.Listener.ElectricMeter_ColTest
        /// POST data:
        /// {mystr: "user"}
        /// </summary>
        /// <param name="mystr"></param>
        /// <returns></returns>
        [BoundAction]
        [CollectionWideAction]
        public string ColTest(string mystr)
        {
            return mystr + "_col";
        }
    }
}
