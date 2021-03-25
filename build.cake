var target = Argument("target", "Test");
var solutions = GetFiles("./**/*.sln");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Restore")
    .Does(() => {
      foreach(var sln in solutions) {
	  Information("Restoring {solution}", sln);
	  NuGetRestore(solutions);
	}
    });

Task("Clean")
    .IsDependentOn("Restore")
    .WithCriteria(c => HasArgument("rebuild"))
    .Does(() =>
{
    CleanDirectory($"./src/host/bin/{configuration}");
});

Task("Build")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DotNetCoreBuild("./", new DotNetCoreBuildSettings
    {
        Configuration = configuration,
        NoRestore = true
    });
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    DotNetCoreTest("./", new DotNetCoreTestSettings
    {
        Configuration = configuration,
        NoBuild = true,
    });
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
