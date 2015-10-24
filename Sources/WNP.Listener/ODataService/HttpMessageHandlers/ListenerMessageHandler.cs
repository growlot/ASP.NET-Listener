// //-----------------------------------------------------------------------
// <copyright file="ListenerMessageHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ODataService.HttpMessageHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Serilog;

    public class ListenerMessageHandler : DelegatingHandler
    {
        /// <inheritdoc/>
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var content = await request.Content.ReadAsStringAsync();
            if (IsJson(content))
            {
                var contentAsExpando =
                    JsonConvert.DeserializeObject<ExpandoObject>(content) as IDictionary<string, object>;
                if (contentAsExpando != null)
                {
                    Dictionary<string, object> header = new Dictionary<string, object>();
                    Dictionary<string, object> body = new Dictionary<string, object>();
                    foreach (var o in contentAsExpando)
                    {
                        if (ListenerRequestHeaderMap.Instance.ContainsKey(o.Key))
                        {
                            header.Add(o.Key, o.Value);
                        }

                        body.Add(o.Key, o.Value);
                    }

                    request.Properties["ListenerRequestBody"] = JsonConvert.SerializeObject(body);

                    var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
                    mediaType.Parameters.Add(new NameValueHeaderValue("odata", "verbose"));
                    request.Content = new ObjectContent(
                        typeof(Dictionary<string, object>),
                        header,
                        new JsonMediaTypeFormatter(),
                        mediaType);
                }
            }

            HttpResponseMessage response = null;
            try
            {
                response = await base.SendAsync(request, cancellationToken);
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Listener message handler failed");
                throw;
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