// -------------------------------------------------------------------------------------------------
// <copyright file="CustomLabelComponent.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//    
//    Creation Date: 04/04/2014
//    Author:  Sharma Siddharth (54626) 
//    Description: Contains the definition for the CustomLabel component
// </summary>
// -------------------------------------------------------------------------------------------------
namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel
{
    using System.Web.Mvc;
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    /// <summary>
    ///     Control which generates a span or label composed of : 
    ///  - the main label
    ///  - a superscript text
    ///  - an asterisk (star) if needed
    ///  - a semicolon if needed
    /// </summary>
    public class CustomLabelComponent : ComponentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomLabelComponent"/> class.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        public CustomLabelComponent(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            this.DisplayColon = true;
            this.DisplayStar = false;
            this.IsHtmlEncode = false;
            this.IsOnlyForAccess = false;
        }
        
        /// <summary>
        /// Gets or sets the ID of the associated control.
        /// </summary>
        public string AssociatedControlId
        {
            get
            {
                if(this.HtmlAttributes.ContainsKey("for"))
                {
                    return this.HtmlAttributes["for"].ToString();
                }

                return string.Empty;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.HtmlAttributes["for"] = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the CSS property applies on the main label
        /// </summary>
        public string CssClassLabel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the colon is required to be display or not. 
        /// </summary>
        public bool IsOnlyForAccess { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the colon is required to be display or not. 
        /// </summary>
        public bool DisplayColon { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the star is required to be display or not. 
        /// </summary>
        public bool DisplayStar { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether label text shall be htmlEncoded or not. 
        /// </summary>
        public bool IsHtmlEncode { get; set; }

        /// <summary>
        /// Gets or sets the text to display in the main label.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the text to display on the superscript text.
        /// </summary>
        public string SuperscriptText { get; set; }

        /// <summary>
        /// Gets or sets the property applies on the superscript text label
        /// </summary>
        public string SuperscriptCssClass { get; set; }

        /// <summary>
        /// Gets or sets the ToolTip property applies on the superscript text label
        /// </summary>
        public string SuperscriptToolTip { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public override string Id { get; set; }

        /// <summary>
        /// The write html.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public override void WriteHtml(HtmlTextWriter writer)
        {
            new CustomLabelHtmlBuilder(this).Build(writer);
        }

        /// <summary>
        /// This writes initial start up script.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public override void WriteInitScript(HtmlTextWriter writer)
        {
        }
    }
}
