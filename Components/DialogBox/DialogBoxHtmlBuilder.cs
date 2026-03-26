namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DialogBox
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;

    public class DialogBoxHtmlBuilder : HtmlBuilderBase<DialogBoxComponent>
    {
        public DialogBoxHtmlBuilder(DialogBoxComponent component) : base(component) { }
        public override void Build(TextWriter writer) { }
    }
}
