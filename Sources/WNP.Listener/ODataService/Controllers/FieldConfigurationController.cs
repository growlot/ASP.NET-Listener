// <copyright file="FieldConfigurationController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using Persistence.Listener;

    public partial class FieldConfigurationController
    {
        partial void UpdateNested(
            FieldConfigurationEntity entity,
            FieldConfigurationEntity newData)
        {
            var toDelete =
                entity.FieldConfigurationEntries.Where(
                    s => newData.FieldConfigurationEntries.All(ss => ss.FieldConfigurationEntryId != s.FieldConfigurationEntryId)).ToList();

            var toUpdate =
                entity.FieldConfigurationEntries.Where(
                    s => newData.FieldConfigurationEntries.Any(ss => ss.FieldConfigurationEntryId == s.FieldConfigurationEntryId)).ToList();

            foreach (FieldConfigurationEntryEntity entry in toDelete)
            {
                this._dbContext.Entry(entry).State = EntityState.Deleted;
            }

            foreach (FieldConfigurationEntryEntity entry in entity.FieldConfigurationEntries)
            {
                var newValues =
                    newData.FieldConfigurationEntries.SingleOrDefault(
                        s => s.FieldConfigurationEntryId == entry.FieldConfigurationEntryId);
                if (newValues != null)
                {
                    this._dbContext.Entry(entry).CurrentValues.SetValues(newValues);
                }
            }

            foreach (FieldConfigurationEntryEntity entry in newData.FieldConfigurationEntries)
            {
                if (entry.FieldConfigurationEntryId <= 0)
                {
                    this._dbContext.Entry(entry).State = EntityState.Added;
                }
            }
        }
    }
}
