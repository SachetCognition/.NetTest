namespace Equant.SAV2000.ComponentLibrary.Tests.FormControls
{
    using FluentAssertions;
    using Xunit;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.TextArea;

    /// <summary>
    /// US-FC-008: TextArea Component Tests
    /// </summary>
    public class TextAreaTests
    {
        /// <summary>TC-FC-008-U01: TextAreaComponent renders textarea element</summary>
        [Fact]
        public void TC_FC_008_U01_TextAreaComponent_RendersTextarea()
        {
            var component = new TextAreaComponent
            {
                Id = "ta1",
                Name = "comments",
                Rows = 5,
                Cols = 40,
                CssClass = "form-control"
            };

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var textarea = doc.QuerySelector("textarea");

            textarea.Should().NotBeNull("should render a textarea element");
            textarea.GetAttribute("id").Should().Be("ta1");
            textarea.GetAttribute("name").Should().Be("comments");
            textarea.GetAttribute("rows").Should().Be("5");
            textarea.GetAttribute("cols").Should().Be("40");
        }

        /// <summary>TC-FC-008-U02: TextArea multiline content model binding</summary>
        [Fact]
        public void TC_FC_008_U02_TextArea_MultilineContentModelBinding()
        {
            var multilineText = "Line 1\nLine 2\nLine 3";
            var component = new TextAreaComponent
            {
                Id = "ta1",
                Name = "description",
                Value = multilineText
            };

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var textarea = doc.QuerySelector("textarea");

            textarea.Should().NotBeNull();
            textarea.TextContent.Should().Contain("Line 1");
            textarea.TextContent.Should().Contain("Line 2");
            textarea.TextContent.Should().Contain("Line 3");
            textarea.GetAttribute("name").Should().Be("description");
        }
    }
}
