// <copyright file="MiniProfilerMessageHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

using System.Text;

namespace AMSLLC.Listener.ODataService.HttpMessageHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Script.Serialization;
    using StackExchange.Profiling;

    public class MiniProfilerMessageHandler : DelegatingHandler
    {
        /// <inheritdoc/>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            MiniProfiler.Start();

            // request.Content.
            var response = await base.SendAsync(request, cancellationToken);
            MiniProfiler.Stop();

            if (response.Content != null)
            {
                var responseMessage = await response.Content.ReadAsStringAsync();
                if (IsJson(responseMessage))
                {
                    var jsSerializer = new JavaScriptSerializer { MaxJsonLength = int.MaxValue };
                    var obj = jsSerializer.DeserializeObject(responseMessage);

                    var dic = (Dictionary<string, object>)obj;
                    dic.Add("@profile", jsSerializer.DeserializeObject(MiniProfiler.ToJson()));

                    response.Content = new StringContent(jsSerializer.Serialize(dic), Encoding.UTF8, "application/json");
                }
            }

            return response;
        }

        private static bool IsJson(string input)
        {
            input = input.Trim();
            return (input.StartsWith("{") && input.EndsWith("}"))
                   || (input.StartsWith("[") && input.EndsWith("]"));
        }
    }
}