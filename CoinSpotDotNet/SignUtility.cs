using CoinSpotDotNet.Common;
using CoinSpotDotNet.Requests;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace CoinSpotDotNet
{
    /// <summary>
    /// Static helper methods to generate request signatures
    /// </summary>
    internal static class SignUtility
    {
        /// <summary>
        /// Creates standard CoinSpot POST body with a single JSON property "nonce"
        /// <para>
        /// See <see href="https://www.coinspot.com.au/api#security"/>
        /// </para>
        /// </summary>
        /// <param name="nonceValue">Optional. Provide a custom nonce value to use. If omitted Unix timestamp is used</param>
        /// <param name="jsonOptions">Optional. Provide custom <see cref="JsonSerializerOptions"/>. If omitted the best case options are used.</param>
        /// <returns><see cref="string"/> containing the serialised JSON to include in the POST body. This value is also used for the signature.</returns>
        internal static string CreatePostData(long? nonceValue = null, JsonSerializerOptions jsonOptions = null)
        {
            var nonce = nonceValue ?? DateTime.UtcNow.ToUnixTimestamp();
            jsonOptions ??= new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = new LowerCaseNamingPolicy(),
                WriteIndented = false,
                Converters =
                {
                    new CoinSpotDateTimeJsonConverter()
                }
            };

            return JsonSerializer.Serialize(new { nonce }, jsonOptions);

        }
        
        /// <summary>
        /// Creates CoinSpot POST body from signable model (i.e. derived from <see cref="CoinSpotRequest"/>)
        /// <para>
        /// See <see href="https://www.coinspot.com.au/api#security"/>
        /// </para>
        /// </summary>
        /// <param name="request">The request body object to serialise</param>
        /// <param name="jsonOptions">Optional. Provide custom <see cref="JsonSerializerOptions"/>. If omitted the best case options are used.</param>
        /// <returns><see cref="string"/> containing the serialised JSON to include in the POST body. This value is also used for the signature.</returns>
        internal static string CreatePostData<TRequest>(TRequest request, JsonSerializerOptions jsonOptions = null)
            where TRequest : CoinSpotRequest
        {
            jsonOptions ??= new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = new LowerCaseNamingPolicy(),
                WriteIndented = false,
                Converters =
                {
                    new CoinSpotDateTimeJsonConverter()
                }
            };

            return JsonSerializer.Serialize(request, jsonOptions);

        }

        /// <summary>
        /// <para>
        /// Sign the <paramref name="postData"/> and return the HMACSHA512 hash encoded in hexadecimal (and forced to lowercase, which CoinSpot requires)
        /// </para>
        /// <para>
        /// See <see href="https://www.coinspot.com.au/api#security"/>
        /// </para>
        /// </summary>
        /// <param name="postData">The JSON string to sign</param>
        /// <param name="secret">Your CoinSpot Secret Key</param>
        /// <returns>The HMACSHA512 hash of <paramref name="postData"/> encoded in hexadecimal</returns>
        internal static string Sign(string postData, string secret)
        {
            var postDataBytes = Encoding.UTF8.GetBytes(postData);
            var postDataHash = ComputeHash(postDataBytes, secret);
            var signature = BitConverter.ToString(postDataHash).Replace("-", string.Empty).ToLowerInvariant();
            return signature;
        }

        /// <summary>
        /// Compute the hash of <paramref name="payload"/> using HMACSHA512 and the <paramref name="secret"/> as a signing key
        /// </summary>
        /// <param name="payload">The <see cref="string"/> to hash</param>
        /// <param name="secret">The secret key used for signature</param>
        /// <returns>A <see cref="byte"/> array representing the hash</returns>
        internal static byte[] ComputeHash(string payload, string secret)
        {
            var bytes = Encoding.UTF8.GetBytes(payload);
            return ComputeHash(bytes, secret);
        }

        /// <summary>
        /// Compute the hash of <paramref name="bytes"/> using HMACSHA512 and the <paramref name="secret"/> as a signing key
        /// </summary>
        /// <param name="bytes">The <see cref="byte"/> array to hash</param>
        /// <param name="secret">The secret key used for signature</param>
        /// <returns>A <see cref="byte"/> array representing the hash</returns>
        internal static byte[] ComputeHash(byte[] bytes, string secret)
        {
            using var hmac = CreateHmac(secret);

            using var ms = new MemoryStream(bytes);

            return hmac.ComputeHash(ms);
        }

        /// <inheritdoc cref="HMACSHA512(byte[])"/>
        internal static HMACSHA512 CreateHmac(string key)
        {
            var secretBytes = Encoding.UTF8.GetBytes(key);
            return new HMACSHA512(secretBytes);
        }
    }
}
