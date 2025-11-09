// -------------------------------------------------------------------------------------------------
// <copyright file="CheckBoxListHtmlBuilder.cs" company="OBS">
// </copyright>
// <summary>
//    Creation Date: 24/06/2014
//    Author:  Joshi Mukesh 
//    Description: Defines the HTML builder for check-box list. 
// </summary>
// -------------------------------------------------------------------------------------------------
namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBoxList
{
    using System;
    using System.IO;
    using System.Text;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;

    /// <summary>
    /// HTML builder for a check-box list.
    /// </summary>
    public class CheckBoxListHtmlBuilder : HtmlBuilderBase<CheckBoxListComponent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckBoxListHtmlBuilder"/> class.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        public CheckBoxListHtmlBuilder(CheckBoxListComponent component)
        {
            this.Component = component;
        }
        /// <summary>
        /// Builds the complete component HTML.
        /// </summary>
        /// <param name="writer"></param>
        public override void Build(TextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentException("The parameter writer cannot be null");
            }

            if (this.Component.IsVisible)
            {
                var controlStringBuiler = new StringBuilder();
                if (!string.IsNullOrEmpty(Component.CheckBoxListLabel.Text))
                {
                    controlStringBuiler.Append(this.Component.CheckBoxListLabel.ToHtmlString());
                }

                // Generate the CheckBoxList only if it is not empty, else generate only the label (done above
                //<field set>
                var tagBuilderFieldSet = new TagBuilder("fieldset");
                if (!String.IsNullOrEmpty(this.Component.CssClassFieldSet))
                {
                    tagBuilderFieldSet.AddCssClass(this.Component.CssClassFieldSet);
                }
                //</field set>
                //<UL>
                //set the UL attributes.
                var tagBuilderUl = new TagBuilder("ul");
                if (!String.IsNullOrEmpty(this.Component.CssClass))
                {
                    tagBuilderUl.AddCssClass("checkboxpadding " + this.Component.CssClass);
                }

                tagBuilderUl.MergeAttribute("id", this.Component.Id);
                tagBuilderUl.MergeAttributes(this.Component.HtmlAttributes);
                //li control will be created through string builder as it will have inner html for the checkbox and label.
                var liStringBuilder = new StringBuilder();
                //Used for creating the complete html for  individual checkbox
                var chkBoxBuilder = new StringBuilder();
                if (this.Component.IsDisabled)
                {
                    this.Component.CssClass = this.Component.CssClassDisabled;
                    tagBuilderFieldSet.MergeAttribute("disabled", "disabled");
                }
                //Generate the checkbox list.

                foreach (var item in this.Component.SourceItems)
                {
                    //<Change author="Nidhi" version="Iteration1" action = "Modification">
                    //Description : If ID is missing from checkbox implementation then this will throw an error.
                    //</Change>
#if DEBUG
                    if (item.Value == null)
                    {

                        throw new ArgumentException("The attribute 'Value' is mandatory to add with each button.");

                    }
#endif                        
                    var tagBuilderLi = new TagBuilder("li");
                    if (!string.IsNullOrEmpty(item.Text))
                    {
                        this.GenerateCheckBox(item, chkBoxBuilder);
                        tagBuilderLi.InnerHtml.SetHtmlContent(chkBoxBuilder.ToString());
                        chkBoxBuilder.Clear();
                    }
                    liStringBuilder.Append(tagBuilderLi);
                }
                //the complete LI will be placed in the UL's inner html.
                if (liStringBuilder.Length == 0)
                {
                    liStringBuilder.Append(new TagBuilder("li"));
                }

                //the complete LI will be placed in the UL's inner html.
                tagBuilderUl.InnerHtml.SetHtmlContent(liStringBuilder.ToString());
                if (!string.IsNullOrEmpty(this.Component.Title))
                {
                    tagBuilderUl.MergeAttribute("title", this.Component.Title);
                }

                var legend = new TagBuilder("legend");
                // Add a hidden span with legend title for reading software.
                legend.MergeAttribute("class", "hide-access");
                var hiddenSpan = new TagBuilder("span");
                hiddenSpan.InnerHtml.SetContent(this.Component.Title);
                legend.InnerHtml.SetHtmlContent(hiddenSpan.ToString());

                var div = new TagBuilder("div");

                var divCssClass = string.IsNullOrEmpty(this.Component.CssClassCheckBoxDiv) ? "checkbox-list-scroll" : "checkbox-list-scroll " + this.Component.CssClassCheckBoxDiv;
                div.MergeAttribute("class", divCssClass);
                var sbinnerHtml = string.Empty;

                tagBuilderFieldSet.InnerHtml.SetHtmlContent(sbinnerHtml.AppendWithBuilder(
                    legend.ToString(),
                    "<div class=\"" + divCssClass + "\">",
                    tagBuilderUl.ToString(),
                    "</div>"));
                if (this.Component.IsOuterDivNeeded)
                {
                    var tagBuilderOuterDiv = new TagBuilder("div");
                    if (!String.IsNullOrEmpty(this.Component.CssClassOuterDiv))
                    {
                        tagBuilderOuterDiv.AddCssClass(this.Component.CssClassOuterDiv);
                    }
                    controlStringBuiler.Append(sbinnerHtml.AppendWithBuilder(
                        "<div class=\"" + this.Component.CssClassOuterDiv + "\">",
                        tagBuilderFieldSet.ToString(),
                        "</div>"));
                }
                else
                {
                    controlStringBuiler.Append(tagBuilderFieldSet);
                }
                writer.Write(controlStringBuiler.ToString());
            }
        }

        /// <summary>
        /// The method generates checkbox with label.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="chkBoxBuilder"></param>
        private void GenerateCheckBox(CheckBoxListItem item, StringBuilder chkBoxBuilder)
        {
            var compId = this.Component.Id + item.Value;
            var tagBuilderCheckBox = new TagBuilder("input");
            tagBuilderCheckBox.MergeAttribute("id", compId);
            tagBuilderCheckBox.MergeAttribute("type", "checkbox");
            tagBuilderCheckBox.MergeAttribute("value", item.Value);
            if (!string.IsNullOrEmpty(this.Component.Name))
            {
                tagBuilderCheckBox.MergeAttribute("name", this.Component.Name);
            }

            if (item.Disabled)
            {
                this.Component.CssClass = this.Component.CssClassDisabled;
                tagBuilderCheckBox.MergeAttribute("disabled", "disabled");
            }
            //Generate checked checkbox.
            if (item.Selected)
            {
                tagBuilderCheckBox.MergeAttribute("checked", "checked");
            }
            //tagbuilder for individual label's for each checkbox.
            var tagBuilderLabel = new TagBuilder("label");
            tagBuilderLabel.MergeAttribute("for", compId);
            tagBuilderLabel.SetInnerText(item.Text);
            chkBoxBuilder.Append(tagBuilderCheckBox.ToString()).Append(tagBuilderLabel);
        }
    }
}
