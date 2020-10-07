#!/bin/bash

echo "Restoring .NET Core tools"
dotnet tool restore

if [ -f "./includes.cake" ]; then
    rm -f "./includes.cake"
fi

echo "Bootstrapping Cake"
dotnet cake recipe.cake --bootstrap

echo "Running Build"
dotnet cake recipe.cake "$@"
