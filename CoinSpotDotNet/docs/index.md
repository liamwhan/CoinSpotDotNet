# CoinSpotDotNet
[![Nuget Badge](https://img.shields.io/nuget/v/CoinSpotDotNet?style=plastic)](https://www.nuget.org/packages/CoinSpotDotNet)
> A .NET Standard 2.0 compatible class library for CoinSpot API. Supports API versions 1 and 2

## Contents
- [CoinSpotDotNet](#coinspotdotnet)
  - [Contents](#contents)
  - [Features](#features)
  - [Documentation](#documentation)
    - [Demo](#demo)
      - [Demo setup instructions](#demo-setup-instructions)
  - [Getting Started](#getting-started)
    - [Installation](#installation)
      - [Nuget](#nuget)
      - [`dotnet` CLI](#dotnet-cli)
    - [Usage](#usage)
      - [ASP.NET Web Application](#aspnet-web-application)
      - [Console applications and other scenarios](#console-applications-and-other-scenarios)
  - [General Notes](#general-notes)
      - [API Support](#api-support)
      - [CoinSpotClient classes](#coinspotclient-classes)
      - [Dependencies](#dependencies)
  - [License](#license)

## Features
 - .NET Standard 2.0 target
 - No 3rd-party dependencies (no `Newtonsoft.Json`, this library uses the `System.Text.Json` serialiser out of the box)
 - Supports both Console and ASP.NET web application scenarios, and probably loads of others.
 - Modern fully `async` C# library

## Documentation
This source code has been documented extensively and the [full API documentation is a available here](https://docs.lilypod.tools/api/index.html).

Additionally a [Demo project](#demo-setup-instructions) is included in this repo and contains Swagger/Open API documentation for all of the supported CoinSpot API endpoints that you can easily set up and try out CoinSpotDotNet

### Demo
There is a Sample ASP.NET Web API project with a swagger you can use to test out the library and as a usage example.

#### Demo setup instructions
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
4. Start debugging/run the project and open a browser to `http://localhost:5000/swagger/index.html`



## Getting Started
### Installation
#### Nuget
```pwsh
Install-Package CoinSpotDotNet -Version 0.0.1
```

#### `dotnet` CLI
```bat
dotnet add package CoinSpotDotNet --version 0.0.1
```

### Usage
#### ASP.NET Web Application
>**NOTE:** Both V1 and V2 clients are used side-by-side in the examples below to demonstrate usage, while this won't cause any problems, in practice you would probably only want one or the other.

If you're building an ASP.NET Web application then getting started is as simple as:
1. Add your [CoinSpot API credentials](https://www.coinspot.com.au/my/api) to the applications `IConfiguration` under the section `CoinSpot`

   This can be done in any way you like, (i.e. User secrets, secure storage etc.) but we recommend you read up on [safe storage of secrets in ASP.NET](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0)

   For example, here is what the credentials would look like in an `appsettings.json` file (which we don't recommend, but are using here to demonstrate the structure that CoinSpotDotNet expects):

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
   This will register [a typed `HttpClient`](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#how-to-use-typed-clients-with-ihttpclientfactory) into the DI Container. See [the CoinSpotClient DI constructor API documentation](https://docs.lilypod.tools/api/CoinSpotDotNet.CoinSpotClientV2.html#CoinSpotDotNet_CoinSpotClientV2__ctor_Microsoft_Extensions_Options_IOptionsMonitor_CoinSpotDotNet_Settings_CoinSpotSettings__Microsoft_Extensions_Logging_ILogger_CoinSpotDotNet_CoinSpotClientV2__System_Net_Http_HttpClient_) for more information.

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

#### Console applications and other scenarios
If you're not using ASP.NET then constructing a Client is arguably even simpler:
```cs
using CoinSpotDotNet;

namespace MyConsoleApp 
{

    public class Program
    {
        public static Main(string[] args)
        {
            CoinSpotSettings settings = new()
            {
                ReadOnlyKey = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                ReadOnlySecret = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
            };
            var clientV1 = new CoinSpotClient(settings);
            var clientV2 = new CoinSpotClientV2(settings);
        }
    }

}

```

## General Notes
#### API Support
The current CoinSpot API documentation and developer tools are limited and this library is not officially supported by, or affiliated with CoinSpot in any way. This makes some parts of the API very  difficult to test, especially where my personal account does not have any data to test with, e.g. the affiliate and referral payments endpoints. As a result this library currently only supports:
- The read-only [v1 API](https://coinspot.com.au/api#rosummary) endpoints
- The read-only [v2 API](https://coinspot.com.au/v2/api#rosummary) endpoints
- The public [v1 API](https://coinspot.com.au/api#latestprices) endpoints
- The public [v2 API](https://www.coinspot.com.au/v2/api#latestprices) endpoints

#### CoinSpotClient classes
CoinSpotDotNet exposes 2 primary classes of interest to consuming applications:
- `ICoinSpotClient` - calls CoinSpot API v1 endpoints
- `ICoinSpotClientV2` - calls CoinSpot API v2 endpoints

They are independent classes and can be used interchangably and they expose mostly the same methods at the moment (the v2 API has more endpoints so the client has more corresponding methods).

#### Dependencies
The target framework is .NET Standard 2.0 and the only dependencies are Microsoft .NET libraries. There are no 3rd-party dependencies.

You can use this library in a console app (or other non-ASP environment) by using the [simple constructor](https://docs.lilypod.tools/api/CoinSpotDotNet.CoinSpotClientV2.html#CoinSpotDotNet_CoinSpotClientV2__ctor_CoinSpotDotNet_Settings_CoinSpotSettings_)


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
