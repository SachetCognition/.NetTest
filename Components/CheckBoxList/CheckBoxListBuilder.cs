// -------------------------------------------------------------------------------------------------
// <copyright file="CheckBoxListBuilder.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//    
//    Creation Date: 24/06/2014
//    Author:  Joshi Mukesh 
//    Legacy mapping:  
//    Description: 
// </summary>
// -------------------------------------------------------------------------------------------------
namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBoxList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.SpanLabel;

    /// <summary>
    /// CheckBox list builder.
    /// </summary>
    public class CheckBoxListBuilder : ComponentBuilderBase<CheckBoxListComponent, CheckBoxListBuilder>
    {
                /// <summary>
        /// Initializes a new instance of the <see cref="CheckBoxListBuilder"/> class.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        /// <param name="modelMetadata"></param>
        public CheckBoxListBuilder(CheckBoxListComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
            this.spanLabelBuilder = new SpanLabelBuilder(this.Component.CheckBoxListLabel, modelMetadata);
        }

        /// <summary>
        /// spanLabelBuilder builder
        /// </summary>
        private readonly SpanLabelBuilder spanLabelBuilder;

        /// <summary>
        /// The method to set CSS Class for the fieldSet.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CheckBoxListBuilder CssClassFieldSet(string value)
        {
            Component.CssClassFieldSet = value;
            return this;
        }

        /// <summary>
        /// The method to set CSS Class for the component.
        /// </summary>
        /// <param name="value">
        /// CSS for checkbox.
        /// </param>
        /// <returns>
        /// The <see cref="CheckBoxListBuilder"/>.
        /// </returns>
        public CheckBoxListBuilder CssClass(string value)
        {
            Component.CssClass = value;
            return this;
        }

        /// <summary>
        /// The method to set CSS Class for the component when it is disabled.
        /// </summary>
        /// <param name="value">
        /// CSS for checkbox.
        /// </param>
        /// <returns>
        /// The <see cref="CheckBoxListBuilder"/>.
        /// </returns>
        public CheckBoxListBuilder CssClassDisabled(string value)
        {
            Component.CssClassDisabled = value;
            return this;
        }

        /// <summary>
        /// The method to set click event for checkbox.
        /// </summary>
        /// <param name="value">
        /// Click Event for checkbox.
        /// </param>
        /// <returns>
        /// The <see cref="CheckBoxListBuilder"/>.
        /// </returns>
        public CheckBoxListBuilder OnClick(string value)
        {
            Component.OnClick = value;
            return this;
        }

        /// <summary>
        /// The method to set change event for checkbox.
        /// </summary>
        /// <param name="value">
        /// Change Event for checkbox.
        /// </param>
        /// <returns>
        /// The <see cref="CheckBoxListBuilder"/>.
        /// </returns>
        public CheckBoxListBuilder OnChange(string value)
        {
            Component.OnChange = value;
            return this;
        }

        /// <summary>
        /// The method to set disabled status for checkbox.
        /// </summary>
        /// <param name="value">
        /// Disabled status for checkbox.
        /// </param>
        /// <returns>
        /// The <see cref="CheckBoxListBuilder"/>.
        /// </returns>
        public CheckBoxListBuilder Disabled(bool value)
        {
            Component.IsDisabled = value;
            return this;
        }

        /// <summary>
        /// The method to set title for checkbox.
        /// </summary>
        /// <param name="value">
        /// Title text for checkbox.
        /// </param>
        /// <returns>
        /// The <see cref="CheckBoxListBuilder"/>.
        /// </returns>
        public CheckBoxListBuilder Title(string value)
        {
            Component.Title = value;
            return this;
        }

        /// <summary>
        /// The method to set outer div for field-set.
        /// </summary>
        /// <param name="value">
        /// outer div creation for field-set.
        /// </param>
        /// <returns>
        /// The <see cref="CheckBoxListBuilder"/>.
        /// </returns>
        public CheckBoxListBuilder IsOuterDivNeeded(bool value)
        {
            Component.IsOuterDivNeeded = value;
            return this;
        }
        /// <summary>
        /// The method to set CSS for outer div for field-set.
        /// </summary>
        /// <param name="value">
        /// outer div creation for field-set.
        /// </param>
        /// <returns>
        /// The <see cref="CheckBoxListBuilder"/>.
        /// </returns>
        public CheckBoxListBuilder CssClassOuterDiv(string value)
        {
            Component.CssClassOuterDiv = value;
            return this;
        }
        /// <summary>
        ///     The custom Label.
        /// </summary>
        /// <param name="setup">
        ///     The setup.
        /// </param>
        /// <returns>
        ///     The <see cref="CheckBoxListBuilder" />.
        /// </returns>
        public CheckBoxListBuilder SpanLabel(Action<SpanLabelBuilder> setup)
        {
            if (setup != null)
            {
                setup(this.spanLabelBuilder);
            }

            return this;
        }


        /// <summary>
        /// The method to set CSS Class for the checkbox div.
        /// </summary>
        /// <param name="value">
        /// CSS for checkbox.
        /// </param>
        /// <returns>
        /// The <see cref="CheckBoxListBuilder"/>.
        /// </returns>
        public CheckBoxListBuilder CssClassCheckBoxDiv(string value)
        {
            Component.CssClassCheckBoxDiv = value;
            return this;
        }


        /// <summary>
        ///     The data bind.
        /// </summary>
        /// <param name="dataSource">
        ///     The data source containing the list of items to populate in the check box list.
        /// </param>
        /// <param name="selectedList">The list of selected items</param>
        /// <param name="toOrder">If true, the selected items will appear first sorted by the Text and the non-selected items will appear next, also sorted
        /// by the Text</param>
        /// <returns>
        ///     The <see cref="CheckBoxListBuilder" />.
        /// </returns>
        public CheckBoxListBuilder DataBind(IEnumerable<CheckBoxListItem> dataSource, IEnumerable<string> selectedList, bool toOrder)
        {
            if (dataSource != null)
            {
                this.Component.SourceItems.Clear();
                this.Component.SourceItems.AddRange(dataSource);
                if (selectedList != null)
                {
                    // For each selected checkbox (value is in selectedList), set the "selected" property of the datasource to true
                    this.Component.SourceItems.Where(y => selectedList.Contains(y.Value)).ToList().ForEach(x => x.Selected = true);
                }

                if (toOrder)
                {
                    // If the CheckBoxList has to be ordered, order it. All selected values appear first, ordered by Text.
                    // All non-selected items appear next, also ordered by Text
                    this.Component.SourceItems = this.Component.SourceItems.OrderBy(o => !o.Selected).ThenBy(o => o.Text).ToList();
                }
            }

            return this;
        }
    }
}
