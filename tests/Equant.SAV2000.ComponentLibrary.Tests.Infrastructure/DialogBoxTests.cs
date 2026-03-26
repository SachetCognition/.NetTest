namespace Equant.SAV2000.ComponentLibrary.Tests.Infrastructure
{
    using System.IO;
    using Xunit;
    using FluentAssertions;
    using AngleSharp;
    using AngleSharp.Html.Parser;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.DialogBox;

    /// <summary>
    /// US-INF-004: DialogBox tests
    /// </summary>
    public class DialogBoxTests
    {
        /// <summary>
        /// TC-INF-004-U01: DialogBox renders modal with OK/Cancel buttons.
        /// Verifies that DialogBoxComponent renders a proper modal dialog structure
        /// with modal classes, header with title, body with content, and footer
        /// with OK and Cancel buttons that have correct attributes.
        /// </summary>
        [Fact]
        public void TC_INF_004_U01_DialogBox_Renders_Modal_With_OkCancel_Buttons()
        {
            // Arrange
            var component = new DialogBoxComponent
            {
                Id = "testDialog",
                Title = "Confirm Action",
                Content = "<p>Are you sure?</p>",
                ShowOkButton = true,
                ShowCancelButton = true,
                OkButtonText = "Yes",
                CancelButtonText = "No",
                OnOkClick = "confirmAction()",
                OnCancelClick = "cancelAction()"
            };

            // Act
            var htmlContent = component.BuildHtml();
            var writer = new StringWriter();
            htmlContent.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
            var html = writer.ToString();

            // Assert - Parse with AngleSharp
            var context = BrowsingContext.New(Configuration.Default);
            var parser = context.GetService<IHtmlParser>();
            var document = parser.ParseDocument("<html><body>" + html + "</body></html>");

            // Verify outer modal div
            var modalDiv = document.QuerySelector("div.modal.sav-dialog");
            modalDiv.Should().NotBeNull("modal div with classes 'modal sav-dialog' should exist");
            modalDiv.GetAttribute("id").Should().Be("testDialog");
            modalDiv.GetAttribute("data-toggle").Should().Be("modal");
            modalDiv.GetAttribute("role").Should().Be("dialog");
            modalDiv.GetAttribute("tabindex").Should().Be("-1");

            // Verify modal-dialog and modal-content structure
            var dialogDiv = modalDiv.QuerySelector("div.modal-dialog");
            dialogDiv.Should().NotBeNull("modal-dialog div should exist");
            var contentDiv = dialogDiv.QuerySelector("div.modal-content");
            contentDiv.Should().NotBeNull("modal-content div should exist");

            // Verify header with title
            var headerDiv = contentDiv.QuerySelector("div.modal-header");
            headerDiv.Should().NotBeNull("modal-header should exist");
            var titleElement = headerDiv.QuerySelector("h4.modal-title");
            titleElement.Should().NotBeNull("modal-title h4 should exist");
            titleElement.TextContent.Should().Be("Confirm Action");

            // Verify body with content
            var bodyDiv = contentDiv.QuerySelector("div.modal-body");
            bodyDiv.Should().NotBeNull("modal-body should exist");
            bodyDiv.InnerHtml.Should().Contain("Are you sure?");

            // Verify footer with buttons
            var footerDiv = contentDiv.QuerySelector("div.modal-footer");
            footerDiv.Should().NotBeNull("modal-footer should exist");

            var buttons = footerDiv.QuerySelectorAll("button");
            buttons.Length.Should().BeGreaterThanOrEqualTo(2, "should have at least OK and Cancel buttons");

            // Verify OK button
            var okButton = footerDiv.QuerySelector("button.btn-primary");
            okButton.Should().NotBeNull("OK button with btn-primary class should exist");
            okButton.TextContent.Should().Be("Yes");
            okButton.GetAttribute("onclick").Should().Be("confirmAction()");
            okButton.GetAttribute("type").Should().Be("button");

            // Verify Cancel button
            var cancelButton = footerDiv.QuerySelector("button.btn-default");
            cancelButton.Should().NotBeNull("Cancel button with btn-default class should exist");
            cancelButton.TextContent.Should().Be("No");
            cancelButton.GetAttribute("data-dismiss").Should().Be("modal");
            cancelButton.GetAttribute("onclick").Should().Be("cancelAction()");
        }
    }
}
