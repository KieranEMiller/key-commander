#tool "nuget:?package=NUnit.Runners&version=2.6.4"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");

var pathBin = Argument("pathBin", "./bin");
var pathSln = Argument("pathSln", "./src/KeyCdr.sln");

Task("Init")
 .IsDependentOn("Clean")
 .IsDependentOn("NuGet")
 .Does(() => {
	Information("cake build started: {0}", DateTime.Now.ToString());
});

Task("Clean")
 .Does(() => {
	Information(string.Format("cleaning directory: {0}", pathBin));
	EnsureDirectoryExists(pathBin);
	CleanDirectory(pathBin);
});

Task("NuGet")
 .Does(() =>
{
	Information("restoring NuGet packages...");
	NuGetRestore(pathSln);
});

Task("Build")
 .IsDependentOn("Init")
 .Does(() =>
{
	Information("building solution: " + pathSln);
	MSBuild(pathSln, new MSBuildSettings(){
			ArgumentCustomization = args=>args.Append("/consoleloggerparameters:ErrorsOnly") 
		}
		.SetConfiguration(configuration)
		.SetVerbosity(Verbosity.Minimal)
	);
});

Task("Publish_Console")
 .IsDependentOn("Build")
 .Does(() =>
{
	var pathConsoleDest = Argument("pathConsole", pathBin + "/console");
	var pathConsoleSource = "./src/KeyCdr.UI.ConsoleApp/bin/" + configuration + "/";

	EnsureDirectoryExists(pathConsoleDest);
	CleanDirectory(pathConsoleDest);

	Information("publishing console UI from {0} to {1}", pathConsoleSource, pathConsoleDest);
	CopyFiles(pathConsoleSource + "*.dll", pathConsoleDest);
	CopyFiles(pathConsoleSource + "*.xml", pathConsoleDest);
	CopyFiles(pathConsoleSource + "*.exe", pathConsoleDest);
	CopyFiles(pathConsoleSource + "*.exe.config", pathConsoleDest);
});

Task("Publish_WPF")
 .IsDependentOn("Build")
 .Does(() =>
{
	Information("publishing WPF UI");

});

Task("Publish")
 .IsDependentOn("Build")
 .IsDependentOn("Publish_Console")
 .IsDependentOn("Publish_WPF")
 .Does(() =>
{ 
	Information("starting publishing");
});

Task("Tests_KeyCdr")
 .IsDependentOn("Build")
 .Does(() =>
{
	Information("running tests...");
	NUnit3("./src/KeyCdr.Tests/bin/" + configuration + "/*.Tests.dll", new NUnit3Settings {

	});
});

Task("Default")
 .IsDependentOn("Init")
 .IsDependentOn("Build")
 .IsDependentOn("Publish")
 .IsDependentOn("Tests_KeyCdr")
 .Does(() => {
	Information("running default task");
});

RunTarget(target);
