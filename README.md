# Dependency Injection Extensions 
by WeKnow  
Weknow.DI.Extensions: [![NuGet](https://img.shields.io/nuget/v/Weknow.DI.Extensions.svg)](https://www.nuget.org/packages/Weknow.DI.Extensions/)  
Weknow.DI.Extensions.Contracts: [![NuGet](https://img.shields.io/nuget/v/Weknow.DI.Extensions.Contracts.svg)](https://www.nuget.org/packages/Weknow.DI.Extensions.Contracts/)  
[![Prepare](https://github.com/weknow-network/Weknow-DI-Extensions/actions/workflows/prepare-nuget.yml/badge.svg)](https://github.com/weknow-network/Weknow-DI-Extensions/actions/workflows/prepare-nuget.yml)
[![Build & Deploy NuGet](https://github.com/weknow-network/Weknow-DI-Extensions/actions/workflows/Deploy.yml/badge.svg)](https://github.com/weknow-network/Weknow-DI-Extensions/actions/workflows/Deploy.yml)

## Keyed Dependency Injection using .NET

You can register your components as:

``` cs
builder.Services.AddKeyedSingleton<IFunctionality, AFunctionality>("A");
builder.Services.AddKeyedSingleton<IFunctionality, BFunctionality>("B");
```

And use it as follow:

``` cs
[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly IKeyed<IFunctionality> _selector;

    public TestController(IKeyed<IFunctionality> selector)
    {
        _selector = selector;
    }

    [HttpGet("a")]
    public string GetA()
    {
        return _selector["A"].Id;
    }

    [HttpGet("b")]
    public string GetB()
    {
        return _selector["B"].Id;
    }
}
```

Looking for other extensions?  
Check the following
- [Json extenssions](https://github.com/weknow-network/Weknow-Json-Extensions)
- [Async extensions](https://github.com/weknow-network/Bnaya.CSharp.AsyncExtensions)
- [Basic extensions](https://github.com/weknow-network/Weknow-BasicExtensions/blob/master/README.md)
