var target = Argument("target", "Default");
var pathBin = Argument("path-bin", "./bin");

Setup(context =>
{
	Information(string.Format("Cleaning directory: {0}", pathBin));
	EnsureDirectoryExists(pathBin);
});

Task("Default").Does(() =>
{
  Information("Hello World!");
});

RunTarget(target);
