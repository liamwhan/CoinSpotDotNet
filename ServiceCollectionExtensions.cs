using CoinSpotDotNet.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace CoinSpotDotNet
{
    /// <summary>
    /// Contains helper methods to register CoinSpotDotNet services for .NET Dependency Injection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers CoinSpot API v1 Services in the DI Container.
        /// 
        /// <para>
        /// CoinSpotDotNet expects your API credentials to be available in the <see cref="IConfiguration"/> object under a section named either "CoinSpot" or "CoinSpotSettings" (case-insensitive). This section will then register an <see cref="IOptions{TOptions}"/> object 
        /// using <c>services.Configure&lt;CoinSpotSettings&gt;(IConfigurationSection)</c> and utilised by the <see cref="ICoinSpotClient"/> when making requests against the API
        /// </para>
        /// <para>
        /// This method registers the following typed <see cref="HttpClient"/> instances in the container using <c>services.AddHttpClient&lt;ICoinSpotClient, CoinSpotClient&gt;</c>:
        /// <list type="bullet">
        /// <item>
        /// <description>
        ///     <see cref="ICoinSpotClient"/>
        /// </description>
        /// </item>
        /// </list>
        /// </para>
        /// </summary>
        /// <example>
        /// In Startup.ConfigureServices()
        /// <code>
        /// services.AddCoinSpotV1(Configuration);
        /// </code>
        /// </example>
        /// <param name="services">The <see cref="IServiceCollection"/> into which the services will be registered</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> object for this application</param>
        /// <returns>The <see cref="IHttpClientBuilder"/> to enable consumers to add Policy Handlers if required</returns>
        public static IHttpClientBuilder AddCoinSpotV1(this IServiceCollection services, IConfiguration configuration)
        {
            var coinspotSection = configuration.GetSection("CoinSpot") ?? configuration.GetSection("CoinSpotSettings");
            if (coinspotSection == null) throw new NullReferenceException("CoinSpotDotNet expects a section named 'CoinSpot' or 'CoinSpotSettings' to be defined but no matching section could be found. Please check your user secrets, appsettings.json etc.");
            services.Configure<CoinSpotSettings>(coinspotSection);
            return services.AddHttpClient<ICoinSpotClient, CoinSpotClient>();

        }

        /// <summary>
        /// Registers CoinSpot API v2 Services in the DI Container.
        /// <para>
        /// CoinSpotDotNet expects your API credentials to be available in the <see cref="IConfiguration"/> object under a section named either "CoinSpot" or "CoinSpotSettings" (case-insensitive). This section will then register an <see cref="IOptions{TOptions}"/> object 
        /// using <c>services.Configure&lt;CoinSpotSettings&gt;(IConfigurationSection)</c> and utilised by the <see cref="ICoinSpotClient"/> when making requests against the API
        /// </para>
        /// <para>
        /// This method registers the following typed <see cref="HttpClient"/> instances in the container using <c>services.AddHttpClient&lt;ICoinSpotClient, CoinSpotClient&gt;</c>:
        /// <list type="bullet">
        /// <item>
        /// <description>
        ///     <see cref="ICoinSpotClientV2"/>
        /// </description>
        /// </item>
        /// </list>
        /// </para>
        /// </summary>
        /// <example>
        /// In Startup.ConfigureServices()
        /// <code>
        /// services.AddCoinSpotV2(Configuration);
        /// </code>
        /// </example>
        /// <param name="services">The <see cref="IServiceCollection"/> into which the services will be registered</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> object for this application</param>
        /// <returns>The <see cref="IHttpClientBuilder"/> to enable consumers to add Policy Handlers if required</returns>
        public static IHttpClientBuilder AddCoinSpotV2(this IServiceCollection services, IConfiguration configuration)
        {
            var coinspotSection = configuration.GetSection("CoinSpot") ?? configuration.GetSection("CoinSpotSettings");
            if (coinspotSection == null) throw new NullReferenceException("CoinSpotDotNet expects a section named 'CoinSpot' or 'CoinSpotSettings' to be defined but no matching section could be found. Please check your user secrets, appsettings.json etc.");
            services.Configure<CoinSpotSettings>(coinspotSection);
            return services.AddHttpClient<ICoinSpotClientV2, CoinSpotClientV2>();
        }
    }
}
