// -------------------------------------------------------------------------------------------------
// <copyright file="CheckBoxComponent.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//    
//    Creation Date: 13/03/2014
//    Author:  Sharma Siddharth (54626) 
//    Description: Contains the definition for the checkbox component
// </summary>
// -------------------------------------------------------------------------------------------------
namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBox
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Web.Mvc;
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.Common.Helper;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// The class which defines the CheckBox Component.
    /// </summary>
    public class CheckBoxComponent : ComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckBoxComponent"/> class.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        public CheckBoxComponent(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            this.IsChecked = false;
            this.IsDisabled = false;
            this.OnClick = "null";
            this.OnChange = "null";
            this.Title = string.Empty;
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
                                    "JsCheckBox",
                                    "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.CheckBox.js",
                                    200,
                                    typeof(CheckBoxComponent))
                            });
            }
        }

        /// <summary>
        /// Gets or sets the CSS class of the input checkbox
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets the CSS class of the input checkbox when it is disabled
        /// </summary>
        public string CssClassDisabled { get; set; }

        /// <summary>
        /// Gets or sets the on click.
        /// </summary>
        public string OnClick { get; set; }

        /// <summary>
        /// Gets or sets the on change.
        /// </summary>
        public string OnChange { get; set; }

        /// <summary>
        /// Checked status of the input checkbox
        /// </summary>
        public bool IsChecked
        {
            get
            {
                return this.HtmlAttributes.ContainsKey("checked");
            }

            set
            {
                if (value)
                {
                    this.HtmlAttributes["checked"] = "checked";
                }
                else
                {
                    if (this.HtmlAttributes.ContainsKey("checked"))
                    {
                        this.HtmlAttributes.Remove("checked");
                    }
                }
            }
        }

        /// <summary>
        /// If checkbox to be Disabled
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// The checkbox title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The write html.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public override void WriteHtml(HtmlTextWriter writer)
        {
            new CheckBoxHtmlBuilder(this).Build(writer);
        }

        /// <summary>
        /// The write INIT script.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public override void WriteInitScript(HtmlTextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentException("The parameter writer cannot be null");
            }

            var options = JsonConvert.SerializeObject(new { onClick = new JRaw(this.OnClick), onChange = new JRaw(this.OnChange) });
            writer.WriteLine("$('#{0}').checkBox({1});", this.Id, options);
        }
    }
}
