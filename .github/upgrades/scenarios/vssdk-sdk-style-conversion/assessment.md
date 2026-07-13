# Assessment: VSSDK SDK-Style Conversion

## Target Project
| Property | Value |
|----------|-------|
| Project | VisualStudio.Templates |
| Path | src/VisualStudio.Templates/VisualStudio.Templates.csproj |
| Current TFM | .NET Framework 4.7.2 |
| Solution format | .sln |
| packages.config | No |

## VSIX Components Found
- [x] VSIX manifest (`source.extension.vsixmanifest`)
- [ ] VSCT command table
- [ ] Tool windows
- [ ] MEF exports
- [ ] Custom editors
- [ ] Language services

## Current Package References
- `editorconfig` 0.16.2
- `Microsoft.VisualStudio.SDK` 17.14.40265
- `Microsoft.VSSDK.BuildTools` 18.5.40034

## Baseline
- Project builds: No
- Solution builds: No

Baseline build errors (pre-existing):
- `CS0234` in `CompanySelectionWizard.cs`: namespace `Microsoft.VisualStudio.TemplateWizard` not found
- `CS0246` in `CompanySelectionWizard.cs`: `IWizard` not found
- `CS0246` in `CompanySelectionWizard.cs`: `WizardRunKind` not found

## Key Findings
- Project is in legacy non-SDK format (`ToolsVersion`, legacy imports, explicit compile includes).
- VSSDK BuildTools version is already above the required minimum (`18.5.40034 >= 18.5.38461`).
- A secondary project (`src/VisualStudio.Templates.Files/VisualStudio.Templates.Files.csproj`) is also legacy and referenced by the VSIX project; conversion should include it for consistent SDK-style solution state.
