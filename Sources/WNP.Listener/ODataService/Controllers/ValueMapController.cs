// <copyright file="ValueMapController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using Persistence.Listener;

    public partial class ValueMapController
    {
        partial void UpdateNested(
            ValueMapEntity entity,
            ValueMapEntity newData)
        {
            var toDelete =
                entity.ValueMapEntries.Where(
                    s => newData.ValueMapEntries.All(ss => ss.ValueMapEntryId != s.ValueMapEntryId)).ToList();

            foreach (ValueMapEntryEntity valueMapEntryEntity in toDelete)
            {
                this._dbContext.Entry(valueMapEntryEntity).State = EntityState.Deleted;
            }

            foreach (ValueMapEntryEntity valueMapEntryEntity in entity.ValueMapEntries)
            {
                var newValues =
                    newData.ValueMapEntries.SingleOrDefault(
                        s => s.ValueMapEntryId == valueMapEntryEntity.ValueMapEntryId);
                if (newValues != null)
                {
                    this._dbContext.Entry(valueMapEntryEntity).CurrentValues.SetValues(newValues);
                }
            }

            foreach (ValueMapEntryEntity valueMapEntryEntity in newData.ValueMapEntries)
            {
                if (valueMapEntryEntity.ValueMapEntryId <= 0)
                {
                    this._dbContext.Entry(valueMapEntryEntity).State = EntityState.Added;
                }
            }
        }
    }
}