## Files Modified
- src/VisualStudio.Templates.Files/VisualStudio.Templates.Files.csproj
- .github/upgrades/scenarios/vssdk-sdk-style-conversion/tasks/03-convert-templates-files-project/task.md

## Build Result
- Command: `msbuild src/VisualStudio.Templates.Files/VisualStudio.Templates.Files.csproj /restore /t:Build`
- Result: Success (0 errors, 0 warnings) after SDK-style adjustments.

## Test Result
- No automated tests executed for this task (project-format and build-target behavior only).

## Changes Summary
- Converted `VisualStudio.Templates.Files.csproj` to SDK-style.
- Preserved VSTemplate packaging items and XML poke targets (`BeforeBuild` / `AfterBuild`).
- Added explicit `Compile Remove` entries for template source artifacts (`Class.cs`, `Exception.cs`, `ExceptionUnitTest.cs`, `Interface.cs`, `RazorComponent.razor.cs`, `XUnitTest.cs`) so SDK globbing does not compile template token files.
- Restored template payload file list as `None` items, including `RazorComponent.razor`.
- Added `SkipCompilerExecution=true` to avoid no-source compiler warnings in this payload-only project.

## Issues Encountered
- Initial build failed because SDK globbing compiled template source files containing `$...$` placeholder tokens.
- Follow-up build produced warning `CS2008` (no source files); resolved with `SkipCompilerExecution=true`.
