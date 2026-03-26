namespace Equant.SAV2000.ComponentLibrary.Tests.Infrastructure
{
    using System;
    using System.IO;
    using Xunit;
    using FluentAssertions;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    /// <summary>
    /// US-INF-003: Style Resource Management tests
    /// </summary>
    public class StyleResourceManagementTests
    {
        /// <summary>
        /// TC-INF-003-U01: CssResource registration and rendering.
        /// Verifies that CSS resources can be registered, ordered by priority,
        /// and rendered as HTML link tags.
        /// </summary>
        [Fact]
        public void TC_INF_003_U01_CssResource_Registration_And_Rendering()
        {
            // Arrange
            var manager = new StyleResourceManager();

            var style1 = new CssResource("base-styles", "/css/base.css", 100, typeof(object));
            var style2 = new CssResource("component-styles", "/css/components.css", 200, typeof(object));
            var style3 = new CssResource("theme-styles", "/css/theme.css", 150, typeof(object));

            // Act - Register styles
            manager.Register(style1);
            manager.Register(style2);
            manager.Register(style3);

            // Assert - Count is correct
            manager.Count.Should().Be(3);

            // Assert - Ordered by priority
            var ordered = manager.GetOrderedStyles();
            ordered.Should().HaveCount(3);
            ordered[0].Name.Should().Be("base-styles");
            ordered[0].Priority.Should().Be(100);
            ordered[1].Name.Should().Be("theme-styles");
            ordered[1].Priority.Should().Be(150);
            ordered[2].Name.Should().Be("component-styles");
            ordered[2].Priority.Should().Be(200);

            // Assert - Renders as link tags
            var rendered = manager.RenderStyles();
            var html = rendered.ToString();
            // The IHtmlContent.ToString() may return the type name, so use StringWriter
            var writer = new StringWriter();
            rendered.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
            var renderedHtml = writer.ToString();

            renderedHtml.Should().Contain("/css/base.css");
            renderedHtml.Should().Contain("/css/theme.css");
            renderedHtml.Should().Contain("/css/components.css");
            renderedHtml.Should().Contain("<link");
            renderedHtml.Should().Contain("stylesheet");

            // Verify deduplication
            var duplicateStyle = new CssResource("base-styles", "/css/base-v2.css", 100, typeof(object));
            manager.Register(duplicateStyle);
            manager.Count.Should().Be(3); // Should not increase
        }
    }
}
