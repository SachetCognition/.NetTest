using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Equant.SAV2000.ComponentLibrary.Common.Helper;
using Equant.SAV2000.ComponentLibrary.Common.Resources;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Image;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Menu;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.VerticalMenu;

public class VerticalMenuComponent : ComponentBase
{
    private readonly ReadOnlyCollection<JsResource> _jsResources;

    public VerticalMenuComponent(IHtmlHelper htmlHelper)
        : base(htmlHelper)
    {
        MenuItems = new List<MenuItem>();
        CopyRightImage = new ImageComponent(htmlHelper);
        var jsRes = new List<JsResource>
        {
            new JsResource(
                "JsVerticalMenu",
                "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.VerticalMenu.js",
                220,
                typeof(VerticalMenuComponent))
        };
        _jsResources = new ReadOnlyCollection<JsResource>(jsRes);
    }

    public override ReadOnlyCollection<JsResource> JsResources => _jsResources;

    internal List<VerticalMenuItemInfo>? MenuItemsInformation { get; set; }
    public bool CauseValidation { get; set; }
    internal List<MenuItem> MenuItems { get; private set; }
    internal const string OuterMenuDivCssClass = "navbar";
    public ImageComponent CopyRightImage { get; private set; }
    public string? CopyRightText { get; set; }
    public string? SelectedMenu { get; set; }

    public override IHtmlContent ToHtml()
    {
        return new VerticalMenuHtmlBuilder(this).Build();
    }

    public override string ToInitScript()
    {
        string options = JsonConvert.SerializeObject(new
        {
            MenuName = Name,
            accessTextOpen = ApplicationStrings.ACCESS000002,
            accessTextClose = ApplicationStrings.LBL000011,
            LinkRenderOptions = MenuItemsInformation
        });

        return string.Format("$('#{0}').verticalMenu({1});", Id, options);
    }
}
