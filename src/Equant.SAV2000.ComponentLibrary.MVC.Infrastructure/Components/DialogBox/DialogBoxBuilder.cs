namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DialogBox
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class DialogBoxBuilder : ComponentBuilderBase<DialogBoxComponent, DialogBoxBuilder>
    {
        public DialogBoxBuilder(DialogBoxComponent component) : base(component) { }
        public DialogBoxBuilder(DialogBoxComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public DialogBoxBuilder Title(string title)
        {
            this.Component.Title = title;
            return this;
        }

        public DialogBoxBuilder Content(string content)
        {
            this.Component.Content = content;
            return this;
        }

        public DialogBoxBuilder ShowOkButton(bool show)
        {
            this.Component.ShowOkButton = show;
            return this;
        }

        public DialogBoxBuilder ShowCancelButton(bool show)
        {
            this.Component.ShowCancelButton = show;
            return this;
        }

        public DialogBoxBuilder OkButtonText(string text)
        {
            this.Component.OkButtonText = text;
            return this;
        }

        public DialogBoxBuilder CancelButtonText(string text)
        {
            this.Component.CancelButtonText = text;
            return this;
        }

        public DialogBoxBuilder OnOkClick(string handler)
        {
            this.Component.OnOkClick = handler;
            return this;
        }

        public DialogBoxBuilder OnCancelClick(string handler)
        {
            this.Component.OnCancelClick = handler;
            return this;
        }

        public DialogBoxBuilder AddButton(DialogButton button)
        {
            this.Component.Buttons.Add(button);
            return this;
        }
    }
}
