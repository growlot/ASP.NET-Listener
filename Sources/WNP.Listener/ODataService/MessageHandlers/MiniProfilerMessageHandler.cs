using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using StackExchange.Profiling;

namespace AMSLLC.Listener.ODataService.MessageHandlers
{
    public class MiniProfilerMessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            MiniProfiler.Start();
            var response = await base.SendAsync(request, cancellationToken);
            MiniProfiler.Stop();

            if (response.Content != null)
            {
                var responseMessage = await response.Content.ReadAsStringAsync();
                if (IsJson(responseMessage))
                {
                    var jsSerializer = new JavaScriptSerializer();
                    var obj = jsSerializer.DeserializeObject(responseMessage);

                    var dic = (Dictionary<string, object>)obj;
                    dic.Add("profile", jsSerializer.DeserializeObject(MiniProfiler.ToJson()));

                    response.Content = new StringContent(jsSerializer.Serialize(dic));
                }
            }

            return response;
        }

        private static bool IsJson(string input)
        {
            input = input.Trim();
            return input.StartsWith("{") && input.EndsWith("}")
                   || input.StartsWith("[") && input.EndsWith("]");
        }
    }
}