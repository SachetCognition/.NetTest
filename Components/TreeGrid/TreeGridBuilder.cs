using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TreeGrid;

public class TreeGridBuilder : ComponentBuilderBase<TreeGridComponent, TreeGridBuilder>
{
    public TreeGridBuilder(TreeGridComponent component, ModelMetadata? modelMetadata)
        : base(component, modelMetadata)
    {
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "TETHYS: The list values are to be provided by the user.")]
    public TreeGridBuilder Columns(List<TreeViewColumn> value)
    {
        Component.Columns = value;
        return this;
    }

    public TreeGridBuilder Data(System.Data.DataTable value)
    {
        Component.Data = value;
        return this;
    }

    public TreeGridBuilder CheckedValues(IEnumerable<string> value)
    {
        Component.CheckedValues = value;
        return this;
    }

    public TreeGridBuilder WidthCss(string value)
    {
        Component.WidthCss = value;
        return this;
    }

    public TreeGridBuilder HighlightRows(IEnumerable<int> idsToHighlight, string color)
    {
        Component.IdsToHighlight = idsToHighlight;
        Component.ColorToHighlightRow = color;
        return this;
    }

    public TreeGridBuilder KeyColumnName(string value)
    {
        Component.KeyColumnName = value;
        return this;
    }
}
