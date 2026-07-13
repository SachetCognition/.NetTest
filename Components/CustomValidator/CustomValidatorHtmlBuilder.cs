namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomValidator
{
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class CustomValidatorHtmlBuilder : HtmlBuilderBase<CustomValidatorComponent>
    {
        public CustomValidatorHtmlBuilder(CustomValidatorComponent component)
        {
            this.Component = component;
        }

        public override void Build(HtmlTextWriter writer)
        {
        }
    }
}
