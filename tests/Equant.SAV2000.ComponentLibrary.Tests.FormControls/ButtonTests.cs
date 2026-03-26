namespace Equant.SAV2000.ComponentLibrary.Tests.FormControls
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using Xunit;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Button;

    /// <summary>
    /// US-FC-001: Button Component Tests
    /// </summary>
    public class ButtonTests
    {
        /// <summary>TC-FC-001-U01: ButtonComponent default property values</summary>
        [Fact]
        public void TC_FC_001_U01_ButtonComponent_DefaultPropertyValues()
        {
            var component = new ButtonComponent();

            component.Id.Should().BeNull();
            component.Name.Should().BeNull();
            component.IsVisible.Should().BeTrue();
            component.IsDisabled.Should().BeFalse();
            component.OnClick.Should().Be("null");
            component.DialogDivId.Should().BeNull();
            component.CssClass.Should().BeNull();
        }

        /// <summary>TC-FC-001-U02: ButtonBuilder fluent API chaining</summary>
        [Fact]
        public void TC_FC_001_U02_ButtonBuilder_FluentApiChaining()
        {
            var component = new ButtonComponent();
            var builder = new ButtonBuilder(component, null);

            var result = builder
                .Id("btn1")
                .Name("submitBtn")
                .Value("Submit")
                .CssClass("btn-primary")
                .OnClick("handleClick")
                .Disabled(false);

            result.Should().BeSameAs(builder, "fluent API should return the same builder instance");
            component.Id.Should().Be("btn1");
            component.Name.Should().Be("submitBtn");
            component.Value.Should().Be("Submit");
            component.CssClass.Should().Be("btn-primary");
            component.OnClick.Should().Be("handleClick");
        }

        /// <summary>TC-FC-001-U03: ButtonHtmlBuilder renders correct HTML structure</summary>
        [Fact]
        public void TC_FC_001_U03_ButtonHtmlBuilder_RendersCorrectHtmlStructure()
        {
            var component = new ButtonComponent
            {
                Id = "btn1",
                Name = "submitBtn",
                CssClass = "btn-primary"
            };
            component.Value = "Submit";

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var input = doc.QuerySelector("input");

            input.Should().NotBeNull("should render an input element");
            input.GetAttribute("type").Should().Be("submit");
            input.GetAttribute("id").Should().Be("btn1");
            input.GetAttribute("name").Should().Be("submitBtn");
            input.GetAttribute("value").Should().Be("Submit");
            input.ClassList.Should().Contain("btn-primary");
        }

        /// <summary>TC-FC-001-U04: ButtonHtmlBuilder disabled state rendering</summary>
        [Fact]
        public void TC_FC_001_U04_ButtonHtmlBuilder_DisabledStateRendering()
        {
            var component = new ButtonComponent
            {
                Id = "btn1",
                Name = "submitBtn",
                CssClass = "btn-primary",
                CssClassReadOnly = "btn-disabled",
                IsDisabled = true
            };
            component.Value = "Submit";

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var input = doc.QuerySelector("input");

            input.Should().NotBeNull();
            input.HasAttribute("disabled").Should().BeTrue("disabled button should have disabled attribute");
            input.ClassList.Should().Contain("btn-disabled");
        }

        /// <summary>TC-FC-001-U05: ButtonHtmlBuilder dialog trigger attributes</summary>
        [Fact]
        public void TC_FC_001_U05_ButtonHtmlBuilder_DialogTriggerAttributes()
        {
            var component = new ButtonComponent
            {
                Id = "btn1",
                Name = "submitBtn",
                DialogDivId = "myDialog"
            };
            component.Value = "Open Dialog";

            var html = component.WriteHtml();
            var doc = TestHelper.ParseHtml(html);
            var input = doc.QuerySelector("input");

            input.Should().NotBeNull();
            input.GetAttribute("data-toggle").Should().Be("modal");
            input.GetAttribute("data-target").Should().Be("#myDialog");
        }

        /// <summary>TC-FC-001-I01: Button jQuery plugin initialization script</summary>
        [Fact]
        public void TC_FC_001_I01_Button_JQueryPluginInitScript()
        {
            var component = new ButtonComponent
            {
                Id = "btn1",
                OnClick = "handleClick"
            };

            var script = component.WriteInitScript();

            script.Should().Contain("$('#btn1')");
            script.Should().Contain(".savbutton(");
            script.Should().Contain("handleClick");
        }

        /// <summary>TC-FC-001-R01: Button HTML output regression vs .NET FW 4.8</summary>
        [Fact]
        public void TC_FC_001_R01_Button_HtmlOutputRegression()
        {
            var component = new ButtonComponent
            {
                Id = "btnSubmit",
                Name = "btnSubmit",
                CssClass = "btn btn-default"
            };
            component.Value = "Save";

            var htmlString = TestHelper.GetHtmlString(component.WriteHtml());

            htmlString.Should().Contain("id=\"btnSubmit\"");
            htmlString.Should().Contain("name=\"btnSubmit\"");
            htmlString.Should().Contain("type=\"submit\"");
            htmlString.Should().Contain("value=\"Save\"");
            htmlString.Should().Contain("btn btn-default");
        }
    }
}
