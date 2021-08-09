using System.Text.Json;

namespace CoinSpotDotNet.Common
{
    /// <summary>
    /// Lowercase Naming Policy for <see cref="System.Text.Json"/> serialiser
    /// </summary>
    public sealed class LowerCaseNamingPolicy : JsonNamingPolicy
    {
        /// <inheritdoc />
        public override string ConvertName(string name) => name.ToLowerInvariant();
    }
}
