using Xunit;
using FluentAssertions;
using Moq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
using System.IO;

namespace Equant.SAV2000.ComponentLibrary.MVC.UnitTests
{
    public class ComponentBaseTests
    {
        private class TestComponent : ComponentBase
        {
            public TestComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }

            public override System.Collections.ObjectModel.ReadOnlyCollection<JsResource> JsResources =>
                new System.Collections.ObjectModel.ReadOnlyCollection<JsResource>(
                    new System.Collections.Generic.List<JsResource>());

            public override void WriteHtml(TextWriter writer)
            {
                writer.Write("<div>Test</div>");
            }

            public override void WriteInitScript(TextWriter writer)
            {
                writer.Write("console.log('test');");
            }
        }

        [Fact]
        public void Constructor_ShouldInitializeHtmlHelper()
        {
            var mockHtmlHelper = new Mock<IHtmlHelper>();
            var component = new TestComponent(mockHtmlHelper.Object);

            component.HtmlHelper.Should().NotBeNull();
            component.HtmlHelper.Should().Be(mockHtmlHelper.Object);
        }

        [Fact]
        public void Constructor_ShouldInitializeHtmlAttributes()
        {
            var mockHtmlHelper = new Mock<IHtmlHelper>();
            var component = new TestComponent(mockHtmlHelper.Object);

            component.HtmlAttributes.Should().NotBeNull();
            component.HtmlAttributes.Should().BeEmpty();
        }

        [Fact]
        public void Id_ShouldBeSettable()
        {
            var mockHtmlHelper = new Mock<IHtmlHelper>();
            var component = new TestComponent(mockHtmlHelper.Object);

            component.Id = "test-id";

            component.Id.Should().Be("test-id");
        }

        [Fact]
        public void IsVisible_ShouldDefaultToTrue()
        {
            var mockHtmlHelper = new Mock<IHtmlHelper>();
            var component = new TestComponent(mockHtmlHelper.Object);

            component.IsVisible.Should().BeTrue();
        }

        [Fact]
        public void WriteHtml_ShouldWriteToTextWriter()
        {
            var mockHtmlHelper = new Mock<IHtmlHelper>();
            var component = new TestComponent(mockHtmlHelper.Object);
            var writer = new StringWriter();

            component.WriteHtml(writer);

            writer.ToString().Should().Be("<div>Test</div>");
        }

        [Fact]
        public void WriteInitScript_ShouldWriteToTextWriter()
        {
            var mockHtmlHelper = new Mock<IHtmlHelper>();
            var component = new TestComponent(mockHtmlHelper.Object);
            var writer = new StringWriter();

            component.WriteInitScript(writer);

            writer.ToString().Should().Be("console.log('test');");
        }
    }
}
