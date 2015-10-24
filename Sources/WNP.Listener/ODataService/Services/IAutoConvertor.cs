// <copyright file="IAutoConvertor.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services
{
    using System;

    public interface IAutoConvertor
    {
        object Convert(object rawData, Type target);
    }
}