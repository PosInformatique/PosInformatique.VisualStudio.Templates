# 02-convert-main-vsix-project: Convert VisualStudio.Templates.csproj to SDK-style with VSSDK overlay

Convert the main VSIX project to SDK-style and apply VSSDK-specific settings: VSIX properties, capabilities, package references, removal of legacy imports/properties, and preservation of manifest/content/project-reference metadata.

**Done when**: `src/VisualStudio.Templates/VisualStudio.Templates.csproj` is SDK-style, contains required VSSDK properties/packages, and no longer contains legacy imports/startup properties.
