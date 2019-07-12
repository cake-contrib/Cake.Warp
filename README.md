# Cake.Warp

[![All Contributors](https://img.shields.io/badge/all_contributors-1-orange.svg?style=flat-square)](#contributors)
[![standard-readme compliant][]][standard-readme]
[![License][licenseimage]][license]
[![Appveyor build][appveyorimage]][appveyor]
[![Travis build][travisimage]][travis]
[![Codecov Report][codecovimage]][codecov]
[![NuGet package][nugetimage]][nuget]

> Cake addin for creating self-contained single binary applications using [warp](https://github.com/dgiagio/warp)

## Table of Contents

- [Install](#install)
- [Usage](#usage)
- [Maintainer](#maintainer)
- [Contributing](#contributing)
  - [Contributors](#contributors)
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

## Maintainer

[Kim J. Nordmo @AdmiringWorm][maintainer]

## Contributing

Cake.Warp follows the [Contributor Covenant][contrib-covenant] Code of Conduct.

We accept Pull Requests.
Please see [the contributing file][contributing] for how to contribute to Cake.Warp.

Commits to the repository is expected to be signed with a valid GPG key with the contributors email addresses.

Small note: If editing the Readme, please conform to the [standard-readme][] specification.

This project follows the [all-contributors][] specification. Contributions of any kind welcome!

### Contributors

Thanks goes to these wonderful people ([emoji key][emoji-key]):

<!-- ALL-CONTRIBUTORS-LIST:START - Do not remove or modify this section -->
<!-- prettier-ignore -->
<table><tr><td align="center"><a href="https://github.com/AdmiringWorm"><img src="https://avatars3.githubusercontent.com/u/1474648?v=4" width="70px;" alt="Kim J. Nordmo"/><br /><sub><b>Kim J. Nordmo</b></sub></a><br /><a href="#maintenance-AdmiringWorm" title="Maintenance">🚧</a></td></tr></table>

<!-- ALL-CONTRIBUTORS-LIST:END -->

## License

[MIT License © Kim J. Nordmo][license]

[all-contributors]: https://github.com/all-contributors/all-contributors
[appveyor]: https://ci.appveyor.com/project/cakecontrib/cake-warp
[appveyorimage]: https://img.shields.io/appveyor/ci/cakecontrib/cake-warp.svg?logo=appveyor&style=flat-square
[codecov]: https://codecov.io/gh/cake-contrib/Cake.Warp
[codecovimage]: https://img.shields.io/codecov/c/github/cake-contrib/Cake.Warp.svg?logo=codecov&style=flat-square
[contrib-covenant]: https://www.contributor-covenant.org/version/1/4/code-of-conduct
[contributing]: https://github.com/cake-contrib/Cake.Warp/blob/develop/CONTRIBUTING.md
[emoji-key]: https://allcontributors.org/docs/en/emoji-key
[maintainer]: https://github.com/AdmiringWorm
[nuget]: https://nuget.org/packages/Cake.Warp
[nugetimage]: https://img.shields.io/nuget/v/Cake.Warp.svg?logo=nuget&style=flat-square
[license]: https://github.com/cake-contrib/Cake.Warp/blob/develop/LICENSE
[licenseimage]: https://img.shields.io/github/license/cake-contrib/Cake.Warp.svg?style=flat-square
[standard-readme]: https://github.com/RichardLitt/standard-readme
[standard-readme compliant]: https://img.shields.io/badge/readme%20style-standard-brightgreen.svg?style=flat-square
[travis]: https://travis-ci.org/cake-contrib/Cake.Warp
[travisimage]: https://img.shields.io/travis/cake-contrib/Cake.Warp.svg?logo=travis&style=flat-square
