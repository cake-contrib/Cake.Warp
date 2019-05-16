((CakeTask)BuildParameters.Tasks.UploadCoverallsReportTask).Actions.Clear();
((CakeTask)BuildParameters.Tasks.UploadCoverallsReportTask).Criterias.Clear();

BuildParameters.Tasks.UploadCoverallsReportTask
    .WithCriteria(() => BuildParameters.CanPublishToCoveralls, "Necessary credentials is missing")
    .Does(() => {
        var files = GetFiles(BuildParameters.Paths.Directories.TestCoverage + "/coverlet/*");
        if (FileExists(BuildParameters.Paths.Files.TestCoverageOutputFilePath)) {
            files += BuildParameters.Paths.Files.TestCoverageOutputFilePath;
        }

        if (files.Any()) {
            var xmlFiles = files.Where(f => f.Name.EndsWith(".xml"));
            var lcovFiles = files.Where(f => f.Name.EndsWith(".info"));
            CoverallsNet
        }
});
