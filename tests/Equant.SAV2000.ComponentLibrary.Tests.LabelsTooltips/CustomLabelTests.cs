namespace Equant.SAV2000.ComponentLibrary.Tests.LabelsTooltips
{
    using System;
    using AngleSharp;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using FluentAssertions;
    using Xunit;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;

    /// <summary>
    /// Test cases for CustomLabel component (US-LT-001)
    /// </summary>
    public class CustomLabelTests
    {
        private readonly IHtmlParser _parser;

        public CustomLabelTests()
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
        /// TC-LT-001-U01: CustomLabel renders label element with for attribute
        /// </summary>
        [Fact]
        public void TC_LT_001_U01_CustomLabel_Renders_Label_Element_With_For_Attribute()
        {
            // Arrange
            var component = new CustomLabelComponent
            {
                Id = "lblTest",
                Text = "Test Label",
                AssociatedControlId = "txtInput"
            };

            // Act
            var html = component.BuildHtml().ToHtmlString();
            var doc = ParseHtml(html);

            // Assert
            var label = doc.QuerySelector("label");
            label.Should().NotBeNull("a label element should be rendered");
            label.GetAttribute("id").Should().Be("lblTest");
            label.GetAttribute("for").Should().Be("txtInput");
        }

        /// <summary>
        /// TC-LT-001-U02: CustomLabel mandatory star indicator when IsMandatory=true
        /// </summary>
        [Fact]
        public void TC_LT_001_U02_CustomLabel_Mandatory_Star_Indicator()
        {
            // Arrange
            var component = new CustomLabelComponent
            {
                Text = "Required Field",
                IsMandatory = true
            };

            // Act
            var html = component.BuildHtml().ToHtmlString();
            var doc = ParseHtml(html);

            // Assert
            var abbr = doc.QuerySelector("label abbr.required");
            abbr.Should().NotBeNull("mandatory star indicator should be rendered");
            abbr.TextContent.Should().Contain("*");
        }

        /// <summary>
        /// TC-LT-001-U03: CustomLabel colon rendering (DisplayColon default true)
        /// </summary>
        [Fact]
        public void TC_LT_001_U03_CustomLabel_Colon_Rendering_Default_True()
        {
            // Arrange - DisplayColon defaults to true
            var componentWithColon = new CustomLabelComponent
            {
                Text = "Label With Colon"
            };

            var componentWithoutColon = new CustomLabelComponent
            {
                Text = "Label Without Colon",
                DisplayColon = false
            };

            // Act
            var htmlWithColon = componentWithColon.BuildHtml().ToHtmlString();
            var htmlWithoutColon = componentWithoutColon.BuildHtml().ToHtmlString();

            // Assert
            htmlWithColon.Should().Contain(":", "colon should be rendered by default");
            componentWithColon.DisplayColon.Should().BeTrue("DisplayColon should default to true");
            htmlWithoutColon.Should().NotContain(" :", "colon should not be rendered when DisplayColon is false");
        }

        /// <summary>
        /// TC-LT-001-U04: CustomLabel superscript text rendering
        /// </summary>
        [Fact]
        public void TC_LT_001_U04_CustomLabel_Superscript_Text_Rendering()
        {
            // Arrange
            var component = new CustomLabelComponent
            {
                Text = "Label",
                SuperscriptText = "1",
                SuperscriptCssClass = "sup-class",
                SuperscriptToolTip = "Footnote 1"
            };

            // Act
            var html = component.BuildHtml().ToHtmlString();
            var doc = ParseHtml(html);

            // Assert
            var sup = doc.QuerySelector("label sup");
            sup.Should().NotBeNull("superscript element should be rendered");
            sup.TextContent.Should().Be("1");
            sup.ClassList.Should().Contain("sup-class");
            sup.GetAttribute("title").Should().Be("Footnote 1");
        }

        /// <summary>
        /// TC-LT-001-U05: CustomLabel accessibility text with hide-access class
        /// </summary>
        [Fact]
        public void TC_LT_001_U05_CustomLabel_Accessibility_Text_With_Hide_Access_Class()
        {
            // Arrange
            var component = new CustomLabelComponent
            {
                Text = "Visible Label",
                AccessText = "Screen reader only text"
            };

            // Act
            var html = component.BuildHtml().ToHtmlString();
            var doc = ParseHtml(html);

            // Assert
            var accessSpan = doc.QuerySelector("label span.hide-access");
            accessSpan.Should().NotBeNull("accessibility span with hide-access class should be rendered");
            accessSpan.TextContent.Should().Be("Screen reader only text");
        }

        /// <summary>
        /// TC-LT-001-U06: CustomLabel AssociatedControlId syncs with HtmlAttributes["for"]
        /// </summary>
        [Fact]
        public void TC_LT_001_U06_CustomLabel_AssociatedControlId_Syncs_With_HtmlAttributes_For()
        {
            // Arrange
            var component = new CustomLabelComponent();

            // Act - Set AssociatedControlId and verify it syncs with HtmlAttributes["for"]
            component.AssociatedControlId = "myControl";

            // Assert
            component.HtmlAttributes.Should().ContainKey("for");
            component.HtmlAttributes["for"].Should().Be("myControl");
            component.AssociatedControlId.Should().Be("myControl");

            // Also verify the builder works with AssociatedControlId
            var builder = new CustomLabelBuilder(new CustomLabelComponent());
            builder.AssociatedControlId("anotherControl");
            builder.Component.HtmlAttributes["for"].Should().Be("anotherControl");
            builder.Component.AssociatedControlId.Should().Be("anotherControl");
        }

        /// <summary>
        /// TC-LT-001-U07: CustomLabel IsHtmlEncode prevents XSS
        /// </summary>
        [Fact]
        public void TC_LT_001_U07_CustomLabel_IsHtmlEncode_Prevents_XSS()
        {
            // Arrange
            var xssText = "<script>alert('xss')</script>";

            var encodedComponent = new CustomLabelComponent
            {
                Text = xssText,
                IsHtmlEncode = true,
                DisplayColon = false
            };

            var unencodedComponent = new CustomLabelComponent
            {
                Text = xssText,
                IsHtmlEncode = false,
                DisplayColon = false
            };

            // Act
            var encodedHtml = encodedComponent.BuildHtml().ToHtmlString();
            var unencodedHtml = unencodedComponent.BuildHtml().ToHtmlString();

            // Assert - When IsHtmlEncode is true, the script tag should be encoded
            encodedHtml.Should().Contain("&lt;script&gt;", "HTML should be encoded to prevent XSS");
            encodedHtml.Should().NotContain("<script>alert", "raw script tags should not appear when encoded");

            // When IsHtmlEncode is false, the raw HTML is passed through
            unencodedHtml.Should().Contain("<script>alert", "raw HTML should be passed through when not encoded");
        }

        /// <summary>
        /// TC-LT-001-U08: CustomLabel IsOnlyForAccess applies hide-access class to label
        /// </summary>
        [Fact]
        public void TC_LT_001_U08_CustomLabel_IsOnlyForAccess_Applies_Hide_Access_Class_To_Label()
        {
            // Arrange
            var component = new CustomLabelComponent
            {
                Id = "lblAccess",
                Text = "Screen Reader Only",
                IsOnlyForAccess = true,
                DisplayColon = false
            };

            // Act
            var html = component.BuildHtml().ToHtmlString();
            var doc = ParseHtml(html);

            // Assert - The label itself should have the hide-access class
            var label = doc.QuerySelector("label.hide-access");
            label.Should().NotBeNull("label should have hide-access class when IsOnlyForAccess is true");
            label.GetAttribute("id").Should().Be("lblAccess");
            label.TextContent.Should().Contain("Screen Reader Only");

            // Verify that without IsOnlyForAccess, the class is not added
            var visibleComponent = new CustomLabelComponent
            {
                Id = "lblVisible",
                Text = "Visible Label",
                IsOnlyForAccess = false,
                DisplayColon = false
            };
            var visibleHtml = visibleComponent.BuildHtml().ToHtmlString();
            visibleHtml.Should().NotContain("hide-access");
        }

        /// <summary>
        /// TC-LT-001-R01: CustomLabel HTML output regression
        /// </summary>
        [Fact]
        public void TC_LT_001_R01_CustomLabel_HTML_Output_Regression()
        {
            // Arrange - Full-featured custom label
            var component = new CustomLabelComponent
            {
                Id = "lblFullTest",
                Text = "Full Label",
                IsMandatory = true,
                DisplayColon = true,
                SuperscriptText = "2",
                SuperscriptCssClass = "footnote",
                SuperscriptToolTip = "See note 2",
                AccessText = "Additional info for screen readers",
                CssClassLabel = "custom-label-class",
                AssociatedControlId = "inputField"
            };

            // Act
            var html = component.BuildHtml().ToHtmlString();
            var doc = ParseHtml(html);

            // Assert - Verify complete structure
            var label = doc.QuerySelector("label");
            label.Should().NotBeNull();
            label.GetAttribute("id").Should().Be("lblFullTest");
            label.GetAttribute("for").Should().Be("inputField");
            label.ClassList.Should().Contain("custom-label-class");

            // Mandatory star
            var abbr = doc.QuerySelector("label abbr.required");
            abbr.Should().NotBeNull();
            abbr.TextContent.Should().Contain("*");

            // Text content
            label.TextContent.Should().Contain("Full Label");

            // Colon
            html.Should().Contain(":");

            // Superscript
            var sup = doc.QuerySelector("label sup.footnote");
            sup.Should().NotBeNull();
            sup.TextContent.Should().Be("2");
            sup.GetAttribute("title").Should().Be("See note 2");

            // Accessibility text
            var accessSpan = doc.QuerySelector("label span.hide-access");
            accessSpan.Should().NotBeNull();
            accessSpan.TextContent.Should().Be("Additional info for screen readers");
        }
    }
}
