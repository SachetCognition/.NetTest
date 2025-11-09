# E2E Tests for Equant.SAV2000.ComponentLibrary.MVC

## Overview

This directory is reserved for End-to-End (E2E) tests using Playwright to test JavaScript interactions and client-side behavior of the migrated components.

## Setup Instructions

To create the E2E test project:

```bash
dotnet new xunit -n Equant.SAV2000.ComponentLibrary.MVC.E2ETests
cd Equant.SAV2000.ComponentLibrary.MVC.E2ETests
dotnet add reference ../Equant.SAV2000.ComponentLibrary.MVC.csproj
dotnet add package Microsoft.Playwright
dotnet add package Microsoft.Playwright.NUnit
```

## Test Coverage

E2E tests should cover:

1. **Button Component JavaScript**
   - OnClick event handlers
   - Disabled state behavior
   - Form submission

2. **CheckBox Component JavaScript**
   - OnClick and OnChange event handlers
   - Checked/unchecked state changes
   - Hidden field synchronization

3. **ActionButton Component JavaScript**
   - Modal dialog integration
   - OnClick event handlers
   - Dialog box triggering

4. **ClickToVoice Component JavaScript**
   - jQuery qtip tooltip initialization
   - Click-to-call functionality
   - Phone number display

## Example Test Structure

```csharp
[Test]
public async Task ButtonComponent_OnClick_ShouldTriggerJavaScriptHandler()
{
    await Page.GotoAsync("http://localhost:5000/test-page");
    await Page.ClickAsync("#btn-submit");
    var result = await Page.EvaluateAsync<string>("() => window.lastClickedButton");
    Assert.AreEqual("btn-submit", result);
}
```

## Running Tests

```bash
dotnet test
```

## Notes

- E2E tests require a running web application
- Playwright will automatically download required browser binaries
- Tests should be isolated and not depend on external services
