// -------------------------------------------------------------------------------------------------
// <copyright file="ActionButtonComponent.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//    
//    Creation Date: 07/04/2014
//    Author:  Sharma Siddharth (54626) 
//    Description: The component class for the ActionButton component
// </summary>
// -------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ActionButton
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Equant.SAV2000.ComponentLibrary.Common.Helper;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// The component class for the ActionButton component
    /// </summary>
    public class ActionButtonComponent : ComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionButtonComponent"/> class.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        public ActionButtonComponent(IHtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            this.OnClick = "null";
            this.Title = string.Empty;
            this.Value = string.Empty;
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
                                    "JsActionButton",
                                    "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.ActionButton.js",
                                    200,
                                    typeof(ActionButtonComponent))
                            });
            }
        }
        
        /// <summary>
        /// Gets or sets the CSS class of the ActionButton
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the Dialog DIV id
        /// </summary>
        public string DialogBoxId { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the ActionButton when disabled
        /// </summary>
        public string CssClassReadOnly { get; set; }

        /// <summary>
        /// CSS class for the span containing the text of the button
        /// </summary>
        public string CssSpan { get; set; }

        /// <summary>
        /// Gets or sets the on click.
        /// </summary>
        public string OnClick { get; set; }

        /// <summary>
        /// The disabled status
        /// </summary>
        public bool IsDisabled
        {
            get
            {
                return this.HtmlAttributes.ContainsKey("disabled");
            }

            set
            {
                if (value)
                {
                    this.HtmlAttributes["disabled"] = "disabled";
                }
                else
                {
                    if (this.HtmlAttributes.ContainsKey("disabled"))
                    {
                        this.HtmlAttributes.Remove("disabled");
                    }
                }
            }
        }

        /// <summary>
        /// The anchor title
        /// </summary>
        public string Title 
        {
            get
            {
                if (this.HtmlAttributes.ContainsKey("title"))
                {
                    return this.HtmlAttributes["title"].ToString();
                }

                return null;
            }

            set
            {
                if (value != null)
                {
                    this.HtmlAttributes["title"] = value;
                }
            }
        }

        /// <summary>
        /// The value to be submit to controller if it is a ActionButton mode
        /// </summary>
        public string Value
        {
            get
            {
                if (!(this.HtmlAttributes.ContainsKey("value")) ||
                   string.IsNullOrEmpty(this.HtmlAttributes["value"].ToString()))
                {
                    throw new ArgumentException("The attribute 'value' is mandatory for submit button ");
                }

                if (this.HtmlAttributes.ContainsKey("value"))
                {
                    return this.HtmlAttributes["value"].ToString();
                }

                return null;
            }

            set
            {
                this.HtmlAttributes["value"] = value;
            }
        }

        /// <summary>
        /// The text to be displayed for button
        /// </summary>
        public string Text { get; set; }
        
        /// <summary>
        /// The write html.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public override void WriteHtml(TextWriter writer)
        {
            new ActionButtonHtmlBuilder(this).Build(writer);
        }

        /// <summary>
        /// This writes initial start-up script.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public override void WriteInitScript(TextWriter writer)
        {
            if (writer != null)
            {
                string options = JsonConvert.SerializeObject(new { onClick = new JRaw(this.OnClick) });
                writer.WriteLine("$('#{0}').actionButton({1});", this.Id, options);
            }
        }
    }
}
