# 03-convert-templates-files-project: Convert VisualStudio.Templates.Files.csproj to SDK-style

Convert the referenced templates payload project to SDK-style while preserving VSTemplate items and XML poke targets used to stamp wizard version values.

## Scope Inventory

### Projects Affected
- `src/VisualStudio.Templates.Files/VisualStudio.Templates.Files.csproj` — VS template payload project consumed by the main VSIX project

### Distinct Concerns
- SDK-style structural conversion for .NET Framework class library project
- Preservation of `VSTemplate` item declarations and XML poke build targets (`BeforeBuild` and `AfterBuild`)

### Change Signals
- Project is currently legacy format and still needs conversion.
- Project contains custom VSSDK-related properties and XML manipulation targets that must be preserved.

### Skill Matches
- `converting-to-sdk-style`: run conversion tool and keep format-only scope.
- `building-projects`: validate converted project with `msbuild`.

## Research Findings

### Files to Modify
- `src/VisualStudio.Templates.Files/VisualStudio.Templates.Files.csproj`

### Preservation Requirements
- Keep target framework equivalent (`net472`).
- Keep all `None` template source files and all `VSTemplate` item entries.
- Keep `BeforeBuild`/`AfterBuild` targets that `XmlPoke` wizard version values.
- Ensure no legacy `<Import>` remains after conversion.
- Exclude template source files (`*.cs`/`.razor.cs` containing template tokens) from compilation in SDK-style to prevent compiler errors.

### Conversion Adjustment
- SDK default globbing re-included template `.cs` files as compile items, causing syntax errors from template tokens (`$...$`).
- Resolution: explicitly remove these files from `Compile` and keep them as `None`; enable `SkipCompilerExecution` for this payload-only project to avoid no-source compilation warnings.

No decomposition needed: single project with one coherent conversion concern.

**Done when**: `src/VisualStudio.Templates.Files/VisualStudio.Templates.Files.csproj` is SDK-style and preserves template packaging behavior.
