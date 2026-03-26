# User Requirement Specification: Migration of Equant.SAV2000.ComponentLibrary.MVC to .NET 8+

## 1. Purpose

This document specifies the functional requirements for migrating the SAV2000 Component Library from .NET Framework 4.8 / ASP.NET MVC 5 to .NET 8+ / ASP.NET Core, ensuring zero deprecation of business functionality.

## 2. Scope

Migration of 25 UI component families, 8 custom validators, 3 model binders, 47 jQuery plugins, and the supporting infrastructure (resource management, localization, extensions, and helper utilities).

## 3. Functional Requirements

### FR-001: Component Rendering Parity

All components must produce equivalent HTML output and client-side behavior after migration. The following component families must be preserved:

| # | Component | Namespace | Key Properties / Behavior |
|---|-----------|-----------|--------------------------|
| 1 | ActionButton | `Components.ActionButton` | URL navigation, modal dialog trigger (`DialogBoxId`), CSS class management, disabled state |
| 2 | Button | `Components.Button` | Submit with JavaScript handlers (`OnClick`), disabled/read-only CSS states, access text for screen readers |
| 3 | CheckBox | `Components.CheckBox` | Checked state, label association, disabled state, JavaScript event handlers |
| 4 | CheckBoxList | `Components.CheckBoxList` | Multi-checkbox with data binding (`DataBind`), selected items tracking, fieldset wrapping, sort ordering |
| 5 | ClickToVoice | `Components.ClickToVoice` | Phone call widget with anchor, image, telephone span, qtip tooltip, `RootService` telephony integration |
| 6 | CompositeDate | `Components.CompositeDate` | Complex date/week range selector combining DateTimeControl + WeekYear + DropDownList, date type switching (16 enum types), conditional rendering/visibility, model vs standard date formats |
| 7 | CompositeDateExt | `Components.CompositeDateExt` | Extended date types enumeration (`EnumExtDateTypes`: Between, EqualCurrentDate, Equal, LessThan, GreaterThan, Add, Subtract, AddSubtract) |
| 8 | CustomLabel | `Components.CustomLabel` | Configurable label with associated control ID, access-only mode, CSS class management |
| 9 | DataTable | `Components.DataTable` | Server-side/client-side data tables with sorting, pagination, column filtering, select-all, modify/delete columns, column reorder/resize, encryption, error page URL generation, refresh time display, record limit messages |
| 10 | DateDurationControl | `Components.DateDurationControl` | Duration input with Hour, Minute, Second, Millisecond fields |
| 11 | DateTimeControl | `Components.DateTimeControl` | Date picker with time dropdowns (hour/minute), information icon tooltip, erase button, current date selector, UTC mode, timezone offset, French/English formats, model date format (D+5, J-3), read-only mode, custom validation |
| 12 | DropDownList | `Components.DropDownList` | Select element with data binding, CSS class, change handler, div wrapping, custom label integration |
| 13 | ErrorComponent | `Components.ErrorComponent` | Error message display with HTML content, error collection, accessible text, CSS styling |
| 14 | HyperLink | `Components.HyperLink` | Anchor with URL/action URL, title, image, CSS class, click handler, target |
| 15 | Image | `Components.Image` | Image display with source (`ImageSrc`), alt text, CSS class |
| 16 | ImageToolTip | `Components.ImageToolTip` | Image with qtip tooltip, persistence mode (Click/Hover), CSS class management for span/inner span, alternate text |
| 17 | Label | `Components.Label` | Text label with CSS class, `for` attribute association, label-specific CSS class |
| 18 | Menu | `Components.Menu` | Menu items with child menus, multiple control types (Linkbutton, Redirect, Custom, RedirectJs, LinkbuttonJs, Imagebutton, ImagebuttonJs), hidden state, action URL/name, onclick events |
| 19 | ScriptRenderer | `Components.ScriptRenderer` | Client-side script rendering with ClientDependency integration, block script rendering |
| 20 | SpanLabel | `Components.SpanLabel` | Inline span-based label component |
| 21 | StyleRender | `Components.StyleRender` | CSS stylesheet rendering with ClientDependency integration |
| 22 | TreeGrid | `Components.TreeGrid` | Hierarchical data grid with image properties, hyperlink columns, updatable rows |
| 23 | VerticalMenu | `Components.VerticalMenu` | Vertical navigation menu with JavaScript resources |
| 24 | WeekYear | `Components.WeekYear` | Week/year selector with French/English format support, week text ID, information icon, associated week year HTML ID, week change handler |

### FR-002: Fluent Builder API Preservation

The consumer API pattern must have a documented equivalent in the new stack:

**Current Pattern (ASP.NET MVC 5):**
```csharp
Html.Sav2000().ComponentName()
    .Property(value)
    .CssClass("class")
    .OnClick("handler()")
```

**Required:** Each component's builder methods must be preserved. The migration must provide one of:
- Tag Helper attributes (preferred for simple components)
- View Component parameters (for complex components)
- Equivalent fluent API in ASP.NET Core

**Builder methods that must be preserved per component** (non-exhaustive, representative):
- `ComponentBuilderBase<T,TBuilder>`: `Id()`, `Name()`, `Visible()`, `HtmlAttributes()`
- `DateTimeBuilder`: `CustomLabel()`, `Value()`, `DisplayTime()`, `DisplayEraseButton()`, `DisplayInformationIcon()`, `CssMainDiv()`, `OnDateChange()`, `StartFromCurrentDate()`, `AssociatedDateHtmlId()`, `CalendarImagePath()`, `ExternalLabelText()`, `EraseImagePath()`, `InformationIconPath()`, `ConditionalAnnotations()`, `Mandatory()`, `MandatoryMessage()`, `CurrentDateSelectionImageUrl()`, `CurrentDateSelectionTitle()`, `DisplayCurrentDateSelector()`
- `CompositeDateBuilder`: `CustomLabel()`, `InformationIcon()`, `DateTypesIncluded()`, `FirstDateAndFormat()`, `SecondDateAndFormat()`, `FirstWeekAndFormat()`, `SecondWeekAndFormat()`, `DateTypes()`, `DisplayTime()`, `DisplayEraseButton()`, all CSS class methods, all onChange methods
- `DataTableBuilder`: `Criteria()`, `ServiceUri()`, `InitialSorting()`, `IsMultiSelect()`, `IsServerSide()`, `IsEncryptionRequired()`, all column configuration methods

### FR-003: JavaScript Plugin Compatibility

All 47 jQuery plugins must continue to function. Each plugin's initialization pattern `$('#id').pluginName(options)` must produce identical behavior. The JSON options structure for each plugin must not change.

**Complete list of jQuery plugins (from Resources/Javascripts):**

| # | Plugin File | Associated Component |
|---|------------|---------------------|
| 1 | ActionButton.js | ActionButton |
| 2 | BreadCrumbs.js | Navigation |
| 3 | Button.js | Button |
| 4 | CheckBox.js | CheckBox |
| 5 | CheckBoxList.js | CheckBoxList |
| 6 | ClickToVoice.js | ClickToVoice |
| 7 | CommentBox.js | CommentBox |
| 8 | Common.js | Shared utilities |
| 9 | Communicator.js | Cross-component messaging |
| 10 | CompositeDate.js | CompositeDate |
| 11 | CompositeDateExt.js | CompositeDateExt |
| 12 | CustomHeader.js | CustomHeader |
| 13 | CustomValidator.js | Validation |
| 14 | DataTable.js | DataTable |
| 15 | DataTableModifyDelete.js | DataTable modify/delete |
| 16 | DataTableSelection.js | DataTable row selection |
| 17 | DateDuration.js | DateDurationControl |
| 18 | DateTime.js | DateTimeControl |
| 19 | DateTimeCommon.js | DateTime shared utilities |
| 20 | DialogBox.js | Modal dialogs |
| 21 | DropDownList.js | DropDownList |
| 22 | DropDownMenu.js | DropDownMenu |
| 23 | DualList.js | DualList |
| 24 | Duration.js | Duration |
| 25 | DurationValidator.js | Duration validation |
| 26 | ErrorDisplay.js | ErrorComponent |
| 27 | HorizontalMenu.js | HorizontalMenu |
| 28 | HorizontalTab.js | HorizontalTab |
| 29 | HyperLink.js | HyperLink |
| 30 | IMOffice.js | IM Office integration |
| 31 | Image.js | Image |
| 32 | ImageToolTip.js | ImageToolTip |
| 33 | ListBox.js | ListBox |
| 34 | PopOver.js | Popover |
| 35 | Popup.js | Popup |
| 36 | PopupImage.js | PopupImage |
| 37 | ProgressBar.js | ProgressBar |
| 38 | RadioButton.js | RadioButton |
| 39 | RadioButtonList.js | RadioButtonList |
| 40 | TextArea.js | TextArea |
| 41 | TextBox.js | TextBox |
| 42 | TreeCommon.js | Tree shared utilities |
| 43 | TreeGrid.js | TreeGrid |
| 44 | TreeView.js | TreeView |
| 45 | Validator.js | Validation framework |
| 46 | VerticalMenu.js | VerticalMenu |
| 47 | WeekYear.js | WeekYear |

### FR-004: Client-Side Validation

All 8 custom validators must produce identical validation behavior on both client and server. The `IClientValidatable` interface and `GetClientValidationRules` methods from ASP.NET MVC must be replaced with ASP.NET Core's equivalent client-side validation approach (e.g., `IClientModelValidator` and `AddValidation` method).

| # | Validator | Business Rule |
|---|-----------|--------------|
| 1 | `CustomStringLengthAttribute` | Validates string length with custom min/max bounds |
| 2 | `DateConditionalRequiredAttribute` | Date fields required based on selected `EnumDateTypes` condition |
| 3 | `DateRequiredAttribute` | Validates that a date value is provided (non-empty) |
| 4 | `DurationValidatorAttribute` | Validates duration fields (hour/minute/second/millisecond) |
| 5 | `EndDateGreaterThanAttribute` | Ensures end date is strictly after start date |
| 6 | `EndWeekGreaterThanAttribute` | Ensures end week/year is after start week/year |
| 7 | `LessThanCurrentDateAttribute` | Validates that a date is before the current date |
| 8 | `WeekConditionalRequiredAttribute` | Week fields required based on selected date type condition |

### FR-005: Model Binding

The following custom model binders must produce identical binding results for the same form data:

| # | Binder | Responsibility |
|---|--------|---------------|
| 1 | `DateTimeWithFormatBinder` | Binds date text, hour, minute, format, time offset, UTC mode, and model indicator into `DateTimeWithFormat` |
| 2 | `WeekYearWithFormatBinder` | Binds week text, year text, format into `WeekYearWithFormat` |
| 3 | `DataTableContextModelBinder` | Binds DataTable server-side request parameters (page, sort, filter, search) into `DataTableContext` |

### FR-006: Localization

All ~60+ localized string keys from `ApplicationStrings.resx` must be preserved. French and English locales must be supported.

**Key categories:**
- **DataTable strings** (15+): `Datatable_sRefreshTime`, `Datatable_sEmptyTable`, `Datatable_sInfo`, `Datatable_sInfoEmpty`, `Datatable_sInfoThousands`, `Datatable_sLoadingRecords`, `Datatable_sProcessing`, `Datatable_sZeroRecords`, `Datatable_sInfoFiltered`, pagination labels (First, Last, Next, Previous), ARIA sort labels
- **Label strings** (15+): `LBL000011`, `LBL000016`, `LBL000017`, `LBL000022`, `LBL000025`-`LBL000030`, `LBL000032`, `LBL000385`, `LBL000911`, `LBL000912`
- **Tooltip strings** (12+): `TIP000001`, `TIP000010`-`TIP000014`, `TIP000021`-`TIP000025`
- **Error strings**: `ERR_DATE_INVALIDE`, `ERR_DATE_OBLIGATOIRE`, `TimeMandatory`
- **Message strings**: `MSG000467`, `MSG000491`, `MSG000509`, `RecordsLimitMessageShort`, `RecordsLimitMessageLong`, `ErrorTitle`
- **Access strings**: `ACCESS000002`, `ACCESS000005`-`ACCESS000007`
- **Date-related**: `MomentFormat`, `DateImageToolTip`, `TPOAR02F02T77CE11_ENTRE`, `TPOAR02F02T77CE11_DATE_DU_JOUR`, `Today_Date`, `equals_To`
- **Multilingual**: `MULBL001708`, `MULBL001709`, `MULBL001722`-`MULBL001725`
- **Format**: `AM`, `PM`, `CleanImageTooltip`
- **UI elements**: `lblAsteriks`, `lblsemiColon`

### FR-007: Resource Delivery

Component JS/CSS must be delivered to the browser with correct load ordering. The current `JsResource`/`CssResource` system with priority-based ordering must be preserved.

**Current mechanism:**
- Each component declares `JsResources` and `CssResources` as `ReadOnlyCollection` with priority integers
- Resources are embedded in assemblies and served via ClientDependency framework
- Conditional resource inclusion (e.g., DataTable filter scripts, information icon scripts) must be preserved

**Migration requirement:**
- Replace ClientDependency with ASP.NET Core static file serving or a modern bundler
- Preserve load ordering via priority values
- Preserve conditional inclusion logic

**Embedded resources from Common project:**
- `Resources/Javascripts/BundledDatatableScripts.js`
- `Resources/Javascripts/jquery.cluetip.custom.js`
- `Resources/Javascripts/jquery.dataTables.columnFilter.js`
- `Resources/Javascripts/jquery.dataTables.colMoveResize.js`
- `Resources/Javascripts/jquery.qtip.js`
- `Resources/Javascripts/jquery-ui-datepicker.js`
- `Resources/Css/jquery.qtip.css`

### FR-008: DataTable Server-Side Processing

Server-side mode (POST to ServiceUri with criteria, encryption, pagination, sorting) must produce identical HTTP requests and handle identical response formats.

**Key features:**
- `IsServerSide` mode with `AjaxSource` URL and `ServerMethod` (POST)
- `CriteriaParameter` with `ClientId`, `PropertyName`, `Value`, `EvalType` (None/Value/Expression)
- `IsEncryptionRequired` with `Crypt.Encrypt()` for parameter encryption
- `DataTableContext` model binding: `iDisplayStart`, `iDisplayLength`, `iSortCol_X`, `sSortDir_X`, `sSearch`, `sEcho`
- Column configuration: `IsVisible`, `IsSortable`, `Class`, `Name`, `Data`, `Render` (JRaw), `DefaultContent`, `ColumnType`
- Column filter: `DataTableColumnFilterColumnOption` with `ColumnType` (Text/Null), `MaxLength`
- Column reorder: `DataTableColumnsReorderOption` with `AllowReorder`, `AllowResize`
- Sorting: `List<KeyValuePair<string, SortDirection>>` with `SortDirection.Asc`/`Desc`
- Language options: Full localization of all DataTable UI strings
- `DrawCallBack`, `ServerParams` as JRaw JavaScript expressions
- Select-all functionality, modify/delete column rendering

### FR-009: Date/Time Business Logic

All date/time business logic must be preserved:

- **UTC mode**: `IsUtcMode` flag with `TimeOffset` for timezone-aware date calculations
- **Timezone offset**: `DateComponentHelper.GetCurrentDate(timeOffset, utcMode)` for server-side current date resolution
- **Model date format**: Support for relative date expressions (D+5, J-3, S+1, etc.) parsed by `DateTimeWithFormatBinder`
- **16 date type enumerations** (`EnumDateTypes`): Between, Empty, Equal, EqualCurrentDate, GreaterThan, GreaternThanOrEqual, LessThan, LessThanOrEqual, ModelBetween, ModelEqual, ModelGreaterThan, ModelGreaterThanOrEqual, ModelLessThan, ModelLessThanOrEqual, Week, WeekBetween
- **French/English date formats**: `DateTimeConstants.EnglishFormat` / `DateTimeConstants.FrenchFormat`, regex patterns for week parsing (`RegexWeekEnglish`, `RegexWeekFrench`)
- **Week/year calculations**: ISO week numbering via `WeekHelper`, first day of week calculation, week-from-current-week with delta
- **Consistency validations**: `ConsistencyCheckOfProvidedValues` ensuring date type matches provided values, `DateTypeSameAsModel` ensuring model types match
- **Extended date types** (`EnumExtDateTypes`): Between, EqualCurrentDate, Equal, LessThan, GreaterThan, Add, Subtract, AddSubtract

### FR-010: Accessibility

All accessibility features must be preserved:

- **Screen-reader labels**: `AccessText` property on components rendered as hidden spans with `hide-access` CSS class
- **Label association**: `for` attributes linking labels to input controls via `AssociatedControlId`
- **Title attributes**: Tooltip text on interactive elements (`Title` on HyperLink, ImageToolTip)
- **Alt text**: `AlternateText` on image components
- **ARIA labels**: DataTable sort direction labels (`SortAscending`, `SortDescending` in `DataTableLanguageAriaOption`)
- **Access text patterns**: Formatted access strings (e.g., `ACCESS000002`, `ACCESS000005`-`ACCESS000007`) providing context-specific screen reader text

### FR-011: DataTable Features

All DataTable interactive features must be preserved:

- **Select-all**: Checkbox in header for bulk selection (`IsMultiSelect`)
- **Delete column**: Row-level delete action rendering
- **Modify column**: Row-level edit action rendering
- **Column reorder**: Drag-and-drop column reordering (`ColReorder`, `AllowReorder`)
- **Column resize**: User-resizable columns (`AllowResize`)
- **Column filter**: Per-column text filtering (`DataTableColumnFilterColumnOption`)
- **Multi-sort**: Multiple column sorting with `InitialSorting` as `List<KeyValuePair<string, SortDirection>>`
- **Encryption**: Parameter encryption via `Crypt.Encrypt()` when `IsEncryptionRequired`
- **Error page URL**: Generated error page URLs for server-side error handling
- **Refresh time display**: `Datatable_sRefreshTime` localized string
- **Record limit messages**: `RecordsLimitMessageShort` / `RecordsLimitMessageLong` for pagination limits
- **Deferred rendering**: `DeferRender` for performance optimization
- **Scroll modes**: `ScrollX`, `ScrollY`, `ScrollCollapse` for fixed-height tables
- **DOM customization**: `Dom` property for DataTable element placement
- **Pagination types**: Configurable `PaginationType`

## 4. Non-Functional Requirements

### NFR-001: Target Framework

.NET 8.0 or later, ASP.NET Core MVC / Razor Pages.

### NFR-002: Project Format

SDK-style csproj, Razor Class Library for component packaging.

### NFR-003: Package Management

NuGet PackageReference format. Remove `packages.config` and legacy package restore.

### NFR-004: Test Coverage

Minimum 80% code coverage with unit tests for all components, validators, and model binders before migration begins. Test categories:
- Component HTML output tests (verify rendered markup)
- Builder fluent API tests (verify property setting)
- Validator logic tests (verify business rules)
- Model binder tests (verify form data binding)
- JavaScript plugin initialization tests (verify JSON options)

### NFR-005: Build Tooling

- Replace `ajaxmin.bat` pre-build event with modern JS bundler (esbuild or webpack)
- Replace FxCop ruleset with Roslyn analyzers (Microsoft.CodeAnalysis.NetAnalyzers)
- Integrate with `dotnet build` / `dotnet publish` pipeline
- Configure CI/CD for automated build and test

### NFR-006: Performance

- Component rendering performance must not regress (benchmark before/after)
- DataTable server-side processing latency must remain equivalent
- Resource delivery (JS/CSS) must use modern caching and compression (gzip/brotli)

### NFR-007: Security

- Replace `Crypt.Encrypt()` with ASP.NET Core Data Protection API or equivalent
- Implement anti-forgery token validation for DataTable POST requests
- Sanitize all user-provided HTML content in error messages

## 5. Migration Mapping

### 5.1 Type Replacements

| Legacy Type | Modern Replacement |
|---|---|
| `System.Web.Mvc.HtmlHelper` | `Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper` |
| `System.Web.Mvc.MvcHtmlString` | `Microsoft.AspNetCore.Html.HtmlString` / `IHtmlContent` |
| `System.Web.Mvc.ModelMetadata` | `Microsoft.AspNetCore.Mvc.ModelBinding.ModelMetadata` |
| `System.Web.Mvc.SelectList` | `Microsoft.AspNetCore.Mvc.Rendering.SelectList` |
| `System.Web.Mvc.SelectListItem` | `Microsoft.AspNetCore.Mvc.Rendering.SelectListItem` |
| `System.Web.Mvc.TagBuilder` | `Microsoft.AspNetCore.Mvc.Rendering.TagBuilder` |
| `System.Web.UI.HtmlTextWriter` | `System.IO.TextWriter` or TagHelper output |
| `System.Web.Mvc.DefaultModelBinder` | `Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder` |
| `System.ComponentModel.DataAnnotations.ValidationAttribute` | Same (unchanged in .NET Core) |
| `System.Web.Mvc.IClientValidatable` | `Microsoft.AspNetCore.Mvc.ModelBinding.Validation.IClientModelValidator` |
| `Newtonsoft.Json.JsonConvert` | `System.Text.Json.JsonSerializer` (or keep Newtonsoft for JRaw support) |
| `Newtonsoft.Json.Linq.JRaw` | Custom `JsonConverter` or keep Newtonsoft.Json |
| `ClientDependency.Core` | ASP.NET Core static files + bundling middleware |

### 5.2 Architecture Changes

| Aspect | Legacy (.NET Framework 4.8) | Modern (.NET 8+) |
|--------|---------------------------|-------------------|
| Project format | Legacy csproj (non-SDK) | SDK-style csproj |
| Component packaging | Class library with embedded resources | Razor Class Library (RCL) |
| Resource embedding | Assembly embedded resources | wwwroot static files in RCL |
| Script rendering | `HtmlTextWriter` + `WriteInitScript` | Tag Helpers + `ITagHelperComponent` |
| CSS/JS delivery | ClientDependency framework | LibMan / npm + bundling middleware |
| Model binders | `DefaultModelBinder` inheritance | `IModelBinder` + `IModelBinderProvider` |
| Validation | `IClientValidatable` + `GetClientValidationRules` | `IClientModelValidator` + `AddValidation` |
| DI/IoC | Manual construction in builders | ASP.NET Core built-in DI |
| Localization | `.resx` with `ResourceManager` | `.resx` with `IStringLocalizer<T>` |
| Configuration | `web.config` | `appsettings.json` + `IOptions<T>` |

### 5.3 Component Migration Strategy

Each component should be migrated following this pattern:

1. **Create Tag Helper** (or View Component for complex components) replacing the `HtmlBuilder`
2. **Preserve the Component class** as a POCO/options model
3. **Convert Builder to Tag Helper attributes** or fluent extension methods on `IHtmlHelper`
4. **Move embedded JS/CSS** to `wwwroot/` in the Razor Class Library
5. **Update `WriteInitScript`** to use `<script>` tag generation via Tag Helper
6. **Replace `HtmlTextWriter`** with `TagHelperOutput` or `IHtmlContent`
7. **Update validation attributes** to implement `IClientModelValidator`

## 6. Risks and Mitigations

| Risk | Impact | Mitigation |
|------|--------|-----------|
| jQuery plugin incompatibility with modern bundlers | High | Test each plugin individually; consider gradual migration to vanilla JS |
| JRaw serialization differences between Newtonsoft and System.Text.Json | Medium | Keep Newtonsoft.Json for components using JRaw; migrate later |
| ClientDependency resource ordering breaks | High | Create integration tests verifying script load order |
| Model binder behavior differences | High | Create comprehensive model binder tests with form data fixtures |
| Date/time calculation differences between frameworks | Medium | Create unit tests for all `WeekHelper` and `DateComponentHelper` methods |
| Accessibility regression | Medium | Automated HTML comparison tests for ARIA attributes |
| French/English locale differences | Low | Test both locales for all localized strings |

## 7. Acceptance Criteria

1. All 25 component families render identical HTML output (verified by snapshot tests)
2. All 47 jQuery plugins initialize correctly with identical options JSON
3. All 8 validators produce identical client-side and server-side validation behavior
4. All 3 model binders bind identical form data to identical model instances
5. All 60+ localized strings are accessible in both French and English
6. All DataTable features (server-side, select-all, column reorder, filter, encryption) function identically
7. All date/time business logic (UTC mode, model dates, week calculations) produces identical results
8. All accessibility features (ARIA labels, screen reader text, label associations) are preserved
9. Build succeeds with zero errors on `dotnet build`
10. Minimum 80% code coverage achieved
11. No security regressions (encryption, anti-forgery, input sanitization)
