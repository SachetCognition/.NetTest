// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerticalMenuComponent.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 26/11/2014
//   Author:  Seema Lal Gulabrani
//   Description: The evrtical menu component
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.VerticalMenu
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.IO;

    using Equant.SAV2000.ComponentLibrary.Common.Components.DataTables;
    using Equant.SAV2000.ComponentLibrary.Common.Helper;
    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Image;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Menu;

    using Newtonsoft.Json;

    /// <summary>
    /// The menu component.
    /// </summary>
    public class VerticalMenuComponent : ComponentBase
    {
        /// <summary>
        /// the JS resources of the component
        /// </summary>
        private readonly ReadOnlyCollection<JsResource> jsResources;

        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalMenuComponent"/> class.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        public VerticalMenuComponent(IHtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            this.MenuItems = new List<MenuItemClass>();
            this.CopyRightImage = new ImageComponent(htmlHelper);
            var jsRes = new List<JsResource>
                                   {
                                       new JsResource(
                                           "JsVerticalMenu", 
                                           "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.VerticalMenu.js", 
                                           220, 
                                           typeof(VerticalMenuComponent))                                           
                                   };
            this.jsResources = new ReadOnlyCollection<JsResource>(jsRes);
        }
      
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
        /// Gets or sets the menu items information.
        /// </summary>
        internal List<VerticalMenuItemInfo> MenuItemsInformation{ get; set; }

        /// <summary>
        /// If true then raise client side validation otherwise not.
        /// </summary>
        public bool CauseValidation { get; set; }


        #region component properties
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        internal List<MenuItemClass> MenuItems { get; private set; }

        /// <summary>
        /// Gets the outer menu div CSS class.
        /// </summary>
        internal const string OuterMenuDivCssClass = "navbar";

        /// <summary>
        ///     The Image Tooltip component
        /// </summary>
        public ImageComponent CopyRightImage { get; private set; }

        /// <summary>
        /// Gets or sets the copy right text.
        /// </summary>
        public string CopyRightText { get; set; }
        #endregion

        #region Overrides of ComponentBase

        /// <summary>
        /// The write html.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public override void WriteHtml(TextWriter writer)
        {
            new VerticalMenuHtmlBuilder(this).Build(writer);
        }

        /// <summary>
        /// The write initialization script.
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

            string options =
                JsonConvert.SerializeObject(
                    new
                        {
                            MenuName = this.Name, accessTextOpen = ApplicationStrings.ACCESS000002, accessTextClose = ApplicationStrings.LBL000011,
                            LinkRenderOptions = this.MenuItemsInformation
                        });

            writer.WriteLine("$('#{0}').verticalMenu({1});", this.Id, options);
        }

        /// <summary>
        ///     The Selected Menu Name
        /// </summary>
        public string SelectedMenu { get; set; }

        #endregion
    }
}
