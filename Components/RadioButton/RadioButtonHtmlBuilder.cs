namespace Equant.SAV2000.ComponentLibrary.MVC.Components.RadioButton
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;

    public class RadioButtonHtmlBuilder : HtmlBuilderBase<RadioButtonComponent>
    {
        public RadioButtonHtmlBuilder(RadioButtonComponent component) : base(component) { }
        public override void Build(TextWriter writer) { }
    }
}
