namespace Equant.SAV2000.ComponentLibrary.Tests.FormControls
{
    using FluentAssertions;
    using Xunit;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CommentBox;

    /// <summary>
    /// US-FC-012: CommentBox Component Tests
    /// </summary>
    public class CommentBoxTests
    {
        /// <summary>TC-FC-012-U01: CommentBoxComponent renders textarea</summary>
        [Fact]
        public void TC_FC_012_U01_CommentBoxComponent_RendersTextarea()
        {
            var component = new CommentBoxComponent
            {
                Id = "comment1",
                Name = "comments",
                Value = "Some comment",
                Rows = 5,
                Cols = 40,
                MaxLength = 500,
                ShowCharCount = true
            };

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var textarea = doc.QuerySelector("textarea");

            textarea.Should().NotBeNull("should render a textarea element");
            textarea.GetAttribute("id").Should().Be("comment1");
            textarea.GetAttribute("name").Should().Be("comments");
            textarea.GetAttribute("rows").Should().Be("5");
            textarea.GetAttribute("cols").Should().Be("40");
            textarea.GetAttribute("maxlength").Should().Be("500");
            textarea.TextContent.Should().Contain("Some comment");

            // Verify character count display
            var charCount = doc.QuerySelector("span.char-count");
            charCount.Should().NotBeNull("should show character count");
            charCount.TextContent.Should().Contain("characters remaining");
        }
    }
}
