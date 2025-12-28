namespace Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;

using System.Reflection;
using Microsoft.AspNetCore.Http;

/// <summary>
/// The page provider - provides access to embedded resources in ASP.NET Core.
/// Replaces the System.Web.UI.Page dependency from .NET Framework.
/// </summary>
public sealed class PageProvider
{
    private static readonly Lazy<PageProvider> Lazy = new Lazy<PageProvider>(() => new PageProvider());

    private PageProvider()
    {
    }

    public static PageProvider Instance => Lazy.Value;

    /// <summary>
    /// Gets the URL for an embedded web resource.
    /// In ASP.NET Core, embedded resources are accessed differently than in .NET Framework.
    /// </summary>
    public string GetWebResourceUrl(Type type, string resourceName)
    {
        return $"/_content/{type.Assembly.GetName().Name}/{resourceName}";
    }

    /// <summary>
    /// Gets the embedded resource stream from an assembly.
    /// </summary>
    public Stream? GetResourceStream(Type type, string resourceName)
    {
        return type.Assembly.GetManifestResourceStream(resourceName);
    }

    /// <summary>
    /// Gets the embedded resource content as a string.
    /// </summary>
    public string? GetResourceContent(Type type, string resourceName)
    {
        using var stream = GetResourceStream(type, resourceName);
        if (stream == null) return null;
        
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}
