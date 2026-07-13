# 01-unload-projects: Unload VSIX projects in Visual Studio

Unload `src/VisualStudio.Templates/VisualStudio.Templates.csproj` and `src/VisualStudio.Templates.Files/VisualStudio.Templates.Files.csproj` before project-file edits so SDK-style changes are applied without IDE lock/caching conflicts.

## Scope Inventory

### Projects Affected
- `src/VisualStudio.Templates/VisualStudio.Templates.csproj` — main VSIX project to be converted
- `src/VisualStudio.Templates.Files/VisualStudio.Templates.Files.csproj` — referenced template payload project to be converted

### Distinct Concerns
- IDE project state management (unload before edit, reload after conversion)

### Change Signals
- Both projects are currently legacy non-SDK format and loaded in the IDE.
- Conversion workflow requires projects to be unloaded before csproj edits.

### Skill Matches
- `building-projects`: relevant for subsequent validation tasks, not required for this unload-only task.

## Research Findings

- Confirmed both target projects are present in the loaded solution and ready for unload/reload operations.
- No decomposition needed: single concern, two direct operations, unambiguous completion criteria.

**Done when**: Both projects are unloaded in the IDE and ready for conversion edits.
