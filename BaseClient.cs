#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using CoinSpotDotNet.Common;
using CoinSpotDotNet.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoinSpotDotNet
{
    /// <summary>
    /// Base class from which <see cref="CoinSpotClient"/> and <see cref="CoinSpotClientV2"/> inherit.
    /// </summary>
    public abstract class BaseClient
    {

        protected readonly IOptionsMonitor<CoinSpotSettings> options;
        protected readonly ILogger logger;
        protected readonly HttpClient client;
        protected readonly JsonSerializerOptions jsonOptions;

        protected BaseClient(IOptionsMonitor<CoinSpotSettings> options, ILogger logger, HttpClient client)
        {
            client.BaseAddress = new Uri("https://www.coinspot.com.au", UriKind.Absolute);
            this.options = options;
            this.logger = logger;
            this.client = client;
            this.jsonOptions = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = new LowerCaseNamingPolicy(),
                WriteIndented = false,
                Converters =
                {
                    new CoinSpotDateTimeJsonConverter(),
                    new CoinSpotStringToDoubleJsonConverter()
                }
            };
        }

        protected async Task<HttpResponseMessage> Post(Uri requestUri, string postData, string sign)
        {
            using var request = new HttpRequestMessage
            {
                RequestUri = requestUri,
                Method = HttpMethod.Post,
                Content = new StringContent(postData, Encoding.UTF8, "application/json")
            };

            var response = await Send(request, sign);

            return response;
        }
        
        protected async Task<HttpResponseMessage> Get(Uri requestUri, string bodyContent, string sign)
        {
            using var request = new HttpRequestMessage
            {
                RequestUri = requestUri,
                Method = HttpMethod.Get,
            };
            
            if (!string.IsNullOrWhiteSpace(bodyContent))
            {
                request.Content = new StringContent(bodyContent, Encoding.UTF8, "application/json");
            }

            var response = await Send(request, sign);

            return response;
        }
        
        private async Task<HttpResponseMessage> Send(HttpRequestMessage request, string sign)
        {
            if (!string.IsNullOrWhiteSpace(sign))
            {
                request.Headers.Add("key", options.CurrentValue.ReadOnlyKey);
                request.Headers.Add("sign", sign);
            }

            var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                logger.LogError("CoinSpot error response on path '{path}'. Status: {statusCode}, message: {message}", request.RequestUri.OriginalString, response.StatusCode, responseBody);
            }

            return response;
        }
        
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
