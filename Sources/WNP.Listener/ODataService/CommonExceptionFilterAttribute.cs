// <copyright file="CommonExceptionFilterAttribute.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService
{
    using System;
    using System.Web.Http.Filters;
    using System.Web.OData.Extensions;
    using Microsoft.OData.Core;

    /// <summary>
    /// Sets OData error messae to the exception message.
    /// </summary>
    public class CommonExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <inheritdoc/>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext == null)
            {
                throw new ArgumentNullException(nameof(actionExecutedContext), "Execution context for the action must be specified.");
            }

            var response = actionExecutedContext.Request.CreateErrorResponse(
                System.Net.HttpStatusCode.InternalServerError,
                new ODataError() { ErrorCode = string.Empty, InnerError = new ODataInnerError(actionExecutedContext.Exception), Message = actionExecutedContext.Exception.Message });
            actionExecutedContext.Response = response;
        }
    }
}
