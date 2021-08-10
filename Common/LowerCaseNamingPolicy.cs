using System.Text.Json;

namespace CoinSpotDotNet.Common
{
    /// <summary>
    /// Lowercase Naming Policy for <see cref="System.Text.Json"/> serialiser
    /// </summary>
    internal sealed class LowerCaseNamingPolicy : JsonNamingPolicy
    {
        /// <summary>
        /// Converts the property name
        /// </summary>
        /// <param name="name">The property name passed in my the serialiser</param>
        /// <returns>The converted property name</returns>
        public override string ConvertName(string name) => name.ToLowerInvariant();
    }
}
