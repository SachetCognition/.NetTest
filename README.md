# Equant.SAV2000 Component Library

Server-side UI component library for ASP.NET MVC 5 (.NET Framework 4.8), plus a demo host web application.

## Solution layout

| Project | Description |
| --- | --- |
| `Equant.SAV2000.ComponentLibrary.MVC` (repo root) | The component library (fluent `Html.Sav2000()` API, embedded jQuery plugins). |
| `Equant.SAV2000.ComponentLibrary.Common` | Sibling class library with shared helpers (`JsResource`, `CssResource`), DataTables option types, and resource strings referenced by the MVC library. |
| `Equant.SAV2000.DemoApp` | ASP.NET MVC 5 host web app with a demo page that renders `ButtonComponent` and `CheckBoxComponent` end to end. |

## Build

Requires MSBuild with the .NET Framework 4.8 targeting pack (e.g. Visual Studio 2022 Build Tools with the "Web development build tools" workload) and `nuget.exe`.

```powershell
nuget restore Equant.SAV2000.ComponentLibrary.sln -PackagesDirectory packages
msbuild Equant.SAV2000.ComponentLibrary.sln /p:Configuration=Debug
```

NuGet packages restore into `packages\` at the repo root; all `HintPath`s point there.

### AjaxMin pre-build event

The library's `.csproj` has a `PreBuildEvent` that runs `ajaxmin.bat` to minify the embedded JavaScript for any configuration **other than Debug**. The AjaxMin tool (`ajaxmin.exe`) is not included in this repo, so either build in Debug configuration (the event is skipped) or install AjaxMin and put it on `PATH` before building Release.

Note: `packages.config` pins EntityFramework 4.1 with `targetFramework="net40"`; it restores and builds fine against net48 (the assembly targets .NET 4.0 and is only referenced, not consumed by any code path exercised here).

## Run the demo app

Any ASP.NET-capable host works; with IIS Express:

```powershell
"C:\Program Files\IIS Express\iisexpress.exe" /path:<repo>\Equant.SAV2000.DemoApp /port:8085
```

Browse to `http://localhost:8085/`. The demo page (`Views/Home/Index.cshtml`) renders a Button and a CheckBox component through `Html.Sav2000()`, and serves the library's embedded JavaScript resources (`Common.js`, `Button.js`, `CheckBox.js`) via `HomeController.EmbeddedScript`.
