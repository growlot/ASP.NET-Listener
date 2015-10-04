// //-----------------------------------------------------------------------
// // <copyright file="ApiUtilities.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApiService
{
    using System;
    using System.Linq;
    using System.Web.Http.ModelBinding;
    using ApplicationService;
    using Core;

    public static class ApiUtilities
    {
        public static ApiResponseMessage AsApiResponseMessage(this IExecutionContext context)
        {
            return AsApiResponseMessage(context, null);
        }

        public static ApiResponseMessage AsApiResponseMessage(this Exception exc)
        {
            var returnValue = new ApiResponseMessage { Success = false };
            returnValue.Messages.Add(new StatusMessage { Message = "An error has occurred" });
            return returnValue;
        }

        public static void FromModelState(this IExecutionContext context, ModelStateDictionary modelState)
        {
            for (int i = 0; i < modelState.Values.Count; i++)
            {
                string key = modelState.Keys.ElementAt(i);
                var errors = modelState.Values.ElementAt(i).Errors;
                for (int j = 0; j < errors.Count; j++)
                {
                    var message = errors[j].ErrorMessage;
                    context.AddError(message, key);
                }
            }
        }

        public static ApiResponseMessage AsApiResponseMessage(this IExecutionContext context, object result)
        {
            var returnValue = new ApiResponseMessage { Success = context.Valid, Data = result };
            returnValue.Messages.AddRange(context.GetErrors());
            return returnValue;
        }
    }
}