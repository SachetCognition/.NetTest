namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DialogBox
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Html;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class DialogBoxComponent : ComponentBase
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool ShowOkButton { get; set; } = true;
        public bool ShowCancelButton { get; set; } = true;
        public string OkButtonText { get; set; } = "OK";
        public string CancelButtonText { get; set; } = "Cancel";
        public string OnOkClick { get; set; }
        public string OnCancelClick { get; set; }
        public List<DialogButton> Buttons { get; set; } = new List<DialogButton>();

        public override IHtmlContent BuildHtml()
        {
            var builder = new DialogBoxHtmlBuilder(this);
            return builder.Build();
        }

        public override IHtmlContent BuildInitScript()
        {
            return HtmlString.Empty;
        }
    }
}
