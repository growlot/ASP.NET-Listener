// <copyright file="IEdmModelGenerator.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services
{
    using Microsoft.OData.Edm;

    /// <summary>
    /// Interface for EDM model generator
    /// </summary>
    public interface IEdmModelGenerator
    {
        /// <summary>
        /// Generates the OData EDM model.
        /// </summary>
        /// <returns>The EDM model</returns>
        IEdmModel GenerateODataModel();
    }
}