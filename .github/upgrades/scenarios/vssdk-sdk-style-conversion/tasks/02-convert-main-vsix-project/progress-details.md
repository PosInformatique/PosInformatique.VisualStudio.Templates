## Files Modified
- src/VisualStudio.Templates/VisualStudio.Templates.csproj
- .github/upgrades/scenarios/vssdk-sdk-style-conversion/tasks/02-convert-main-vsix-project/task.md

## Build Result
- Command: `msbuild src/VisualStudio.Templates/VisualStudio.Templates.csproj /t:Clean` then `/restore /t:Build`
- Result: Failed with pre-existing compile errors unrelated to SDK-style overlay changes:
  - `CS0234` (`Microsoft.VisualStudio.TemplateWizard` namespace not found)
  - `CS0246` (`IWizard` not found)
  - `CS0246` (`WizardRunKind` not found)
- These same errors were present in baseline assessment before conversion.

## Test Result
- No automated tests executed for this task (project format/properties task only).

## Changes Summary
- Kept project in SDK-style format (`<Project Sdk="Microsoft.NET.Sdk">`).
- Added required VSSDK SDK-style properties:
  - `VSSDKBuildToolsAutoSetup=true`
  - `VsixDeployOnDebug=true`
  - `UseCodebase=true`
- Preserved existing `GeneratePkgDefFile=false` value (existing project behavior preserved).
- Added VSIX capability item: `<ProjectCapability Include="CreateVsixContainer" />`.
- Removed legacy debug launch properties from project file (`StartAction`, `StartProgram`, `StartArguments`).
- Restored and aligned package references:
  - `editorconfig` `0.16.2`
  - `Microsoft.VisualStudio.SDK` `17.14.40265`
  - `Microsoft.VSSDK.BuildTools` `18.5.40034` (kept above required minimum, no downgrade)
- Restored VSIX project reference metadata for template payload inclusion:
  - `VSIXSubPath`
  - `ReferenceOutputAssembly=false`
  - `IncludeOutputGroupsInVSIX`
- Kept existing signing and version-generation target behavior.

## Issues Encountered
- The conversion tool had downgraded VSSDK package versions and removed project-reference metadata; both were corrected as part of this task.
