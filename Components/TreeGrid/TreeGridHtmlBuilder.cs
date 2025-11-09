using System.Linq;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TreeGrid
{
    using System;
    using System.Data;

    using System.Globalization;
    using System.Text;
    using System.Threading;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.IO;

    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;

    /// <summary>
    /// The multi column tree view html builder.
    /// </summary>
    public class TreeGridHtmlBuilder : HtmlBuilderBase<TreeGridComponent>
    {
        /// <summary>
        /// counter to be used for generation of name of check boxes.
        /// </summary>
        private int Counter = 1;

        /// <summary>
        /// The split items.
        /// </summary>
        //private HashSet<string> splitItems;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeGridHtmlBuilder"/> class.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        public TreeGridHtmlBuilder(TreeGridComponent component)
        {
            this.Component = component;
        }

        /// <summary>
        /// The build.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public override void Build(TextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentException("The parameter writer cannot be null");
            }

            if (this.Component.IsVisible)
            {
                var strComponent = new StringBuilder();
                var outerdiv = new TagBuilder("div");
                if (!string.IsNullOrWhiteSpace(this.Component.WidthCss))
                {
                    outerdiv.MergeAttribute("class", this.Component.WidthCss);
                }
                else
                {
                    outerdiv.MergeAttribute("class", "tree-width");
                }
                outerdiv.AddCssClass("tree-overflow");

                var tagBuilderdiv = new TagBuilder("div");
                tagBuilderdiv.MergeAttribute("class", "tree row");
                tagBuilderdiv.MergeAttribute("id", this.Component.Id);
                // tagBuilderdiv.MergeAttribute("width","1500px");
                tagBuilderdiv.MergeAttributes(this.Component.HtmlAttributes);

                //if for attribute is not defined or it is empty then add component's ID itself to associated control

                var sbCustomInnerHtml = new StringBuilder();
                sbCustomInnerHtml.Append(this.InnerulTag());
                tagBuilderdiv.InnerHtml.AppendHtml(sbCustomInnerHtml.ToString());
                if (  this.Component.Data == null || this.Component.Data.Rows.Count == 0)
                {
                    tagBuilderdiv.InnerHtml.AppendHtml(ReturnEmptylistDiv());
                }
                outerdiv.InnerHtml.AppendHtml(tagBuilderdiv.ToString());
                strComponent.Append(outerdiv);
                writer.Write(strComponent.ToString());
            }
        }

        /// <summary>
        /// The create image.
        /// </summary>
        /// <param name="imageProperties">
        /// The image properties.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string CreateImage(TreeGridimage imageProperties, string id)
        {
            // HyperLinkComponent Image ;
            var lnk = new HyperLinkComponent(this.Component.HtmlHelper);
            return new HyperLinkBuilder(lnk, lnk.ModelMetadata).ActionUrl(imageProperties.ActionUrl)
                .Title(imageProperties.Title)
                .ImageUrl(imageProperties.Source)
                .HtmlAttributes(imageProperties.HtmlAttributes)
                .Id(id).ToString();

        }

        /// <summary>
        /// The inner UL tag.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string InnerulTag()
        {
            var tagBuilderul = new TagBuilder("ul");
            tagBuilderul.MergeAttribute("class", "lvl-0");

            var sbCustomInnerHtml = new StringBuilder(tagBuilderul.InnerHtml);
            sbCustomInnerHtml.Append(this.Makeheader());
            sbCustomInnerHtml.Append(this.Maketree());
            tagBuilderul.InnerHtml.AppendHtml(sbCustomInnerHtml.ToString());

            return tagBuilderul.ToString();
        }


        /// <summary>
        /// The Make header. It will generate the header of tree grid.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>

        private string Makeheader()
        {
            var tagBuilderHeaderLi = new TagBuilder("li");
            tagBuilderHeaderLi.MergeAttribute("class", "tree-head");


            var tagBuilderinnerDiv = new TagBuilder("div");
            tagBuilderinnerDiv.MergeAttribute("class", "li-row");

            var headerString = new StringBuilder();
            var x = 1;
            foreach (var variable in this.Component.Columns)
            {
                var tagBuilderDiv = new TagBuilder("div");
                tagBuilderDiv.MergeAttribute("class", "cell cell-" + x + "");

                if (variable.TreeColumnType == ColumnType.Custom)
                {
                    tagBuilderDiv.AddCssClass("tree-Custommargin");
                }

                var tagSpan = new TagBuilder("span") { InnerHtml = variable.HeaderText };
                if (!variable.IsHeaderVisible)
                {
                    tagSpan.MergeAttribute("class", "tree-headervisibility");
                }
                x++;
                tagBuilderDiv.InnerHtml.AppendHtml(tagSpan.ToString());

                headerString.Append(tagBuilderDiv);
            }


            tagBuilderinnerDiv.InnerHtml.AppendHtml(headerString.ToString());

            tagBuilderHeaderLi.InnerHtml.AppendHtml(tagBuilderinnerDiv.ToString());

            return tagBuilderHeaderLi.ToString();
        }

        /// <summary>
        /// Adding a blank footer to Give a nice closing look to the Tree.
        /// </summary>
        /// <returns></returns>
        private static string MakeFooter()
        {
            var tagBuilderHeaderLi = new TagBuilder("li");
            tagBuilderHeaderLi.MergeAttribute("class", "lastchild");


            var tagBuilderinnerDiv = new TagBuilder("div");
            tagBuilderinnerDiv.MergeAttribute("class", "li-row");


            tagBuilderinnerDiv.InnerHtml.AppendHtml("&nbsp);");
            tagBuilderHeaderLi.InnerHtml.AppendHtml(tagBuilderinnerDiv.ToString());

            return tagBuilderHeaderLi.ToString();
        }

        /// <summary>
        /// it returns empty div in case od no list item.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string ReturnEmptylistDiv()
        {
            var sbEmptydiv = new TagBuilder("div");
            sbEmptydiv.MergeAttribute("class", "Emptytree");
            sbEmptydiv.InnerHtml.AppendHtml(ApplicationStrings.LBL000021);

            return sbEmptydiv.ToString();
        }

        /// <summary>
        /// The MAKETREE.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string Maketree()
        {
            var treeString = new StringBuilder();

           var dataTable = this.Component.Data;
            if (dataTable != null)
            {
                var foundRows = dataTable.Select("ParentID = 0");
                
                foreach (var row in foundRows)
                {
                    if (!(bool)row["Created"])
                    {
                        var tagBulderli = new TagBuilder("li");
                        var sb = new StringBuilder();
                        if ((string)row["HasChild"] == "Yes")
                        {
                            tagBulderli.MergeAttribute("class", "parent_li");
                            var tagBulderanchor = new TagBuilder("a");
                            tagBulderanchor.MergeAttribute("href", "###");
                            tagBulderanchor.MergeAttribute("title", "");

                            var tagbuilderspan = new TagBuilder("span");
                            tagbuilderspan.MergeAttribute("class", "glyphicon glyphicon-minus");
                            tagBulderanchor.InnerHtml.AppendHtml(tagbuilderspan.ToString());
                            sb.Append(tagBulderanchor);
                        }
                        else if (foundRows.Last() == row)
                        {
                            tagBulderli.MergeAttribute("class", "lastchild");
                        }
                        else
                        {
                            tagBulderli.MergeAttribute("class", "child");
                        }

                        sb.Append(this.MakeNode(row));
                        if ((string)row["HasChild"] == "Yes")
                        {
                            sb.Append(this.Makechild(row));
                        }

                        tagBulderli.InnerHtml.AppendHtml(sb.ToString());

                        treeString.Append(tagBulderli);
                    }
                }

                if (foundRows.Count() != 0 && (foundRows.Last()["HasChild"] as string) == "Yes")
                {
                    treeString.Append(MakeFooter());
                }
            }

            return treeString.ToString();
        }

        /// <summary>
        /// The make node. It will create the node of tree.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string MakeNode(DataRow dr)
        {
            dr["Created"] = true;
            var tagbuilderLineRowDiv = new TagBuilder("div");

            if (this.Component.IdsToHighlight != null)
            {
                if (this.Component.IdsToHighlight.Any())
                {
                    if (this.Component.IdsToHighlight.Contains(Convert.ToInt32(dr["ID"], Thread.CurrentThread.CurrentCulture)))
                    {
                        if (!string.IsNullOrWhiteSpace(this.Component.ColorToHighlightRow))
                        {
                            tagbuilderLineRowDiv.MergeAttribute("style", "background-color:" + this.Component.ColorToHighlightRow);
                        }

                    }

                }
            }

            tagbuilderLineRowDiv.MergeAttribute("class", "li-row");
            var childString = new StringBuilder();
            var x = 1;

            foreach (var col in this.Component.Columns)
            {
                var tagBuilderDiv = new TagBuilder("div");
                tagBuilderDiv.MergeAttribute("class", "cell cell-" + x + "");

                if (col.TreeColumnType == ColumnType.Custom)
                {
                    tagBuilderDiv.MergeAttribute("style", "margin-left:10px");
                }

                var tagBuilderinnerDiv = new TagBuilder("div");
                tagBuilderinnerDiv.MergeAttribute("class", "inner");

                var tagSpan = new TagBuilder("span");

                if (col.TreeColumnType == ColumnType.Custom)
                {
                    tagSpan.MergeAttribute("id", "spn" + this.Component.Id + col.PropertyName + dr[this.Component.KeyColumnName]);
                }
                var sb = new StringBuilder();

                if (col.TreeColumnType == ColumnType.CheckBox)
                {
                    var drow = (CheckBoxTextColumn)dr[col.PropertyName];
                    
                      var taglbl = new TagBuilder("label");
                    if (!string.IsNullOrEmpty(drow.AccessText))
                    {
                        var spanHideAccess = new TagBuilder("Span");
                        spanHideAccess.AddCssClass("hide-access");
                        spanHideAccess.InnerHtml.AppendHtml(drow.AccessText + " ");
                        taglbl.InnerHtml.AppendHtml(spanHideAccess.ToString().AppendWithBuilder(drow.Text));
                    }
                    else if (!string.IsNullOrEmpty(col.AccessText))
                    {
                        var spanHideAccess = new TagBuilder("Span");
                        spanHideAccess.AddCssClass("hide-access");
                        spanHideAccess.InnerHtml.AppendHtml(col.AccessText + " ");
                        taglbl.InnerHtml.AppendHtml(spanHideAccess.ToString().AppendWithBuilder(drow.Text));
                    }
                    else
                    {
                        taglbl.InnerHtml.AppendHtml(drow.Text);

                    }

                    if (!drow.IsVisible)
                    {
                        sb.Append(taglbl);
                    }

                    if (drow.IsVisible)
                    {
                        var tagchk = new TagBuilder("input");
                        var checkboxId = this.Component.Id + this.Counter.ToString(CultureInfo.InvariantCulture);
                        this.Counter++;
                        tagchk.MergeAttribute("type", "checkbox");
                        tagchk.MergeAttribute("id", checkboxId);

                        if (!string.IsNullOrEmpty(this.Component.Name))
                        {
                            tagchk.MergeAttribute("Name", this.Component.Name);
                        }
                        if (!String.IsNullOrEmpty(drow.AccessText))
                        {
                            tagchk.MergeAttribute("title", drow.AccessText);
                        }
                        tagchk.MergeAttribute("Value", Convert.ToString(dr[this.Component.KeyColumnName], CultureInfo.InvariantCulture));
                        taglbl.MergeAttribute("for", checkboxId);

                        if (this.Component.CheckedValues != null)
                        {
                            if (this.Component.CheckedValues.Contains(Convert.ToString(dr[this.Component.KeyColumnName], CultureInfo.InvariantCulture)))
                            {
                                tagchk.MergeAttribute("checked", "checked");
                            }
                            if (!drow.IsUpdatable)
                            {
                                tagchk.MergeAttribute("disabled", "disabled");
                            }
                        }
                        sb.Append(tagchk.ToString(TagRenderMode.SelfClosing));

                        sb.Append(taglbl);

                    }
                }

                if (col.TreeColumnType == ColumnType.Image)
                {
                    var drow = (ImageTextColumn)dr[col.PropertyName];
                    var ax = 0;
                    foreach (var imageProperties in drow.LstImage)
                    {
                        if (imageProperties.IsVisible)
                        {
                            sb.Append(this.CreateImage(imageProperties, this.Component.Id + "_ctrl_" + x + "_" + dr[this.Component.KeyColumnName] + "_" + ax++));
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(drow.Text))
                    {
                        sb.Append(Convert.ToString(drow.Text, CultureInfo.InvariantCulture));
                    }
                }

                if (col.TreeColumnType == ColumnType.Text)
                {
                    sb.Append(Convert.ToString(dr[col.PropertyName], CultureInfo.InvariantCulture));
                }

                tagSpan.InnerHtml.AppendHtml(sb.ToString());

                x++;
                tagBuilderinnerDiv.InnerHtml.AppendHtml(tagSpan.ToString());
                tagBuilderDiv.InnerHtml.AppendHtml(tagBuilderinnerDiv.ToString());
                childString.Append(tagBuilderDiv);
            }

            //  ChildString.Append(this.MakeChild(dr, 1));
            tagbuilderLineRowDiv.InnerHtml.AppendHtml(childString.ToString());
            return tagbuilderLineRowDiv.ToString();
        }

        /// <summary>
        /// The STR.
        /// </summary>
        private StringBuilder str = new StringBuilder();

        /// <summary>
        /// The make child. It will make the tree structure.
        /// </summary>
        /// <param name="drow">
        /// The d row.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string Makechild(DataRow drow)
        {
            var strChild = new StringBuilder();
            var tagBulderul = new TagBuilder("ul");
            var deptCss = Convert.ToInt32(drow["Depth"], CultureInfo.InvariantCulture) + 1;

            tagBulderul.MergeAttribute("class", "lvl-" + deptCss % 3);

            var dataTable = this.Component.Data;
            if (dataTable != null)
            {
                var foundRows = dataTable.Select("ParentID=" + Convert.ToInt32(drow["ID"], CultureInfo.InvariantCulture));
                int samelevel = foundRows.Count();
                var i = 0;
                foreach (var foundRow in foundRows)
                {
                    var tagbuilderli = new TagBuilder("li");

                    if ((string)foundRow["HasChild"] == "Yes")
                    {
                        i++;
                        tagbuilderli.MergeAttribute("class", i == samelevel ? "lastchild parent_li" : "child parent_li");
                        var tagBulderanchor = new TagBuilder("a");
                        tagBulderanchor.MergeAttribute("href", "###");
                        tagBulderanchor.MergeAttribute("title", "");

                        var tagbuilderspan = new TagBuilder("span");
                        tagbuilderspan.MergeAttribute("class", "glyphicon glyphicon-minus");
                        tagBulderanchor.InnerHtml.AppendHtml(tagbuilderspan.ToString());
                        this.str.Append(tagBulderanchor);
                    }
                    else if (foundRows.Last() == foundRow)
                    {
                        this.str = new StringBuilder();
                        tagbuilderli.MergeAttribute("class", "lastchild");
                    }
                    else
                    {
                        this.str = new StringBuilder();
                        tagbuilderli.MergeAttribute("class", "child");
                    }

                    tagbuilderli.InnerHtml.AppendHtml(this.str.ToString());
                    tagbuilderli.InnerHtml.AppendHtml(this.MakeNode(foundRow));
                    if ((string)foundRow["HasChild"] == "Yes")
                    {
                        tagbuilderli.InnerHtml.AppendHtml(this.Makechild(foundRow));
                    }

                    strChild.Append(tagbuilderli);
                }
            }

            tagBulderul.InnerHtml.AppendHtml(Convert.ToString(strChild.ToString(), CultureInfo.InvariantCulture));
            return tagBulderul.ToString();
        }
    }
}
