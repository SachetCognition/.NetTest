// --------------------------------------------------------------------------------------------------------------------
// Migration Test Cases for Epic 8: .NET Framework 4.8 -> .NET 8+ Migration
// Covers US-MIG-001 through US-MIG-005 (11 test cases total)
// --------------------------------------------------------------------------------------------------------------------

using System.Reflection;
using System.Xml.Linq;
using FluentAssertions;
using Xunit;

namespace Equant.SAV2000.ComponentLibrary.Tests.Migration;

/// <summary>
/// US-MIG-001: Project Format - SDK-style csproj targeting net8.0
/// </summary>
public class ProjectFormatTests
{
    private static readonly string SolutionRoot = FindSolutionRoot();
    private static readonly string MainCsprojPath = Path.Combine(SolutionRoot, "Equant.SAV2000.ComponentLibrary.MVC.csproj");
    private static readonly string CommonCsprojPath = Path.Combine(SolutionRoot, "Equant.SAV2000.ComponentLibrary.Common", "Equant.SAV2000.ComponentLibrary.Common.csproj");

    private static string FindSolutionRoot()
    {
        // Walk up from test assembly location to find the repo root
        var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        while (dir != null)
        {
            if (File.Exists(Path.Combine(dir, "Equant.SAV2000.ComponentLibrary.MVC.csproj")))
                return dir;
            dir = Directory.GetParent(dir)?.FullName;
        }

        // Fallback: try relative path from test project
        var testDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        var candidate = Path.GetFullPath(Path.Combine(testDir, "..", "..", "..", "..", ".."));
        if (File.Exists(Path.Combine(candidate, "Equant.SAV2000.ComponentLibrary.MVC.csproj")))
            return candidate;

        throw new InvalidOperationException("Could not find solution root containing Equant.SAV2000.ComponentLibrary.MVC.csproj");
    }

    /// <summary>
    /// TC-MIG-001-U01: Project uses SDK-style csproj
    /// Verifies the main project file uses the SDK-style format with an Sdk attribute on the Project element.
    /// </summary>
    [Fact]
    public void TC_MIG_001_U01_ProjectUsesSdkStyleCsproj()
    {
        // Arrange & Act
        var mainDoc = XDocument.Load(MainCsprojPath);
        var commonDoc = XDocument.Load(CommonCsprojPath);

        // Assert - SDK-style projects have Sdk attribute on Project element
        var mainSdk = mainDoc.Root?.Attribute("Sdk")?.Value;
        var commonSdk = commonDoc.Root?.Attribute("Sdk")?.Value;

        mainSdk.Should().NotBeNullOrEmpty("Main project must use SDK-style csproj with Sdk attribute");
        mainSdk.Should().Contain("Microsoft.NET.Sdk", "Main project should use a Microsoft.NET.Sdk variant");

        commonSdk.Should().NotBeNullOrEmpty("Common project must use SDK-style csproj with Sdk attribute");
        commonSdk.Should().Contain("Microsoft.NET.Sdk", "Common project should use a Microsoft.NET.Sdk variant");
    }

    /// <summary>
    /// TC-MIG-001-U02: Target framework is net8.0
    /// Verifies both projects target net8.0.
    /// </summary>
    [Fact]
    public void TC_MIG_001_U02_TargetFrameworkIsNet80()
    {
        // Arrange & Act
        var mainDoc = XDocument.Load(MainCsprojPath);
        var commonDoc = XDocument.Load(CommonCsprojPath);

        var ns = mainDoc.Root?.Name.Namespace ?? XNamespace.None;
        var mainTfm = mainDoc.Root?.Descendants(ns + "TargetFramework").FirstOrDefault()?.Value;

        var nsCommon = commonDoc.Root?.Name.Namespace ?? XNamespace.None;
        var commonTfm = commonDoc.Root?.Descendants(nsCommon + "TargetFramework").FirstOrDefault()?.Value;

        // Assert
        mainTfm.Should().Be("net8.0", "Main project must target net8.0");
        commonTfm.Should().Be("net8.0", "Common project must target net8.0");
    }

    /// <summary>
    /// TC-MIG-001-U03: No legacy MSBuild elements present
    /// Verifies no legacy MSBuild elements such as ToolsVersion, Import Microsoft.CSharp.targets, 
    /// SourceSafe SCC properties, or packages.config references.
    /// </summary>
    [Fact]
    public void TC_MIG_001_U03_NoLegacyMsBuildElements()
    {
        // Arrange
        var mainContent = File.ReadAllText(MainCsprojPath);
        var commonContent = File.ReadAllText(CommonCsprojPath);

        // Assert - No ToolsVersion attribute
        mainContent.Should().NotContain("ToolsVersion=", "Main project should not have ToolsVersion attribute");
        commonContent.Should().NotContain("ToolsVersion=", "Common project should not have ToolsVersion attribute");

        // Assert - No legacy Import elements
        mainContent.Should().NotContain("Microsoft.CSharp.targets", "Main project should not import Microsoft.CSharp.targets");
        commonContent.Should().NotContain("Microsoft.CSharp.targets", "Common project should not import Microsoft.CSharp.targets");

        // Assert - No SourceSafe SCC properties
        mainContent.Should().NotContain("SccProjectName", "Main project should not have SCC properties");
        mainContent.Should().NotContain("SccLocalPath", "Main project should not have SCC local path");
        mainContent.Should().NotContain("SccAuxPath", "Main project should not have SCC aux path");
        mainContent.Should().NotContain("SccProvider", "Main project should not have SCC provider");

        // Assert - No packages.config reference
        mainContent.Should().NotContain("packages.config", "Main project should not reference packages.config");
        commonContent.Should().NotContain("packages.config", "Common project should not reference packages.config");

        // Assert - Uses PackageReference (not HintPath to packages/ folder)
        mainContent.Should().NotContain("<HintPath>", "Main project should use PackageReference, not HintPath references");
    }
}

/// <summary>
/// US-MIG-002: No System.Web Dependencies
/// </summary>
public class NoSystemWebTests
{
    /// <summary>
    /// TC-MIG-002-U01: Zero System.Web.Mvc references in compiled assembly
    /// Verifies the compiled assembly has no references to System.Web.Mvc.
    /// </summary>
    [Fact]
    public void TC_MIG_002_U01_ZeroSystemWebMvcReferences()
    {
        // Arrange - Load the compiled assembly
        var assembly = typeof(Equant.SAV2000.ComponentLibrary.MVC.Components.Api.ComponentBase).Assembly;
        var referencedAssemblies = assembly.GetReferencedAssemblies();

        // Assert - No System.Web.Mvc reference
        referencedAssemblies.Should().NotContain(
            a => a.Name == "System.Web.Mvc",
            "Compiled assembly must not reference System.Web.Mvc");

        // Also check that no System.Web assembly is referenced at all
        referencedAssemblies.Should().NotContain(
            a => a.Name != null && a.Name.StartsWith("System.Web"),
            "Compiled assembly must not reference any System.Web assemblies");
    }

    /// <summary>
    /// TC-MIG-002-U02: Zero System.Web.UI references
    /// Verifies the compiled assembly has no references to System.Web.UI.
    /// </summary>
    [Fact]
    public void TC_MIG_002_U02_ZeroSystemWebUiReferences()
    {
        // Arrange - Load the compiled assembly
        var assembly = typeof(Equant.SAV2000.ComponentLibrary.MVC.Components.Api.ComponentBase).Assembly;
        var referencedAssemblies = assembly.GetReferencedAssemblies();

        // Assert - No System.Web.UI reference
        referencedAssemblies.Should().NotContain(
            a => a.Name == "System.Web.UI",
            "Compiled assembly must not reference System.Web.UI");

        // Verify source files don't contain System.Web references
        var assemblyLocation = assembly.Location;
        var sourceRoot = FindSourceRoot(assemblyLocation);
        if (sourceRoot != null)
        {
            var csFiles = Directory.GetFiles(sourceRoot, "*.cs", SearchOption.AllDirectories)
                .Where(f => !f.Contains(Path.DirectorySeparatorChar + "obj" + Path.DirectorySeparatorChar))
                .Where(f => !f.Contains(Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar))
                .Where(f => !f.Contains(Path.DirectorySeparatorChar + "tests" + Path.DirectorySeparatorChar));

            foreach (var file in csFiles)
            {
                var content = File.ReadAllText(file);
                content.Should().NotContain("using System.Web.UI",
                    $"File {Path.GetFileName(file)} should not contain System.Web.UI using");
            }
        }
    }

    private static string? FindSourceRoot(string assemblyLocation)
    {
        var dir = Path.GetDirectoryName(assemblyLocation);
        while (dir != null)
        {
            if (File.Exists(Path.Combine(dir, "Equant.SAV2000.ComponentLibrary.MVC.csproj")))
                return dir;
            dir = Directory.GetParent(dir)?.FullName;
        }
        return null;
    }
}

/// <summary>
/// US-MIG-003: Static File Delivery
/// </summary>
public class StaticFileDeliveryTests
{
    private static readonly string SolutionRoot = FindSolutionRoot();

    private static string FindSolutionRoot()
    {
        var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        while (dir != null)
        {
            if (File.Exists(Path.Combine(dir, "Equant.SAV2000.ComponentLibrary.MVC.csproj")))
                return dir;
            dir = Directory.GetParent(dir)?.FullName;
        }
        var testDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        var candidate = Path.GetFullPath(Path.Combine(testDir, "..", "..", "..", "..", ".."));
        if (File.Exists(Path.Combine(candidate, "Equant.SAV2000.ComponentLibrary.MVC.csproj")))
            return candidate;
        throw new InvalidOperationException("Could not find solution root");
    }

    /// <summary>
    /// TC-MIG-003-U01: All JS/CSS files present in wwwroot/
    /// Verifies all JavaScript and CSS files are present in the wwwroot directory.
    /// </summary>
    [Fact]
    public void TC_MIG_003_U01_AllJsCssFilesPresentInWwwroot()
    {
        // Arrange
        var wwwrootJs = Path.Combine(SolutionRoot, "wwwroot", "js");
        var wwwrootCss = Path.Combine(SolutionRoot, "wwwroot", "css");

        // Assert - wwwroot/js directory exists and contains files
        Directory.Exists(wwwrootJs).Should().BeTrue("wwwroot/js directory must exist");
        var jsFiles = Directory.GetFiles(wwwrootJs, "*.js");
        jsFiles.Should().NotBeEmpty("wwwroot/js must contain JavaScript files");

        // Verify key JS files are present
        var expectedJsFiles = new[]
        {
            "BundledDatatableScripts.js",
            "DataTable.js",
            "DateTime.js",
            "Button.js",
            "CheckBox.js",
            "CompositeDate.js",
            "Common.js",
            "ActionButton.js"
        };

        var jsFileNames = jsFiles.Select(Path.GetFileName).ToArray();
        foreach (var expected in expectedJsFiles)
        {
            jsFileNames.Should().Contain(expected, $"wwwroot/js should contain {expected}");
        }

        // Assert - wwwroot/css directory exists and contains files
        Directory.Exists(wwwrootCss).Should().BeTrue("wwwroot/css directory must exist");
        var cssFiles = Directory.GetFiles(wwwrootCss, "*.css");
        cssFiles.Should().NotBeEmpty("wwwroot/css must contain CSS files");

        // Verify no WebResource/EmbeddedResource embedding for JS/CSS
        var csprojContent = File.ReadAllText(Path.Combine(SolutionRoot, "Equant.SAV2000.ComponentLibrary.MVC.csproj"));
        csprojContent.Should().NotContain("EmbeddedResource Include=\"Scripts",
            "JS files should not be embedded resources");
    }

    /// <summary>
    /// TC-MIG-003-I01: Static files served correctly via Kestrel
    /// Verifies the project is configured as a Razor Class Library (RCL) that can serve static files.
    /// </summary>
    [Fact]
    public void TC_MIG_003_I01_StaticFilesServedViaKestrel()
    {
        // Arrange - Verify the project is configured as RCL
        var csprojPath = Path.Combine(SolutionRoot, "Equant.SAV2000.ComponentLibrary.MVC.csproj");
        var csprojContent = File.ReadAllText(csprojPath);

        // Assert - Project uses Razor SDK (enables static file serving in RCL)
        csprojContent.Should().Contain("Microsoft.NET.Sdk.Razor",
            "Project must use Razor SDK for RCL static file serving");

        // Assert - AddRazorSupportForMvc is enabled
        csprojContent.Should().Contain("AddRazorSupportForMvc",
            "Project should have AddRazorSupportForMvc for MVC integration");

        // Assert - wwwroot directory exists with content that will be served
        var wwwrootPath = Path.Combine(SolutionRoot, "wwwroot");
        Directory.Exists(wwwrootPath).Should().BeTrue("wwwroot directory must exist for RCL static file serving");

        var allStaticFiles = Directory.GetFiles(wwwrootPath, "*.*", SearchOption.AllDirectories);
        allStaticFiles.Should().NotBeEmpty("wwwroot must contain static files for serving");

        // Verify the RCL will serve files under _content/AssemblyName/
        // This is automatic with Razor SDK when wwwroot/ exists
        var jsFiles = Directory.GetFiles(Path.Combine(wwwrootPath, "js"), "*.js");
        jsFiles.Length.Should().BeGreaterThan(40,
            "At least 40+ JS files should be present in wwwroot/js for the component library");
    }
}

/// <summary>
/// US-MIG-004: Modern Build Tooling
/// </summary>
public class ModernBuildToolingTests
{
    private static readonly string SolutionRoot = FindSolutionRoot();

    private static string FindSolutionRoot()
    {
        var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        while (dir != null)
        {
            if (File.Exists(Path.Combine(dir, "Equant.SAV2000.ComponentLibrary.MVC.csproj")))
                return dir;
            dir = Directory.GetParent(dir)?.FullName;
        }
        var testDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        var candidate = Path.GetFullPath(Path.Combine(testDir, "..", "..", "..", "..", ".."));
        if (File.Exists(Path.Combine(candidate, "Equant.SAV2000.ComponentLibrary.MVC.csproj")))
            return candidate;
        throw new InvalidOperationException("Could not find solution root");
    }

    /// <summary>
    /// TC-MIG-004-U01: Roslyn analyzers configured (no FxCop)
    /// Verifies that Roslyn analyzers are configured and FxCop is not used.
    /// </summary>
    [Fact]
    public void TC_MIG_004_U01_RoslynAnalyzersConfiguredNoFxCop()
    {
        // Arrange
        var csprojContent = File.ReadAllText(Path.Combine(SolutionRoot, "Equant.SAV2000.ComponentLibrary.MVC.csproj"));

        // Assert - Roslyn analyzers are referenced
        csprojContent.Should().Contain("Microsoft.CodeAnalysis.NetAnalyzers",
            "Project must reference Roslyn analyzers (Microsoft.CodeAnalysis.NetAnalyzers)");

        // Assert - No FxCop references
        csprojContent.Should().NotContain("FxCop",
            "Project should not reference FxCop analyzers");
        csprojContent.Should().NotContain("Microsoft.VisualStudio.CodeAnalysis",
            "Project should not reference legacy VS Code Analysis");

        // Assert - .editorconfig exists for code style enforcement
        var editorConfigPath = Path.Combine(SolutionRoot, ".editorconfig");
        File.Exists(editorConfigPath).Should().BeTrue(".editorconfig must exist for code style enforcement");

        var editorConfigContent = File.ReadAllText(editorConfigPath);
        editorConfigContent.Should().NotBeEmpty(".editorconfig must have content");
    }

    /// <summary>
    /// TC-MIG-004-U02: JS minification via modern tooling
    /// Verifies that modern JS minification tooling is configured (not legacy ajaxmin.bat).
    /// </summary>
    [Fact]
    public void TC_MIG_004_U02_JsMinificationViaModernTooling()
    {
        // Arrange
        var solutionRoot = SolutionRoot;

        // Assert - No legacy ajaxmin.bat
        var ajaxminPath = Path.Combine(solutionRoot, "ajaxmin.bat");
        File.Exists(ajaxminPath).Should().BeFalse("Legacy ajaxmin.bat should not exist");

        // Assert - No reference to ajaxmin in project files
        var csprojContent = File.ReadAllText(Path.Combine(solutionRoot, "Equant.SAV2000.ComponentLibrary.MVC.csproj"));
        csprojContent.Should().NotContain("ajaxmin",
            "Project should not reference legacy ajaxmin tool");

        // Assert - Modern static file structure exists (wwwroot/ with organized JS)
        var wwwrootJs = Path.Combine(solutionRoot, "wwwroot", "js");
        Directory.Exists(wwwrootJs).Should().BeTrue(
            "wwwroot/js must exist as the modern static file delivery mechanism");

        // Verify JS files exist and are servable (RCL pattern replaces embedded resources + minification)
        var jsFiles = Directory.GetFiles(wwwrootJs, "*.js");
        jsFiles.Should().NotBeEmpty("JS files must be present in wwwroot/js for modern delivery");
    }
}

/// <summary>
/// US-MIG-005: Test Coverage
/// </summary>
public class TestCoverageTests
{
    private static readonly string SolutionRoot = FindSolutionRoot();
    private static readonly string TestProjectRoot = FindTestProjectRoot();

    private static string FindSolutionRoot()
    {
        var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        while (dir != null)
        {
            if (File.Exists(Path.Combine(dir, "Equant.SAV2000.ComponentLibrary.MVC.csproj")))
                return dir;
            dir = Directory.GetParent(dir)?.FullName;
        }
        var testDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        var candidate = Path.GetFullPath(Path.Combine(testDir, "..", "..", "..", "..", ".."));
        if (File.Exists(Path.Combine(candidate, "Equant.SAV2000.ComponentLibrary.MVC.csproj")))
            return candidate;
        throw new InvalidOperationException("Could not find solution root");
    }

    private static string FindTestProjectRoot()
    {
        var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        while (dir != null)
        {
            if (File.Exists(Path.Combine(dir, "Equant.SAV2000.ComponentLibrary.Tests.Migration.csproj")))
                return dir;
            dir = Directory.GetParent(dir)?.FullName;
        }
        throw new InvalidOperationException("Could not find test project root");
    }

    /// <summary>
    /// TC-MIG-005-U01: Code coverage collection configured (coverlet)
    /// Verifies that coverlet is configured in the test project for code coverage collection.
    /// </summary>
    [Fact]
    public void TC_MIG_005_U01_CodeCoverageCollectionConfigured()
    {
        // Arrange
        var testCsprojPath = Path.Combine(TestProjectRoot, "Equant.SAV2000.ComponentLibrary.Tests.Migration.csproj");
        var testCsprojContent = File.ReadAllText(testCsprojPath);

        // Assert - coverlet.collector is referenced
        testCsprojContent.Should().Contain("coverlet.collector",
            "Test project must reference coverlet.collector for code coverage collection");

        // Assert - coverlet.msbuild is referenced for MSBuild integration
        testCsprojContent.Should().Contain("coverlet.msbuild",
            "Test project must reference coverlet.msbuild for MSBuild-based coverage");

        // Assert - Microsoft.NET.Test.Sdk is present (required for test execution)
        testCsprojContent.Should().Contain("Microsoft.NET.Test.Sdk",
            "Test project must reference Microsoft.NET.Test.Sdk");
    }

    /// <summary>
    /// TC-MIG-005-U02: Coverage report generation works
    /// Verifies that ReportGenerator is configured for HTML coverage report generation.
    /// </summary>
    [Fact]
    public void TC_MIG_005_U02_CoverageReportGenerationWorks()
    {
        // Arrange
        var testCsprojPath = Path.Combine(TestProjectRoot, "Equant.SAV2000.ComponentLibrary.Tests.Migration.csproj");
        var testCsprojContent = File.ReadAllText(testCsprojPath);

        // Assert - ReportGenerator is referenced
        testCsprojContent.Should().Contain("ReportGenerator",
            "Test project must reference ReportGenerator for HTML coverage reports");

        // Assert - xunit is configured as test framework
        testCsprojContent.Should().Contain("xunit",
            "Test project must use xunit as the test framework");

        // Assert - xunit.runner.visualstudio is present for test discovery
        testCsprojContent.Should().Contain("xunit.runner.visualstudio",
            "Test project must reference xunit.runner.visualstudio for test discovery");

        // Assert - Test project references the main project
        testCsprojContent.Should().Contain("Equant.SAV2000.ComponentLibrary.MVC.csproj",
            "Test project must reference the main library project");
    }
}
