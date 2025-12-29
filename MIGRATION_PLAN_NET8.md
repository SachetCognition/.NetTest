# Equant SAV2000 MVC Component Library - .NET 8 Migration Plan

## Executive Summary

This document provides a detailed, phased migration plan for migrating the Equant SAV2000 MVC Component Library from .NET Framework 4.8 to .NET 8. The library consists of 77 C# files, 57 component files, 8 validators, and 47 embedded JavaScript resources. The migration involves replacing deprecated System.Web dependencies with ASP.NET Core equivalents and modernizing the asset bundling strategy.

---

## Table of Contents

1. [Current State Analysis](#1-current-state-analysis)
2. [Module Complexity Scoring](#2-module-complexity-scoring)
3. [Migration Phases Overview](#3-migration-phases-overview)
4. [Phase 1: Foundation and Infrastructure](#4-phase-1-foundation-and-infrastructure)
5. [Phase 2: Core Base Classes](#5-phase-2-core-base-classes)
6. [Phase 3: Simple Components](#6-phase-3-simple-components)
7. [Phase 4: Composite Components](#7-phase-4-composite-components)
8. [Phase 5: Data Components and Model Binders](#8-phase-5-data-components-and-model-binders)
9. [Phase 6: Validators and Client-Side Validation](#9-phase-6-validators-and-client-side-validation)
10. [Phase 7: Resource Management and Asset Bundling](#10-phase-7-resource-management-and-asset-bundling)
11. [Phase 8: Integration and Entry Point](#11-phase-8-integration-and-entry-point)
12. [Test Case Strategy](#12-test-case-strategy)
13. [Risk Assessment and Mitigation](#13-risk-assessment-and-mitigation)
14. [Appendix: API Mapping Reference](#14-appendix-api-mapping-reference)

---

## 1. Current State Analysis

### 1.1 Project Structure

```
TechMTest/
├── Components/           (57 files - UI components)
│   ├── ActionButton/
│   ├── Button/
│   ├── CheckBox/
│   ├── CheckBoxList/
│   ├── ClickToVoice/
│   ├── CompositeDate/
│   ├── CustomLabel/
│   ├── DataTable/
│   ├── DateTimeControl/
│   ├── TreeGrid/
│   ├── VerticalMenu/
│   └── ... (additional component directories)
├── Extensions/           (3 files - HtmlHelper extensions)
├── Helpers/              (3 files - utility helpers)
├── Infrastructure/       (4 files - core infrastructure)
├── Models/               (1 file - data models)
├── Resources/
│   └── Javascripts/      (47 embedded JS files)
├── Validators/           (8 files - validation attributes)
└── Properties/           (assembly info)
```

### 1.2 Current Dependencies

| Package | Version | .NET 8 Equivalent |
|---------|---------|-------------------|
| ClientDependency | 1.8.2.1 | WebOptimizer / Custom solution |
| ClientDependency-Mvc5 | 1.8.0.0 | WebOptimizer / Custom solution |
| EntityFramework | 4.1.10311.0 | EF Core 8.x |
| Microsoft.AspNet.Mvc | 5.2.3 | Microsoft.AspNetCore.Mvc |
| Microsoft.AspNet.Razor | 3.2.3 | Microsoft.AspNetCore.Mvc.Razor |
| Microsoft.AspNet.WebPages | 3.2.3 | Built into ASP.NET Core |
| Newtonsoft.Json | 6.0.4 | System.Text.Json or Newtonsoft.Json 13.x |

### 1.3 System.Web Dependencies Summary

Based on codebase analysis, the following System.Web namespaces are used across 77 files:

| Namespace | Files Affected | Migration Impact |
|-----------|----------------|------------------|
| System.Web.Mvc | 45+ files | CRITICAL - Replace with Microsoft.AspNetCore.Mvc |
| System.Web.UI | 35+ files | CRITICAL - Replace HtmlTextWriter with IHtmlContent |
| System.Web.Mvc.Html | 1 file | HIGH - Replace with ASP.NET Core Tag Helpers |
| System.Web.SessionState | 1 file | MEDIUM - Replace with ASP.NET Core session |
| System.Web | 2 files | MEDIUM - Replace HttpContext usage |

---

## 2. Module Complexity Scoring

Each module is scored on a scale of 1-10 based on the following criteria:
- **File Count**: Number of files to modify
- **System.Web Depth**: How deeply integrated with System.Web APIs
- **External Dependencies**: Reliance on third-party packages
- **Breaking Change Impact**: Severity of required changes
- **Interdependencies**: Coupling with other modules

### 2.1 Module Scores

| Module | Files | System.Web Depth | External Deps | Breaking Impact | Interdeps | **Total Score** |
|--------|-------|------------------|---------------|-----------------|-----------|-----------------|
| **Infrastructure** | 4 | High (PageProvider uses System.Web.UI.Page) | Low | CRITICAL | High | **9/10** |
| **Extensions** | 3 | Very High (HtmlHelper, ClientDependency) | High | CRITICAL | Very High | **10/10** |
| **Components/Api** | 7 | Very High (ComponentBase, HtmlBuilderBase) | Medium | CRITICAL | Very High | **10/10** |
| **Components/DataTable** | 12 | High (IModelBinder, complex serialization) | High | HIGH | Medium | **8/10** |
| **Components/CompositeDate** | 5 | High (nested components) | Medium | HIGH | High | **7/10** |
| **Components/DateTimeControl** | 6 | High (custom model binders) | Medium | HIGH | High | **7/10** |
| **Components/WeekYear** | 6 | High (custom model binders) | Medium | HIGH | Medium | **6/10** |
| **Components/TreeGrid** | 7 | Medium | Low | MEDIUM | Low | **5/10** |
| **Validators** | 8 | High (IClientValidatable) | Low | HIGH | Medium | **7/10** |
| **Simple Components** | 25+ | Medium (standard patterns) | Low | MEDIUM | Low | **4/10** |
| **Helpers** | 3 | Low | Low | LOW | Medium | **3/10** |
| **Models** | 1 | None | None | LOW | Low | **1/10** |

### 2.2 Complexity Distribution

```
CRITICAL (9-10): Infrastructure, Extensions, Components/Api
HIGH (7-8):      DataTable, Validators, CompositeDate, DateTimeControl
MEDIUM (5-6):    WeekYear, TreeGrid
LOW (1-4):       Simple Components, Helpers, Models
```

---

## 3. Migration Phases Overview

The migration is organized into 8 sequential phases, designed to minimize risk and allow incremental testing:

```
Phase 1: Foundation (Week 1-2)
    └── Project file conversion, package updates, infrastructure
    
Phase 2: Core Base Classes (Week 2-3)
    └── ComponentBase, HtmlBuilderBase, ComponentBuilderBase
    
Phase 3: Simple Components (Week 3-4)
    └── Button, CheckBox, TextBox, Label, etc.
    
Phase 4: Composite Components (Week 4-5)
    └── CompositeDate, CheckBoxList, DualList, etc.
    
Phase 5: Data Components (Week 5-6)
    └── DataTable, TreeGrid, Model Binders
    
Phase 6: Validators (Week 6-7)
    └── All 8 validation attributes with client-side support
    
Phase 7: Resource Management (Week 7-8)
    └── Replace ClientDependency, asset bundling
    
Phase 8: Integration (Week 8-9)
    └── HtmlHelperExtension, final integration, testing
```

---

## 4. Phase 1: Foundation and Infrastructure

### 4.1 Objectives
- Convert project file from legacy .csproj to SDK-style
- Update all NuGet package references
- Replace PageProvider infrastructure
- Establish compatibility shims where needed

### 4.2 Prerequisites
- .NET 8 SDK installed
- Visual Studio 2022 or later / VS Code with C# extension
- Access to NuGet package sources

### 4.3 Step-by-Step Instructions

#### Step 1.1: Create New SDK-Style Project File

**File:** `Equant.SAV2000.ComponentLibrary.MVC.csproj`

Replace the entire content with:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>disable</ImplicitUsings>
    <Nullable>disable</Nullable>
    <RootNamespace>Equant.SAV2000.ComponentLibrary.MVC</RootNamespace>
    <AssemblyName>Equant.SAV2000.ComponentLibrary.MVC</AssemblyName>
    <GenerateEmbeddedFilesManifest>true</GenerateEmbeddedFilesManifest>
  </PropertyGroup>

  <ItemGroup>
    <FrameworkReference Include="Microsoft.AspNetCore.App" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.FileProviders.Embedded" Version="8.0.0" />
    <PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
  </ItemGroup>

  <ItemGroup>
    <EmbeddedResource Include="Resources\Javascripts\**\*.js" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Equant.SAV2000.ComponentLibrary.Common\Equant.SAV2000.ComponentLibrary.Common.csproj" />
  </ItemGroup>

</Project>
```

#### Step 1.2: Delete Legacy Files

Remove the following files that are no longer needed:
- `packages.config`
- `app.config`
- `packages/` directory

#### Step 1.3: Migrate PageProvider

**File:** `Infrastructure/PageProvider.cs`

**Current Code:**
```csharp
using System.Web.UI;

public sealed class PageProvider
{
    private static readonly Lazy<Page> Lazy = new Lazy<Page>(() => new Page());
    
    public static Page Instance
    {
        get { return Lazy.Value; }
    }
}
```

**Migrated Code:**
```csharp
using System;
using System.Reflection;
using Microsoft.Extensions.FileProviders;

namespace Equant.SAV2000.ComponentLibrary.MVC.Infrastructure
{
    /// <summary>
    /// Provides access to embedded resources in .NET 8.
    /// Replaces System.Web.UI.Page functionality for resource retrieval.
    /// </summary>
    public sealed class ResourceProvider
    {
        private static readonly Lazy<ResourceProvider> LazyInstance = 
            new Lazy<ResourceProvider>(() => new ResourceProvider());

        private readonly EmbeddedFileProvider _fileProvider;

        private ResourceProvider()
        {
            _fileProvider = new EmbeddedFileProvider(
                Assembly.GetExecutingAssembly(),
                "Equant.SAV2000.ComponentLibrary.MVC.Resources");
        }

        public static ResourceProvider Instance => LazyInstance.Value;

        public EmbeddedFileProvider FileProvider => _fileProvider;

        /// <summary>
        /// Gets the URL for an embedded resource.
        /// In ASP.NET Core, this returns a virtual path that must be
        /// handled by middleware or served via StaticFiles.
        /// </summary>
        public string GetWebResourceUrl(Type type, string resourceName)
        {
            // Return a virtual path that will be handled by the resource middleware
            return $"/_content/Equant.SAV2000.ComponentLibrary.MVC/{resourceName}";
        }
    }
}
```

#### Step 1.4: Update DateTimeConstants

**File:** `Infrastructure/DateTimeConstants.cs`

This file contains only constants and requires no changes. Verify it compiles without System.Web references.

#### Step 1.5: Update ConditionalAttributes

**File:** `Infrastructure/ConditionalAttributes.cs`

Review and update any System.Web.Mvc references to Microsoft.AspNetCore.Mvc.

### 4.4 Success Criteria for Phase 1
- [ ] Project file converts to SDK-style format
- [ ] All NuGet packages restore successfully
- [ ] PageProvider/ResourceProvider compiles without errors
- [ ] No System.Web.UI references remain in Infrastructure folder

### 4.5 Risk Mitigation
- **Risk:** Referenced Common project may also need migration
- **Mitigation:** Create interface abstractions to decouple dependencies temporarily

---

## 5. Phase 2: Core Base Classes

### 5.1 Objectives
- Migrate ComponentBase to use IHtmlContent instead of HtmlTextWriter
- Migrate HtmlBuilderBase with new rendering approach
- Update ComponentBuilderBase for ASP.NET Core compatibility
- Migrate ComponentFactory

### 5.2 Critical API Changes

| Old API | New API | Notes |
|---------|---------|-------|
| `HtmlTextWriter` | `IHtmlContent` / `TagBuilder` | Complete paradigm shift |
| `HtmlHelper` | `IHtmlHelper` | Interface-based |
| `TagBuilder.ToString(TagRenderMode)` | `TagBuilder.WriteTo()` | Different output method |
| `MvcHtmlString` | `HtmlString` / `IHtmlContent` | Return type change |

### 5.3 Step-by-Step Instructions

#### Step 2.1: Create IHtmlContent Adapter

Create a new file to bridge the old HtmlTextWriter pattern:

**New File:** `Components/Api/HtmlContentWriter.cs`

```csharp
using System;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    /// <summary>
    /// Adapter class that provides HtmlTextWriter-like functionality
    /// while producing IHtmlContent for ASP.NET Core.
    /// </summary>
    public class HtmlContentWriter : IDisposable
    {
        private readonly StringBuilder _builder;
        private readonly StringWriter _writer;

        public HtmlContentWriter()
        {
            _builder = new StringBuilder();
            _writer = new StringWriter(_builder);
        }

        public void Write(string value)
        {
            _writer.Write(value);
        }

        public void WriteLine(string value)
        {
            _writer.WriteLine(value);
        }

        public void WriteLine(string format, params object[] args)
        {
            _writer.WriteLine(format, args);
        }

        public IHtmlContent ToHtmlContent()
        {
            return new HtmlString(_builder.ToString());
        }

        public string ToHtmlString()
        {
            return _builder.ToString();
        }

        public void Dispose()
        {
            _writer?.Dispose();
        }
    }
}
```

#### Step 2.2: Migrate ComponentBase

**File:** `Components/Api/ComponentBase.cs`

**Key Changes:**
1. Replace `HtmlHelper` with `IHtmlHelper`
2. Replace `WriteHtml(HtmlTextWriter)` with `RenderHtml()` returning `IHtmlContent`
3. Replace `WriteInitScript(HtmlTextWriter)` with `RenderInitScript()` returning `IHtmlContent`

**Migration Pattern:**

```csharp
// OLD
using System.Web.Mvc;
using System.Web.UI;

public abstract class ComponentBase
{
    public HtmlHelper HtmlHelper { get; }
    public abstract void WriteHtml(HtmlTextWriter writer);
    public abstract void WriteInitScript(HtmlTextWriter writer);
}

// NEW
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    public abstract class ComponentBase
    {
        protected ComponentBase(IHtmlHelper htmlHelper)
        {
            HtmlHelper = htmlHelper ?? throw new ArgumentNullException(nameof(htmlHelper));
            HtmlAttributes = new Dictionary<string, object>();
        }

        public IHtmlHelper HtmlHelper { get; }
        
        public ModelMetadata ModelMetadata { get; protected set; }
        
        public Dictionary<string, object> HtmlAttributes { get; }
        
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public bool IsVisible { get; set; } = true;

        public abstract ReadOnlyCollection<JsResource> JsResources { get; }
        
        public virtual ReadOnlyCollection<CssResource> CssResources => 
            new ReadOnlyCollection<CssResource>(new List<CssResource>());

        /// <summary>
        /// Renders the HTML content for this component.
        /// </summary>
        public abstract IHtmlContent RenderHtml();

        /// <summary>
        /// Renders the initialization script for this component.
        /// </summary>
        public abstract IHtmlContent RenderInitScript();

        // Legacy compatibility methods - mark as obsolete
        [Obsolete("Use RenderHtml() instead")]
        public void WriteHtml(HtmlContentWriter writer)
        {
            var content = RenderHtml();
            using (var sw = new StringWriter())
            {
                content.WriteTo(sw, HtmlEncoder.Default);
                writer.Write(sw.ToString());
            }
        }

        [Obsolete("Use RenderInitScript() instead")]
        public void WriteInitScript(HtmlContentWriter writer)
        {
            var content = RenderInitScript();
            using (var sw = new StringWriter())
            {
                content.WriteTo(sw, HtmlEncoder.Default);
                writer.Write(sw.ToString());
            }
        }
    }
}
```

#### Step 2.3: Migrate HtmlBuilderBase

**File:** `Components/Api/HtmlBuilderBase.cs`

```csharp
using Microsoft.AspNetCore.Html;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    /// <summary>
    /// Base class for HTML builders that generate component markup.
    /// </summary>
    /// <typeparam name="TComponent">The component type this builder renders.</typeparam>
    public abstract class HtmlBuilderBase<TComponent> where TComponent : ComponentBase
    {
        protected TComponent Component { get; set; }

        /// <summary>
        /// Builds and returns the HTML content for the component.
        /// </summary>
        public abstract IHtmlContent Build();

        // Legacy compatibility method
        [Obsolete("Use Build() instead")]
        public void Build(HtmlContentWriter writer)
        {
            var content = Build();
            using (var sw = new StringWriter())
            {
                content.WriteTo(sw, HtmlEncoder.Default);
                writer.Write(sw.ToString());
            }
        }
    }
}
```

#### Step 2.4: Migrate ComponentBuilderBase

**File:** `Components/Api/ComponentBuilderBase.cs`

```csharp
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    /// <summary>
    /// Base class for component builders providing fluent configuration API.
    /// </summary>
    public abstract class ComponentBuilderBase<TComponent, TBuilder> : IHtmlContent
        where TComponent : ComponentBase
        where TBuilder : ComponentBuilderBase<TComponent, TBuilder>
    {
        protected ComponentBuilderBase(TComponent component, ModelMetadata modelMetadata)
        {
            Component = component ?? throw new ArgumentNullException(nameof(component));
            ModelMetadata = modelMetadata;
        }

        public TComponent Component { get; }
        
        protected ModelMetadata ModelMetadata { get; }

        /// <summary>
        /// Sets the HTML id attribute.
        /// </summary>
        public TBuilder Id(string id)
        {
            Component.Id = id;
            return (TBuilder)this;
        }

        /// <summary>
        /// Sets the HTML name attribute.
        /// </summary>
        public TBuilder Name(string name)
        {
            Component.Name = name;
            return (TBuilder)this;
        }

        /// <summary>
        /// Adds an HTML attribute.
        /// </summary>
        public TBuilder HtmlAttribute(string key, object value)
        {
            Component.HtmlAttributes[key] = value;
            return (TBuilder)this;
        }

        /// <summary>
        /// Sets the visibility of the component.
        /// </summary>
        public TBuilder Visible(bool isVisible)
        {
            Component.IsVisible = isVisible;
            return (TBuilder)this;
        }

        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            var html = Component.RenderHtml();
            html.WriteTo(writer, encoder);
        }

        public IHtmlContent Render()
        {
            return Component.RenderHtml();
        }
    }
}
```

#### Step 2.5: Migrate ComponentFactory

**File:** `Components/Api/ComponentFactory.cs`

Update to use `IHtmlHelper<TModel>` instead of `HtmlHelper<TModel>`.

### 5.4 Success Criteria for Phase 2
- [ ] All base classes compile without System.Web references
- [ ] IHtmlContent is used throughout instead of HtmlTextWriter
- [ ] ComponentFactory creates components with IHtmlHelper
- [ ] Unit tests pass for base class functionality

---

## 6. Phase 3: Simple Components

### 6.1 Objectives
- Migrate all simple (non-composite) components
- Apply consistent migration pattern across all components
- Maintain backward compatibility where possible

### 6.2 Components in Scope

| Component | Files | Priority | Dependencies |
|-----------|-------|----------|--------------|
| Button | 3 | High | None |
| ActionButton | 3 | High | DialogBox |
| CheckBox | 3 | High | None |
| TextBox | 3 | High | None |
| TextArea | 3 | High | None |
| Label | 3 | Medium | None |
| CustomLabel | 4 | Medium | None |
| SpanLabel | 3 | Medium | None |
| Image | 4 | Medium | None |
| HyperLink | 3 | Medium | None |
| RadioButton | 3 | Medium | None |
| DropDownList | 4 | Medium | None |
| ListBox | 3 | Medium | None |
| ErrorComponent | 4 | Low | None |
| ProgressBar | 3 | Low | None |
| PopOver | 3 | Low | None |
| PopupImage | 3 | Low | None |
| ImageToolTip | 3 | Low | None |

### 6.3 Migration Template for Simple Components

Use this template for each simple component:

#### Step 3.1: Migrate Component Class

**Example: ButtonComponent.cs**

```csharp
// OLD
using System.Web.Mvc;
using System.Web.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ButtonComponent : ComponentBase
{
    public ButtonComponent(HtmlHelper htmlHelper) : base(htmlHelper) { }
    
    public override void WriteHtml(HtmlTextWriter writer)
    {
        new ButtonHtmlBuilder(this).Build(writer);
    }

    public override void WriteInitScript(HtmlTextWriter writer)
    {
        var options = JsonConvert.SerializeObject(new { onClick = new JRaw(this.OnClick) });
        writer.WriteLine("$('#{0}').savbutton({1});", this.Id, options);
    }
}

// NEW
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Button
{
    public class ButtonComponent : ComponentBase
    {
        public ButtonComponent(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
            OnClick = "null";
            Title = string.Empty;
            Value = string.Empty;
        }

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get
            {
                return new ReadOnlyCollection<JsResource>(
                    new List<JsResource>
                    {
                        new JsResource(
                            "JsButton",
                            "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.Button.js",
                            200,
                            typeof(ButtonComponent))
                    });
            }
        }

        public string CssClass { get; set; }
        public string CssClassReadOnly { get; set; }
        public string OnClick { get; set; }
        public string DialogDivId { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }

        public bool IsDisabled
        {
            get => HtmlAttributes.ContainsKey("disabled");
            set
            {
                if (value)
                    HtmlAttributes["disabled"] = "disabled";
                else
                    HtmlAttributes.Remove("disabled");
            }
        }

        public override IHtmlContent RenderHtml()
        {
            return new ButtonHtmlBuilder(this).Build();
        }

        public override IHtmlContent RenderInitScript()
        {
            if (string.IsNullOrEmpty(Id)) return HtmlString.Empty;
            
            var options = JsonConvert.SerializeObject(new { onClick = new JRaw(OnClick) });
            return new HtmlString($"$('#{Id}').savbutton({options});");
        }
    }
}
```

#### Step 3.2: Migrate HtmlBuilder Class

**Example: ButtonHtmlBuilder.cs**

```csharp
// OLD
using System.Web.Mvc;
using System.Web.UI;

public class ButtonHtmlBuilder : HtmlBuilderBase<ButtonComponent>
{
    public override void Build(HtmlTextWriter writer)
    {
        var tagBuilder = new TagBuilder("input");
        tagBuilder.MergeAttribute("id", Component.Id);
        tagBuilder.MergeAttribute("type", "submit");
        tagBuilder.MergeAttributes(Component.HtmlAttributes);
        writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
    }
}

// NEW
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Button
{
    public class ButtonHtmlBuilder : HtmlBuilderBase<ButtonComponent>
    {
        public ButtonHtmlBuilder(ButtonComponent component)
        {
            Component = component;
        }

        public override IHtmlContent Build()
        {
            if (!Component.IsVisible)
                return HtmlString.Empty;

            var tagBuilder = new TagBuilder("input");
            tagBuilder.Attributes["id"] = Component.Id;

            if (!string.IsNullOrEmpty(Component.Name))
                tagBuilder.Attributes["name"] = Component.Name;

            tagBuilder.Attributes["type"] = "submit";

            if (!string.IsNullOrWhiteSpace(Component.DialogDivId))
            {
                tagBuilder.Attributes["data-toggle"] = "modal";
                tagBuilder.Attributes["data-target"] = "#" + Component.DialogDivId;
            }

            foreach (var attr in Component.HtmlAttributes)
            {
                tagBuilder.Attributes[attr.Key] = attr.Value?.ToString();
            }

            tagBuilder.AddCssClass(Component.IsDisabled 
                ? Component.CssClassReadOnly 
                : Component.CssClass);

            tagBuilder.TagRenderMode = TagRenderMode.SelfClosing;
            return tagBuilder;
        }
    }
}
```

#### Step 3.3: Migrate Builder Class

**Example: ButtonBuilder.cs**

```csharp
// NEW
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Button
{
    public class ButtonBuilder : ComponentBuilderBase<ButtonComponent, ButtonBuilder>
    {
        public ButtonBuilder(ButtonComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }

        public ButtonBuilder CssClass(string cssClass)
        {
            Component.CssClass = cssClass;
            return this;
        }

        public ButtonBuilder CssClassReadOnly(string cssClass)
        {
            Component.CssClassReadOnly = cssClass;
            return this;
        }

        public ButtonBuilder OnClick(string onClick)
        {
            Component.OnClick = onClick;
            return this;
        }

        public ButtonBuilder Value(string value)
        {
            Component.Value = value;
            return this;
        }

        public ButtonBuilder Disabled(bool isDisabled)
        {
            Component.IsDisabled = isDisabled;
            return this;
        }

        public ButtonBuilder Title(string title)
        {
            Component.Title = title;
            return this;
        }

        public ButtonBuilder DialogDivId(string dialogDivId)
        {
            Component.DialogDivId = dialogDivId;
            return this;
        }
    }
}
```

### 6.4 Migration Order for Simple Components

Execute in this order to minimize dependency issues:

1. **Wave 1 (No dependencies):**
   - Button, CheckBox, TextBox, TextArea, Label

2. **Wave 2 (Basic dependencies):**
   - CustomLabel, SpanLabel, Image, HyperLink, RadioButton

3. **Wave 3 (List components):**
   - DropDownList, ListBox

4. **Wave 4 (Utility components):**
   - ErrorComponent, ProgressBar, PopOver, PopupImage, ImageToolTip

### 6.5 Success Criteria for Phase 3
- [ ] All simple components compile without System.Web references
- [ ] Each component renders correct HTML output
- [ ] JavaScript initialization scripts generate correctly
- [ ] Fluent builder APIs work as expected

---

## 7. Phase 4: Composite Components

### 7.1 Objectives
- Migrate composite components that contain nested components
- Handle complex component hierarchies
- Maintain proper resource aggregation

### 7.2 Components in Scope

| Component | Files | Complexity | Nested Components |
|-----------|-------|------------|-------------------|
| CompositeDate | 5 | High | DateTimeComponent, WeekYearComponent, DropDownList, CustomLabel |
| CompositeDateExt | 5 | High | Similar to CompositeDate |
| CheckBoxList | 4 | Medium | CheckBox, SpanLabel |
| RadioButtonList | 4 | Medium | RadioButton |
| DualList | 3 | Medium | ListBox |
| DateDuration | 4 | Medium | DateTime, Duration |
| Duration | 3 | Medium | DropDownList |
| DialogBox | 4 | Medium | Button |
| HorizontalMenu | 4 | Medium | MenuItem |
| VerticalMenu | 4 | Medium | MenuItem |
| DropDownMenu | 4 | Medium | ChildMenu |
| HorizontalTab | 3 | Low | None |
| BreadCrumbs | 4 | Low | BreadCrumbsItem |
| Communicator | 3 | Low | ClickToVoice |
| ClickToVoice | 3 | Low | ImageToolTip |

### 7.3 Migration Strategy for Composite Components

#### Step 4.1: Migrate Nested Components First

Before migrating a composite component, ensure all its nested components are migrated:

```
CompositeDate requires:
├── DateTimeComponent (Phase 3 or 4)
├── WeekYearComponent (Phase 4)
├── DropDownListComponent (Phase 3)
├── CustomLabelComponent (Phase 3)
├── LabelComponent (Phase 3)
└── ImageToolTipComponent (Phase 3)
```

#### Step 4.2: Update Resource Aggregation

Composite components aggregate JS/CSS resources from nested components:

```csharp
// NEW Pattern for JsResources in composite components
public override ReadOnlyCollection<JsResource> JsResources
{
    get
    {
        var jsRes = new List<JsResource>
        {
            new JsResource(
                "JsCompositeDate",
                "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.CompositeDate.js",
                200,
                typeof(CompositeDateComponent))
        };

        // Aggregate from nested components
        if (FirstDate != null)
            jsRes.AddRange(FirstDate.JsResources);
        
        if (SecondDate != null)
            jsRes.AddRange(SecondDate.JsResources);

        if (DropDownDateTypes != null)
            jsRes.AddRange(DropDownDateTypes.JsResources);

        return new ReadOnlyCollection<JsResource>(jsRes);
    }
}
```

#### Step 4.3: Migrate CompositeDateComponent

**File:** `Components/CompositeDate/CompositeDateComponent.cs`

Key changes:
1. Update constructor to accept `IHtmlHelper`
2. Update `WriteHtml` to `RenderHtml`
3. Update `WriteInitScript` to `RenderInitScript`
4. Update nested component initialization

```csharp
public class CompositeDateComponent : ComponentBase
{
    public CompositeDateComponent(IHtmlHelper htmlHelper)
        : this(htmlHelper, null, null, null, null)
    {
    }

    public CompositeDateComponent(
        IHtmlHelper htmlHelper,
        DateTimeBuilder firstDateBuilder,
        DateTimeBuilder secondDateBuilder,
        WeekYearBuilder firstWeekBuilder,
        WeekYearBuilder secondWeekBuilder)
        : base(htmlHelper)
    {
        // Initialize nested components
        CustomLabel = new CustomLabelComponent(HtmlHelper);
        InformationIcon = new ImageToolTipComponent(HtmlHelper);
        DropDownDateTypes = new DropDownListComponent(HtmlHelper);
        AndLabel = new LabelComponent(HtmlHelper);
        
        // Initialize date components
        FirstDate = firstDateBuilder?.Component ?? new DateTimeComponent(HtmlHelper);
        SecondDate = secondDateBuilder?.Component ?? new DateTimeComponent(HtmlHelper);
        FirstWeek = firstWeekBuilder?.Component ?? new WeekYearComponent(HtmlHelper);
        SecondWeek = secondWeekBuilder?.Component ?? new WeekYearComponent(HtmlHelper);
        
        // Set defaults
        DisplayTime = true;
        DisplayEraseButton = true;
    }

    public override IHtmlContent RenderHtml()
    {
        return new CompositeDateHtmlBuilder(this).Build();
    }

    public override IHtmlContent RenderInitScript()
    {
        var builder = new StringBuilder();
        
        // Render nested component scripts
        if (!string.IsNullOrEmpty(InformationIcon.Text))
        {
            using (var sw = new StringWriter())
            {
                InformationIcon.RenderInitScript().WriteTo(sw, HtmlEncoder.Default);
                builder.Append(sw.ToString());
            }
        }

        if (DropDownDateTypes != null)
        {
            using (var sw = new StringWriter())
            {
                DropDownDateTypes.RenderInitScript().WriteTo(sw, HtmlEncoder.Default);
                builder.Append(sw.ToString());
            }
        }

        // Render date component scripts based on configuration
        if (DatesToRender != DateRenderer.SkipBothDates && FirstDate != null)
        {
            using (var sw = new StringWriter())
            {
                FirstDate.RenderInitScript().WriteTo(sw, HtmlEncoder.Default);
                builder.Append(sw.ToString());
            }
        }

        // ... continue for other nested components

        return new HtmlString(builder.ToString());
    }
}
```

### 7.4 Migration Order for Composite Components

1. **Wave 1 (Simple composites):**
   - CheckBoxList, RadioButtonList, BreadCrumbs, HorizontalTab

2. **Wave 2 (Medium composites):**
   - Duration, DualList, DialogBox, ClickToVoice

3. **Wave 3 (Menu components):**
   - HorizontalMenu, VerticalMenu, DropDownMenu

4. **Wave 4 (Complex composites):**
   - DateTimeControl (with binder), WeekYear (with binder)
   - DateDuration, CompositeDate, CompositeDateExt

### 7.5 Success Criteria for Phase 4
- [ ] All composite components compile without System.Web references
- [ ] Nested component rendering works correctly
- [ ] Resource aggregation produces correct JS/CSS lists
- [ ] Complex components like CompositeDate render all sub-components

---

## 8. Phase 5: Data Components and Model Binders

### 8.1 Objectives
- Migrate DataTable component with complex serialization
- Migrate TreeGrid component
- Convert all custom model binders to ASP.NET Core
- Update JSON serialization patterns

### 8.2 Components in Scope

| Component | Files | Complexity | Special Considerations |
|-----------|-------|------------|------------------------|
| DataTable | 12 | Very High | Custom serializer, model binder, header templates |
| TreeGrid | 7 | High | Custom columns, image handling |
| DateTimeWithFormatBinder | 1 | High | Complex validation logic |
| WeekYearWithFormatBinder | 1 | High | Similar to DateTime binder |
| DataTableContextModelBinder | 1 | Medium | JSON deserialization |

### 8.3 Model Binder Migration

#### Step 5.1: Migrate DateTimeWithFormatBinder

**File:** `Components/DateTimeControl/DateTimeWithFormatBinder.cs`

**Old Pattern (DefaultModelBinder):**
```csharp
using System.Web.Mvc;

public class DateTimeWithFormatBinder : DefaultModelBinder
{
    public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    {
        // ... binding logic
    }
}
```

**New Pattern (IModelBinder):**
```csharp
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl
{
    public class DateTimeWithFormatBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var modelName = bindingContext.ModelName;

            // Get value providers
            var datePropertyName = $"{modelName}.Date";
            var formatPropertyName = $"{modelName}.Format";
            var typePropertyName = $"{modelName}.Type";
            var timeOffsetPropertyName = $"{modelName}.TimeOffset";
            var utcPropertyName = $"{modelName}.Utc";

            var dateValue = bindingContext.ValueProvider.GetValue(datePropertyName);
            var formatValue = bindingContext.ValueProvider.GetValue(formatPropertyName);
            var typeValue = bindingContext.ValueProvider.GetValue(typePropertyName);
            var timeOffsetValue = bindingContext.ValueProvider.GetValue(timeOffsetPropertyName);
            var utcValue = bindingContext.ValueProvider.GetValue(utcPropertyName);

            if (dateValue == ValueProviderResult.None ||
                formatValue == ValueProviderResult.None ||
                typeValue == ValueProviderResult.None ||
                timeOffsetValue == ValueProviderResult.None ||
                utcValue == ValueProviderResult.None)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            // Parse values
            var dateString = dateValue.FirstValue;
            var formatString = formatValue.FirstValue;
            var typeString = typeValue.FirstValue;
            var timeOffsetString = timeOffsetValue.FirstValue;
            var utcString = utcValue.FirstValue;

            // Validate format
            if (!IsValidFormat(formatString))
            {
                bindingContext.ModelState.AddModelError(modelName, "Invalid date format");
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            // Parse time offset
            if (!double.TryParse(timeOffsetString, out var timeOffset))
            {
                bindingContext.ModelState.AddModelError(modelName, "Invalid time offset");
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            var isUtcMode = utcString == DateTimeConstants.IsUtc;
            var isModel = typeString == DateTimeConstants.ModelFormat;

            // Get hour and minute values
            var hourValue = bindingContext.ValueProvider.GetValue($"{modelName}.DropDownHours");
            var minuteValue = bindingContext.ValueProvider.GetValue($"{modelName}.DropDownMins");

            var hourString = hourValue != ValueProviderResult.None ? hourValue.FirstValue : string.Empty;
            var minuteString = minuteValue != ValueProviderResult.None ? minuteValue.FirstValue : string.Empty;

            // Create result
            var result = new DateTimeWithFormat(formatString, isModel)
            {
                DateText = dateString,
                HourValue = hourString,
                MinuteValue = minuteString,
                TimeOffset = timeOffset,
                IsUtcMode = isUtcMode
            };

            // Validate and parse date
            if (!string.IsNullOrEmpty(dateString))
            {
                if (!TryParseDate(dateString, formatString, typeString, out var parsedDate))
                {
                    bindingContext.ModelState.AddModelError(modelName, "Invalid date");
                }
            }

            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }

        private bool IsValidFormat(string format)
        {
            return format == DateTimeConstants.EnglishFormat || 
                   format == DateTimeConstants.FrenchFormat;
        }

        private bool TryParseDate(string dateValue, string format, string type, out DateTime result)
        {
            result = DateTime.MinValue;
            
            if (type == DateTimeConstants.StandardFormat)
            {
                return DateTime.TryParseExact(dateValue, format, null, 
                    DateTimeStyles.None, out result);
            }
            
            // Handle model format parsing
            result = DateComponentHelper.ParseModelDate(dateValue, format, 0, false);
            return result != DateTime.MinValue;
        }
    }

    /// <summary>
    /// Provider for DateTimeWithFormatBinder
    /// </summary>
    public class DateTimeWithFormatBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(DateTimeWithFormat))
            {
                return new DateTimeWithFormatBinder();
            }
            return null;
        }
    }
}
```

#### Step 5.2: Migrate DataTableContextModelBinder

**File:** `Components/DataTable/Context/DataTableContextModelBinder.cs`

```csharp
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable.Context
{
    public class DataTableContextModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var contextName = $"{bindingContext.ModelName}.Context";
            var value = bindingContext.ValueProvider.GetValue(contextName);

            if (value == ValueProviderResult.None)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            var attemptedValue = value.FirstValue;
            if (string.IsNullOrEmpty(attemptedValue))
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            try
            {
                var retrievedValue = JsonConvert.DeserializeObject<DataTableContext>(attemptedValue);
                bindingContext.Result = ModelBindingResult.Success(retrievedValue);
            }
            catch (JsonException)
            {
                bindingContext.ModelState.AddModelError(contextName, "Invalid DataTable context");
                bindingContext.Result = ModelBindingResult.Failed();
            }

            return Task.CompletedTask;
        }
    }

    public class DataTableContextBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(DataTableContext))
            {
                return new DataTableContextModelBinder();
            }
            return null;
        }
    }
}
```

#### Step 5.3: Register Model Binders

Create a registration extension:

**New File:** `Extensions/ModelBinderExtensions.cs`

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace Equant.SAV2000.ComponentLibrary.MVC.Extensions
{
    public static class ModelBinderExtensions
    {
        public static IMvcBuilder AddSav2000ModelBinders(this IMvcBuilder builder)
        {
            builder.AddMvcOptions(options =>
            {
                options.ModelBinderProviders.Insert(0, new DateTimeWithFormatBinderProvider());
                options.ModelBinderProviders.Insert(0, new WeekYearWithFormatBinderProvider());
                options.ModelBinderProviders.Insert(0, new DataTableContextBinderProvider());
            });

            return builder;
        }
    }
}
```

### 8.4 DataTable Component Migration

The DataTable component is the most complex component in the library. Key migration points:

1. **DataTableOptionsSerializer** - Update JSON serialization
2. **IDataTableHeaderTemplate** - Update TagBuilder usage
3. **DataTableHtmlBuilder** - Complex HTML generation

#### Step 5.4: Migrate DataTableComponent

Key changes for DataTableComponent:
- Update `HtmlHelper` to `IHtmlHelper`
- Update `WriteHtml` to `RenderHtml`
- Update `WriteInitScript` to `RenderInitScript`
- Ensure JSON serialization compatibility

### 8.5 Success Criteria for Phase 5
- [ ] All model binders implement IModelBinder
- [ ] Model binder providers are created and registered
- [ ] DataTable renders correctly with all features
- [ ] TreeGrid renders correctly
- [ ] JSON serialization produces correct output

---

## 9. Phase 6: Validators and Client-Side Validation

### 9.1 Objectives
- Migrate all 8 validation attributes
- Replace IClientValidatable with ASP.NET Core equivalent
- Maintain client-side validation functionality
- Register validators with ASP.NET Core validation system

### 9.2 Validators in Scope

| Validator | Client Validation | Complexity |
|-----------|-------------------|------------|
| CustomStringLengthAttribute | Yes | Low |
| DateConditionalRequiredAttribute | Yes | Medium |
| DateRequiredAttribute | Yes | Medium |
| DurationValidatorAttribute | Yes | High |
| EndDateGreaterThanAttribute | Yes | Medium |
| EndWeekGreaterThanAttribute | Yes | Medium |
| LessThanCurrentDateAttribute | Yes | Medium |
| WeekConditionalRequiredAttribute | Yes | Medium |

### 9.3 Client-Side Validation Migration

ASP.NET Core uses `IClientModelValidator` instead of `IClientValidatable`:

#### Step 6.1: Create Base Validator Class

**New File:** `Validators/ClientValidatorBase.cs`

```csharp
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Equant.SAV2000.ComponentLibrary.MVC.Validators
{
    /// <summary>
    /// Base class for validators that support client-side validation.
    /// </summary>
    public abstract class ClientValidatorBase : ValidationAttribute, IClientModelValidator
    {
        protected ClientValidatorBase() : base() { }
        
        protected ClientValidatorBase(string errorMessage) : base(errorMessage) { }

        /// <summary>
        /// Adds client-side validation attributes.
        /// </summary>
        public abstract void AddValidation(ClientModelValidationContext context);

        /// <summary>
        /// Merges an attribute if it doesn't already exist.
        /// </summary>
        protected bool MergeAttribute(
            IDictionary<string, string> attributes,
            string key,
            string value)
        {
            if (attributes.ContainsKey(key))
                return false;

            attributes.Add(key, value);
            return true;
        }
    }
}
```

#### Step 6.2: Migrate DateConditionalRequiredAttribute

**File:** `Validators/DateConditionalRequiredAttribute.cs`

```csharp
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Equant.SAV2000.ComponentLibrary.MVC.Validators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DateConditionalRequiredAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string _otherPropertyName;

        public DateConditionalRequiredAttribute(string otherPropertyName, string errorMessage)
            : base(errorMessage)
        {
            _otherPropertyName = otherPropertyName;
        }

        public DateConditionalRequiredAttribute(string otherPropertyName)
            : this(otherPropertyName, string.Empty)
        {
        }

        public string OtherPropertyName => _otherPropertyName;
        
        public string DateTypePropertyName { get; set; }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-dateconditionalrequired", 
                GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-dateconditionalrequired-otherproperty", 
                _otherPropertyName);
            
            if (!string.IsNullOrEmpty(DateTypePropertyName))
            {
                MergeAttribute(context.Attributes, "data-val-dateconditionalrequired-datetypeproperty",
                    DateTypePropertyName);
            }
        }

        private string GetErrorMessage(ClientModelValidationContext context)
        {
            return !string.IsNullOrEmpty(ErrorMessage) 
                ? ErrorMessage 
                : ErrorMessageString;
        }

        private void MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (!attributes.ContainsKey(key))
                attributes.Add(key, value);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var validationResult = ValidationResult.Success;

            if (value == null)
                return validationResult;

            var thisDate = value as DateTimeWithFormat;
            if (thisDate == null)
            {
                return new ValidationResult(
                    "An error occurred while validating the property. Property is not of type DateTimeWithFormat");
            }

            if (validationContext == null)
                throw new ArgumentException("validation context cannot be null");

            if (!string.IsNullOrEmpty(DateTypePropertyName))
            {
                var dateTypePropertyInfo = validationContext.ObjectType.GetProperty(DateTypePropertyName);
                var dateTypeSelected = (string)dateTypePropertyInfo.GetValue(validationContext.ObjectInstance, null);
                if (!DateComponentHelper.GetDateTypesDouble().Contains(dateTypeSelected))
                    return validationResult;
            }

            var otherPropertyInfo = validationContext.ObjectType.GetProperty(_otherPropertyName);

            if (otherPropertyInfo.PropertyType != typeof(DateTimeWithFormat))
            {
                return new ValidationResult(
                    "An error occurred while validating the property. OtherProperty is not of type DateTimeWithFormat");
            }

            var otherDate = (DateTimeWithFormat)otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

            if (otherDate?.Date != null && 
                string.IsNullOrEmpty(thisDate.DateText) && 
                string.IsNullOrEmpty(thisDate.HourValue) && 
                string.IsNullOrEmpty(thisDate.MinuteValue))
            {
                validationResult = new ValidationResult(ErrorMessageString);
            }

            return validationResult;
        }
    }
}
```

#### Step 6.3: Migrate EndDateGreaterThanAttribute

```csharp
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Equant.SAV2000.ComponentLibrary.MVC.Validators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class EndDateGreaterThanAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string _otherPropertyName;

        public EndDateGreaterThanAttribute(string otherPropertyName, string errorMessage)
            : base(errorMessage)
        {
            _otherPropertyName = otherPropertyName;
        }

        public EndDateGreaterThanAttribute(string otherPropertyName)
            : this(otherPropertyName, string.Empty)
        {
        }

        public string OtherPropertyName => _otherPropertyName;

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-enddategreaterthan", 
                GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-enddategreaterthan-otherproperty",
                _otherPropertyName);
        }

        private string GetErrorMessage(ClientModelValidationContext context)
        {
            return !string.IsNullOrEmpty(ErrorMessage) 
                ? ErrorMessage 
                : ErrorMessageString;
        }

        private void MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (!attributes.ContainsKey(key))
                attributes.Add(key, value);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // ... (keep existing validation logic)
        }
    }
}
```

#### Step 6.4: Migrate DurationValidatorAttribute

The DurationValidatorAttribute is the most complex validator with multiple property references:

```csharp
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Equant.SAV2000.ComponentLibrary.MVC.Validators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class DurationValidatorAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly string _firstPropertyName;
        private readonly string _secondPropertyName;
        private readonly string _firstPropertyHtmlId;
        private readonly string _secondPropertyHtmlId;
        private readonly string _thirdPropertyHtmlId;

        public DurationValidatorAttribute(
            string firstPropertyName,
            string secondPropertyName,
            string firstPropertyHtmlId,
            string secondPropertyHtmlId,
            string thirdPropertyHtmlId,
            string errorMessage)
            : base(errorMessage)
        {
            _firstPropertyName = firstPropertyName;
            _secondPropertyName = secondPropertyName;
            _firstPropertyHtmlId = firstPropertyHtmlId;
            _secondPropertyHtmlId = secondPropertyHtmlId;
            _thirdPropertyHtmlId = thirdPropertyHtmlId;
        }

        public DurationValidatorAttribute(
            string firstPropertyName,
            string secondPropertyName,
            string firstPropertyHtmlId,
            string secondPropertyHtmlId,
            string thirdPropertyHtmlId)
            : this(firstPropertyName, secondPropertyName, firstPropertyHtmlId, 
                   secondPropertyHtmlId, thirdPropertyHtmlId, string.Empty)
        {
        }

        public string FirstPropertyName => _firstPropertyName;
        public string SecondPropertyName => _secondPropertyName;
        public string FirstPropertyHtmlId => _firstPropertyHtmlId;
        public string SecondPropertyHtmlId => _secondPropertyHtmlId;
        public string ThirdPropertyHtmlId => _thirdPropertyHtmlId;

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-durationvalidator", 
                GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-durationvalidator-startdateid", 
                _firstPropertyHtmlId);
            MergeAttribute(context.Attributes, "data-val-durationvalidator-enddateid", 
                _secondPropertyHtmlId);
            MergeAttribute(context.Attributes, "data-val-durationvalidator-datedurationid", 
                _thirdPropertyHtmlId);
        }

        private string GetErrorMessage(ClientModelValidationContext context)
        {
            return !string.IsNullOrEmpty(ErrorMessage) 
                ? ErrorMessage 
                : ErrorMessageString;
        }

        private void MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (!attributes.ContainsKey(key))
                attributes.Add(key, value);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // ... (keep existing validation logic)
        }
    }
}
```

### 9.4 Client-Side JavaScript Updates

The existing JavaScript validation files need to be updated to work with the new data attributes:

**File:** `Resources/Javascripts/Validator.js`

Add adapters for ASP.NET Core unobtrusive validation:

```javascript
// Add these adapters for ASP.NET Core compatibility
$.validator.unobtrusive.adapters.add('dateconditionalrequired', ['otherproperty'], function (options) {
    options.rules['dateconditionalrequired'] = {
        otherproperty: options.params.otherproperty
    };
    options.messages['dateconditionalrequired'] = options.message;
});

$.validator.unobtrusive.adapters.add('enddategreaterthan', ['otherproperty'], function (options) {
    options.rules['enddategreaterthan'] = {
        otherproperty: options.params.otherproperty
    };
    options.messages['enddategreaterthan'] = options.message;
});

$.validator.unobtrusive.adapters.add('durationvalidator', ['startdateid', 'enddateid', 'datedurationid'], function (options) {
    options.rules['durationvalidator'] = {
        startdateid: options.params.startdateid,
        enddateid: options.params.enddateid,
        datedurationid: options.params.datedurationid
    };
    options.messages['durationvalidator'] = options.message;
});
```

### 9.5 Success Criteria for Phase 6
- [ ] All 8 validators implement IClientModelValidator
- [ ] Server-side validation logic preserved
- [ ] Client-side validation attributes generated correctly
- [ ] JavaScript validation adapters work with unobtrusive validation
- [ ] Validation error messages display correctly

---

## 10. Phase 7: Resource Management and Asset Bundling

### 10.1 Objectives
- Replace ClientDependency framework
- Implement modern asset bundling strategy
- Configure embedded resource serving
- Update script/style rendering components

### 10.2 ClientDependency Replacement Strategy

The ClientDependency framework has no .NET Core equivalent. Options:

| Option | Pros | Cons | Recommendation |
|--------|------|------|----------------|
| WebOptimizer | Built-in bundling, caching | Different API | **Recommended** |
| Custom Middleware | Full control | More work | For complex needs |
| Static Files | Simple | No bundling | Not recommended |

### 10.3 Implementation Steps

#### Step 7.1: Add WebOptimizer Package

Update the project file:

```xml
<PackageReference Include="LigerShark.WebOptimizer.Core" Version="3.0.405" />
```

#### Step 7.2: Create Resource Middleware

**New File:** `Infrastructure/EmbeddedResourceMiddleware.cs`

```csharp
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace Equant.SAV2000.ComponentLibrary.MVC.Infrastructure
{
    public class EmbeddedResourceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly EmbeddedFileProvider _fileProvider;
        private readonly string _requestPath;

        public EmbeddedResourceMiddleware(
            RequestDelegate next,
            Assembly assembly,
            string resourceNamespace,
            string requestPath)
        {
            _next = next;
            _fileProvider = new EmbeddedFileProvider(assembly, resourceNamespace);
            _requestPath = requestPath;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value;
            
            if (path != null && path.StartsWith(_requestPath))
            {
                var resourcePath = path.Substring(_requestPath.Length).TrimStart('/');
                var fileInfo = _fileProvider.GetFileInfo(resourcePath);
                
                if (fileInfo.Exists)
                {
                    context.Response.ContentType = GetContentType(resourcePath);
                    
                    using var stream = fileInfo.CreateReadStream();
                    await stream.CopyToAsync(context.Response.Body);
                    return;
                }
            }

            await _next(context);
        }

        private string GetContentType(string path)
        {
            if (path.EndsWith(".js")) return "application/javascript";
            if (path.EndsWith(".css")) return "text/css";
            if (path.EndsWith(".png")) return "image/png";
            if (path.EndsWith(".jpg") || path.EndsWith(".jpeg")) return "image/jpeg";
            return "application/octet-stream";
        }
    }
}
```

#### Step 7.3: Create Service Collection Extensions

**New File:** `Extensions/ServiceCollectionExtensions.cs`

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Equant.SAV2000.ComponentLibrary.MVC.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSav2000Components(this IServiceCollection services)
        {
            // Register component services
            services.AddSingleton<IScriptRenderer, ScriptRenderer>();
            services.AddSingleton<IStyleRenderer, StyleRenderer>();
            
            return services;
        }

        public static IApplicationBuilder UseSav2000Resources(this IApplicationBuilder app)
        {
            // Serve embedded JavaScript resources
            app.UseMiddleware<EmbeddedResourceMiddleware>(
                Assembly.GetExecutingAssembly(),
                "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts",
                "/_sav2000/js");

            return app;
        }
    }
}
```

#### Step 7.4: Migrate ScriptRendererComponent

**File:** `Components/ScriptRenderer/ScriptRendererComponent.cs`

```csharp
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ScriptRenderer
{
    public class ScriptRendererComponent : ComponentBase
    {
        private readonly HashSet<string> _registeredScripts = new();
        private readonly List<JsResource> _scripts = new();
        private readonly bool _isLightRequirement;

        public ScriptRendererComponent(IHtmlHelper htmlHelper, bool isLightRequirement = false)
            : base(htmlHelper)
        {
            _isLightRequirement = isLightRequirement;
        }

        public void RegisterScript(JsResource resource)
        {
            if (!_registeredScripts.Contains(resource.Name))
            {
                _registeredScripts.Add(resource.Name);
                _scripts.Add(resource);
            }
        }

        public void RegisterScripts(IEnumerable<JsResource> resources)
        {
            foreach (var resource in resources)
            {
                RegisterScript(resource);
            }
        }

        public override IHtmlContent RenderHtml()
        {
            var builder = new StringBuilder();
            
            // Sort scripts by order
            var orderedScripts = _scripts.OrderBy(s => s.Order).ToList();

            foreach (var script in orderedScripts)
            {
                var url = GetScriptUrl(script);
                builder.AppendLine($"<script src=\"{url}\"></script>");
            }

            return new HtmlString(builder.ToString());
        }

        public override IHtmlContent RenderInitScript()
        {
            return HtmlString.Empty;
        }

        public override ReadOnlyCollection<JsResource> JsResources => 
            new ReadOnlyCollection<JsResource>(new List<JsResource>());

        private string GetScriptUrl(JsResource resource)
        {
            // Convert embedded resource path to URL
            var resourcePath = resource.Path
                .Replace("Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.", "")
                .Replace("Equant.SAV2000.ComponentLibrary.Common.Resources.Javascripts.", "");
            
            return $"/_sav2000/js/{resourcePath}";
        }
    }
}
```

#### Step 7.5: Migrate StyleRendererComponent

Similar pattern to ScriptRendererComponent for CSS resources.

### 10.4 Success Criteria for Phase 7
- [ ] Embedded resources served via middleware
- [ ] Script registration and rendering works
- [ ] Style registration and rendering works
- [ ] No ClientDependency references remain
- [ ] Resources load correctly in browser

---

## 11. Phase 8: Integration and Entry Point

### 11.1 Objectives
- Migrate HtmlHelperExtension (main entry point)
- Create Tag Helper alternatives
- Final integration testing
- Documentation updates

### 11.2 HtmlHelperExtension Migration

**File:** `Extensions/HtmlHelperExtension.cs`

```csharp
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Equant.SAV2000.ComponentLibrary.MVC.Extensions
{
    public static class HtmlHelperExtension
    {
        /// <summary>
        /// SAV2000 component factory entry point.
        /// </summary>
        public static ComponentFactory<TModel> Sav2000<TModel>(this IHtmlHelper<TModel> helper)
        {
            return helper.Sav2000(false);
        }

        /// <summary>
        /// SAV2000 component factory with light requirement option.
        /// </summary>
        public static ComponentFactory<TModel> Sav2000<TModel>(
            this IHtmlHelper<TModel> helper, 
            bool isLightRequirement)
        {
            // Get or create script renderer from HttpContext
            var httpContext = helper.ViewContext.HttpContext;
            
            var scriptRenderer = httpContext.Items[ScriptRendererComponent.ContextKey] as ScriptRendererComponent
                ?? new ScriptRendererComponent(helper, isLightRequirement);
            
            if (!httpContext.Items.ContainsKey(ScriptRendererComponent.ContextKey))
            {
                httpContext.Items[ScriptRendererComponent.ContextKey] = scriptRenderer;
            }

            var styleRenderer = httpContext.Items[StyleRendererComponent.ContextKey] as StyleRendererComponent
                ?? new StyleRendererComponent(helper);
            
            if (!httpContext.Items.ContainsKey(StyleRendererComponent.ContextKey))
            {
                httpContext.Items[StyleRendererComponent.ContextKey] = styleRenderer;
            }

            return new ComponentFactory<TModel>(
                helper,
                new ScriptRendererBuilder(scriptRenderer, null),
                new StyleRendererBuilder(styleRenderer, null));
        }

        /// <summary>
        /// Validation message helper with accessibility attributes.
        /// </summary>
        public static IHtmlContent Sav2000ValidationMessageFor<TModel, TProperty>(
            this IHtmlHelper<TModel> htmlHelper,
            System.Linq.Expressions.Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.ValidationMessageFor(expression, null, new { role = "alert" });
        }
    }
}
```

### 11.3 Optional: Create Tag Helpers

For modern ASP.NET Core development, consider creating Tag Helpers:

**New File:** `TagHelpers/Sav2000ButtonTagHelper.cs`

```csharp
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Equant.SAV2000.ComponentLibrary.MVC.TagHelpers
{
    [HtmlTargetElement("sav2000-button")]
    public class Sav2000ButtonTagHelper : TagHelper
    {
        [HtmlAttributeName("css-class")]
        public string CssClass { get; set; }

        [HtmlAttributeName("on-click")]
        public string OnClick { get; set; }

        [HtmlAttributeName("value")]
        public string Value { get; set; }

        [HtmlAttributeName("disabled")]
        public bool Disabled { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "input";
            output.TagMode = TagMode.SelfClosing;
            
            output.Attributes.SetAttribute("type", "submit");
            output.Attributes.SetAttribute("value", Value);
            
            if (!string.IsNullOrEmpty(CssClass))
                output.Attributes.SetAttribute("class", CssClass);
            
            if (Disabled)
                output.Attributes.SetAttribute("disabled", "disabled");
            
            if (!string.IsNullOrEmpty(OnClick))
                output.Attributes.SetAttribute("data-onclick", OnClick);
        }
    }
}
```

### 11.4 Final Integration Checklist

- [ ] All components compile without errors
- [ ] HtmlHelperExtension works as entry point
- [ ] ComponentFactory creates all component types
- [ ] Script rendering produces correct output
- [ ] Style rendering produces correct output
- [ ] Model binders registered and working
- [ ] Validators registered and working
- [ ] Client-side validation functional
- [ ] All 47 JavaScript files accessible

### 11.5 Success Criteria for Phase 8
- [ ] `@Html.Sav2000()` extension method works
- [ ] All component builders accessible via factory
- [ ] Complete rendering pipeline functional
- [ ] No runtime errors in component rendering
- [ ] Integration tests pass

---

## 12. Test Case Strategy

### 12.1 Testing Framework Recommendations

Since the project currently has no test infrastructure, implement:

| Framework | Purpose | Package |
|-----------|---------|---------|
| xUnit | Unit testing | xunit, xunit.runner.visualstudio |
| Moq | Mocking | Moq |
| FluentAssertions | Assertions | FluentAssertions |
| AngleSharp | HTML parsing | AngleSharp |
| bUnit | Blazor/Razor testing | bunit |

### 12.2 Test Categories by Module

#### 12.2.1 Infrastructure Tests (Priority: Critical)

```csharp
public class ResourceProviderTests
{
    [Fact]
    public void Instance_ReturnsSameInstance()
    {
        var instance1 = ResourceProvider.Instance;
        var instance2 = ResourceProvider.Instance;
        Assert.Same(instance1, instance2);
    }

    [Fact]
    public void GetWebResourceUrl_ReturnsCorrectPath()
    {
        var url = ResourceProvider.Instance.GetWebResourceUrl(
            typeof(ButtonComponent), 
            "Button.js");
        Assert.Contains("Button.js", url);
    }
}
```

#### 12.2.2 Component Base Tests (Priority: Critical)

```csharp
public class ComponentBaseTests
{
    private readonly Mock<IHtmlHelper> _mockHtmlHelper;

    public ComponentBaseTests()
    {
        _mockHtmlHelper = new Mock<IHtmlHelper>();
    }

    [Fact]
    public void Constructor_SetsHtmlHelper()
    {
        var component = new TestComponent(_mockHtmlHelper.Object);
        Assert.NotNull(component.HtmlHelper);
    }

    [Fact]
    public void HtmlAttributes_InitializedAsEmpty()
    {
        var component = new TestComponent(_mockHtmlHelper.Object);
        Assert.Empty(component.HtmlAttributes);
    }
}
```

#### 12.2.3 Simple Component Tests (Priority: High)

```csharp
public class ButtonComponentTests
{
    [Fact]
    public void RenderHtml_WhenVisible_ReturnsInputTag()
    {
        var component = CreateButtonComponent();
        component.Id = "testButton";
        component.Value = "Click Me";
        component.IsVisible = true;

        var html = component.RenderHtml();
        var htmlString = GetHtmlString(html);

        Assert.Contains("<input", htmlString);
        Assert.Contains("id=\"testButton\"", htmlString);
        Assert.Contains("type=\"submit\"", htmlString);
    }

    [Fact]
    public void RenderHtml_WhenNotVisible_ReturnsEmpty()
    {
        var component = CreateButtonComponent();
        component.IsVisible = false;

        var html = component.RenderHtml();
        var htmlString = GetHtmlString(html);

        Assert.Empty(htmlString);
    }

    [Fact]
    public void RenderInitScript_GeneratesJQueryCall()
    {
        var component = CreateButtonComponent();
        component.Id = "testButton";
        component.OnClick = "handleClick()";

        var script = component.RenderInitScript();
        var scriptString = GetHtmlString(script);

        Assert.Contains("$('#testButton')", scriptString);
        Assert.Contains("savbutton", scriptString);
    }
}
```

#### 12.2.4 Composite Component Tests (Priority: High)

```csharp
public class CompositeDateComponentTests
{
    [Fact]
    public void JsResources_AggregatesNestedComponentResources()
    {
        var component = CreateCompositeDateComponent();
        
        var resources = component.JsResources;
        
        Assert.Contains(resources, r => r.Name == "JsCompositeDate");
        Assert.True(resources.Count > 1); // Should include nested resources
    }

    [Fact]
    public void RenderHtml_RendersAllNestedComponents()
    {
        var component = CreateCompositeDateComponent();
        component.DatesToRender = DateRenderer.RenderBothDates;

        var html = component.RenderHtml();
        var htmlString = GetHtmlString(html);

        // Verify nested components rendered
        Assert.Contains("FirstDate", htmlString);
        Assert.Contains("SecondDate", htmlString);
    }
}
```

#### 12.2.5 Model Binder Tests (Priority: High)

```csharp
public class DateTimeWithFormatBinderTests
{
    [Fact]
    public async Task BindModelAsync_ValidDate_ReturnsSuccess()
    {
        var binder = new DateTimeWithFormatBinder();
        var context = CreateBindingContext("2024-01-15", "dd/MM/yyyy");

        await binder.BindModelAsync(context);

        Assert.True(context.Result.IsModelSet);
        var result = context.Result.Model as DateTimeWithFormat;
        Assert.NotNull(result);
    }

    [Fact]
    public async Task BindModelAsync_InvalidDate_AddsModelError()
    {
        var binder = new DateTimeWithFormatBinder();
        var context = CreateBindingContext("invalid-date", "dd/MM/yyyy");

        await binder.BindModelAsync(context);

        Assert.True(context.ModelState.ErrorCount > 0);
    }
}
```

#### 12.2.6 Validator Tests (Priority: High)

```csharp
public class DateConditionalRequiredAttributeTests
{
    [Fact]
    public void IsValid_WhenOtherDateSet_RequiresThisDate()
    {
        var attribute = new DateConditionalRequiredAttribute("OtherDate", "Date is required");
        var model = new TestModel
        {
            OtherDate = new DateTimeWithFormat { Date = DateTime.Now },
            ThisDate = new DateTimeWithFormat { DateText = "" }
        };

        var result = attribute.GetValidationResult(model.ThisDate, CreateContext(model));

        Assert.NotEqual(ValidationResult.Success, result);
    }

    [Fact]
    public void AddValidation_AddsCorrectAttributes()
    {
        var attribute = new DateConditionalRequiredAttribute("OtherDate");
        var context = CreateClientValidationContext();

        attribute.AddValidation(context);

        Assert.Contains("data-val-dateconditionalrequired", context.Attributes.Keys);
    }
}
```

#### 12.2.7 Integration Tests (Priority: Medium)

```csharp
public class HtmlHelperExtensionTests
{
    [Fact]
    public void Sav2000_ReturnsComponentFactory()
    {
        var htmlHelper = CreateHtmlHelper<TestModel>();

        var factory = htmlHelper.Sav2000();

        Assert.NotNull(factory);
    }

    [Fact]
    public void Sav2000_CreatesScriptRenderer()
    {
        var htmlHelper = CreateHtmlHelper<TestModel>();

        var factory = htmlHelper.Sav2000();

        Assert.NotNull(factory.ScriptRenderer);
    }
}
```

### 12.3 Test Coverage Targets

| Module | Target Coverage | Priority |
|--------|-----------------|----------|
| Infrastructure | 90% | Critical |
| Components/Api | 90% | Critical |
| Simple Components | 80% | High |
| Composite Components | 75% | High |
| Model Binders | 85% | High |
| Validators | 90% | High |
| Extensions | 80% | Medium |
| Helpers | 70% | Low |

### 12.4 Testing Phases

1. **Phase 1-2 Testing:** Unit tests for infrastructure and base classes
2. **Phase 3 Testing:** Unit tests for each simple component
3. **Phase 4 Testing:** Integration tests for composite components
4. **Phase 5 Testing:** Model binder tests with mock contexts
5. **Phase 6 Testing:** Validator tests including client-side attribute generation
6. **Phase 7 Testing:** Resource serving and rendering tests
7. **Phase 8 Testing:** End-to-end integration tests

---

## 13. Risk Assessment and Mitigation

### 13.1 Risk Matrix

| Risk | Probability | Impact | Mitigation |
|------|-------------|--------|------------|
| Common project not migrated | High | Critical | Create abstraction layer, migrate in parallel |
| ClientDependency replacement gaps | Medium | High | Implement custom middleware, document limitations |
| Breaking changes in consuming apps | High | High | Provide migration guide, maintain API compatibility |
| JavaScript compatibility issues | Medium | Medium | Test all 47 JS files, update jQuery calls if needed |
| Performance regression | Low | Medium | Benchmark before/after, optimize hot paths |
| Missing functionality | Medium | Medium | Document gaps, provide workarounds |

### 13.2 Rollback Strategy

1. **Version Control:** Tag current state before migration
2. **Branch Strategy:** Use feature branch for migration
3. **Parallel Deployment:** Run both versions during transition
4. **Feature Flags:** Enable gradual rollout

### 13.3 Dependencies on External Projects

The library references `Equant.SAV2000.ComponentLibrary.Common` which must also be migrated. Options:

1. **Migrate Common First:** Recommended if possible
2. **Create Abstraction Layer:** Interface-based decoupling
3. **Multi-targeting:** Support both frameworks temporarily

---

## 14. Appendix: API Mapping Reference

### 14.1 System.Web.Mvc to Microsoft.AspNetCore.Mvc

| Old API | New API |
|---------|---------|
| `HtmlHelper` | `IHtmlHelper` |
| `HtmlHelper<TModel>` | `IHtmlHelper<TModel>` |
| `TagBuilder` | `TagBuilder` (different namespace) |
| `MvcHtmlString` | `HtmlString` / `IHtmlContent` |
| `TagRenderMode` | `TagRenderMode` |
| `ModelMetadata` | `ModelMetadata` (different namespace) |
| `ControllerContext` | `ControllerContext` |
| `ModelBindingContext` | `ModelBindingContext` |
| `IModelBinder` | `IModelBinder` (async) |
| `DefaultModelBinder` | Implement `IModelBinder` |
| `IClientValidatable` | `IClientModelValidator` |
| `ModelClientValidationRule` | Use `AddValidation` method |

### 14.2 System.Web.UI to ASP.NET Core

| Old API | New API |
|---------|---------|
| `HtmlTextWriter` | `IHtmlContent` / `StringBuilder` |
| `Page` | `EmbeddedFileProvider` |
| `WebResource` | Embedded resources via middleware |

### 14.3 Newtonsoft.Json Updates

| Old Pattern | New Pattern |
|-------------|-------------|
| `JsonConvert.SerializeObject` | Same (upgrade to 13.x) |
| `JRaw` | Same |
| Custom converters | Same patterns work |

---

## Document History

| Version | Date | Author | Changes |
|---------|------|--------|---------|
| 1.0 | 2024-XX-XX | Migration Team | Initial document |

---

## Conclusion

This migration plan provides a systematic approach to migrating the Equant SAV2000 MVC Component Library from .NET Framework 4.8 to .NET 8. The phased approach minimizes risk by addressing dependencies in order and allowing incremental testing at each phase.

Key success factors:
1. Follow the phase order strictly
2. Complete testing at each phase before proceeding
3. Document any deviations or issues encountered
4. Maintain communication with consuming application teams
5. Plan for parallel operation during transition period

Estimated total effort: 8-9 weeks with a team of 2-3 developers.
