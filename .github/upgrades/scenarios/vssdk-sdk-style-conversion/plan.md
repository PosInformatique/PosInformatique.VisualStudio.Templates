# VSSDK SDK-Style Conversion Plan

## Overview

**Target**: Convert Visual Studio VSIX projects from legacy MSBuild format to SDK-style while preserving packaging and debugging behavior.
**Scope**: 2 projects in the solution (`VisualStudio.Templates` and `VisualStudio.Templates.Files`) plus solution deploy markers.

## Tasks

### 01-unload-projects: Unload VSIX projects in Visual Studio

Unload `src/VisualStudio.Templates/VisualStudio.Templates.csproj` and `src/VisualStudio.Templates.Files/VisualStudio.Templates.Files.csproj` before project-file edits so SDK-style changes are applied without IDE lock/caching conflicts.

**Done when**: Both projects are unloaded in the IDE and ready for conversion edits.

---

### 02-convert-main-vsix-project: Convert VisualStudio.Templates.csproj to SDK-style with VSSDK overlay

Convert the main VSIX project to SDK-style and apply VSSDK-specific settings: VSIX properties, capabilities, package references, removal of legacy imports/properties, and preservation of manifest/content/project-reference metadata.

**Done when**: `src/VisualStudio.Templates/VisualStudio.Templates.csproj` is SDK-style, contains required VSSDK properties/packages, and no longer contains legacy imports/startup properties.

---

### 03-convert-templates-files-project: Convert VisualStudio.Templates.Files.csproj to SDK-style

Convert the referenced templates payload project to SDK-style while preserving VSTemplate items and XML poke targets used to stamp wizard version values.

**Done when**: `src/VisualStudio.Templates.Files/VisualStudio.Templates.Files.csproj` is SDK-style and preserves template packaging behavior.

---

### 04-update-solution-deploy-markers: Add deploy markers for VSIX debugging

Update the `.sln` file with `Deploy.0` entries for the main VSIX project so F5 deploy/debug behavior is preserved after removing legacy StartAction-based launch configuration.

**Done when**: Solution `ProjectConfigurationPlatforms` contains deploy entries for Debug and Release configurations of the VSIX project.

---

### 05-reload-and-validate: Reload projects and validate build output

Reload converted projects in Visual Studio, run a clean build, and verify SDK-style integrity conditions (no legacy imports/startup properties, VSIX output generation, deploy markers in solution).

**Done when**: Build is successful (or only blocked by pre-existing unrelated issues), conversion checks pass, and validation results are documented.
