# 05-reload-and-validate: Reload projects and validate build output

Reload converted projects in Visual Studio, run a clean build, and verify SDK-style integrity conditions (no legacy imports/startup properties, VSIX output generation, deploy markers in solution).

## Scope Inventory

### Projects Affected
- `src/VisualStudio.Templates/VisualStudio.Templates.csproj`
- `src/VisualStudio.Templates.Files/VisualStudio.Templates.Files.csproj`
- `PosInformatique.VisualStudio.Templates.sln`

### Distinct Concerns
- IDE project reload after SDK-style edits
- Build validation using appropriate tool for .NET Framework/VSSDK (`msbuild`)
- Static integrity checks of converted project and solution markers

## Research Findings

- Both converted projects were previously unloaded for safe editing and must be reloaded before final validation.
- Validation should tolerate known pre-existing compile failures identified at baseline (`CompanySelectionWizard.cs` TemplateWizard symbols), provided no new conversion regressions are introduced.
- Expected conversion checks include:
  - no legacy imports and startup launch properties in converted csproj files
  - deploy markers present in `.sln`
  - SDK-style root format in both csproj files

**Done when**: Build is successful (or only blocked by pre-existing unrelated issues), conversion checks pass, and validation results are documented.
