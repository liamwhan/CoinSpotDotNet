# CoinSpotDotNet
[![Nuget Badge](https://img.shields.io/nuget/v/CoinSpotDotNet?style=plastic)](https://www.nuget.org/packages/CoinSpotDotNet)
> A .NET Standard 2.0 compatible class library for CoinSpot API. Supports API versions 1 and 2

### Disclaimer
This library is in a pre-alpha state and currently only supports the read-only <a href="https://coinspot.com.au/api#rosummary" target="_blank">v1 API</a> and <a href="https://coinspot.com.au/v2/api#rosummary" target="_blank">v2 API</a>.

## Demo
There is a Sample ASP.NET Web API project with a swagger you can use to test out the library and as a usage example.

### Demo setup instructions
1. Clone the project 
   ```sh
   git clone https://github.com/liamwhan/CoinSpotDotNet
   ```
2. Add your CoinSpot read-only API key and secret as a user secret with the dotnet CLI
   ```sh
   cd CoinSpotDotNet/CoinSpotDotNet.Samples
   dotnet user-secrets init
   dotnet user-secrets set "CoinSpot:ReadOnlyKey" "{YOUR_READ_ONLY_KEY}"
   dotnet user-secrets set "CoinSpot:ReadOnlySecret" "{YOUR_READ_ONLY_SECRET}"
   ```
3. Open the solution in Visual Studio and start debugging the CoinSpotDotNet.Samples project
4. Explore the swagger API documentation



## Getting Started

### Nuget Install
```pwsh
Install-Package CoinSpotDotNet -Version 0.0.1
```

### `dotnet` CLI Install
```bat
dotnet add package CoinSpotDotNet --version 0.0.1
```


### ASP.NET Web Application
>**NOTE:** Both V1 and V2 clients are used side-by-side in the examples below to demonstrate usage, while this won't cause any problems, in practice you would probably only want one or the other.

If you're building an ASP.NET Web application then getting started is as simple as:
1. Add your <a href="https://www.coinspot.com.au/my/api" target="_blank">CoinSpot API credentials</a> to the applications `IConfiguration` under the section `CoinSpot`

   This can be done in any way you like, (i.e. User secrets, secure storage etc.) but we recommend you read up on <a href="https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0" target="_blank">safe storage of secrets in ASP.NET</a>

   For example, here is what the credentials would look like in an `appsettings.json` file (which we don't recommend, but are using here to demonstrate the structure that the CoinSpotDotNet expects):

   ```json
    {
        "CoinSpot": 
        {
            "ReadOnlyKey": "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
            "ReadOnlySecret": "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
        }
    }
   ```

2. Register CoinSpotDotNet in the .NET DI Container. Example `Startup.cs` class
   ```cs
    using CoinSpotDotNet;

    namespace MyProject 
    {
        public class Startup 
        {
            public IConfiguration Configuration { get; set; }
            
            public Startup(IConfiguration config)
            {
                Configuration = config;
            }

            public void ConfigureServices(IServiceCollection services)
            {
                // ... Standard .NET service setup
                services.AddCoinSpotV1(Configuration); // Registers ICoinSpotClient for CoinSpot v1 API;
                services.AddCoinSpotV2(Configuration); // Registers ICoinSpotClientV2 for CoinSpot v2 API;

            }
        }
    }
   ```
   This will register <a href="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#how-to-use-typed-clients-with-ihttpclientfactory" target="_blank">a typed `HttpClient`</a> into the DI Container
3. Inject `ICoinSpotClient` or `ICoinSpotClientV2` into your consuming services. 
   
   ```cs
    using CoinSpotDotNet;
    namespace MyProject.Services 
    {

        public class MyService 
        {
            private readonly ICoinSpotClient clientV1;
            private readonly ICoinSpotClientV2 clientV2;

            public MyService(ICoinSpotClient clientV1, ICoinSpotClientV2 clientV2)
            {
                this.clientV1 = clientV1;
                this.clientV2 = clientV2;
            }

            public async Task MyMethod()
            {
                var balancesV1 = await clientV1.ListMyBalances();
                var balancesV2 = await clientV2.ListMyBalances();

            }
        }

    }
   
   ```



## General Notes
#### CoinSpotClient classes
CoinSpotDotNet exposes 2 primary classes of interest to consuming applications:
- `ICoinSpotClient` - calls CoinSpot API v1 endpoints
- `ICoinSpotClientV2` - calls CoinSpot API v2 endpoints

They are independent classes and can be used interchangably and they expose mostly the same methods at the moment (the v2 API has more endpoints so the client has more corresponding methods).

#### Dependencies
The target framework is .NET Standard 2.0 and the only dependencies are Microsoft .NET libraries. There are no 3rd-party dependencies.

The library does currently require `IOptions<TOptions>` support (Microsoft.Extensions.Options) and assumes logging has been configured and `ILogger<T>` (Microsoft.Extensions.Logging.Abstractions) instances are available. 

If you are using this library in other contexts (i.e. Console applications), please lodge a ticket on the <a href="https://github.com/liamwhan/CoinSpotDotNet">GitHub repo</a> and I will consider removing these dependencies.


## License
MIT License

Copyright (c) 2021 Liam Whan

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
