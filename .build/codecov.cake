((CakeTask)BuildParameters.Tasks.UploadCodecovReportTask.Task).Actions.Clear();

BuildParameters.Tasks.UploadCodecovReportTask
    .Does(() => RequireTool(BuildParameters.IsDotNetCoreBuild ? ToolSettings.CodecovGlobalTool : ToolSettings.CodecovTool, () =>
{
    var coverageFiles = GetFiles(BuildParameters.Paths.Directories.TestCoverage + "/coverlet/*.xml");
    if (FileExists(BuildParameters.Paths.Files.TestCoverageOutputFilePath))
    {
        coverageFiles += BuildParameters.Paths.Files.TestCoverageOutputFilePath;
    }

    if (coverageFiles.Any())
    {
        var settings = new CodecovSettings {
            Files = coverageFiles.Select(f => f.FullPath),
            Required = true
        };

        Codecov(settings);
    }
}));
