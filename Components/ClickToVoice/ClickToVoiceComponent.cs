// -------------------------------------------------------------------------------------------------
// <copyright file="ClickToVoiceComponent.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//    
//    Creation Date: 23/09/2014
//    Author:  Arun Kumar (060644) 
//    Description: The component class for the ClickToVoice component
// </summary>
// -------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ClickToVoice
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// The ClickToVoice component to directly open web call interface.
    /// </summary>
    public class ClickToVoiceComponent : ComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClickToVoiceComponent"/> class.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        public ClickToVoiceComponent(IHtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            this.Title = string.Empty;
            this.TelephoneNumber = string.Empty;
            this.ActionUrl = string.Empty;
            this.ImageUrl = string.Empty;
            this.AlternateText = string.Empty;
        }

        /// <summary>
        /// Gets the JS resources.
        /// </summary>
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
                                         "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.jquery.qtip.js",
                                         200,
                                         typeof(ClickToVoiceComponent)),
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
                                "Equant.SAV2000.ComponentLibrary.MVC.Resources.Css.jquery.qtip.css",
                                200,
                                typeof(ClickToVoiceComponent))
                        });
            }
        }

        /// <summary>
        /// Gets or sets the CSS class of the anchor tag
        /// </summary>
        public string CssAnchorTag { get; set; }

        /// <summary>
        /// CSS class for the image tag
        /// </summary>
        public string CssClassImage { get; set; }

        /// <summary>
        /// CSS class for the TelephoneNumber Span
        /// </summary>
        public string CssTelephoneNumberSpan { get; set; }

        /// <summary>
        /// The anchor title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The anchor action method
        /// </summary>
        public string ActionUrl { get; set; }

        /// <summary>
        /// The  image url
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// The alternate text for the image
        /// </summary>
        public string AlternateText { get; set; }

        /// <summary>
        /// This will hold the Telephone Number to give the call.
        /// </summary>
        public string TelephoneNumber { get; set; }

        /// <summary>
        /// Boolean to set the visibility of the TelephoneNumber Span.
        /// </summary>
        public bool IsNumberSpanVisible { get; set; }

        /// <summary>
        /// This will hold the ControlId of the Control that have the Phone Number.
        /// </summary>
        public string TelephoneControlId { get; set; }

        /// <summary>
        /// This will hold the root service to give the call.
        /// </summary>
        public string RootService { get; set; }

        /// <summary>
        /// The write html.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public override void WriteHtml(TextWriter writer)
        {
            new ClickToVoiceHtmlBuilder(this).Build(writer);
        }

        /// <summary>
        /// It writes initial JavaScript.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public override void WriteInitScript(TextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentException("The parameter writer cannot be null");
            }

            if (!string.IsNullOrEmpty(this.RootService))
            {
                this.ActionUrl = this.ActionUrl.Replace("{1}", System.Net.WebUtility.UrlEncode(this.RootService));
            }

            var options =
                JsonConvert.SerializeObject(
                    new
                        {
                            id = this.Id,
                            url = this.ActionUrl,
                            telephoneNumber = this.TelephoneNumber,
                            telephoneControlId = this.TelephoneControlId,
                            title = "Click to call"
                        });
            writer.WriteLine("$('#{0}').clickToVoice({1});", this.Id, options);
        }
    }
}
