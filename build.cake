var target = Argument("target", "Test");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Restore")
    .Does(() => {
        DotNetCoreRestore("./");
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