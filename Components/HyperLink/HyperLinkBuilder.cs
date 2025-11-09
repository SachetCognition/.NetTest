namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    /// <summary>
    /// </summary>
    public class HyperLinkBuilder : ComponentBuilderBase<HyperLinkComponent, HyperLinkBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HyperLinkBuilder"/> class.
        /// </summary>
        public HyperLinkBuilder(HyperLinkComponent component, ModelMetadata? modelMetadata)
            : base(component, modelMetadata)
        {
        }

        /// <summary>
        /// </summary>
        public HyperLinkBuilder Url(string url)
        {
            Component.Url = url;
            return this;
        }

        /// <summary>
        /// </summary>
        public HyperLinkBuilder Text(string text)
        {
            Component.Text = text;
            return this;
        }

        /// <summary>
        /// </summary>
        public HyperLinkBuilder Target(string target)
        {
            Component.Target = target;
            return this;
        }

        /// <summary>
        /// </summary>
        public HyperLinkBuilder CssClass(string cssClass)
        {
            Component.CssClass = cssClass;
            return this;
        }

        /// <summary>
        /// </summary>
        public HyperLinkBuilder Title(string title)
        {
            Component.Title = title;
            return this;
        }

        public HyperLinkBuilder Css(string cssClass)
        {
            Component.CssClass = cssClass;
            return this;
        }

        public HyperLinkBuilder ActionUrl(string actionUrl)
        {
            Component.Url = actionUrl;
            return this;
        }

        public HyperLinkBuilder ImageUrl(string imageUrl)
        {
            Component.ImageUrl = imageUrl;
            return this;
        }

        public HyperLinkBuilder HtmlAttributes(object htmlAttributes)
        {
            if (htmlAttributes != null)
            {
                var properties = htmlAttributes.GetType().GetProperties();
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(htmlAttributes);
                    if (value != null)
                    {
                        Component.HtmlAttributes[prop.Name] = value;
                    }
                }
            }
            return this;
        }
    }
}
