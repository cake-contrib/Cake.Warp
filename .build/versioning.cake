#tool dotnet:?package=dotnet-ccvarn&version=1.1.1
#addin nuget:?package=Newtonsoft.Json&version=12.0.3
#addin nuget:?package=Cake.Json&version=5.2.0

public class BuildVersionWrapper
{
    public int Commits { get; set; }
    public string FullSemVer { get; set; }
    public int Major { get; set; }
    public string MajorMinorPatch { get; set; }
    public string Metadata { get; set; }
    public int Minor { get; set; }
    public int Patch { get; set; }
    public string PreReleaseLable { get; set; }
    public string PreReleaseTag { get; set; }
    public string SemVer { get; set; }
    public int Weight { get; set; }
}

public class BuildDataWrapper
{
    public BuildVersionWrapper Version { get; set; }
}

Setup(context => {
    context.Information("Asserting version");
    var exec = context.Tools.Resolve("dotnet-ccvarn") ?? context.Tools.Resolve("dotnet-ccvarn.exe");
    var outputPath = "./tools/data.json";

	var exitCode = StartProcess(exec, new ProcessSettings
	{
		Arguments = new ProcessArgumentBuilder()
			.Append("parse")
			.AppendQuoted(outputPath)
			.AppendSwitchQuoted("--output", " ", BuildParameters.MilestoneReleaseNotesFilePath.ToString())
            .AppendSwitchQuoted("--output", " ", BuildParameters.FullReleaseNotesFilePath.ToString())
	});

	var buildData = DeserializeJsonFromFile<BuildDataWrapper>(outputPath);

	return new BuildVersion
    {
        Version = buildData.Version.MajorMinorPatch,
        SemVersion = buildData.Version.SemVer,
        Milestone = buildData.Version.MajorMinorPatch,
        CakeVersion = "",
        InformationalVersion = buildData.Version.FullSemVer,
        FullSemVersion = buildData.Version.FullSemVer,
        AssemblySemVer = buildData.Version.MajorMinorPatch + "." + buildData.Version.Commits
    };
});
