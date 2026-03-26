namespace Equant.SAV2000.ComponentLibrary.MVC.Infrastructure
{
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Provides page-level context for ASP.NET Core (replaces System.Web.UI.Page).
    /// </summary>
    public class PageProvider
    {
        private readonly HttpContext _httpContext;

        public PageProvider(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        public HttpContext HttpContext => _httpContext;
    }
}
