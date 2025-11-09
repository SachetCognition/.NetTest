using System;
using System.Collections.Generic;
using System.Linq;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TreeGrid
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.IO;

    using Equant.SAV2000.ComponentLibrary.Common.Helper;
    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.Collections.ObjectModel;


    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// The multi column tree view component.
    /// </summary>
    public class TreeGridComponent : ComponentBase
    {
        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification ="TETHYS: This input is required."),
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification =
            "TETHYS: The list values are to be provided by the user.")]
        public List<TreeViewColumn> Columns { get; set; }
        /// <summary>
        /// The JS resources.
        /// </summary>
        private readonly ReadOnlyCollection<JsResource> jsResources;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeGridComponent"/> class.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="treeData">
        /// The tree data.
        /// </param>
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

        /// <summary>
        /// Gets or sets the DT.
        /// </summary>
        public System.Data.DataTable Data { get; set; }

        /// <summary>
        /// Gets the JS resources.
        /// </summary>
        public override ReadOnlyCollection<JsResource> JsResources
        {
            get
            {
                return this.jsResources;
            }
        }

        /// <summary>
        /// The write html.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public override void WriteHtml(TextWriter writer)
        {
            new TreeGridHtmlBuilder(this).Build(writer);
        }

        /// <summary>
        /// Gets or sets the width of tree grid from CSS.
        /// </summary>
        public string WidthCss { get; set; }

        /// <summary>
        /// Gets or sets the color to highlight row.
        /// </summary>
        public string ColorToHighlightRow { get; set; }

        /// <summary>
        /// Gets or sets the ids to highlight.
        /// </summary>
        public IEnumerable<int> IdsToHighlight { get; set; }

        /// <summary>
        /// Gets or sets the checked values.
        /// </summary>
        public IEnumerable<string> CheckedValues { get; set; }

        /// <summary>
        /// Gets or sets the id col.
        /// </summary>
        public string KeyColumnName { get; set; }

        /// <summary>
        /// This writes initial start up script.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
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
                options =
                    JsonConvert.SerializeObject(
                        new
                            {
                                hasCheckBoxColumn = true,
                                id = this.Id,
                                data = this.Data,
                                IdColumn = this.KeyColumnName,
                                Columns = columns,
                                checkedValuesId = "hdn" + this.Id,
                                //Title = ApplicationStrings.ACCESS000005,
                                //close = ApplicationStrings.LBL000011,
                                //open = ApplicationStrings.ACCESS000002,
                                //CloseTitle = ApplicationStrings.ACCESS000006
                            });
            }
            else if (this.Columns.Any(x => x.TreeColumnType == ColumnType.CheckBox))
            {
                options =
                  JsonConvert.SerializeObject(
                      new
                          {
                              hasCheckBoxColumn = true,
                              id = this.Id,
                              data = this.Data,
                              checkedValuesId = "hdn" + this.Id,
                              //Title = ApplicationStrings.ACCESS000005,
                              //close = ApplicationStrings.LBL000011,
                              //open = ApplicationStrings.ACCESS000002,
                              //CloseTitle = ApplicationStrings.ACCESS000006
                          });
            }
            else if (this.Columns.Any(x => x.TreeColumnType == ColumnType.Custom))
            {
                options =
                    JsonConvert.SerializeObject(
                        new
                        {
                            hasCheckBoxColumn = false,
                            id = this.Id,
                            data = this.Data,
                            IdColumn = this.KeyColumnName,
                            Columns = columns,
                            //Title = ApplicationStrings.ACCESS000005,
                            //close = ApplicationStrings.LBL000011,
                            //open = ApplicationStrings.ACCESS000002,
                            //CloseTitle = ApplicationStrings.ACCESS000006
                        });
            }
            else
            {
                options = JsonConvert.SerializeObject(new
                {
                    id = this.Id,
                    //Title = ApplicationStrings.ACCESS000005,
                    //close = ApplicationStrings.LBL000011,
                    //open = ApplicationStrings.ACCESS000002,
                    //CloseTitle = ApplicationStrings.ACCESS000006
                });
            }

            writer.WriteLine("$('#{0}').treegrid({1});", this.Id, options);
        }
    }
}
