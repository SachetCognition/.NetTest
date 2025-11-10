using Xunit;
using FluentAssertions;
using Moq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Button;
using Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBox;
using Equant.SAV2000.ComponentLibrary.MVC.Components.ActionButton;
using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;
using Equant.SAV2000.ComponentLibrary.MVC.Components.ClickToVoice;
using System.IO;

namespace Equant.SAV2000.ComponentLibrary.MVC.IntegrationTests
{
    public class ComponentRenderingTests
    {
        private readonly Mock<IHtmlHelper> _mockHtmlHelper;

        public ComponentRenderingTests()
        {
            _mockHtmlHelper = new Mock<IHtmlHelper>();
        }

        [Fact]
        public void ButtonComponent_ShouldRenderCompleteHtmlWithAllAttributes()
        {
            var component = new ButtonComponent(_mockHtmlHelper.Object)
            {
                Id = "btn-submit",
                Value = "Submit Form",
                OnClick = "submitForm()",
                IsDisabled = false
            };
            component.HtmlAttributes["data-test"] = "submit-button";

            var writer = new StringWriter();
            component.WriteHtml(writer);

            var html = writer.ToString();
            html.Should().Contain("id=\"btn-submit\"");
            html.Should().Contain("value=\"Submit Form\"");
            html.Should().Contain("type=\"submit\"");
            html.Should().Contain("data-test=\"submit-button\"");
        }

        [Fact]
        public void CheckBoxComponent_ShouldRenderWithHiddenFieldForFormBinding()
        {
            var component = new CheckBoxComponent(_mockHtmlHelper.Object)
            {
                Id = "chk-terms",
                IsChecked = true
            };

            var writer = new StringWriter();
            component.WriteHtml(writer);

            var html = writer.ToString();
            html.Should().Contain("type=\"hidden\"");
            html.Should().Contain("type=\"checkbox\"");
            html.Should().Contain("checked");
        }

        [Fact]
        public void ActionButtonComponent_ShouldRenderWithModalDialogAttributes()
        {
            var component = new ActionButtonComponent(_mockHtmlHelper.Object)
            {
                Id = "btn-delete",
                Value = "Delete",
                DialogBoxId = "confirm-delete-dialog"
            };

            var writer = new StringWriter();
            component.WriteHtml(writer);

            var html = writer.ToString();
            html.Should().Contain("btn-delete");
            html.Should().Contain("data-toggle");
            html.Should().Contain("confirm-delete-dialog");
        }

        [Fact]
        public void CustomLabelComponent_ShouldRenderWithAllFormattingElements()
        {
            var component = new CustomLabelComponent(_mockHtmlHelper.Object)
            {
                Id = "lbl-username",
                Text = "Username",
                DisplayStar = true,
                DisplayColon = true,
                SuperscriptText = "Required",
                AssociatedControlId = "txt-username"
            };

            var writer = new StringWriter();
            component.WriteHtml(writer);

            var html = writer.ToString();
            html.Should().Contain("<label");
            html.Should().Contain("Username");
            html.Should().Contain("*");
            html.Should().Contain(":");
            html.Should().Contain("Required");
            html.Should().Contain("for=\"txt-username\"");
        }

        [Fact]
        public void ClickToVoiceComponent_ShouldRenderAnchorWithImageAndPhoneNumber()
        {
            var component = new ClickToVoiceComponent(_mockHtmlHelper.Object)
            {
                Id = "ctv-support",
                TelephoneNumber = "+1-800-555-0123",
                IsNumberSpanVisible = true,
                Title = "Call support"
            };

            var writer = new StringWriter();
            component.WriteHtml(writer);

            var html = writer.ToString();
            html.Should().Contain("<a");
            html.Should().Contain("ctv-support");
            html.Should().Contain("+1-800-555-0123");
            html.Should().Contain("<img");
        }

        [Fact]
        public void MultipleComponents_ShouldRenderIndependently()
        {
            var button = new ButtonComponent(_mockHtmlHelper.Object) { Id = "btn1", Value = "Button1" };
            var checkbox = new CheckBoxComponent(_mockHtmlHelper.Object) { Id = "chk1", IsChecked = true };
            var label = new CustomLabelComponent(_mockHtmlHelper.Object) { Text = "Label1", AssociatedControlId = "chk1" };

            var writer = new StringWriter();
            label.WriteHtml(writer);
            checkbox.WriteHtml(writer);
            button.WriteHtml(writer);

            var html = writer.ToString();
            html.Should().Contain("for=\"chk1\"");
            html.Should().Contain("id=\"chk1\"");
            html.Should().Contain("id=\"btn1\"");
            html.Should().Contain("Label1");
            html.Should().Contain("value=\"Button1\"");
        }

        [Fact]
        public void ComponentWithJavaScript_ShouldGenerateInitializationScript()
        {
            var button = new ButtonComponent(_mockHtmlHelper.Object)
            {
                Id = "btn-ajax",
                OnClick = "handleAjaxSubmit()"
            };

            var scriptWriter = new StringWriter();
            button.WriteInitScript(scriptWriter);

            var script = scriptWriter.ToString();
            script.Should().Contain("btn-ajax");
            script.Should().Contain("handleAjaxSubmit");
        }

        [Fact]
        public void DisabledComponents_ShouldRenderWithDisabledAttribute()
        {
            var button = new ButtonComponent(_mockHtmlHelper.Object)
            {
                Id = "btn-disabled",
                IsDisabled = true
            };
            var checkbox = new CheckBoxComponent(_mockHtmlHelper.Object)
            {
                Id = "chk-disabled",
                IsDisabled = true
            };

            var buttonWriter = new StringWriter();
            var checkboxWriter = new StringWriter();
            
            button.WriteHtml(buttonWriter);
            checkbox.WriteHtml(checkboxWriter);

            buttonWriter.ToString().Should().Contain("disabled");
            checkboxWriter.ToString().Should().Contain("disabled");
        }
    }
}
