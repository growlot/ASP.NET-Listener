using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSLLC.Listener.ODataService.Controllers
{
    using System.Reflection;
    using System.Web.Http;
    using System.Web.OData;
    using System.Web.OData.Query;
    using Communication;
    using Core;
    using DbContext;
    using Domain.Listener.Transaction;
    using Model;
    using Newtonsoft.Json;
    using Persistence.Listener;
    using Serilog;

    public class ProtocolMetadataController : ApiController
    {
        private readonly ListenerODataContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProtocolMetadataController" /> class.
        /// </summary>
        /// <param name="dbctx">The db context.</param>
        public ProtocolMetadataController(ListenerODataContext dbctx)
        {
            this._dbContext = dbctx;
        }

        /// <summary>
        /// Get the IQueryable of the served entity.
        /// </summary>
        /// <returns>System.Linq.IQueryable&lt;AMSLLC.Listener.Persistence.Listener.TransactionRegistryViewEntity&gt;.</returns>
        public IHttpActionResult GetMetadata()
        {
            try
            {
                List<ProtocolMetadata> returnValue = new List<ProtocolMetadata>();

                var protocols = this._dbContext.ProtocolType;

                foreach (ProtocolTypeEntity protocolTypeEntity in protocols.ToList())
                {
                    var pConnection =
                        ApplicationIntegration.DependencyResolver.ResolveNamed<IConnectionConfigurationBuilder>($"connection-builder-{protocolTypeEntity.Name}");
                    var tp = pConnection.Create(new IntegrationEndpointConfigurationMemento(null, "{}", "{}", EndpointTriggerType.Undefined));
                    var props = tp.GetType().GetProperties();
                    var record = new ProtocolMetadata();
                    record.ProtocolType = protocolTypeEntity.Name;
                    foreach (PropertyInfo propertyInfo in props)
                    {
                        record.Connection.Add(new ProtocolMetadataModel { DataType = this.ToCommonType(propertyInfo.PropertyType), IsArray = propertyInfo.PropertyType.IsArray, PropertyName = propertyInfo.Name });
                    }
                    returnValue.Add(record);
                }

                return this.Json(returnValue);//.Ok(JsonConvert.SerializeObject(returnValue));
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }

        private CommonDataType ToCommonType(Type type)
        {
            if (type == typeof(int))
            {
                return CommonDataType.Integer;
            }

            if (type == typeof(string))
            {
                return CommonDataType.String;
            }

            return CommonDataType.Undefined;
        }
    }
}
