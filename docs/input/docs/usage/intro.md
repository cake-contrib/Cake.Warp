---
Order: 10
Title: Introduction
Author: Kim Nordmo
---

# Getting Started

This addin is designed to be used inside of cake scripts. To start using it, first you must add a cake [preprocessor directive](http://cakebuild.net/docs/fundamentals/preprocessor-directives) to your script as below.

```cs
// latest version 
#addin "Cake.Warp"

// or
#addin "nuget?package=Cake.Warp"

// for a specific version, use ?version=
#addin "Cake.Warp?version=0.3.0"
```

When the cake script is run, this will download the latest version of the `Cake.Warp` nuget package and will now be available to use inside of the script.
