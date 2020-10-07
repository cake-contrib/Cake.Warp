((CakeTask)BuildParameters.Tasks.CreateReleaseNotesTask.Task).Actions.Clear();

BuildParameters.Tasks.CreateReleaseNotesTask
    .WithCriteria(() => BuildParameters.IsTagged)
    .Does<BuildVersion>((context, buildVersion) => RequireTool(BuildParameters.IsDotNetCoreBuild ? ToolSettings.GitReleaseManagerGlobalTool : ToolSettings.GitReleaseManagerTool, () => {
    if (BuildParameters.CanUseGitReleaseManager)
    {
        var settings = new GitReleaseManagerCreateSettings
        {
            Name = buildVersion.Milestone,
            TargetCommitish = BuildParameters.MasterBranchName,
            Prerelease = context.HasArgument("create-pr-release"),
            InputFilePath = BuildParameters.FullReleaseNotesFilePath
        };

        GitReleaseManagerCreate(BuildParameters.GitHub.Token, BuildParameters.RepositoryOwner, BuildParameters.RepositoryName, settings);
    }
    else
    {
        Warning("Unable to use GitReleaseManager, as necessary credentials are not available");
    }
}));
