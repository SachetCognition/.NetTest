// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompositeDateViewModel.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 01/07/2014
//   Author:  Sharma Siddharth (54626)
//   Description: This declares Composite Date View Model to be used as a template.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDate
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear;
    using Equant.SAV2000.ComponentLibrary.MVC.Validators;

    /// <summary>
    /// The composite date view model.
    /// </summary>
    [Serializable]
    public class CompositeDateViewModel
    {
        /// <summary>
        /// Gets or sets the date label.
        /// </summary>
        public string DateLabel { get; set; }

        /// <summary>
        /// Gets or sets the first date.
        /// </summary>
        [DateConditionalRequired("SecondDate",  ErrorMessageResourceType = typeof(ApplicationStrings), 
            ErrorMessageResourceName = "ERR_DATE_OBLIGATOIRE")]
        public DateTimeWithFormat FirstDate { get; set; }

        /// <summary>
        /// Gets or sets the second date.
        /// </summary>
        [DateConditionalRequired("FirstDate",  DateTypePropertyName = "DateTypesSelectedValue", 
            ErrorMessageResourceType = typeof(ApplicationStrings), 
            ErrorMessageResourceName = "ERR_DATE_OBLIGATOIRE")]
        [EndDateGreaterThan("FirstDate",  "End Date must be greater than Start Date")]
        public DateTimeWithFormat SecondDate { get; set; }

        /// <summary>
        /// Gets or sets the week value.
        /// </summary>
        [WeekConditionalRequired("SecondWeek", "secondWeekId", ErrorMessageResourceType = typeof(ApplicationStrings), 
            ErrorMessageResourceName = "MSG000509")]
        public WeekYearWithFormat FirstWeek { get; set; }

        /// <summary>
        /// Gets or sets the week value.
        /// </summary>
        [EndWeekGreaterThan("FirstWeek", "firstWeekId", ErrorMessageResourceType = typeof(ApplicationStrings), ErrorMessageResourceName = "MSG000467")]
        [WeekConditionalRequired("FirstWeek", "firstWeekId", DateTypePropertyName = "DateTypesSelectedValue",
        ErrorMessageResourceType = typeof(ApplicationStrings), 
        ErrorMessageResourceName = "MSG000509")]
        public WeekYearWithFormat SecondWeek { get; set; }

        /// <summary>
        /// Gets or sets the date types selected value.
        /// </summary>
        public string DateTypesSelectedValue { get; set; }

        /// <summary>
        /// Gets or sets the date types.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification =
            "TETHYS: This input is required."), 
        System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", 
            Justification = "TETHYS: This property is to be set in the controller. It cannot be readonly")]
        public List<EnumDateTypes> DateTypes { get; set; }
    }
}
