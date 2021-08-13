# CoinSpotDotNet
A .NET Standard 2.0 wrapper library for the CoinSpot API v1 and v2.

## Features
 - Well [documented](https://docs.lilypod.tools) with a [full suite of examples](https://docs.lilypod.tools/#demo-setup-instructions)
 - .NET Standard 2.0 target
 - No 3rd-party dependencies (no `Newtonsoft.Json`, this library uses the `System.Text.Json` serialiser out of the box), only `Microsoft` and `System` namespaced dependecies required
 - Supports both Console and ASP.NET web application scenarios, and probably loads of others.
 - Modern fully `async` C# library
 - Currently supports CoinSpot all public and read-only [API v1](https://coinspot.com.au/api) and [API v2 (beta)](https://coinspot.com.au/v2/api) endpoints:
 - Write endpoints are currently not implemented but planned for 1.3.0 release


## Documentation
The source code has been [documented extensively](https://docs.lilypod.tools) and the [full API documentation is a available here](https://docs.lilypod.tools/api/index.html).

Additionally a [Demo project](https://docs.lilypod.tools/#demo-setup-instructions) is included in this repo and contains Swagger/Open API documentation for all of the supported CoinSpot API endpoints that you can easily set up and try out CoinSpotDotNet