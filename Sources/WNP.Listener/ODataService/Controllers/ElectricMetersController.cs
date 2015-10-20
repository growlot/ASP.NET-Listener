//-----------------------------------------------------------------------
// <copyright file="ElectricMetersController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AMSLLC.Listener.ODataService.Controllers
{
    using System.Web.Http;
    using System.Web.OData;
    using MetadataService;
    using Services;
    using Services.FilterTransformer;
    using System;
    using Persistence;

    public class ElectricMetersController : WNPController
    {
        public ElectricMetersController(IMetadataService metadataService, WNPDBContext dbContext, IFilterTransformer filterTransformer, IAutoConvertor convertor, IActionConfigurator actionConfigurator)
            : base(metadataService, dbContext, filterTransformer, convertor, actionConfigurator)
        {
        }

        ////[HttpPost]
        ////public IHttpActionResult Install([FromODataUri] string key, ODataActionParameters parameters)
        ////{
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
        ////}
    }
}
