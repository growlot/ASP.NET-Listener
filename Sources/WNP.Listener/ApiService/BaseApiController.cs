﻿// //-----------------------------------------------------------------------
// // <copyright file="BaseApiController.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApiService
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Results;
    using ApplicationService;
    using Newtonsoft.Json;

    public abstract class BaseApiController : ApiController
    {
        public string CompanyCode { get; set; }
        public string ApplicationKey { get; set; }


        /// <summary>
        /// Executes the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>Task.</returns>
        protected async Task<IHttpActionResult> TryExecuteOperationAsync(Func<IExecutionContext, Task> action)
        {
            try
            {
                IExecutionContext executionContext = new ExecutionContext();
                if (this.ModelState.IsValid)
                {
                    await action(executionContext);
                    return Ok(executionContext.AsApiResponseMessage());
                }

                executionContext.FromModelState(this.ModelState);
                return BuildResponseMessage(HttpStatusCode.BadRequest, executionContext.AsApiResponseMessage());
            }
            catch
            {
                return
                    BuildResponseMessage(HttpStatusCode.InternalServerError, new ApiResponseMessage() { Success = false });
            }
        }

        private static ResponseMessageResult BuildResponseMessage(HttpStatusCode statusCode,
            ApiResponseMessage responseMessage)
        {
            return new ResponseMessageResult(new HttpResponseMessage(statusCode)
            {
                Content =
                    new StringContent(JsonConvert.SerializeObject(responseMessage))
            });
        }
    }
}