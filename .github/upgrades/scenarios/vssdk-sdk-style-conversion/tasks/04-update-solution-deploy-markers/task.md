# 04-update-solution-deploy-markers: Add deploy markers for VSIX debugging

Update the `.sln` file with `Deploy.0` entries for the main VSIX project so F5 deploy/debug behavior is preserved after removing legacy StartAction-based launch configuration.

**Done when**: Solution `ProjectConfigurationPlatforms` contains deploy entries for Debug and Release configurations of the VSIX project.
