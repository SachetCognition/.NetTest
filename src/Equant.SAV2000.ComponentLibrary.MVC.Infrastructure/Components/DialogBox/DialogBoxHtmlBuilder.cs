namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DialogBox
{
    using System.IO;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    /// <summary>
    /// Renders a modal dialog box with configurable buttons (OK/Cancel).
    /// Uses data-toggle/data-target attributes for triggering.
    /// </summary>
    public class DialogBoxHtmlBuilder : HtmlBuilderBase<DialogBoxComponent>
    {
        public DialogBoxHtmlBuilder(DialogBoxComponent component) : base(component) { }

        public override IHtmlContent Build()
        {
            var outerDiv = new TagBuilder("div");
            outerDiv.AddCssClass("modal");
            outerDiv.AddCssClass("sav-dialog");

            if (!string.IsNullOrEmpty(Component.Id))
            {
                outerDiv.Attributes["id"] = Component.Id;
            }

            outerDiv.Attributes["tabindex"] = "-1";
            outerDiv.Attributes["role"] = "dialog";
            outerDiv.Attributes["data-toggle"] = "modal";

            // Modal dialog wrapper
            var dialogDiv = new TagBuilder("div");
            dialogDiv.AddCssClass("modal-dialog");

            var contentDiv = new TagBuilder("div");
            contentDiv.AddCssClass("modal-content");

            // Header
            if (!string.IsNullOrEmpty(Component.Title))
            {
                var headerDiv = new TagBuilder("div");
                headerDiv.AddCssClass("modal-header");

                var titleTag = new TagBuilder("h4");
                titleTag.AddCssClass("modal-title");
                titleTag.InnerHtml.Append(Component.Title);

                headerDiv.InnerHtml.AppendHtml(titleTag);
                contentDiv.InnerHtml.AppendHtml(headerDiv);
            }

            // Body
            var bodyDiv = new TagBuilder("div");
            bodyDiv.AddCssClass("modal-body");
            if (!string.IsNullOrEmpty(Component.Content))
            {
                bodyDiv.InnerHtml.AppendHtml(Component.Content);
            }
            contentDiv.InnerHtml.AppendHtml(bodyDiv);

            // Footer with buttons
            var footerDiv = new TagBuilder("div");
            footerDiv.AddCssClass("modal-footer");

            if (Component.ShowOkButton)
            {
                var okBtn = new TagBuilder("button");
                okBtn.AddCssClass("btn");
                okBtn.AddCssClass("btn-primary");
                okBtn.Attributes["type"] = "button";
                okBtn.InnerHtml.Append(Component.OkButtonText ?? "OK");
                if (!string.IsNullOrEmpty(Component.OnOkClick))
                {
                    okBtn.Attributes["onclick"] = Component.OnOkClick;
                }
                footerDiv.InnerHtml.AppendHtml(okBtn);
            }

            if (Component.ShowCancelButton)
            {
                var cancelBtn = new TagBuilder("button");
                cancelBtn.AddCssClass("btn");
                cancelBtn.AddCssClass("btn-default");
                cancelBtn.Attributes["type"] = "button";
                cancelBtn.Attributes["data-dismiss"] = "modal";
                cancelBtn.InnerHtml.Append(Component.CancelButtonText ?? "Cancel");
                if (!string.IsNullOrEmpty(Component.OnCancelClick))
                {
                    cancelBtn.Attributes["onclick"] = Component.OnCancelClick;
                }
                footerDiv.InnerHtml.AppendHtml(cancelBtn);
            }

            // Add custom buttons
            foreach (var button in Component.Buttons)
            {
                var btn = new TagBuilder("button");
                btn.AddCssClass("btn");
                if (!string.IsNullOrEmpty(button.CssClass))
                {
                    btn.AddCssClass(button.CssClass);
                }
                btn.Attributes["type"] = "button";
                btn.InnerHtml.Append(button.Text ?? string.Empty);
                if (!string.IsNullOrEmpty(button.OnClick))
                {
                    btn.Attributes["onclick"] = button.OnClick;
                }
                footerDiv.InnerHtml.AppendHtml(btn);
            }

            contentDiv.InnerHtml.AppendHtml(footerDiv);
            dialogDiv.InnerHtml.AppendHtml(contentDiv);
            outerDiv.InnerHtml.AppendHtml(dialogDiv);

            // Add HTML attributes
            foreach (var attr in Component.HtmlAttributes)
            {
                outerDiv.Attributes[attr.Key] = attr.Value?.ToString();
            }

            var writer = new StringWriter();
            outerDiv.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}
