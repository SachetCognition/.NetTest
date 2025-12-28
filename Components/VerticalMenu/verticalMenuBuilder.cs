using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Image;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Menu;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.VerticalMenu;

public class VerticalMenuBuilder : ComponentBuilderBase<VerticalMenuComponent, VerticalMenuBuilder>
{
    private readonly ImageBuilder _copyRightImgBuilder;

    public VerticalMenuBuilder(VerticalMenuComponent component, ModelMetadata? modelMetadata)
        : base(component, modelMetadata)
    {
        _copyRightImgBuilder = new ImageBuilder(Component.CopyRightImage, modelMetadata);
    }

    public VerticalMenuBuilder Image(Action<ImageBuilder> setup)
    {
        if (setup != null)
        {
            setup(_copyRightImgBuilder);
        }

        return this;
    }

    public VerticalMenuBuilder DataBind(IEnumerable<MenuItem> dataSource)
    {
        if (dataSource != null)
        {
            Component.MenuItems.Clear();
            foreach (var item in dataSource)
            {
                Component.MenuItems.Add(item);
            }
        }

        return this;
    }

    public VerticalMenuBuilder CopyRightText(string copyRightTxt)
    {
        Component.CopyRightText = copyRightTxt;
        return this;
    }

    public VerticalMenuBuilder SelectedMenu(string value)
    {
        Component.SelectedMenu = value;
        return this;
    }

    public VerticalMenuBuilder CauseValidation(bool value)
    {
        Component.CauseValidation = value;
        return this;
    }
}
