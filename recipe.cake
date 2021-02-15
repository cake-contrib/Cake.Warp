#define CUSTOM_VERSIONING
#load ./.build/versioning.cake
#load nuget:?package=Cake.Recipe&version=2.0.0
#load ./.build/release-notes.cake
#load ./.build/codecov.cake

const string WarpVersion = "0.4.4";
DirectoryPath downloadDir = (DirectoryPath)"./src/Cake.Warp/warp";
const string baseDownloadFormat = "https://github.com/fintermobilityas/warp/releases/download/v" + WarpVersion + "/{0}.warp-packer";
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
                              shouldBuildNugetSourcePackage: true,
                              shouldRunCodecov: true,
                              shouldRunCoveralls: false,
                              shouldRunDotNetCorePack: true,
                              shouldDownloadMilestoneReleaseNotes: false,
                              milestoneReleaseNotesFilePath: "./tools/RELEASE_NOTES.txt",
                              fullReleaseNotesFilePath: "./tools/RELEASE_NOTES.md"); // Just a hack

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

BuildParameters.Tasks.DotNetCoreBuildTask.IsDependentOn(BuildParameters.Tasks.CreateReleaseNotesTask);

Build.RunDotNetCore();
