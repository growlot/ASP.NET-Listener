// <copyright file="CircuitsController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.OData.Routing;
    using ApplicationService;
    using ApplicationService.Commands;
    using Base;
    using MetadataService;
    using Newtonsoft.Json;
    using Persistence.WNP;
    using Persistence.WNP.Metadata;
    using Repository.WNP;
    using Services.FilterTransformer;
    using MetadataService.Attributes;

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
        public CircuitsController(IMetadataProvider metadataService, IWNPUnitOfWork unitOfWork, IFilterTransformer filterTransformer, IActionConfigurator actionConfigurator, ICommandBus commandBus)
            : base(metadataService, unitOfWork, filterTransformer, actionConfigurator, commandBus)
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

            var queryOptions = this.ConstructQueryOptions();
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

            var queryOptions = this.ConstructQueryOptions();

            var modelMapping = this.metadataService.GetModelMapping(this.EdmEntityClrType);
            var entityConfig = modelMapping.EntityConfiguration;
            var hasCompositeKey = entityConfig.Key?.Count() > 1;

            var hasRequiredRelations = entityConfig.RequiredRelations;

            var jsonKey = queryOptions.Context.Path.Segments[1] as KeyValuePathSegment;
            if (jsonKey == null)
            {
                return Task.FromResult<IHttpActionResult>(this.BadRequest("Invalid key specified"));
            }

            var key = new Dictionary<string, object>();
            if (hasCompositeKey)
            {
                key = jsonKey.ToCompositeKeyDictionary();
            }
            else
            {
                key.Add(entityConfig.Key.ToArray()[0], JsonConvert.DeserializeObject(jsonKey.Value));
            }

            var orderedKey = key.ToArray();

            var sql =
                Sql.Builder.Select("*")
                    .From($"{modelMapping.TableName}")
                    .Where(
                        orderedKey.Select((kvp, ind) => $"{modelMapping.ModelToColumnMappings[kvp.Key]}=@{ind}")
                            .Aggregate((s, s1) => $"{s} AND {s1}"),
                        orderedKey.Select(kvp => kvp.Value).ToArray());

            var existingCircuit = ((WNPUnitOfWork)this.unitOfWork).DbContext.FirstOrDefault<CircuitEntity>(sql);

            if (existingCircuit != null)
            {
                var circuitDelta = this.GetRequestEntityDelta<CircuitEntity>();
                if (circuitDelta.GetChangedPropertyNames().Count() > 0)
                {
                    throw new NotImplementedException("Circuit update is currently not implemented.");
                }

                return this.PrepareUpdatedResponse(existingCircuit);
            }
            else
            {
                var descriptionFieldName = modelMapping.ColumnToModelMappings[DBMetadata.Circuit.CircuitDesc.ToUpperInvariant()];
                if (modelMapping.FieldInfo[descriptionFieldName].IsPrimaryKey)
                {
                    var circuit = this.GetRequestEntity<CircuitEntity>();

                    circuit.Site = (int)orderedKey.First(item => item.Key == modelMapping.ColumnToModelMappings[DBMetadata.Circuit.Site]).Value;
                    circuit.CircuitDesc = (string)orderedKey.First(item => item.Key == modelMapping.ColumnToModelMappings[DBMetadata.Circuit.CircuitDesc]).Value;
                    return this.CreateCircuit(circuit);
                }
                else
                {
                    return this.PrepareGetResponse(existingCircuit);
                }
            }
        }

        /// <summary>
        /// Adds the equipment to circiut.
        /// </summary>
        /// <param name="circuitId">The circuit identifier.</param>
        /// <param name="equipmentType">Type of the equipment.</param>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="installDate">The date when installatio was performed.</param>
        /// <param name="installUser">The user who did the install.</param>
        /// <param name="installServiceOrderStarted">The date when install service order was created.</param>
        /// <param name="installServiceOrderCompleted">The date when install service order was completed.</param>
        [BoundAction]
        public void AddEquipment(
                    [BoundEntityKey] string circuitId,
                    string equipmentType,
                    string equipmentNumber,
                    DateTime installDate,
                    string installUser,
                    DateTime? installServiceOrderStarted = null,
                    DateTime? installServiceOrderCompleted = null)
        {

        }

        private async Task<IHttpActionResult> CreateCircuit(CircuitEntity circuit)
        {
            var createCircuitCommand = new CreateCircuitCommand()
            {
                OwnerId = this.Owner,
                SiteId = circuit.Site.Value,
                Description = circuit.CircuitDesc,
                EnclosureType = circuit.EnclosureType,
                InstallDate = circuit.InstallDate,
                Latitude = (decimal?)circuit.Latitude,
                Longitude = (decimal?)circuit.Longitude,
                NumberOfConductorsPerPhase = circuit.ConductorsPerPhase,
                ServiceAmperage = (decimal?)circuit.ServiceAmps,
                ServiceLocation = circuit.ServiceLocation,
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

            await this.commandBus.PublishAsync(createCircuitCommand);

            var createdCircuit = ((WNPUnitOfWork)this.unitOfWork).DbContext.SingleOrDefault<CircuitEntity>($"WHERE {DBMetadata.Circuit.Owner}=@0 and {DBMetadata.Circuit.Site}=@1 and {DBMetadata.Circuit.CircuitDesc}=@2", this.Owner, circuit.Site, circuit.CircuitDesc);

            return await this.PrepareCreatedResponse(createdCircuit);
        }
    }
}
