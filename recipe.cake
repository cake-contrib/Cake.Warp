#load nuget:https://f.feedz.io/wormiecorp/packages/nuget?package=Cake.Recipe&version=2.0.0-unstable0312&prerelease

const string WarpVersion = "0.3.0";
DirectoryPath downloadDir = (DirectoryPath)"./src/Cake.Warp/warp";
const string baseDownloadFormat = "https://github.com/dgiagio/warp/releases/download/v" + WarpVersion + "/{0}.warp-packer";
readonly string[] downloadUrls = {
    string.Format(baseDownloadFormat, "linux-x64"),
    string.Format(baseDownloadFormat, "macos-x64"),
    string.Format(baseDownloadFormat, "windows-x64") + ".exe"
};

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                              buildSystem: BuildSystem,
                              sourceDirectoryPath: "./src",
                              title: "Cake.Warp",
                              repositoryOwner: "cake-contrib",
                              repositoryName: "Cake.Warp",
                              appVeyorAccountName: "cakecontrib",
                              shouldRunGitVersion: true,
                              shouldRunCodecov: true,
                              shouldRunDotNetCorePack: true,
                              shouldDownloadMilestoneReleaseNotes: true,
                              shouldDownloadFullReleaseNotes: true,
                              fullReleaseNotesFilePath: "./CHANGELOG.md");

BuildParameters.PrintParameters(Context);
BuildParameters.Tasks.ExportReleaseNotesTask.IsDependentOn("Clean");

ToolSettings.SetToolSettings(context: Context,
    dupFinderExcludePattern: new[] {
        "**/*Tests/**/*.cs",
        "**/*.Demo/**/*.cs",
        "**/*.AssemblyInfo.cs"
    },
    testCoverageFilter: "+[Cake.Warp*]* -[*Tests]*");

// TODO: Add a task to publish Demo project,
// so we can run integration tests against it.

Task("Download-Warp")
    .IsDependeeOf("DotNetCore-Build")
    .Does(() => {
        EnsureDirectoryExists(downloadDir);
}).DoesForEach(downloadUrls, (url) =>
{
    var fileName = url.Split('/').Last();
    var fullFilePath = downloadDir.CombineWithFilePath(fileName);
    if (!FileExists(fullFilePath)) {
        DownloadFile(url, fullFilePath);
    }
});

Build.RunDotNetCore();
