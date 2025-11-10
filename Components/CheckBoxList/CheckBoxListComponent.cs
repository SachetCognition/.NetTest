// -------------------------------------------------------------------------------------------------
// <copyright file="CheckBoxListComponent.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//    
//    Creation Date: 24/06/2014
//    Author:  Joshi Mukesh 
//    Legacy mapping:  
//    Description: 
// </summary>
// -------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBoxList
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.SpanLabel;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    /// <summary>
    /// CheckBoxList Component
    /// </summary>
    public class CheckBoxListComponent: ComponentBase
    {
         /// <summary>
        /// Initializes a new instance of the <see cref="CheckBoxListComponent"/> class.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        public CheckBoxListComponent(IHtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            this.SourceItems = new List<CheckBoxListItem>();
            this.CheckBoxListLabel = new SpanLabelComponent(htmlHelper);
            this.IsDisabled = false;
            this.OnClick = "null";
            this.OnChange = "null";
            this.Title = string.Empty;
        }

        /// <summary>
        /// Gets or sets the CSS class of the field set
        /// </summary>
        public string CssClassFieldSet { get; set; }
        /// <summary>
        /// Gets or sets the CSS class of the checkbox list
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
        /// If checkbox to be Disabled
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// The checkbox title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// CSS of the field-set div
        /// </summary>
        public string CssClassOuterDiv { get; set; }
        /// <summary>
        /// The div needed outside field-set.
        /// </summary>
        public bool IsOuterDivNeeded { get; set; }
        /// <summary>
        /// Gets or sets the CSS class of the div associated with checkbox list
        /// </summary>
        public string CssClassCheckBoxDiv { get; set; }

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
                                    "JsCheckBoxList",
                                    "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.CheckBoxList.js",
                                    200,
                                    typeof(CheckBoxListComponent))
                            });
            }
        }

        /// <summary>
        ///     Gets the items.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", 
            Justification = "Collection needs to be set from the DataBind method"), 
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = 
            "Generic List is used by the FW")]
        public List<CheckBoxListItem> SourceItems { get; set; }

        /// <summary>
        /// The checkbox list label
        /// </summary>
        public SpanLabelComponent CheckBoxListLabel { get; set; }

        /// <summary>
        /// Write HTML 
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteHtml(TextWriter writer)
        {
            new CheckBoxListHtmlBuilder(this).Build(writer);
        }

        /// <summary>
        /// Method for script writing.
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteInitScript(TextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentException("The parameter writer cannot be null");
            }

            var options = JsonConvert.SerializeObject(new { onClick = new JRaw(this.OnClick), onChange = new JRaw(this.OnChange) });
            writer.WriteLine("$('#{0}').checkBoxList({1});", this.Id, options);
        }
    }
}
