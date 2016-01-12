// <copyright file="CircuitsController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.OData;
    using ApplicationService;
    using ApplicationService.Commands;
    using Base;
    using MetadataService;
    using MetadataService.Attributes;
    using Persistence.WNP;
    using Persistence.WNP.Metadata;
    using Repository.WNP;
    using Services.Filter;
    using Services.Query;
    using Utilities;

    /// <summary>
    /// Controller for Circuits.
    /// </summary>
    public class CircuitsController : WNPEntityControllerBase, IBoundActionsContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CircuitsController" /> class.
        /// </summary>
        /// <param name="metadataService">The metadata service.</param>
        /// <param name="unitOfWork">The database context.</param>
        /// <param name="filterTransformer">The filter transformer.</param>
        /// <param name="actionConfigurator">The action configurator.</param>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="queryHandlerFactory">The query handler factory.</param>
        public CircuitsController(
            IMetadataProvider metadataService,
            IWNPUnitOfWork unitOfWork,
            IFilterTransformer filterTransformer,
            IActionConfigurator actionConfigurator,
            ICommandBus commandBus,
            IODataQueryHandlerFactory queryHandlerFactory)
            : base(metadataService, unitOfWork, filterTransformer, actionConfigurator, commandBus, queryHandlerFactory)
        {
        }

        /// <inheritdoc/>
        public override string GetEntityTableName() => DBMetadata.Circuit.FullTableName;

        /// <summary>
        /// Adds new Circuit.
        /// </summary>
        /// <returns>The OData response for newly created Circuit.</returns>
        public Task<IHttpActionResult> Post()
        {
            if (!this.ModelState.IsValid)
            {
                return Task.FromResult<IHttpActionResult>(this.BadRequest(this.ModelState));
            }

            this.ConstructQueryOptions();
            var circuit = this.GetRequestEntity<CircuitEntity>();
            return this.CreateCircuit(circuit);
        }

        /// <summary>
        /// Updates existing Circiut or creates new one if it didn't exist.
        /// </summary>
        /// <returns>The OData response for upserted Circuit.</returns>
        public Task<IHttpActionResult> Patch()
        {
            if (!this.ModelState.IsValid)
            {
                return Task.FromResult<IHttpActionResult>(this.BadRequest(this.ModelState));
            }

            this.ConstructQueryOptions();
            var siteModelMapping = this.MetadataService.GetModelMappingByTableName(DBMetadata.Site.FullTableName);

            var siteKey = this.GetRequestKey(siteModelMapping, 1);
            if (siteKey == null)
            {
                return Task.FromResult<IHttpActionResult>(this.BadRequest(StringUtilities.Invariant($"Invalid key specified for the {siteModelMapping.ClassName}.")));
            }

            var existingSite = this.GetEntity<SiteEntity>(siteKey, siteModelMapping, DBMetadata.Site.Site);

            if (existingSite == null)
            {
                return Task.FromResult<IHttpActionResult>(this.NotFound());
            }

            var circuitModelMapping = this.MetadataService.GetModelMapping(this.EdmEntityClrType);
            var circuitKey = this.GetRequestKey(circuitModelMapping, 3);

            if (circuitKey == null)
            {
                return Task.FromResult<IHttpActionResult>(this.BadRequest(StringUtilities.Invariant($"Invalid key specified for the {circuitModelMapping.ClassName}.")));
            }

            var circuitKeyList = circuitKey.ToList();
            circuitKeyList.Add(new KeyValuePair<string, object>(circuitModelMapping.ColumnToModelMappings[DBMetadata.Circuit.Site], existingSite.Site));

            var existingCircuit = this.GetEntity<CircuitEntity>(circuitKeyList.ToArray());
            if (existingCircuit != null)
            {
                var circuitDelta = this.GetRequestEntityDelta<CircuitEntity>();
                return this.UpdateCircuit(existingCircuit, circuitDelta, circuitKeyList.ToArray());
            }
            else
            {
                var circuit = this.GetRequestEntity<CircuitEntity>();
                circuit.Site = (int)circuitKeyList.First(item => item.Key == circuitModelMapping.ColumnToModelMappings[DBMetadata.Circuit.Site]).Value;

                var primaryKeyFieldName = circuitModelMapping.FieldInfo.First(fi => fi.Value.IsPrimaryKey).Key;
                if (circuitModelMapping.ColumnToModelMappings[DBMetadata.Circuit.MeterPoint.ToUpper(CultureInfo.InvariantCulture)] == primaryKeyFieldName)
                {
                    circuit.MeterPoint = (string)circuitKeyList.First(item => item.Key == primaryKeyFieldName).Value;
                }
                else if (circuitModelMapping.ColumnToModelMappings[DBMetadata.Circuit.ServicePoint.ToUpper(CultureInfo.InvariantCulture)] == primaryKeyFieldName)
                {
                    circuit.ServicePoint = (string)circuitKeyList.First(item => item.Key == primaryKeyFieldName).Value;
                }
                else if (circuitModelMapping.ColumnToModelMappings[DBMetadata.Circuit.CircuitDesc.ToUpper(CultureInfo.InvariantCulture)] == primaryKeyFieldName)
                {
                    circuit.CircuitDesc = (string)circuitKeyList.First(item => item.Key == primaryKeyFieldName).Value;
                }
                else
                {
                    throw new NotSupportedException(StringUtilities.Invariant($"Field {primaryKeyFieldName} is not supported as primary key."));
                }

                return this.CreateCircuit(circuit);
            }
        }

        /// <summary>
        /// Adds the equipment to circiut.
        /// </summary>
        /// <param name="equipmentType">Type of the equipment.</param>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="installDate">The date when installatio was performed.</param>
        /// <param name="installUser">The user who did the install.</param>
        /// <param name="installServiceOrderStarted">The date when install service order was created.</param>
        /// <param name="installServiceOrderCompleted">The date when install service order was completed.</param>
        /// <returns>The result of action.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "This is OData action and it must support optional parameters.")]
        [BoundAction]
        public async Task<IHttpActionResult> AddEquipment(
                    string equipmentType,
                    string equipmentNumber,
                    DateTime installDate,
                    string installUser,
                    DateTime? installServiceOrderStarted = null,
                    DateTime? installServiceOrderCompleted = null)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            this.ConstructQueryOptions();
            var siteModelMapping = this.MetadataService.GetModelMappingByTableName(DBMetadata.Site.FullTableName);

            var siteKey = this.GetRequestKey(siteModelMapping, 1);
            if (siteKey == null)
            {
                return this.BadRequest($"Invalid key specified for the {siteModelMapping.ClassName}.");
            }

            var existingSite = this.GetEntity<SiteEntity>(siteKey, siteModelMapping, DBMetadata.Site.Site);

            if (existingSite == null)
            {
                return this.NotFound();
            }

            var circuitModelMapping = this.MetadataService.GetModelMapping(this.EdmEntityClrType);
            var circuitKey = this.GetRequestKey(circuitModelMapping, 3);

            if (circuitKey == null)
            {
                return this.BadRequest($"Invalid key specified for the {circuitModelMapping.ClassName}.");
            }

            var circuitKeyList = circuitKey.ToList();
            circuitKeyList.Add(new KeyValuePair<string, object>(circuitModelMapping.ColumnToModelMappings[DBMetadata.Circuit.Site], existingSite.Site));

            var existingCircuit = this.GetEntity<CircuitEntity>(circuitKeyList.ToArray());

            if (existingCircuit == null)
            {
                return this.NotFound();
            }

            switch (equipmentType)
            {
                case "EM":
                    var installMeterCommand = new InstallMeterCommand()
                    {
                        CircuitId = existingCircuit.Circuit.Value,
                        EquipmentNumber = equipmentNumber,
                        InstallDate = installDate,
                        InstallServiceOrderCompleted = installServiceOrderCompleted,
                        InstallServiceOrderStarted = installServiceOrderStarted,
                        InstallUser = installUser,
                        SiteId = existingCircuit.Site.Value
                    };

                    await this.CommandBus.PublishAsync(installMeterCommand);
                    return this.StatusCode(HttpStatusCode.NoContent);

                default:
                    throw new NotSupportedException(StringUtilities.Invariant($"Installation of equipment with type {equipmentType} currently not supported."));
            }
        }

        private async Task<IHttpActionResult> CreateCircuit(CircuitEntity circuit)
        {
            var createCircuitCommand = new CreateCircuitCommand()
            {
                OwnerId = this.Owner,
                SiteId = circuit.Site.Value,
                Description = circuit.CircuitDesc,
                EnclosureType = circuit.EnclosureType,
                HasBracket = circuit.HasBracket == "Y" ? true : false,
                InstallDate = circuit.InstallDate,
                Latitude = (decimal?)circuit.Latitude,
                Longitude = (decimal?)circuit.Longitude,
                MeterPoint = circuit.MeterPoint,
                NumberOfConductorsPerPhase = circuit.ConductorsPerPhase,
                ServiceAmperage = (decimal?)circuit.ServiceAmps,
                ServiceLocation = circuit.ServiceLocation,
                ServicePoint = circuit.ServicePoint,
                ServiceVoltage = (decimal?)circuit.ServiceVoltage,
                WireLocation = circuit.WireLocation,
                WireSize = circuit.WireSize,
                WireType = circuit.WireType
            };

            int intValue;
            if (int.TryParse(circuit.ServicePhase, out intValue))
            {
                createCircuitCommand.ServicePhases = intValue;
            }

            if (int.TryParse(circuit.ServiceWire, out intValue))
            {
                createCircuitCommand.ServiceWires = intValue;
            }

            await this.CommandBus.PublishAsync(createCircuitCommand);

            var createdCircuit = ((WNPUnitOfWork)this.UnitOfWork).DbContext.SingleOrDefault<CircuitEntity>($"WHERE {DBMetadata.Circuit.Owner}=@0 and {DBMetadata.Circuit.Site}=@1 and {DBMetadata.Circuit.CircuitDesc}=@2", this.Owner, circuit.Site, circuit.CircuitDesc);

            return await this.PrepareCreatedResponse(createdCircuit);
        }

        private async Task<IHttpActionResult> UpdateCircuit(CircuitEntity existingCircuit, Delta<CircuitEntity> circuitDelta, KeyValuePair<string, object>[] key)
        {
            var changedProperties = circuitDelta.GetChangedPropertyNames();

            if (changedProperties.Contains(nameof(SiteEntity.InterconnectUtility))
                || changedProperties.Contains(nameof(SiteEntity.IsInterconnect)))
            {
                throw new NotImplementedException("Update of the properties InterconnectUtility, IsInterconnect, is not yet implemented");
            }

            List<Task> commandResults = new List<Task>();

            if (changedProperties.Contains(nameof(CircuitEntity.CircuitDesc))
                || changedProperties.Contains(nameof(CircuitEntity.EnclosureType))
                || changedProperties.Contains(nameof(CircuitEntity.ServicePoint))
                || changedProperties.Contains(nameof(CircuitEntity.InstallDate))
                || changedProperties.Contains(nameof(CircuitEntity.MeterPoint))
                || changedProperties.Contains(nameof(CircuitEntity.HasBracket)))
            {
                var updateCircuitDetails = new UpdateCircuitDetailsCommand()
                {
                    Owner = this.Owner,
                    SiteId = existingCircuit.Site.Value,
                    CircuitId = existingCircuit.Circuit.Value,
                    CircuitDescription = GetChangedOrCurrent(circuitDelta, nameof(CircuitEntity.CircuitDesc), existingCircuit.CircuitDesc),
                    EnclosureType = GetChangedOrCurrent(circuitDelta, nameof(CircuitEntity.EnclosureType), existingCircuit.EnclosureType),
                    ServicePoint = GetChangedOrCurrent(circuitDelta, nameof(CircuitEntity.ServicePoint), existingCircuit.ServicePoint),
                    InstallDate = GetChangedOrCurrent(circuitDelta, nameof(CircuitEntity.InstallDate), existingCircuit.InstallDate),
                    MeterPoint = GetChangedOrCurrent(circuitDelta, nameof(CircuitEntity.MeterPoint), existingCircuit.MeterPoint),
                    HasBracket = GetChangedOrCurrent(circuitDelta, nameof(CircuitEntity.HasBracket), existingCircuit.HasBracket) == "Y" ? true : false,
                };

                commandResults.Add(this.CommandBus.PublishAsync(updateCircuitDetails));
            }

            if (changedProperties.Contains(nameof(CircuitEntity.Latitude))
                || changedProperties.Contains(nameof(CircuitEntity.Longitude)))
            {
                throw new NotImplementedException("Update of the properties Latitude, Longitude is not yet implemented");
            }

            if (changedProperties.Contains(nameof(CircuitEntity.SflDesiredAccDel))
                || changedProperties.Contains(nameof(CircuitEntity.SflDesiredAccRec))
                || changedProperties.Contains(nameof(CircuitEntity.SllDesiredAccDel))
                || changedProperties.Contains(nameof(CircuitEntity.SllDesiredAccRec))
                || changedProperties.Contains(nameof(CircuitEntity.SpfDesiredAccDel))
                || changedProperties.Contains(nameof(CircuitEntity.SpfDesiredAccRec)))
            {
                throw new NotImplementedException("Update of the properties SflDesiredAccDel, SflDesiredAccRec, SllDesiredAccDel, SllDesiredAccRec, SpfDesiredAccDel, SpfDesiredAccRec is not yet implemented");
            }

            if (changedProperties.Contains(nameof(CircuitEntity.ServiceLocation))
                || changedProperties.Contains(nameof(CircuitEntity.ServiceVoltage))
                || changedProperties.Contains(nameof(CircuitEntity.ServiceAmps))
                || changedProperties.Contains(nameof(CircuitEntity.ServicePhase))
                || changedProperties.Contains(nameof(CircuitEntity.ServiceWire)))
            {
                throw new NotImplementedException("Update of the properties ServiceLocation, ServiceVoltage, ServiceAmps, ServicePhase, ServiceWire is not yet implemented");
            }

            if (changedProperties.Contains(nameof(CircuitEntity.WireType))
                || changedProperties.Contains(nameof(CircuitEntity.WireSize))
                || changedProperties.Contains(nameof(CircuitEntity.ConductorsPerPhase))
                || changedProperties.Contains(nameof(CircuitEntity.WireLocation)))
            {
                throw new NotImplementedException("Update of the properties WireType, WireSize, ConductorsPerPhase, WireLocation is not yet implemented");
            }

            await Task.WhenAll(commandResults);

            var updateEntity = this.GetEntity<CircuitEntity>(key);

            return await this.PrepareUpdatedResponse(updateEntity);
        }
    }
}
