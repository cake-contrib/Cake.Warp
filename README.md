# Cake.Warp

[![All Contributors](https://img.shields.io/badge/all_contributors-0-orange.svg?style=flat-square)](#contributors)
[![standard-readme compliant](https://img.shields.io/badge/standard--readme-OK-green.svg?style=flat-square)](https://github.com/RichardLitt/standard-readme)
![GitHub](https://img.shields.io/github/license/cake-contrib/Cake.Warp.svg?style=flat-square)
[![AppVeyor](https://img.shields.io/appveyor/ci/cakecontrib/cake-warp.svg?logo=AppVeyor&style=flat-square)](https://ci.appveyor.com/project/cakecontrib/cake-warp)
[![Travis (.org)](https://img.shields.io/travis/cake-contrib/Cake.Warp.svg?logo=travis&style=flat-square)](https://travis-ci.org/cake-contrib/Cake.Warp)
[![Codecov](https://img.shields.io/codecov/c/github/cake-contrib/Cake.Warp.svg?logo=codecov&style=flat-square)](https://codecov.io/gh/cake-contrib/Cake.Warp)
[![Nuget](https://img.shields.io/nuget/v/Cake.Warp.svg?logo=nuget&style=flat-square)](https://nuget.org/packages/Cake.Warp)

> Cake addin for creating self-contained single binary applications using [warp](https://github.com/dgiagio/warp)

## Table of Contents

- [Install](#install)
- [Usage](#usage)
- [Maintainers](#maintainers)
- [Contributing](#contributing)
- [License](#license)

## Install

You may start using Cake.Warp as soon as you have imported the addin into you cake build script with the following:

```cs
#addin nuget:?package=Cake.Warp&version=0.1.0
```

## Usage

The most basic use of Cake.Warp is by first creating a standalone
executable through dotnet publish, then calling Warp to create the self-contained binary.

```cs
#addin nuget:?package=Cake.Warp&version=0.1.0

Task("Create-Warp-Binary")
    .Does(() =>
{
    DotNetCorePublish("./src/Cake.Warp.Demo", new DotNetCorePublishSettings {
        Framework         = "netcoreapp2.0",
        RuntimeIdentifier = "linux-x64",
        Configuration     = "Release",
        OutputDirectory   = "./artifacts/output"
    });
    Warp("./artifacts/output",
         "Cake.Warp.Demo", // Must include .exe if creating for windows
         "./artifacts/cake-warp-demo",
         WarpPlatforms.LinuxX64
    );
});
```

## Maintainers

[@AdmiringWorm](https://github.com/AdmiringWorm)

## Contributing

See [the contributing file](CONTRIBUTING.md)!

PRs accepted.

Cake.Warp follows the [Contributor Covenant](https://www.contributor-covenant.org/version/1/4/code-of-conduct) Code of Conduct.

Commits to the repository is required to be signed with a valid GPG key with the contributors email addresses.

Small note: If editing the README, please conform to the [standard-readme](https://github.com/RichardLitt/standard-readme) specification.

Thanks goes to these wonderful people ([emoji key](https://allcontributors.org/docs/en/emoji-key)):

This project follows the [all-contributors](https://github.com/all-contributors/all-contributors) specification. Contributions of any kind welcome!

### Contributors

<!-- ALL-CONTRIBUTORS-LIST:START - Do not remove or modify this section -->
<!-- prettier-ignore -->
<!-- ALL-CONTRIBUTORS-LIST:END -->

## License

MIT © 2019 Kim J. Nordmo
