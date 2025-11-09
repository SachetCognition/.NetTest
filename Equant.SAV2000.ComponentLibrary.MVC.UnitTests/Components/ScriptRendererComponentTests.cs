using Xunit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.IO;
using Equant.SAV2000.ComponentLibrary.MVC.Components.ScriptRenderer;

namespace Equant.SAV2000.ComponentLibrary.MVC.UnitTests.Components
{
    public class ScriptRendererComponentTests
    {
        private readonly Mock<IHtmlHelper> _mockHtmlHelper;

        public ScriptRendererComponentTests()
        {
            _mockHtmlHelper = new Mock<IHtmlHelper>();
        }

        [Fact]
        public void Constructor_InitializesWithDefaultValues()
        {
            var renderers = new System.Collections.ObjectModel.ReadOnlyCollection<object>(new System.Collections.Generic.List<object>());
            var component = new ScriptRendererComponent(_mockHtmlHelper.Object, renderers, false);

            Assert.NotNull(component);
            Assert.True(component.IsVisible);
        }

        [Fact]
        public void Builder_SetsId()
        {
            var renderers = new System.Collections.ObjectModel.ReadOnlyCollection<object>(new System.Collections.Generic.List<object>());
            var component = new ScriptRendererComponent(_mockHtmlHelper.Object, renderers, false);
            var builder = new ScriptRendererBuilder(component, null);

            builder.Id("myScriptRenderer");

            Assert.Equal("myScriptRenderer", component.Id);
        }

        [Fact]
        public void Builder_SetsName()
        {
            var renderers = new System.Collections.ObjectModel.ReadOnlyCollection<object>(new System.Collections.Generic.List<object>());
            var component = new ScriptRendererComponent(_mockHtmlHelper.Object, renderers, false);
            var builder = new ScriptRendererBuilder(component, null);

            builder.Name("ScriptRenderer1");

            Assert.Equal("ScriptRenderer1", component.Name);
        }

        [Fact]
        public void JsResources_ReturnsExpectedResources()
        {
            var renderers = new System.Collections.ObjectModel.ReadOnlyCollection<object>(new System.Collections.Generic.List<object>());
            var component = new ScriptRendererComponent(_mockHtmlHelper.Object, renderers, false);

            var resources = component.JsResources;

            Assert.NotNull(resources);
        }

        [Fact]
        public void Builder_ReturnsBuilderForFluentApi()
        {
            var renderers = new System.Collections.ObjectModel.ReadOnlyCollection<object>(new System.Collections.Generic.List<object>());
            var component = new ScriptRendererComponent(_mockHtmlHelper.Object, renderers, false);
            var builder = new ScriptRendererBuilder(component, null);

            var result = builder.Id("test").Name("testName");

            Assert.IsType<ScriptRendererBuilder>(result);
            Assert.Equal("test", component.Id);
            Assert.Equal("testName", component.Name);
        }
    }
}
