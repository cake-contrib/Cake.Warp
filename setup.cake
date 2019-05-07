#load nuget:?package=Cake.Recipe&version=1.0.0

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                              buildSystem: BuildSystem,
                              sourceDirectoryPath: "./src",
                              title: "Cake.Warp",
                              repositoryOwner: "cake-contrib",
                              repositoryName: "Cake.Warp",
                              appVeyorAccountName: "cakecontrib",
                              shouldRunGitVersion: true);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context);

// TODO: Add a task to publish Demo project,
// so we can run integration tests against it.

Build.RunDotNetCore();
