---
Order: 1
Title: Building on Windows
Author: Kim Nordmo
---

## Requirements

The following are need to build Cake.Warp on Windows:

- Visual Studio 2019 (or as long as MSBuild 16.0 is installed)
- .NET Core SDK 2.1 *(could work with other versions as well)*
- .NET Framework 4.7.1

All other dependencies will be automatically downloaded when invoking the build script.

## Invoking the build itself

1. To build the Cake.Warp library, just open powershell and navigate to the root of
downloaded/cloned repository.
2. After that just type `.\build.ps1` and everything will be automatically built and all unit tests
will run.

## Creating a redistributable nuget package

To create a nuget package you can follow the same process as when building the library,
with the exception of calling `.\build.ps1` without any arguments.
The only difference is to run the build script with the following: `.\build.ps1 -Target Package`.
