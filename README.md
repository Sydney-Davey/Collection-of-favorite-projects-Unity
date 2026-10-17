Known Issues and Notes

As this is an older project, some errors have been more difficult to identify and resolve. The following notes document the issues encountered during development and their current status.

October 1, 2026 — Unity Version-Related Errors

Status: Resolved

Errors related to the Unity Editor version were identified and fixed.

October 6, 2026 — Missing File Errors

Status: Resolved

Console errors related to missing files were identified and fixed.

October 16, 2026 — URP Shader Warning

Status: Investigation and resolution in progress

When opening the project in the Unity Editor, the following warning may appear repeatedly in the Console:

Host type is not matching any asset type at Path Packages/com.unity.render-pipelines.universal/Shaders/AutodeskInteractive/AutodeskInteractiveTransparent.shadergraph

This issue has only been observed after obtaining the project from GitHub. It appears to be related to Unity's asset processing of a Shader Graph file within the Universal Render Pipeline (URP). The exact cause has not yet been confirmed.

The project was built and tested successfully despite this warning. Further investigation is underway to identify and resolve the issue.
