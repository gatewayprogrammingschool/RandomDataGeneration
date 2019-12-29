# Gateway Programming School - Random Data Generation

![GitHub Release Date](https://img.shields.io/github/release-date/gatewayprogrammingschool/RandomDataGeneration?style=flat-square) ![GitHub Workflow Status](https://img.shields.io/github/workflow/status/gatewayprogrammingschool/randomdatageneration/.NET%20Core?style=flat-square) ![GitHub release (latest SemVer including pre-releases)](https://img.shields.io/github/v/release/gatewayprogrammingschool/randomdatageneration?include_prereleases&sort=semver&style=flat-square)

A reusable and extendable library for creating randomized data with Dotnet Core.

Data is generated in a repeatable pattern based on the specified `Random` seed.

## Packages

Name | Downloads | Version |
-----|-------|------|
[Core](https://www.nuget.org/packages/GPS.RandomDataGenerator) | ![Nuget](https://img.shields.io/nuget/dt/GPS.RandomDataGenerator?label=downloads&style=flat-square) | ![Nuget](https://img.shields.io/nuget/vpre/GPS.RandomDataGenerator?label=version&style=flat-square) |
[Abstractions](https://www.nuget.org/packages/GPS.RandomDataGenerator.Abstractions) | ![Nuget](https://img.shields.io/nuget/dt/GPS.RandomDataGenerator.Abstractions?label=downloads&style=flat-square) | ![Nuget](https://img.shields.io/nuget/vpre/GPS.RandomDataGenerator.Abstractions?label=version&style=flat-square) |
[Base Data](https://www.nuget.org/packages/GPS.RandomDataGenerator.BaseData) | ![Nuget](https://img.shields.io/nuget/dt/GPS.RandomDataGenerator.BaseData?label=downloads&style=flat-square) | ![Nuget](https://img.shields.io/nuget/vpre/GPS.RandomDataGenerator.BaseData?label=version&style=flat-square) |

## Usage

```csharp
    // In your Dependency Injection setup
    services.AddGenerators();


    // To Generate Data

    // Integers
    int seed = 0, count = 10, min = -5, max = 5;

    var ints = Provider.GetService<IntegerGenerator>()?
        .Generate(seed, count, min, max)?
        .ToList();

    // ints contains { 2,3,2,0,-3,0,4,-1,4,-3 }
```

### Simple Record Generation

_TODO_
