#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using CoinSpotDotNet.Common;
using CoinSpotDotNet.Internal;
using CoinSpotDotNet.Requests;
using CoinSpotDotNet.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoinSpotDotNet
{
    /// <summary>
    /// Base class from which <see cref="CoinSpotClient"/> and <see cref="CoinSpotClientV2"/> inherit.
    /// </summary>
    public abstract class BaseClient
    {
        protected CoinSpotSettings Settings
        {
            get
            {
                return options?.CurrentValue ?? settings;
            }
        }

        protected readonly CoinSpotSettings settings;
        protected readonly IOptionsMonitor<CoinSpotSettings> options;
        protected readonly ILogger logger;
        protected readonly HttpClient client;
        protected readonly JsonSerializerOptions jsonOptions = new()
        {
            AllowTrailingCommas = true,
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = new LowerCaseNamingPolicy(),
            WriteIndented = false,
            Converters =
                {
                    new CoinSpotDateTimeJsonConverter(),
                    new CoinSpotStringToDoubleJsonConverter(),
                    new CoinSpotDateTimeResponseJsonConverter()
                }
        };

        protected static readonly Regex invalidCharRegex = new("[^a-zA-Z0-9 /-]", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.CultureInvariant);

        protected BaseClient(CoinSpotSettings settings)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://www.coinspot.com.au", UriKind.Absolute)
            };
            this.settings = settings;
            this.client = client;
            this.logger = new InternalLogger(GetType().Name);

        }

        protected BaseClient(IOptionsMonitor<CoinSpotSettings> options, ILogger logger, HttpClient client)
        {
            client.BaseAddress = new Uri("https://www.coinspot.com.au", UriKind.Absolute);
            this.options = options;
            this.logger = logger;
            this.client = client;
        }

        protected async Task<HttpResponseMessage> Post<TRequest>(Uri requestUri, TRequest request)
            where TRequest : CoinSpotRequest
        {
            var postData = SignUtility.CreatePostData(request, jsonOptions);
            var sign = SignUtility.Sign(postData, Settings.ReadOnlySecret);

            return await Post(requestUri, postData, sign);
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


        protected async Task<HttpResponseMessage> Get<TRequest>(Uri requestUri, TRequest request)
            where TRequest : CoinSpotRequest
        {
            var bodyContent = SignUtility.CreatePostData(request, jsonOptions);
            var sign = SignUtility.Sign(bodyContent, Settings.ReadOnlySecret);

            return await Get(requestUri, bodyContent, sign);
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
                request.Headers.Add("key", Settings.ReadOnlyKey);
                request.Headers.Add("sign", sign);
            }

            var response = await client.SendAsync(request);
#if TRACE
            var responseString = await response.Content.ReadAsStringAsync();
            logger.LogInformation(responseString);
#endif
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
