namespace Equant.SAV2000.DemoApp.Controllers
{
    using System.IO;
    using System.Web.Mvc;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Button;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(Duration = 3600)]
        public ActionResult EmbeddedScript(string name)
        {
            var assembly = typeof(ButtonComponent).Assembly;
            var resourceName = "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts." + name;
            var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                return HttpNotFound();
            }

            using (var reader = new StreamReader(stream))
            {
                return Content(reader.ReadToEnd(), "application/javascript");
            }
        }
    }
}
