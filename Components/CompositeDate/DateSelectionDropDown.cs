// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateSelectionDropDown.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 09/06/2014
//   Author:  Sharma Siddharth (54626)
//   Description: The possible drop down values for composite date control date selection. EnumDateTypes
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDate
{
    /// <summary>
    /// The possible drop down values for composite date control date selection.
    /// </summary>
    public enum EnumDateTypes
    {
        /// <summary>
        /// The less than.
        /// </summary>
        LessThan = 0,

        /// <summary>
        /// The more than.
        /// </summary>
        GreaterThan = 1, 

        /// <summary>
        /// The less than equal to.
        /// </summary>
        LessThanOrEqual = 2, 

        /// <summary>
        /// The more than equal to.
        /// </summary>
        GreaternThanOrEqual = 3, 

        /// <summary>
        /// The empty.
        /// </summary>
        Empty = 4, 

        /// <summary>
        /// The between.
        /// </summary>
        Between = 5, 

        /// <summary>
        /// The equal current date.
        /// </summary>
        EqualCurrentDate = 6, 

        /// <summary>
        /// The equal.
        /// </summary>
        Equal = 7, 

        /// <summary>
        /// The day.
        /// </summary>
        ModelLessThan = 8, 

        /// <summary>
        /// The model greater than.
        /// </summary>
        ModelGreaterThan = 9, 

        /// <summary>
        /// The model equal.
        /// </summary>
        ModelEqual = 10, 

        /// <summary>
        /// The model between from.
        /// </summary>
        ModelBetween = 11, 

        /// <summary>
        /// The week.
        /// </summary>
        Week = 12, 

        /// <summary>
        /// The week between.
        /// </summary>
        WeekBetween = 13, 

        /// <summary>
        /// The model less than or equal.
        /// </summary>
        ModelLessThanOrEqual = 14, 

        /// <summary>
        /// The model greater than or Equal.
        /// </summary>
        ModelGreaterThanOrEqual = 15
    }
}
