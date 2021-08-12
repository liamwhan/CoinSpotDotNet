namespace CoinSpotDotNet.Internal
{
    internal static class Helpers
    {
        internal static string Format(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }
}
