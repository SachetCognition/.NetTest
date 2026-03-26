using System.Linq;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TreeGrid
{
    using System;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Text.Encodings.Web;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class TreeGridHtmlBuilder : HtmlBuilderBase<TreeGridComponent>
    {
        private int Counter = 1;

        public TreeGridHtmlBuilder(TreeGridComponent component)
        {
            this.Component = component;
        }

        private static string TagToString(TagBuilder tag)
        {
            using (var sw = new StringWriter())
            {
                tag.WriteTo(sw, HtmlEncoder.Default);
                return sw.ToString();
            }
        }

        public override void Build(TextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentException("The parameter writer cannot be null");
            }

            if (this.Component.IsVisible)
            {
                var outerdiv = new TagBuilder("div");
                if (!string.IsNullOrWhiteSpace(this.Component.WidthCss))
                {
                    outerdiv.Attributes["class"] = this.Component.WidthCss;
                }
                else
                {
                    outerdiv.Attributes["class"] = "tree-width";
                }
                outerdiv.AddCssClass("tree-overflow");

                var tagBuilderdiv = new TagBuilder("div");
                tagBuilderdiv.Attributes["class"] = "tree row";
                tagBuilderdiv.Attributes["id"] = this.Component.Id;

                foreach (var attr in this.Component.HtmlAttributes)
                {
                    tagBuilderdiv.MergeAttribute(attr.Key, attr.Value?.ToString());
                }

                var innerContent = new StringBuilder();
                innerContent.Append(this.InnerulTag());
                tagBuilderdiv.InnerHtml.AppendHtml(innerContent.ToString());

                if (this.Component.Data == null || this.Component.Data.Rows.Count == 0)
                {
                    tagBuilderdiv.InnerHtml.AppendHtml(ReturnEmptylistDiv());
                }

                outerdiv.InnerHtml.AppendHtml(TagToString(tagBuilderdiv));
                outerdiv.WriteTo(writer, HtmlEncoder.Default);
            }
        }

        private string InnerulTag()
        {
            var tagBuilderul = new TagBuilder("ul");
            tagBuilderul.Attributes["class"] = "lvl-0";

            var innerContent = new StringBuilder();
            innerContent.Append(this.Makeheader());
            innerContent.Append(this.Maketree());
            tagBuilderul.InnerHtml.AppendHtml(innerContent.ToString());

            return TagToString(tagBuilderul);
        }

        private string Makeheader()
        {
            var tagBuilderHeaderLi = new TagBuilder("li");
            tagBuilderHeaderLi.Attributes["class"] = "tree-head";

            var tagBuilderinnerDiv = new TagBuilder("div");
            tagBuilderinnerDiv.Attributes["class"] = "li-row";

            var headerString = new StringBuilder();
            var x = 1;
            foreach (var variable in this.Component.Columns)
            {
                var tagBuilderDiv = new TagBuilder("div");
                tagBuilderDiv.Attributes["class"] = "cell cell-" + x;

                if (variable.TreeColumnType == ColumnType.Custom)
                {
                    tagBuilderDiv.AddCssClass("tree-Custommargin");
                }

                var tagSpan = new TagBuilder("span");
                tagSpan.InnerHtml.Append(variable.HeaderText);
                if (!variable.IsHeaderVisible)
                {
                    tagSpan.Attributes["class"] = "tree-headervisibility";
                }
                x++;
                tagBuilderDiv.InnerHtml.AppendHtml(TagToString(tagSpan));
                headerString.Append(TagToString(tagBuilderDiv));
            }

            tagBuilderinnerDiv.InnerHtml.AppendHtml(headerString.ToString());
            tagBuilderHeaderLi.InnerHtml.AppendHtml(TagToString(tagBuilderinnerDiv));

            return TagToString(tagBuilderHeaderLi);
        }

        private static string MakeFooter()
        {
            var tagBuilderHeaderLi = new TagBuilder("li");
            tagBuilderHeaderLi.Attributes["class"] = "lastchild";

            var tagBuilderinnerDiv = new TagBuilder("div");
            tagBuilderinnerDiv.Attributes["class"] = "li-row";
            tagBuilderinnerDiv.InnerHtml.AppendHtml("&nbsp;");

            tagBuilderHeaderLi.InnerHtml.AppendHtml(TagToString(tagBuilderinnerDiv));
            return TagToString(tagBuilderHeaderLi);
        }

        private static string ReturnEmptylistDiv()
        {
            var sbEmptydiv = new TagBuilder("div");
            sbEmptydiv.Attributes["class"] = "Emptytree";
            sbEmptydiv.InnerHtml.Append(ApplicationStrings.LBL000021 ?? "No data available");
            return TagToString(sbEmptydiv);
        }

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
                            tagBulderli.Attributes["class"] = "parent_li";
                            var tagBulderanchor = new TagBuilder("a");
                            tagBulderanchor.Attributes["href"] = "###";
                            tagBulderanchor.Attributes["title"] = "";

                            var tagbuilderspan = new TagBuilder("span");
                            tagbuilderspan.Attributes["class"] = "glyphicon glyphicon-minus";
                            tagBulderanchor.InnerHtml.AppendHtml(TagToString(tagbuilderspan));
                            sb.Append(TagToString(tagBulderanchor));
                        }
                        else if (foundRows.Last() == row)
                        {
                            tagBulderli.Attributes["class"] = "lastchild";
                        }
                        else
                        {
                            tagBulderli.Attributes["class"] = "child";
                        }

                        sb.Append(this.MakeNode(row));
                        if ((string)row["HasChild"] == "Yes")
                        {
                            sb.Append(this.Makechild(row));
                        }

                        tagBulderli.InnerHtml.AppendHtml(sb.ToString());
                        treeString.Append(TagToString(tagBulderli));
                    }
                }

                if (foundRows.Count() != 0 && (foundRows.Last()["HasChild"] as string) == "Yes")
                {
                    treeString.Append(MakeFooter());
                }
            }

            return treeString.ToString();
        }

        private string MakeNode(DataRow dr)
        {
            dr["Created"] = true;
            var tagbuilderLineRowDiv = new TagBuilder("div");

            if (this.Component.IdsToHighlight != null)
            {
                if (this.Component.IdsToHighlight.Any())
                {
                    if (this.Component.IdsToHighlight.Contains(Convert.ToInt32(dr["ID"], CultureInfo.InvariantCulture)))
                    {
                        if (!string.IsNullOrWhiteSpace(this.Component.ColorToHighlightRow))
                        {
                            tagbuilderLineRowDiv.Attributes["style"] = "background-color:" + this.Component.ColorToHighlightRow;
                        }
                    }
                }
            }

            tagbuilderLineRowDiv.Attributes["class"] = "li-row";
            var childString = new StringBuilder();
            var x = 1;

            foreach (var col in this.Component.Columns)
            {
                var tagBuilderDiv = new TagBuilder("div");
                tagBuilderDiv.Attributes["class"] = "cell cell-" + x;

                if (col.TreeColumnType == ColumnType.Custom)
                {
                    tagBuilderDiv.Attributes["style"] = "margin-left:10px";
                }

                var tagBuilderinnerDiv = new TagBuilder("div");
                tagBuilderinnerDiv.Attributes["class"] = "inner";

                var tagSpan = new TagBuilder("span");

                if (col.TreeColumnType == ColumnType.Custom)
                {
                    tagSpan.Attributes["id"] = "spn" + this.Component.Id + col.PropertyName + dr[this.Component.KeyColumnName];
                }
                var sb = new StringBuilder();

                if (col.TreeColumnType == ColumnType.CheckBox)
                {
                    var drow = (CheckBoxTextColumn)dr[col.PropertyName];
                    var taglbl = new TagBuilder("label");

                    if (!string.IsNullOrEmpty(drow.AccessText))
                    {
                        var spanHideAccess = new TagBuilder("span");
                        spanHideAccess.AddCssClass("hide-access");
                        spanHideAccess.InnerHtml.Append(drow.AccessText + " ");
                        taglbl.InnerHtml.AppendHtml(TagToString(spanHideAccess));
                        taglbl.InnerHtml.Append(drow.Text);
                    }
                    else if (!string.IsNullOrEmpty(col.AccessText))
                    {
                        var spanHideAccess = new TagBuilder("span");
                        spanHideAccess.AddCssClass("hide-access");
                        spanHideAccess.InnerHtml.Append(col.AccessText + " ");
                        taglbl.InnerHtml.AppendHtml(TagToString(spanHideAccess));
                        taglbl.InnerHtml.Append(drow.Text);
                    }
                    else
                    {
                        taglbl.InnerHtml.Append(drow.Text);
                    }

                    if (!drow.IsVisible)
                    {
                        sb.Append(TagToString(taglbl));
                    }

                    if (drow.IsVisible)
                    {
                        var tagchk = new TagBuilder("input");
                        var checkboxId = this.Component.Id + this.Counter.ToString(CultureInfo.InvariantCulture);
                        this.Counter++;
                        tagchk.Attributes["type"] = "checkbox";
                        tagchk.Attributes["id"] = checkboxId;

                        if (!string.IsNullOrEmpty(this.Component.Name))
                        {
                            tagchk.Attributes["Name"] = this.Component.Name;
                        }
                        if (!string.IsNullOrEmpty(drow.AccessText))
                        {
                            tagchk.Attributes["title"] = drow.AccessText;
                        }
                        tagchk.Attributes["Value"] = Convert.ToString(dr[this.Component.KeyColumnName], CultureInfo.InvariantCulture);
                        taglbl.Attributes["for"] = checkboxId;

                        if (this.Component.CheckedValues != null)
                        {
                            if (this.Component.CheckedValues.Contains(Convert.ToString(dr[this.Component.KeyColumnName], CultureInfo.InvariantCulture)))
                            {
                                tagchk.Attributes["checked"] = "checked";
                            }
                            if (!drow.IsUpdatable)
                            {
                                tagchk.Attributes["disabled"] = "disabled";
                            }
                        }

                        tagchk.TagRenderMode = TagRenderMode.SelfClosing;
                        sb.Append(TagToString(tagchk));
                        sb.Append(TagToString(taglbl));
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
                            var imgId = this.Component.Id + "_ctrl_" + x + "_" + dr[this.Component.KeyColumnName] + "_" + ax++;
                            var imgTag = new TagBuilder("a");
                            imgTag.Attributes["href"] = imageProperties.ActionUrl ?? "#";
                            imgTag.Attributes["title"] = imageProperties.Title ?? "";
                            imgTag.Attributes["id"] = imgId;
                            var imgEl = new TagBuilder("img");
                            imgEl.Attributes["src"] = imageProperties.Source ?? "";
                            imgEl.Attributes["alt"] = imageProperties.Title ?? "";
                            imgEl.TagRenderMode = TagRenderMode.SelfClosing;
                            imgTag.InnerHtml.AppendHtml(TagToString(imgEl));
                            sb.Append(TagToString(imgTag));
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
                tagBuilderinnerDiv.InnerHtml.AppendHtml(TagToString(tagSpan));
                tagBuilderDiv.InnerHtml.AppendHtml(TagToString(tagBuilderinnerDiv));
                childString.Append(TagToString(tagBuilderDiv));
            }

            tagbuilderLineRowDiv.InnerHtml.AppendHtml(childString.ToString());
            return TagToString(tagbuilderLineRowDiv);
        }

        private string Makechild(DataRow drow)
        {
            var strChild = new StringBuilder();
            var tagBulderul = new TagBuilder("ul");
            var deptCss = Convert.ToInt32(drow["Depth"], CultureInfo.InvariantCulture) + 1;
            tagBulderul.Attributes["class"] = "lvl-" + deptCss % 3;

            var dataTable = this.Component.Data;
            if (dataTable != null)
            {
                var foundRows = dataTable.Select("ParentID=" + Convert.ToInt32(drow["ID"], CultureInfo.InvariantCulture));
                int samelevel = foundRows.Count();
                var i = 0;
                foreach (var foundRow in foundRows)
                {
                    var tagbuilderli = new TagBuilder("li");
                    var anchorHtml = new StringBuilder();

                    if ((string)foundRow["HasChild"] == "Yes")
                    {
                        i++;
                        tagbuilderli.Attributes["class"] = i == samelevel ? "lastchild parent_li" : "child parent_li";
                        var tagBulderanchor = new TagBuilder("a");
                        tagBulderanchor.Attributes["href"] = "###";
                        tagBulderanchor.Attributes["title"] = "";

                        var tagbuilderspan = new TagBuilder("span");
                        tagbuilderspan.Attributes["class"] = "glyphicon glyphicon-minus";
                        tagBulderanchor.InnerHtml.AppendHtml(TagToString(tagbuilderspan));
                        anchorHtml.Append(TagToString(tagBulderanchor));
                    }
                    else if (foundRows.Last() == foundRow)
                    {
                        tagbuilderli.Attributes["class"] = "lastchild";
                    }
                    else
                    {
                        tagbuilderli.Attributes["class"] = "child";
                    }

                    var liContent = new StringBuilder();
                    liContent.Append(anchorHtml.ToString());
                    liContent.Append(this.MakeNode(foundRow));
                    if ((string)foundRow["HasChild"] == "Yes")
                    {
                        liContent.Append(this.Makechild(foundRow));
                    }

                    tagbuilderli.InnerHtml.AppendHtml(liContent.ToString());
                    strChild.Append(TagToString(tagbuilderli));
                }
            }

            tagBulderul.InnerHtml.AppendHtml(strChild.ToString());
            return TagToString(tagBulderul);
        }
    }
}
