namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ClickToVoice
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ClickToVoiceBuilder : ComponentBuilderBase<ClickToVoiceComponent, ClickToVoiceBuilder>
    {
        public ClickToVoiceBuilder(ClickToVoiceComponent component) : base(component) { }
        public ClickToVoiceBuilder(ClickToVoiceComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }

        public ClickToVoiceBuilder CssAnchorTag(string value)
        {
            Component.CssAnchorTag = value;
            return this;
        }

        public ClickToVoiceBuilder CssClassImage(string css)
        {
            Component.CssClassImage = css;
            return this;
        }

        public ClickToVoiceBuilder CssTelephoneNumberSpan(string css)
        {
            Component.CssTelephoneNumberSpan = css;
            return this;
        }

        public ClickToVoiceBuilder IsNumberSpanVisible(bool value)
        {
            Component.IsNumberSpanVisible = value;
            return this;
        }

        public ClickToVoiceBuilder ActionUrl(string value)
        {
            Component.ActionUrl = value;
            return this;
        }

        public ClickToVoiceBuilder Title(string value)
        {
            Component.Title = value;
            return this;
        }

        public ClickToVoiceBuilder ImageUrl(string value)
        {
            Component.ImageUrl = value;
            return this;
        }

        public ClickToVoiceBuilder AlternateText(string value)
        {
            Component.AlternateText = value;
            return this;
        }

        public ClickToVoiceBuilder TelephoneNumber(string value)
        {
            Component.TelephoneNumber = value;
            return this;
        }

        public ClickToVoiceBuilder TelephoneControlId(string value)
        {
            Component.TelephoneControlId = value;
            return this;
        }

        public ClickToVoiceBuilder RootService(string value)
        {
            Component.RootService = value;
            return this;
        }
    }
}
