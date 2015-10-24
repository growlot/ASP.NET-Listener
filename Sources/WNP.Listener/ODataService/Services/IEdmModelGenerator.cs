// <copyright file="IEdmModelGenerator.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services
{
    using Microsoft.OData.Edm;

    public interface IEdmModelGenerator
    {
        IEdmModel GenerateODataModel();
    }
}