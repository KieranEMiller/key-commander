var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");

var pathBin = Argument("pathBin", "./bin");
var pathSln = Argument("pathSln", "./src/KeyCdr.sln");

Setup(context =>
{ });

Task("Clean")
 .Does(() => {
	Information(string.Format("Cleaning directory: {0}", pathBin));
	EnsureDirectoryExists(pathBin);
}

Task("Restore-NuGet-Packages")
 .IsDependentOn("Clean")
 .Does(() =>
{
	Information("restoring NuGet packages...");
	NuGetRestore(pathSln);
});

Task("Build")
 .IsDependentOn("Restore-NuGet-Packages")
 .Does(() =>
{
	Information("building solution: " + pathSln);
	MSBuild(pathSln, settings => settings.SetConfiguration(configuration));
});

Task("Run-Tests")
 .IsDependentOn("Build")
 .Does(() =>
{
	Information("running tests...");
	NUnit3("./src/KeyCdr.Tests/bin/" + configuration + "/*.Tests.dll", new NUnit3Settings {

	});
});

RunTarget(target);
