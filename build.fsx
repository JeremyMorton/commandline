#r "packages/FAKE/tools/FakeLib.dll"
open Fake
open Fake.Core
open Fake.Core.Globbing.Operators
open Fake.Core.TargetOperators

let buildDir = "./build/"
let testDir = "./build/test/"
let packagingDir = "./nuget/"

let authors = ["Giacomo Stelluti Scala"]
let projectDescription = "The Command Line Parser Library offers to CLR applications a clean and concise API for manipulating command line arguments and related tasks."
let projectSummary = "Command Line Parser Library"
let buildVersion = "2.0.0.0"

Target.Create "Clean" (fun _ ->
    CleanDirs [buildDir; testDir]
)

Target.Create "Default" (fun _ ->
    Trace.trace "Command Line Parser Library 2.0 pre-release"
)

Target.Create "BuildLib" (fun _ ->
    Fake.DotNet.MsBuild.MSBuildRelease buildDir "Build" ["./src/CommandLine/CommandLine.csproj"]
    |> Trace.Log "LibBuild-Output: "
)

Target.Create "BuildTest" (fun _ ->
    Fake.DotNet.MsBuild.MSBuildRelease testDir "Build" ["./tests/CommandLine.Tests/CommandLine.Tests.csproj"]
    |> Trace.Log "TestBuild-Output: "
)

Target.Create "Test" (fun _ ->
    //trace "Running Tests..."
    !! (testDir @@ "\CommandLine.Tests.dll")
      |> Fake.DotNet.Testing.XUnit2.xUnit2 (fun p -> {p with HtmlOutputPath = Some(testDir @@ "xunit.html"); ToolPath = "./packages/xunit.runner.console/tools/netcoreapp2.0/xunit.console.dll" })
)

// Dependencies
"Clean"
    ==> "BuildLib"
    ==> "BuildTest"
    ==> "Test"
    ==> "Default"

Target.RunOrDefault "Default"
