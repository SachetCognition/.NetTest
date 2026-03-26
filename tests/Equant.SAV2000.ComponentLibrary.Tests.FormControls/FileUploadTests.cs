namespace Equant.SAV2000.ComponentLibrary.Tests.FormControls
{
    using FluentAssertions;
    using Xunit;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.FileUpload;

    /// <summary>
    /// US-FC-013: FileUpload Component Tests
    /// </summary>
    public class FileUploadTests
    {
        /// <summary>TC-FC-013-U01: FileUploadComponent renders input[type=file]</summary>
        [Fact]
        public void TC_FC_013_U01_FileUploadComponent_RendersFileInput()
        {
            var component = new FileUploadComponent
            {
                Id = "upload1",
                Name = "document",
                Accept = ".pdf,.doc",
                CssClass = "file-input"
            };

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var input = doc.QuerySelector("input[type='file']");

            input.Should().NotBeNull("should render an input element with type=file");
            input.GetAttribute("id").Should().Be("upload1");
            input.GetAttribute("name").Should().Be("document");
            input.GetAttribute("accept").Should().Be(".pdf,.doc");
        }

        /// <summary>TC-FC-013-U02: FileUpload multipart form integration</summary>
        [Fact]
        public void TC_FC_013_U02_FileUpload_MultipartFormIntegration()
        {
            var component = new FileUploadComponent
            {
                Id = "upload1",
                Name = "files",
                AllowMultiple = true,
                Accept = "image/*"
            };

            var htmlString = TestHelper.GetHtmlString(component.WriteHtml());

            htmlString.Should().Contain("type=\"file\"");
            htmlString.Should().Contain("name=\"files\"");
            htmlString.Should().Contain("multiple=\"multiple\"");
            htmlString.Should().Contain("accept=\"image/*\"");
        }
    }
}
