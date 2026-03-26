namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HorizontalTab
{
    using System.IO;
    using System.Text;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class HorizontalTabHtmlBuilder : HtmlBuilderBase<HorizontalTabComponent>
    {
        public HorizontalTabHtmlBuilder(HorizontalTabComponent component) : base(component) { }

        public override void Build(TextWriter writer)
        {
            var container = new TagBuilder("div");
            container.Attributes["id"] = this.Component.Id ?? string.Empty;
            if (!string.IsNullOrEmpty(this.Component.CssClass))
            {
                container.AddCssClass(this.Component.CssClass);
            }
            container.AddCssClass("horizontal-tab");

            // Tab headers
            var ul = new TagBuilder("ul");
            ul.AddCssClass("nav nav-tabs");
            ul.Attributes["role"] = "tablist";

            var headerHtml = new StringBuilder();

            for (int i = 0; i < this.Component.Tabs.Count; i++)
            {
                var tab = this.Component.Tabs[i];
                bool isActive = tab.IsActive ||
                    (!string.IsNullOrEmpty(this.Component.ActiveTab) &&
                     this.Component.ActiveTab == tab.Text);

                // If no explicit active tab is set, default to first tab
                if (string.IsNullOrEmpty(this.Component.ActiveTab) && !HasAnyActiveTab() && i == 0)
                {
                    isActive = true;
                }

                var li = new TagBuilder("li");
                li.AddCssClass("nav-item");
                li.Attributes["role"] = "presentation";

                var anchor = new TagBuilder("a");
                anchor.AddCssClass("nav-link");
                if (isActive)
                {
                    anchor.AddCssClass("active");
                }
                anchor.Attributes["href"] = "#" + (tab.ContentId ?? "tab-" + i);
                anchor.Attributes["role"] = "tab";
                anchor.Attributes["data-toggle"] = "tab";
                anchor.InnerHtml.Append(tab.Text ?? string.Empty);

                using (var sw = new StringWriter())
                {
                    anchor.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
                    li.InnerHtml.AppendHtml(sw.ToString());
                }

                using (var sw = new StringWriter())
                {
                    li.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
                    headerHtml.Append(sw.ToString());
                }
            }

            ul.InnerHtml.AppendHtml(headerHtml.ToString());

            // Tab content panels
            var tabContent = new TagBuilder("div");
            tabContent.AddCssClass("tab-content");

            var panelHtml = new StringBuilder();

            for (int i = 0; i < this.Component.Tabs.Count; i++)
            {
                var tab = this.Component.Tabs[i];
                bool isActive = tab.IsActive ||
                    (!string.IsNullOrEmpty(this.Component.ActiveTab) &&
                     this.Component.ActiveTab == tab.Text);

                if (string.IsNullOrEmpty(this.Component.ActiveTab) && !HasAnyActiveTab() && i == 0)
                {
                    isActive = true;
                }

                var panel = new TagBuilder("div");
                panel.AddCssClass("tab-pane");
                if (isActive)
                {
                    panel.AddCssClass("active");
                }
                panel.Attributes["id"] = tab.ContentId ?? "tab-" + i;
                panel.Attributes["role"] = "tabpanel";

                using (var sw = new StringWriter())
                {
                    panel.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
                    panelHtml.Append(sw.ToString());
                }
            }

            tabContent.InnerHtml.AppendHtml(panelHtml.ToString());

            // Combine headers and panels
            using (var sw = new StringWriter())
            {
                ul.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
                container.InnerHtml.AppendHtml(sw.ToString());
            }

            using (var sw = new StringWriter())
            {
                tabContent.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
                container.InnerHtml.AppendHtml(sw.ToString());
            }

            container.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
        }

        private bool HasAnyActiveTab()
        {
            foreach (var tab in this.Component.Tabs)
            {
                if (tab.IsActive)
                    return true;
                if (!string.IsNullOrEmpty(this.Component.ActiveTab) && this.Component.ActiveTab == tab.Text)
                    return true;
            }
            return false;
        }
    }
}
