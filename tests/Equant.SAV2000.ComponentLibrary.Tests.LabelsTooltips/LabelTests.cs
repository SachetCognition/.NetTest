namespace Equant.SAV2000.ComponentLibrary.Tests.LabelsTooltips
{
    using AngleSharp;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using FluentAssertions;
    using Xunit;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Label;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.SpanLabel;

    /// <summary>
    /// Test cases for Label/SpanLabel components (US-LT-002)
    /// </summary>
    public class LabelTests
    {
        private readonly IHtmlParser _parser;

        public LabelTests()
        {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            _parser = context.GetService<IHtmlParser>();
        }

        private IDocument ParseHtml(string html)
        {
            return _parser.ParseDocument($"<html><body>{html}</body></html>");
        }

        /// <summary>
        /// TC-LT-002-U01: LabelComponent renders label element
        /// </summary>
        [Fact]
        public void TC_LT_002_U01_LabelComponent_Renders_Label_Element()
        {
            // Arrange
            var component = new LabelComponent
            {
                Id = "lblSimple",
                Text = "Simple Label",
                CssClass = "label-style"
            };

            // Act
            var html = component.BuildHtml().ToHtmlString();
            var doc = ParseHtml(html);

            // Assert
            var label = doc.QuerySelector("label");
            label.Should().NotBeNull("a label element should be rendered");
            label.GetAttribute("id").Should().Be("lblSimple");
            label.TextContent.Should().Be("Simple Label");
            label.ClassList.Should().Contain("label-style");
        }

        /// <summary>
        /// TC-LT-002-U02: SpanLabel renders span element
        /// </summary>
        [Fact]
        public void TC_LT_002_U02_SpanLabel_Renders_Span_Element()
        {
            // Arrange
            var component = new SpanLabelComponent
            {
                Id = "spnLabel",
                Text = "Span Label Text",
                CssClass = "span-style"
            };

            // Act
            var html = component.BuildHtml().ToHtmlString();
            var doc = ParseHtml(html);

            // Assert
            var span = doc.QuerySelector("span");
            span.Should().NotBeNull("a span element should be rendered");
            span.GetAttribute("id").Should().Be("spnLabel");
            span.TextContent.Should().Be("Span Label Text");
            span.ClassList.Should().Contain("span-style");
        }

        /// <summary>
        /// TC-LT-002-U03: Label builder fluent API
        /// </summary>
        [Fact]
        public void TC_LT_002_U03_Label_Builder_Fluent_API()
        {
            // Arrange & Act - Test fluent API chaining
            var component = new LabelComponent();
            var builder = new LabelBuilder(component);

            var result = builder
                .Id("lblFluent")
                .Name("labelName")
                .Text("Fluent Label")
                .CssClassLabel("fluent-css");

            // Assert - Builder returns itself for chaining
            result.Should().BeSameAs(builder, "fluent API should return the same builder instance");
            component.Id.Should().Be("lblFluent");
            component.Name.Should().Be("labelName");
            component.Text.Should().Be("Fluent Label");
            component.CssClass.Should().Be("fluent-css");

            // Verify the rendered output
            var html = component.BuildHtml().ToHtmlString();
            var doc = ParseHtml(html);
            var label = doc.QuerySelector("label");
            label.Should().NotBeNull();
            label.GetAttribute("id").Should().Be("lblFluent");
        }
    }
}
