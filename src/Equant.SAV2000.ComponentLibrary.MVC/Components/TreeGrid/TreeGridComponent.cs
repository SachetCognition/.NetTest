using System;
using System.Collections.Generic;
using System.Linq;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TreeGrid
{
    using System.IO;
    using System.Collections.ObjectModel;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.Common.Helper;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class TreeGridComponent : ComponentBase
    {
        public List<TreeViewColumn> Columns { get; set; }
        private readonly ReadOnlyCollection<JsResource> jsResources;

        public TreeGridComponent(IHtmlHelper htmlHelper, System.Data.DataTable treeData)
            : base(htmlHelper)
        {
            var jsRes = new List<JsResource>
            {
                new JsResource(
                    "JsTreeCommon",
                    "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.TreeCommon.js",
                    200,
                    typeof(TreeGridComponent)),
            };

            this.jsResources = new ReadOnlyCollection<JsResource>(jsRes);
            this.Data = treeData;
        }

        public TreeGridComponent() : this(null, null) { }

        public System.Data.DataTable Data { get; set; }

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get { return this.jsResources; }
        }

        public override void WriteHtml(TextWriter writer)
        {
            new TreeGridHtmlBuilder(this).Build(writer);
        }

        public string WidthCss { get; set; }
        public string ColorToHighlightRow { get; set; }
        public IEnumerable<int> IdsToHighlight { get; set; }
        public IEnumerable<string> CheckedValues { get; set; }
        public string KeyColumnName { get; set; }

        public override void WriteInitScript(TextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            var columns = new List<TreeCustomColumnData>();

            foreach (var column in this.Columns.Where(x => x.TreeColumnType == ColumnType.Custom).ToList())
            {
                columns.Add(
                    new TreeCustomColumnData
                    {
                        PropertyName = column.PropertyName,
                        Data = string.IsNullOrEmpty(column.Render) ? null : new JRaw(column.Render),
                    });
            }

            string options;

            if (this.Columns.Any(x => x.TreeColumnType == ColumnType.CheckBox) && this.Columns.Any(x => x.TreeColumnType == ColumnType.Custom))
            {
                options = JsonConvert.SerializeObject(new
                {
                    hasCheckBoxColumn = true,
                    id = this.Id,
                    data = this.Data,
                    IdColumn = this.KeyColumnName,
                    Columns = columns,
                    checkedValuesId = "hdn" + this.Id,
                });
            }
            else if (this.Columns.Any(x => x.TreeColumnType == ColumnType.CheckBox))
            {
                options = JsonConvert.SerializeObject(new
                {
                    hasCheckBoxColumn = true,
                    id = this.Id,
                    data = this.Data,
                    checkedValuesId = "hdn" + this.Id,
                });
            }
            else if (this.Columns.Any(x => x.TreeColumnType == ColumnType.Custom))
            {
                options = JsonConvert.SerializeObject(new
                {
                    hasCheckBoxColumn = false,
                    id = this.Id,
                    data = this.Data,
                    IdColumn = this.KeyColumnName,
                    Columns = columns,
                });
            }
            else
            {
                options = JsonConvert.SerializeObject(new
                {
                    id = this.Id,
                });
            }

            writer.WriteLine("$('#{0}').treegrid({1});", this.Id, options);
        }
    }
}
