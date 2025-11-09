// -------------------------------------------------------------------------------------------------
// <copyright file="CustomLabelHtmlBuilder.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//    
//    Creation Date: 04/04/2014
//    Author:  Sharma Siddharth (54626) 
//    Description: Contains methods to build HTML for the CustomLabel component
// </summary>
// -------------------------------------------------------------------------------------------------
namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;

    /// <summary>
    /// The check box html builder.
        /// </summary>
    public class CustomLabelHtmlBuilder : HtmlBuilderBase<CustomLabelComponent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomLabelHtmlBuilder"/> class.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        public CustomLabelHtmlBuilder(CustomLabelComponent component)
        {
            this.Component = component;
        }

        /// <summary>
        /// The build.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public override void Build(HtmlTextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentException("The parameter writer cannot be null");
            }

            if (this.Component.IsVisible)
            {
#if DEBUG
                if (this.Component.HtmlAttributes["for"] == null)
                {
                   
                    throw new ArgumentException("The attribute 'for' is mendatory to add with each label.");

                }
#endif
                var tagBuilderCustomLabel = new TagBuilder("label");
                //tagBuilderCustomLabel.MergeAttribute("Id",this.Component.Id );
                tagBuilderCustomLabel.MergeAttributes(this.Component.HtmlAttributes);

                var sbCustomLabelInnerHtml = new StringBuilder(tagBuilderCustomLabel.InnerHtml);
                sbCustomLabelInnerHtml.Append(
                    LabelHelper.InnerSpanTag(
                        !string.IsNullOrEmpty(this.Component.Text) ? this.Component.Text : string.Empty,
                        string.Empty,
                        string.Empty,
                        this.Component.IsHtmlEncode));

                if (!this.Component.IsOnlyForAccess)
                {
                    if (this.Component.CssClassLabel.IsEmpty())
                    {
                        this.Component.CssClassLabel = "pull-right";
                    }

                    if (!string.IsNullOrEmpty(this.Component.CssClassLabel))
                    {
                        tagBuilderCustomLabel.AddCssClass(this.Component.CssClassLabel);
                    }

                    if (!string.IsNullOrEmpty(this.Component.SuperscriptText))
                    {
                        var superScriptCss = !string.IsNullOrEmpty(this.Component.SuperscriptCssClass) ? this.Component.SuperscriptCssClass : "importantfield";

                        sbCustomLabelInnerHtml.Append(LabelHelper.InnerSpanTag(this.Component.SuperscriptText, superScriptCss, this.Component.SuperscriptToolTip, false));
                    }

                    if (this.Component.DisplayStar)
                    {
                        sbCustomLabelInnerHtml.Append(LabelHelper.InnerAbbrTag(ApplicationStrings.lblAsteriks,
                            string.Format(CultureInfo.CurrentCulture, ApplicationStrings.TIP000010, this.Component.Text)));
                    }

                    if (this.Component.DisplayColon)
                    {
                        sbCustomLabelInnerHtml.Append(LabelHelper.InnerSpanTag(ApplicationStrings.lblsemiColon, "paddingColon", string.Empty, false));
                    }
                    if (!string.IsNullOrEmpty(this.Component.AccessText))
                    {
                        sbCustomLabelInnerHtml.Append(LabelHelper.InnerSpanTag(this.Component.AccessText, "hide-access", string.Empty, false));
                    }
                }
                else
                {
                    tagBuilderCustomLabel.AddCssClass("hide-access");
                }

                tagBuilderCustomLabel.InnerHtml = sbCustomLabelInnerHtml.ToString();

                writer.Write(tagBuilderCustomLabel);
            }
        }
    }
}
