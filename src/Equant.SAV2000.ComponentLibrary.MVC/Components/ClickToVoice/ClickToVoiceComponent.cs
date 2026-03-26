namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ClickToVoice
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net;
    using System.Text.Json;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.Common;
    using Equant.SAV2000.ComponentLibrary.Common.Helper;
    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ClickToVoiceComponent : ComponentBase
    {
        public ClickToVoiceComponent()
        {
            this.Title = string.Empty;
            this.TelephoneNumber = string.Empty;
            this.ActionUrl = string.Empty;
            this.ImageUrl = string.Empty;
            this.AlternateText = string.Empty;
        }

        public ClickToVoiceComponent(IHtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            this.Title = string.Empty;
            this.TelephoneNumber = string.Empty;
            this.ActionUrl = string.Empty;
            this.ImageUrl = string.Empty;
            this.AlternateText = string.Empty;
        }

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get
            {
                return
                    new ReadOnlyCollection<JsResource>(
                        new List<JsResource>
                            {
                                new JsResource(
                                         "jsQtip",
                                         "Equant.SAV2000.ComponentLibrary.Common.Resources.Javascripts.jquery.qtip.js",
                                         200,
                                         typeof(Locator)),
                                new JsResource(
                                    "JsClickToVoice", "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.ClickToVoice.js", 200, typeof(ClickToVoiceComponent))
                            });
            }
        }

        public override ReadOnlyCollection<CssResource> CssResources
        {
            get
            {
                return
                    new ReadOnlyCollection<CssResource>(
                        new List<CssResource>
                        {
                            new CssResource(
                                "Cssjqueryqtip",
                                "Equant.SAV2000.ComponentLibrary.Common.Resources.Css.jquery.qtip.css",
                                200,
                                typeof(Locator))
                        });
            }
        }

        public string CssAnchorTag { get; set; }
        public string CssClassImage { get; set; }
        public string CssTelephoneNumberSpan { get; set; }
        public string Title { get; set; }
        public string ActionUrl { get; set; }
        public string ImageUrl { get; set; }
        public string AlternateText { get; set; }
        public string TelephoneNumber { get; set; }
        public bool IsNumberSpanVisible { get; set; }
        public string TelephoneControlId { get; set; }
        public string RootService { get; set; }

        public override IHtmlContent BuildHtml()
        {
            return new ClickToVoiceHtmlBuilder(this).Build();
        }

        public override string BuildInitScript()
        {
            if (!string.IsNullOrEmpty(this.RootService))
            {
                this.ActionUrl = this.ActionUrl.Replace("{1}", WebUtility.UrlEncode(this.RootService));
            }

            var options = JsonSerializer.Serialize(new
            {
                id = this.Id,
                url = this.ActionUrl,
                telephoneNumber = this.TelephoneNumber,
                telephoneControlId = this.TelephoneControlId,
                title = ApplicationStrings.TIP000022
            });
            return string.Format("$('#{0}').clickToVoice({1});", this.Id, options);
        }
    }
}
