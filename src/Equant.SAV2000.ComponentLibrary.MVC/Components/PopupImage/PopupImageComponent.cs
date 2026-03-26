namespace Equant.SAV2000.ComponentLibrary.MVC.Components.PopupImage
{
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class PopupImageComponent : ComponentBase
    {
        public PopupImageComponent() : base() { }
        public PopupImageComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        public string Src { get; set; }
        public string Alt { get; set; }
        public string Title { get; set; }
        public string PopupUrl { get; set; }
        public string PopupTitle { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public int PopupWidth { get; set; } = 800;
        public int PopupHeight { get; set; } = 600;

        public override void WriteHtml(TextWriter writer)
        {
            new PopupImageHtmlBuilder(this).Build(writer);
        }

        public override void WriteInitScript(TextWriter writer)
        {
            if (writer != null)
            {
                writer.WriteLine("$('#{0}').click(function(e) {{ e.preventDefault(); window.open('{1}', '{2}', 'width={3},height={4}'); }});",
                    this.Id,
                    this.PopupUrl ?? this.Src ?? string.Empty,
                    this.PopupTitle ?? this.Title ?? string.Empty,
                    this.PopupWidth,
                    this.PopupHeight);
            }
        }
    }
}
