using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.Common.Resources;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
using Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink;
using Equant.SAV2000.ComponentLibrary.MVC.Extensions;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TreeGrid;

public class TreeGridHtmlBuilder : HtmlBuilderBase<TreeGridComponent>
{
    private int _counter = 1;

    public TreeGridHtmlBuilder(TreeGridComponent component)
    {
        Component = component;
    }

    public override IHtmlContent Build()
    {
        if (!Component.IsVisible)
        {
            return HtmlString.Empty;
        }

        var strComponent = new StringBuilder();
        var outerdiv = new TagBuilder("div");
        if (!string.IsNullOrWhiteSpace(Component.WidthCss))
        {
            outerdiv.MergeAttribute("class", Component.WidthCss);
        }
        else
        {
            outerdiv.MergeAttribute("class", "tree-width");
        }
        outerdiv.AddCssClass("tree-overflow");

        var tagBuilderdiv = new TagBuilder("div");
        tagBuilderdiv.MergeAttribute("class", "tree row");
        tagBuilderdiv.MergeAttribute("id", Component.Id);
        tagBuilderdiv.MergeAttributes(Component.HtmlAttributes);

        var sbCustomInnerHtml = new StringBuilder();
        sbCustomInnerHtml.Append(InnerulTag());
        tagBuilderdiv.InnerHtml.AppendHtml(sbCustomInnerHtml.ToString());
        if (Component.Data == null || Component.Data.Rows.Count == 0)
        {
            tagBuilderdiv.InnerHtml.AppendHtml(ReturnEmptylistDiv());
        }
        outerdiv.InnerHtml.AppendHtml(RenderTagBuilder(tagBuilderdiv));
        strComponent.Append(RenderTagBuilder(outerdiv));
        return new HtmlString(strComponent.ToString());
    }

    private static string RenderTagBuilder(TagBuilder tagBuilder)
    {
        using var writer = new StringWriter();
        tagBuilder.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
    }

    private string CreateImage(TreeGridimage imageProperties, string id)
    {
        var lnk = new HyperLinkComponent(Component.HtmlHelper);
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

        var sbCustomInnerHtml = new StringBuilder();
        sbCustomInnerHtml.Append(Makeheader());
        sbCustomInnerHtml.Append(Maketree());
        tagBuilderul.InnerHtml.SetHtmlContent(sbCustomInnerHtml.ToString());

        return RenderTagBuilder(tagBuilderul);
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
        foreach (var variable in Component.Columns)
        {
            var tagBuilderDiv = new TagBuilder("div");
            tagBuilderDiv.MergeAttribute("class", "cell cell-" + x + "");

            if (variable.TreeColumnType == ColumnType.Custom)
            {
                tagBuilderDiv.AddCssClass("tree-Custommargin");
            }

            var tagSpan = new TagBuilder("span");
            tagSpan.InnerHtml.SetHtmlContent(variable.HeaderText ?? string.Empty);
            if (!variable.IsHeaderVisible)
            {
                tagSpan.MergeAttribute("class", "tree-headervisibility");
            }
            x++;
            tagBuilderDiv.InnerHtml.SetHtmlContent(RenderTagBuilder(tagSpan));

            headerString.Append(RenderTagBuilder(tagBuilderDiv));
        }

        tagBuilderinnerDiv.InnerHtml.SetHtmlContent(headerString.ToString());
        tagBuilderHeaderLi.InnerHtml.SetHtmlContent(RenderTagBuilder(tagBuilderinnerDiv));

        return RenderTagBuilder(tagBuilderHeaderLi);
    }

    private string MakeFooter()
    {
        var tagBuilderHeaderLi = new TagBuilder("li");
        tagBuilderHeaderLi.MergeAttribute("class", "lastchild");

        var tagBuilderinnerDiv = new TagBuilder("div");
        tagBuilderinnerDiv.MergeAttribute("class", "li-row");

        tagBuilderinnerDiv.InnerHtml.SetHtmlContent("&nbsp;");
        tagBuilderHeaderLi.InnerHtml.SetHtmlContent(RenderTagBuilder(tagBuilderinnerDiv));

        return RenderTagBuilder(tagBuilderHeaderLi);
    }

    private string ReturnEmptylistDiv()
    {
        var sbEmptydiv = new TagBuilder("div");
        sbEmptydiv.MergeAttribute("class", "Emptytree");
        sbEmptydiv.InnerHtml.SetHtmlContent(ApplicationStrings.LBL000021 ?? string.Empty);

        return RenderTagBuilder(sbEmptydiv);
    }

    private string Maketree()
    {
        var treeString = new StringBuilder();

        var dataTable = Component.Data;
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
                        tagBulderanchor.InnerHtml.SetHtmlContent(RenderTagBuilder(tagbuilderspan));
                        sb.Append(RenderTagBuilder(tagBulderanchor));
                    }
                    else if (foundRows.Last() == row)
                    {
                        tagBulderli.MergeAttribute("class", "lastchild");
                    }
                    else
                    {
                        tagBulderli.MergeAttribute("class", "child");
                    }

                    sb.Append(MakeNode(row));
                    if ((string)row["HasChild"] == "Yes")
                    {
                        sb.Append(Makechild(row));
                    }

                    tagBulderli.InnerHtml.SetHtmlContent(sb.ToString());

                    treeString.Append(RenderTagBuilder(tagBulderli));
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

        if (Component.IdsToHighlight != null)
        {
            if (Component.IdsToHighlight.Any())
            {
                if (Component.IdsToHighlight.Contains(Convert.ToInt32(dr["ID"], Thread.CurrentThread.CurrentCulture)))
                {
                    if (!string.IsNullOrWhiteSpace(Component.ColorToHighlightRow))
                    {
                        tagbuilderLineRowDiv.MergeAttribute("style", "background-color:" + Component.ColorToHighlightRow);
                    }
                }
            }
        }

        tagbuilderLineRowDiv.MergeAttribute("class", "li-row");
        var childString = new StringBuilder();
        var x = 1;

        foreach (var col in Component.Columns)
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
                tagSpan.MergeAttribute("id", "spn" + Component.Id + col.PropertyName + dr[Component.KeyColumnName]);
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
                    spanHideAccess.InnerHtml.SetHtmlContent(drow.AccessText + " ");
                    taglbl.InnerHtml.SetHtmlContent(RenderTagBuilder(spanHideAccess) + drow.Text);
                }
                else if (!string.IsNullOrEmpty(col.AccessText))
                {
                    var spanHideAccess = new TagBuilder("span");
                    spanHideAccess.AddCssClass("hide-access");
                    spanHideAccess.InnerHtml.SetHtmlContent(col.AccessText + " ");
                    taglbl.InnerHtml.SetHtmlContent(RenderTagBuilder(spanHideAccess) + drow.Text);
                }
                else
                {
                    taglbl.InnerHtml.SetHtmlContent(drow.Text ?? string.Empty);
                }

                if (!drow.IsVisible)
                {
                    sb.Append(RenderTagBuilder(taglbl));
                }

                if (drow.IsVisible)
                {
                    var tagchk = new TagBuilder("input");
                    tagchk.TagRenderMode = TagRenderMode.SelfClosing;
                    var checkboxId = Component.Id + _counter.ToString(CultureInfo.InvariantCulture);
                    _counter++;
                    tagchk.MergeAttribute("type", "checkbox");
                    tagchk.MergeAttribute("id", checkboxId);

                    if (!string.IsNullOrEmpty(Component.Name))
                    {
                        tagchk.MergeAttribute("Name", Component.Name);
                    }
                    if (!string.IsNullOrEmpty(drow.AccessText))
                    {
                        tagchk.MergeAttribute("title", drow.AccessText);
                    }
                    tagchk.MergeAttribute("Value", Convert.ToString(dr[Component.KeyColumnName], CultureInfo.InvariantCulture));
                    taglbl.MergeAttribute("for", checkboxId);

                    if (Component.CheckedValues != null)
                    {
                        if (Component.CheckedValues.Contains(Convert.ToString(dr[Component.KeyColumnName], CultureInfo.InvariantCulture)))
                        {
                            tagchk.MergeAttribute("checked", "checked");
                        }
                        if (!drow.IsUpdatable)
                        {
                            tagchk.MergeAttribute("disabled", "disabled");
                        }
                    }
                    sb.Append(RenderTagBuilder(tagchk));
                    sb.Append(RenderTagBuilder(taglbl));
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
                        sb.Append(CreateImage(imageProperties, Component.Id + "_ctrl_" + x + "_" + dr[Component.KeyColumnName] + "_" + ax++));
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

            tagSpan.InnerHtml.SetHtmlContent(sb.ToString());

            x++;
            tagBuilderinnerDiv.InnerHtml.SetHtmlContent(RenderTagBuilder(tagSpan));
            tagBuilderDiv.InnerHtml.SetHtmlContent(RenderTagBuilder(tagBuilderinnerDiv));
            childString.Append(RenderTagBuilder(tagBuilderDiv));
        }

        tagbuilderLineRowDiv.InnerHtml.SetHtmlContent(childString.ToString());
        return RenderTagBuilder(tagbuilderLineRowDiv);
    }

    private StringBuilder _str = new StringBuilder();

    private string Makechild(DataRow drow)
    {
        var strChild = new StringBuilder();
        var tagBulderul = new TagBuilder("ul");
        var deptCss = Convert.ToInt32(drow["Depth"], CultureInfo.InvariantCulture) + 1;

        tagBulderul.MergeAttribute("class", "lvl-" + deptCss % 3);

        var dataTable = Component.Data;
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
                    tagBulderanchor.InnerHtml.SetHtmlContent(RenderTagBuilder(tagbuilderspan));
                    _str.Append(RenderTagBuilder(tagBulderanchor));
                }
                else if (foundRows.Last() == foundRow)
                {
                    _str = new StringBuilder();
                    tagbuilderli.MergeAttribute("class", "lastchild");
                }
                else
                {
                    _str = new StringBuilder();
                    tagbuilderli.MergeAttribute("class", "child");
                }

                var liContent = new StringBuilder();
                liContent.Append(_str.ToString());
                liContent.Append(MakeNode(foundRow));
                if ((string)foundRow["HasChild"] == "Yes")
                {
                    liContent.Append(Makechild(foundRow));
                }
                tagbuilderli.InnerHtml.SetHtmlContent(liContent.ToString());

                strChild.Append(RenderTagBuilder(tagbuilderli));
            }
        }

        tagBulderul.InnerHtml.SetHtmlContent(strChild.ToString());
        return RenderTagBuilder(tagBulderul);
    }
}
