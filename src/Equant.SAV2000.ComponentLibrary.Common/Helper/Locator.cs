namespace Equant.SAV2000.ComponentLibrary.Common
{
    /// <summary>
    /// Marker type used to locate embedded resources within this assembly.
    /// Components reference typeof(Locator) when registering resources that
    /// are embedded in the Common assembly (JS plugins, CSS files, etc.).
    /// </summary>
    public sealed class Locator
    {
        private Locator()
        {
        }
    }
}
