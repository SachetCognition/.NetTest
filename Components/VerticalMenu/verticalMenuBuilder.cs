// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerticalMenuBuilder.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 26/11/2014
//   Author:  Seema Lal Gulabrani
//   Description: The image tool tip builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.VerticalMenu
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.Common.Components.DataTables;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Image;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Menu;

    /// <summary>
    /// The menu builder.
    /// </summary>
    public class VerticalMenuBuilder:ComponentBuilderBase<VerticalMenuComponent, VerticalMenuBuilder>
    {
        /// <summary>
        ///     The image tool tip builder.
        /// </summary>
        private readonly ImageBuilder copyRightImgBuilder;

        /// <summary>
        /// The image tool tip.
        /// </summary>
        /// <param name="setup">
        /// The setup.
        /// </param>
        /// <returns>
        /// The <see cref="VerticalMenuBuilder"/>.
        /// </returns>
        public VerticalMenuBuilder Image(Action<ImageBuilder> setup)
        {
            if (setup != null)
            {
                setup(this.copyRightImgBuilder);
            }

            return this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalMenuBuilder"/> class.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        /// <param name="modelMetadata">
        /// </param>
        public VerticalMenuBuilder(VerticalMenuComponent component, ModelMetadata? modelMetadata)
            : base(component, modelMetadata)
        {
            this.copyRightImgBuilder = new ImageBuilder(this.Component.CopyRightImage, modelMetadata);
        }

        /// <summary>
        /// The data bind.
        /// </summary>
        /// <param name="dataSource">
        /// The data source.
        /// </param>
        /// <returns>
        /// The <see cref="VerticalMenuBuilder"/>.
        /// </returns>
        public VerticalMenuBuilder DataBind(IEnumerable<MenuItem> dataSource)
        {
            if (dataSource != null)
            {
                this.Component.MenuItems.Clear();
                foreach (var item in dataSource)
                {
                    this.Component.MenuItems.Add(item);
                }
            }

            return this;
        }

        /// <summary>
        /// The copy right text.
        /// </summary>
        /// <param name="copyRightTxt">
        /// The copy right text parameter.
        /// </param>
        /// <returns>
        /// The <see cref="VerticalMenuBuilder"/>.
        /// </returns>
        public VerticalMenuBuilder CopyRightText(string copyRightTxt)
        {
            this.Component.CopyRightText = copyRightTxt;
            return this;
        }

        /// <summary>
        /// The method to set selectedMenuName for component.
        /// </summary>
        /// <param name="value">
        /// Selected Menu name for Vertical Menu
        /// </param>
        /// <returns>
        /// The <see cref="VerticalMenuBuilder"/>.
        /// </returns>
        public VerticalMenuBuilder SelectedMenu(string value)
        {
            this.Component.SelectedMenu = value;
            return this;
        }


        /// <summary>
        /// Set cause validation properties to decide if validation need to be raise or not. 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public VerticalMenuBuilder CauseValidation(bool value)
        {
            this.Component.CauseValidation = value;

            return this;
        }
    }
}
