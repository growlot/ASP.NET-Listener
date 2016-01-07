using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using AMSLLC.Listener.Persistence.Listener;
using Serilog;

namespace AMSLLC.Listener.ODataService.Controllers
{
    partial class EntityCategoryOperationController
    {
        /// <summary>
        /// Get the single entity or null using primary key
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Web.Http.IHttpActionResult.</returns>
        /// <returns>IHttpActionResult.</returns>
        public IHttpActionResult Get([FromODataUri] int key)
        {
            try
            {
                var result =
                    this._dbContext.Set<EntityCategoryOperationEntity>()
                    .Include(s => s.EntityCategory)
                    .Include(s => s.EnabledOperation)
                    .Include(s => s.EnabledOperation.Operation)
                        .Include(s => s.OperationEndpoints.Select(ss => ss.Endpoint))
                        .SingleOrDefault(s => s.EntityCategoryOperationId == key);
                return this.Ok(result);
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }

        partial void UpdateNested(
            EntityCategoryOperationEntity entity,
            EntityCategoryOperationEntity newData)
        {
            var toDelete =
                entity.OperationEndpoints.Where(
                    s => newData.OperationEndpoints.All(ss => ss.OperationEndpointId != s.OperationEndpointId)).ToList();

            //var toUpdate =
            //    entity.OperationEndpoints.Where(
            //        s => newData.OperationEndpoints.Any(ss => ss.OperationEndpointId == s.OperationEndpointId)).ToList();

            foreach (OperationEndpointEntity e in toDelete)
            {
                this._dbContext.Entry(e).State = EntityState.Deleted;
            }

            foreach (OperationEndpointEntity e in entity.OperationEndpoints)
            {
                var newValues =
                    newData.OperationEndpoints.SingleOrDefault(
                        s => s.OperationEndpointId == e.OperationEndpointId);
                if (newValues != null)
                {
                    this._dbContext.Entry(e).CurrentValues.SetValues(newValues);
                }

            }

            foreach (OperationEndpointEntity e in newData.OperationEndpoints)
            {
                if (e.OperationEndpointId <= 0)
                {
                    this._dbContext.Entry(e).State = EntityState.Added;
                }
            }
        }
    }
}
