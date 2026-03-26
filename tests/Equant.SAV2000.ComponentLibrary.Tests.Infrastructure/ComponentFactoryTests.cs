namespace Equant.SAV2000.ComponentLibrary.Tests.Infrastructure
{
    using System;
    using Xunit;
    using FluentAssertions;
    using Moq;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DialogBox;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomHeader;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ScriptRenderer;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.StyleRender;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;

    /// <summary>
    /// US-INF-001: ComponentFactory tests
    /// </summary>
    public class ComponentFactoryTests
    {
        private readonly Mock<IHtmlHelper<object>> _htmlHelperMock;

        public ComponentFactoryTests()
        {
            _htmlHelperMock = new Mock<IHtmlHelper<object>>();
        }

        /// <summary>
        /// TC-INF-001-U01: ComponentFactory returns typed builder for each component.
        /// Verifies that the factory returns the correct builder type for DialogBox, CustomHeader,
        /// ScriptRenderer, and StyleRenderer.
        /// </summary>
        [Fact]
        public void TC_INF_001_U01_ComponentFactory_Returns_Typed_Builder_For_Each_Component()
        {
            // Arrange
            var factory = new ComponentFactory<object>(_htmlHelperMock.Object);

            // Act & Assert - DialogBox returns DialogBoxBuilder
            var dialogBoxBuilder = factory.DialogBox();
            dialogBoxBuilder.Should().NotBeNull();
            dialogBoxBuilder.Should().BeOfType<DialogBoxBuilder>();
            dialogBoxBuilder.Component.Should().NotBeNull();
            dialogBoxBuilder.Component.Should().BeOfType<DialogBoxComponent>();

            // Act & Assert - CustomHeader returns CustomHeaderBuilder
            var customHeaderBuilder = factory.CustomHeader();
            customHeaderBuilder.Should().NotBeNull();
            customHeaderBuilder.Should().BeOfType<CustomHeaderBuilder>();
            customHeaderBuilder.Component.Should().NotBeNull();
            customHeaderBuilder.Component.Should().BeOfType<CustomHeaderComponent>();

            // Act & Assert - ScriptRenderer returns ScriptRendererBuilder
            var scriptRendererBuilder = factory.ScriptRenderer();
            scriptRendererBuilder.Should().NotBeNull();
            scriptRendererBuilder.Should().BeOfType<ScriptRendererBuilder>();

            // Act & Assert - StyleRenderer returns StyleRendererBuilder
            var styleRendererBuilder = factory.StyleRenderer();
            styleRendererBuilder.Should().NotBeNull();
            styleRendererBuilder.Should().BeOfType<StyleRendererBuilder>();
        }

        /// <summary>
        /// TC-INF-001-U02: ComponentFactory light requirement mode.
        /// Verifies that IsLightRequirement is correctly set when passed to the factory.
        /// </summary>
        [Fact]
        public void TC_INF_001_U02_ComponentFactory_Light_Requirement_Mode()
        {
            // Arrange & Act - Default mode (not light)
            var factoryDefault = new ComponentFactory<object>(_htmlHelperMock.Object);
            factoryDefault.IsLightRequirement.Should().BeFalse();

            // Arrange & Act - Light requirement mode
            var factoryLight = new ComponentFactory<object>(_htmlHelperMock.Object, true);
            factoryLight.IsLightRequirement.Should().BeTrue();

            // Arrange & Act - Explicit non-light mode
            var factoryNonLight = new ComponentFactory<object>(_htmlHelperMock.Object, false);
            factoryNonLight.IsLightRequirement.Should().BeFalse();
        }

        /// <summary>
        /// TC-INF-001-I01: Html.Sav2000() extension method integration.
        /// Verifies that the Sav2000 extension method returns a properly configured ComponentFactory
        /// with script and style renderers wired up.
        /// </summary>
        [Fact]
        public void TC_INF_001_I01_Html_Sav2000_Extension_Method_Integration()
        {
            // Arrange
            var htmlHelperMock = new Mock<IHtmlHelper<object>>();

            // Act - Default call
            var factory = htmlHelperMock.Object.Sav2000();
            factory.Should().NotBeNull();
            factory.Should().BeOfType<ComponentFactory<object>>();
            factory.HtmlHelper.Should().BeSameAs(htmlHelperMock.Object);
            factory.IsLightRequirement.Should().BeFalse();
            factory.ScriptRendererBuilder.Should().NotBeNull();
            factory.StyleRendererBuilder.Should().NotBeNull();

            // Act - Light requirement mode
            var factoryLight = htmlHelperMock.Object.Sav2000(true);
            factoryLight.Should().NotBeNull();
            factoryLight.IsLightRequirement.Should().BeTrue();
            factoryLight.ScriptRendererBuilder.Should().NotBeNull();
            factoryLight.StyleRendererBuilder.Should().NotBeNull();

            // Verify fluent API chain works
            var dialogBuilder = factory.DialogBox().Title("Test").Id("dlg1");
            dialogBuilder.Should().NotBeNull();
            dialogBuilder.Component.Title.Should().Be("Test");
            dialogBuilder.Component.Id.Should().Be("dlg1");
        }
    }
}
