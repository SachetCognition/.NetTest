namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TreeGrid
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class TreeGridBuilder : ComponentBuilderBase<TreeGridComponent, TreeGridBuilder>
    {
        public TreeGridBuilder(TreeGridComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }

        public TreeGridBuilder(TreeGridComponent component)
            : base(component)
        {
        }

        public TreeGridBuilder Columns(List<TreeViewColumn> value) { this.Component.Columns = value; return this; }
        public TreeGridBuilder Data(System.Data.DataTable value) { this.Component.Data = value; return this; }
        public TreeGridBuilder KeyColumnName(string value) { this.Component.KeyColumnName = value; return this; }
        public TreeGridBuilder CheckedValues(IEnumerable<string> value) { this.Component.CheckedValues = value; return this; }
        public TreeGridBuilder IdsToHighlight(IEnumerable<int> value) { this.Component.IdsToHighlight = value; return this; }
        public TreeGridBuilder ColorToHighlightRow(string value) { this.Component.ColorToHighlightRow = value; return this; }
        public TreeGridBuilder WidthCss(string value) { this.Component.WidthCss = value; return this; }
    }
}
