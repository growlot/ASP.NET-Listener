// <copyright file="HttpRequestMessageExtensions.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace System.Net.Http
{
    using AMSLLC.Listener.Domain;

    /// <summary>
    /// Defines custom extensions for <see cref="HttpRequestMessage"/> type
    /// </summary>
    public static class HttpRequestMessageExtensions
    {
        private const string UnitOfWork = "UnitOfWork";

        /// <summary>
        /// Sets the unit of work in request properties.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public static void SetUnitOfWork(this HttpRequestMessage request, IUnitOfWork unitOfWork)
        {
            request.Properties[UnitOfWork] = unitOfWork;
        }

        /// <summary>
        /// Gets the unit of work from request properties, or constructs new one.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The unit of work.</returns>
        public static IUnitOfWork GetUnitOfWork(this HttpRequestMessage request)
        {
            object unitOfWork;
            if (request.Properties.TryGetValue(UnitOfWork, out unitOfWork))
            {
                return (IUnitOfWork)unitOfWork;
            }

            return null;
        }
    }
}
