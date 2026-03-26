# Equant.SAV2000.ComponentLibrary

A 42-component server-side UI component library for ASP.NET MVC applications, providing form controls, data display, navigation, date/time pickers, validation, and infrastructure components via a fluent builder API (`Html.Sav2000().Component()`).

## Projects

- **Equant.SAV2000.ComponentLibrary.MVC** — Main component library targeting .NET Framework 4.8 / ASP.NET MVC 5
- **Equant.SAV2000.ComponentLibrary.Common** — Shared types (DataTable options, resources, helpers)

## Building

Requires .NET Framework 4.8 SDK or Mono with MSBuild:

```bash
msbuild Equant.SAV2000.ComponentLibrary.MVC.csproj
```

## URS Documentation

A comprehensive User Requirement Specification for the .NET 8+ migration is available as a self-contained HTML document.

To view the URS:

1. `cd docs/`
2. Run: `./serve.sh` (Linux/Mac) or `serve.bat` (Windows)
3. Open http://localhost:8080 in your browser

Alternative (Node.js):
```bash
cd docs/
npx serve -l 8080 .
```

The document includes:
- 65+ User Stories organized by 8 epics
- Functional and Non-Functional Requirements
- Risk Assessment and Migration Mapping
- Complete Component Inventory (42 components)
- Reusability Matrix
