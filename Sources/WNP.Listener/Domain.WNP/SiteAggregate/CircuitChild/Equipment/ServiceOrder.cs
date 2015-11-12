//-----------------------------------------------------------------------
// <copyright file="ServiceOrder.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.WNP.SiteAggregate.CircuitChild.Equipment
{
    using System;

    /// <summary>
    /// Geographical location coordinates.
    /// </summary>
    public sealed class ServiceOrder : ValueObject<ServiceOrder>
    {
        /// <summary>
        /// Date the service order was issued
        /// </summary>
        private readonly DateTime? orderIssued;

        /// <summary>
        /// Date the service order was completed
        /// </summary>
        private readonly DateTime? orderCompleted;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceOrder" /> class.
        /// </summary>
        /// <param name="orderIssued">Date the service order was issued.</param>
        /// <param name="orderCompleted">Date the service order was completed.</param>
        public ServiceOrder(DateTime? orderIssued, DateTime? orderCompleted)
        {
            if (orderIssued.HasValue && orderCompleted.HasValue && (orderIssued.Value > orderCompleted.Value))
            {
                throw new ArgumentOutOfRangeException(nameof(orderCompleted), "Service order can not be completed before it was issued.");
            }

            this.orderIssued = orderIssued;
            this.orderCompleted = orderCompleted;
        }

        /// <summary>
        /// Gets the date the service order was issued
        /// </summary>
        /// <value>
        /// Date the service order was issued
        /// </value>
        public DateTime? OrderIssued
        {
            get
            {
                return this.orderIssued;
            }
        }

        /// <summary>
        /// Gets the date the service order was completed
        /// </summary>
        /// <value>
        /// Date the service order was completed
        /// </value>
        public DateTime? OrderCompleted
        {
            get
            {
                return this.orderCompleted;
            }
        }
    }
}
