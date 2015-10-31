// <copyright file="Migration_202000140_TransactionStatus_Populate.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;
    using FluentMigrator.Runner.Extensions;

    /// <summary>
    /// Database migration step
    /// </summary>
    [Migration(202000140)]
    public class Migration_202000140_TransactionStatus_Populate : Migration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            this.Update.Table("TransactionStatus").Set(new
            {
                TransactionStatusId = 1,
                Description = "Pending"
            }).Where(new
            {
                TransactionStatusId = 1,
                Description = "In Progress"
            });

            var records = new object[]
            {
                new
                {
                    TransactionStatusId = 4,
                    Description = "Processing"
                },
                new
                {
                    TransactionStatusId = 5,
                    Description = "Invalid"
                },
                new
                {
                    TransactionStatusId = 6,
                    Description = "Canceled"
                }
            };

            foreach (var record in records)
            {
                this.Insert.IntoTable("TransactionStatus")
                    .Row(record);
            }
        }

        /// <summary>
        /// Perform database downgrade action
        /// </summary>
        public override void Down()
        {
            this.Delete.FromTable("TransactionStatus").Row(new
            {
                TransactionStatusId = 4,
                Description = "Processing"
            });

            this.Delete.FromTable("TransactionStatus").Row(new
            {
                TransactionStatusId = 5,
                Description = "Invalid"
            });
        }
    }
}