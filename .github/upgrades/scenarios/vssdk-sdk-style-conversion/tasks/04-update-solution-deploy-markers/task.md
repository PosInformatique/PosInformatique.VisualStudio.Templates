# 04-update-solution-deploy-markers: Add deploy markers for VSIX debugging

Update the `.sln` file with `Deploy.0` entries for the main VSIX project so F5 deploy/debug behavior is preserved after removing legacy StartAction-based launch configuration.

## Scope Inventory

### Projects Affected
- `PosInformatique.VisualStudio.Templates.sln` — solution-level deploy metadata for VSIX project

### Distinct Concerns
- Add classic `.sln` `Deploy.0` entries for VSIX project configurations

### Change Signals
- Main VSIX project GUID: `{E643690C-E124-410C-9F5B-9E7E2353C1F2}`
- `ActiveCfg` and `Build.0` entries exist for Debug/Release and platform variants; `Deploy.0` entries are currently missing.

## Research Findings

- This solution is classic `.sln` (not `.slnx`), so deploy markers must be added in `GlobalSection(ProjectConfigurationPlatforms)`.
- Required minimum per scenario: add `Deploy.0` for `Debug|Any CPU` and `Release|Any CPU` for the VSIX project GUID.

No decomposition needed: one file and one concern.

**Done when**: Solution `ProjectConfigurationPlatforms` contains deploy entries for Debug and Release configurations of the VSIX project.
