using System.Collections.Generic;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TreeGrid
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// The multi column tree view builder.
    /// </summary>
    public class TreeGridBuilder : ComponentBuilderBase<TreeGridComponent,TreeGridBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeGridBuilder"/> class.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        /// <param name="modelMetadata">
        /// The model metadata.
        /// </param>
        public TreeGridBuilder(TreeGridComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }

        /// <summary>
        /// The columns.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="TreeGridBuilder"/>.
        /// </returns>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification =
            "TETHYS: The list values are to be provided by the user.")]
        public TreeGridBuilder Columns(List<TreeViewColumn> value)
        {
            this.Component.Columns = value;
            return this;
        }

        /// <summary>
        /// The TREEDATATABLE.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="TreeGridBuilder"/>.
        /// </returns>
        public TreeGridBuilder Data(System.Data.DataTable value)
       {
           Component.Data = value;
           return this;
       }

        /// <summary>
        /// The checked values.
        /// </summary>
        /// <param name="value">
        /// Ids to checked checkboxes on load
        /// </param>
        /// <returns>
        /// The <see cref="TreeGridBuilder"/>.
        /// </returns>
        public TreeGridBuilder CheckedValues(IEnumerable<string> value)
        {
            this.Component.CheckedValues = value;
            return this;
        }

        /// <summary>
        /// it is the CSS, which contains the width of tree grid.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="TreeGridBuilder"/>.
        /// </returns>
        public TreeGridBuilder WidthCss(string value)
        {
            this.Component.WidthCss = value;
            return this;
        }

        /// <summary>
        /// The highlight rows.
        /// </summary>
        /// <param name="idsToHighlight">
        /// it contains the list of id which needs to highlight.
        /// </param>
        /// <param name="color">
        /// The color.
        /// </param>
        /// <returns>
        /// The <see cref="TreeGridBuilder"/>.
        /// </returns>
        public TreeGridBuilder HighlightRows(IEnumerable<int> idsToHighlight, string color)
        {
            this.Component.IdsToHighlight = idsToHighlight;
            this.Component.ColorToHighlightRow = color;
            return this;
        }
        

        /// <summary>
        /// The id col.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public TreeGridBuilder KeyColumnName(string value)
        {
            this.Component.KeyColumnName = value;
            return this;
        }
    }
}
