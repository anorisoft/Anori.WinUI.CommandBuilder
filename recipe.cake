#load nuget:?package=Cake.Recipe&version=2.2.0
#load nuget:?package=Microsoft.CodeAnalysis.Scripting&version=3.9.0

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context,
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./Source",
    title: "Anori.WinUI.Commands",
    repositoryOwner: "anorisoft",
    repositoryName: "Anori.WinUI.Commands",
    appVeyorAccountName: "anorisoft",
	shouldGenerateDocumentation: true,
    shouldRunDupFinder: false,
    shouldRunCodecov: false,
    shouldRunCoveralls: true,
    shouldRunDotNetCorePack: true);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(
    context: Context,
    buildMSBuildToolVersion: MSBuildToolVersion.VS2019);

Build.RunDotNetCore();
