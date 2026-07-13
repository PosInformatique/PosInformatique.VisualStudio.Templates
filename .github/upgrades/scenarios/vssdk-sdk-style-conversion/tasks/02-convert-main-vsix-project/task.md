# 02-convert-main-vsix-project: Convert VisualStudio.Templates.csproj to SDK-style with VSSDK overlay

Convert the main VSIX project to SDK-style and apply VSSDK-specific settings: VSIX properties, capabilities, package references, removal of legacy imports/properties, and preservation of manifest/content/project-reference metadata.

## Scope Inventory

### Projects Affected
- `src/VisualStudio.Templates/VisualStudio.Templates.csproj` — main VSIX extension project
- `src/VisualStudio.Templates.Files/VisualStudio.Templates.Files.csproj` — referenced template payload project metadata must remain in `ProjectReference`

### Distinct Concerns
- SDK-style structural conversion (already performed via conversion tool)
- VSSDK overlay properties/capabilities/package configuration
- Preservation of VSIX manifest and `ProjectReference` metadata required for packaging

### Change Signals
- Conversion tool changed package versions (`Microsoft.VisualStudio.SDK` and `Microsoft.VSSDK.BuildTools`) and dropped VSIX-specific `ProjectReference` metadata.
- Legacy debug launch properties (`StartAction`/`StartProgram`/`StartArguments`) remain and must be removed.
- Build tools package must remain at least `18.5.38461` and must not be downgraded from existing higher version.

### Skill Matches
- `converting-to-sdk-style`: base conversion completed; now applying VSSDK overlay.
- `managing-package-references`: restore/align package references and preserve required assets metadata.
- `modifying-project-properties`: apply/remove VSIX properties safely in SDK-style format.
- `building-projects`: validate with `msbuild` for .NET Framework/VSSDK project.

## Research Findings

### Files to Modify
- `src/VisualStudio.Templates/VisualStudio.Templates.csproj` — apply VSSDK SDK-style overlay and restore removed metadata.

### Package Decisions
- Keep `Microsoft.VSSDK.BuildTools` at `18.5.40034` (existing version above minimum, no downgrade).
- Keep `Microsoft.VisualStudio.SDK` at `17.14.40265`.
- Restore `editorconfig` package reference removed by base conversion to preserve existing project behavior.

### Key Preservation Requirements
- Keep target framework equivalent (`net472` from original `v4.7.2`) and do not upgrade framework in this task.
- Preserve VSIX project reference metadata to `VisualStudio.Templates.Files` (`VSIXSubPath`, `ReferenceOutputAssembly=false`, `IncludeOutputGroupsInVSIX`).
- Keep assembly signing and generated version file target behavior.

**Done when**: `src/VisualStudio.Templates/VisualStudio.Templates.csproj` is SDK-style, contains required VSSDK properties/packages, and no longer contains legacy imports/startup properties.
