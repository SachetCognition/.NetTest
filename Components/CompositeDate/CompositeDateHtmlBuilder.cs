// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompositeDateHtmlBuilder.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 09/06/2014
//   Author:  Sharma Siddharth (54626)
//   Description: Composite DateTime Html builder
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDate
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Label;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;
    using Equant.SAV2000.ComponentLibrary.MVC.Helpers;

    /// <summary>
    /// The composite date html builder.
    /// </summary>
    public class CompositeDateHtmlBuilder : HtmlBuilderBase<CompositeDateComponent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeDateHtmlBuilder"/> class.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        public CompositeDateHtmlBuilder(CompositeDateComponent component)
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

            if (!this.Component.IsVisible)
            {
                return;
            }

            #region Check Selected Date Types value

            EnumDateTypes parsedDateType;
            var isDateType = Enum.TryParse(this.Component.DateTypesValue, true, out parsedDateType);
            if (!isDateType)
            {
                throw new ArgumentException("The given selected DateTypesValue :".AppendWithBuilder(this.Component.DateTypesValue,
                    "is not a valid EnumDateTypes"));
            }

            var isSelectedDateTypeInGivenList = this.Component.DateTypesIncluded.Contains(parsedDateType);
            if (!isSelectedDateTypeInGivenList)
            {
                throw new ArgumentException("The given selected DateTypesValue :".AppendWithBuilder(this.Component.DateTypesValue,
                    " is not in the given DateTypesIncluded list"));
            }

            #endregion
            var tagBuilderMainDiv = new TagBuilder("div");
            if (!string.IsNullOrEmpty(this.Component.CssMainDiv))
            {
                tagBuilderMainDiv.AddCssClass(this.Component.CssMainDiv);
            }

            tagBuilderMainDiv.MergeAttribute("id", this.Component.Id);

            #region Dates and Week to Render or not to Render
            this.Component.DatesToRender = this.CheckWhichDateToRender();
            this.Component.WeeksToRender = this.CheckWhichWeekToRender();
            this.Component.RenderAndLabel = RenderAndLabel(this.Component.DatesToRender, this.Component.WeeksToRender);
            #endregion

            #region Dates and Week to Hide or Show
            var weeksToShow = CheckWhichWeekToShow(parsedDateType);
            var datesToShow = CheckWhichDateToShow(parsedDateType);
            var showAndLabel = ShowAndLabel(datesToShow, weeksToShow);
            #endregion

            //check consistency of date with provided values.
            this.ConsistencyCheckOfProvidedValues(datesToShow, weeksToShow);

            //set associated control id for label to first date textbox
            this.Component.CustomLabel.AssociatedControlId = this.GetForAttribute(datesToShow, weeksToShow);
            var tagBuilderLabelDiv = new TagBuilder("div") { InnerHtml = this.Component.CustomLabel.ToHtmlString() };

            if (!string.IsNullOrEmpty(this.Component.CssClassLabelDiv))
            {
                tagBuilderLabelDiv.AddCssClass(this.Component.CssClassLabelDiv);
            }

            var sbTagMainDivInnerHtml = new StringBuilder(tagBuilderLabelDiv.ToString());

            //add dropdown list here
            sbTagMainDivInnerHtml.Append(this.DateTypesHtml());

            if (this.Component.DatesToRender != DateRenderer.SkipBothDates)
            {
                //Set remaining properties for first Date and render it
                this.Component.FirstDateBuilder.DisplayTime(this.Component.DisplayTime)
                    .DisplayEraseButton(this.Component.DisplayEraseButton)
                    .DisplayInformationIcon(false)
                    .StartFromCurrentDate(false)
                    .AssociatedDateHtmlId(this.Component.SecondDate.Id)
                    .CssClassDateDiv(this.Component.CssClassFirstDateDateDiv)
                    .CssMainDiv(this.Component.CssFirstDateMainDiv)
                    .OnDateChange(this.Component.OnFirstDateChange).ExternalLabelText(this.Component.CustomLabel.Text);
                if (datesToShow == DateVisibility.HideBothDates)
                {
                    this.Component.FirstDateBuilder.CssMainDiv("displaynone");
                }

                sbTagMainDivInnerHtml.Append(this.Component.FirstDate.ToHtmlString());
            }

            if (this.Component.WeeksToRender != WeekRenderer.SkipBothWeeks)
            {
                // set remaining properties for first week and render it
                this.Component.FirstWeekBuilder.DisplayInformationIcon(false)
                    .AssociatedWeekYearHtmlId(this.Component.SecondWeek.Id)
                    .CssClassWeekDiv(this.Component.CssClassFirstWeekWeekDiv)
                    .CssMainDiv(this.Component.CssFirstWeekMainDiv)
                    .OnWeekChange(this.Component.OnFirstWeekChange);
                if (weeksToShow == WeekVisibility.HideBothWeeks)
                {
                    this.Component.FirstWeekBuilder.CssMainDiv("displaynone");
                }

                sbTagMainDivInnerHtml.Append(this.Component.FirstWeek.ToHtmlString());
            }

            if (this.Component.RenderAndLabel)
            {
                if (this.Component.CssClassAndLabel.IsNull())
                {
                    this.Component.CssClassAndLabel = "andLabel";
                }

                if (!showAndLabel)
                {
                    this.Component.CssClassAndLabel = "displaynone ".AppendWithBuilder(this.Component.CssClassAndLabel);
                }

                new LabelBuilder(this.Component.AndLabel, this.Component.AndLabel.ModelMetadata).HtmlAttributes(new { Id = this.Component.AndLabelId })
                    .Text(ApplicationStrings.LBL000385)
                    .CssClassLabel(this.Component.CssClassAndLabel);
                sbTagMainDivInnerHtml.Append(this.Component.AndLabel.ToHtmlString());
            }

            if (this.Component.DatesToRender == DateRenderer.RenderBothDates)
            {
                //Set remaining properties for second Date and render it
                this.Component.SecondDateBuilder.DisplayTime(this.Component.DisplayTime)
                    .DisplayEraseButton(this.Component.DisplayEraseButton)
                    .DisplayInformationIcon(false)
                    .StartFromCurrentDate(false)
                    .AssociatedDateHtmlId(this.Component.FirstDate.Id)
                    .CssClassDateDiv(this.Component.CssClassSecondDateDateDiv)
                    .CssMainDiv(this.Component.CssSecondDateMainDiv)
                    .OnDateChange(this.Component.OnSecondDateChange).ExternalLabelText(this.Component.CustomLabel.Text);

                if (datesToShow == DateVisibility.HideBothDates || datesToShow == DateVisibility.HideSecondDate)
                {
                    this.Component.SecondDateBuilder.CssMainDiv("displaynone");
                }

                sbTagMainDivInnerHtml.Append(this.Component.SecondDate.ToHtmlString());
            }

            if (this.Component.WeeksToRender == WeekRenderer.RenderBothWeeks)
            {
                // set remaining properties for second week and render it
                this.Component.SecondWeekBuilder.DisplayInformationIcon(false)
                    .AssociatedWeekYearHtmlId(this.Component.FirstWeek.Id)
                    .CssClassWeekDiv(this.Component.CssClassSecondWeekWeekDiv)
                    .CssMainDiv(this.Component.CssSecondWeekMainDiv)
                    .OnWeekChange(this.Component.OnSecondWeekChange);
                if (weeksToShow == WeekVisibility.HideSecondWeek || weeksToShow == WeekVisibility.HideBothWeeks)
                {
                    this.Component.SecondWeekBuilder.CssMainDiv("displaynone");
                }

                sbTagMainDivInnerHtml.Append(this.Component.SecondWeek.ToHtmlString());
            }

            //add information icon
            if (!string.IsNullOrEmpty(this.Component.InformationIcon.Text))
            {
                this.Component.InformationIcon.Name = this.Component.Id.AppendWithBuilder("Name", "InformationIcon");
                this.Component.InformationIcon.Id = this.Component.Id.AppendWithBuilder("InformationIcon");
                this.Component.InformationIcon.ToolTipId = this.Component.Id.AppendWithBuilder("InformationIconToolTip");
                sbTagMainDivInnerHtml.Append(this.Component.InformationIcon.ToHtmlString());
            }

            tagBuilderMainDiv.InnerHtml = sbTagMainDivInnerHtml.ToString();
            writer.Write(tagBuilderMainDiv.ToString());
        }

        /// <summary>
        /// This detects which of the two weeks to render.
        /// </summary>
        /// <returns>
        /// The <see cref="WeekRenderer"/>.
        /// </returns>
        private WeekRenderer CheckWhichWeekToRender()
        {
            var doubleWeekTypes = DoubleWeekTypes();
            var renderSecondWeek = this.Component.DateTypesIncluded.Any(x => doubleWeekTypes.Any(y => y == x));

            if (renderSecondWeek)
            {
                return WeekRenderer.RenderBothWeeks;
            }

            var singleWeekTypes = SingleWeekTypes();
            var renderFirstDate = this.Component.DateTypesIncluded.Any(x => singleWeekTypes.Any(y => y == x));

            if (renderFirstDate)
            {
                return WeekRenderer.SkipSecondWeek;
            }

            return WeekRenderer.SkipBothWeeks;
        }

        /// <summary>
        /// This detects which of the two dates to render.
        /// </summary>
        /// <returns>
        /// The <see cref="DateRenderer"/>.
        /// </returns>
        private DateRenderer CheckWhichDateToRender()
        {
            var doubleDateTypes = DoubleDateTypes();
            var renderSecondDate = this.Component.DateTypesIncluded.Any(x => doubleDateTypes.Any(y => y == x));

            if (renderSecondDate)
            {
                return DateRenderer.RenderBothDates;
            }

            var singleDateTypes = SingleDateTypes();
            var renderFirstDate = this.Component.DateTypesIncluded.Any(x => singleDateTypes.Any(y => y == x));

            if (renderFirstDate)
            {
                return DateRenderer.SkipSecondDate;
            }

            return DateRenderer.SkipBothDates;
        }

        /// <summary>
        /// This decides whether to render the and label.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool RenderAndLabel(DateRenderer dateToRender, WeekRenderer weekToRender)
        {
            if ((dateToRender == DateRenderer.RenderBothDates) || (weekToRender == WeekRenderer.RenderBothWeeks))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// This detects which of the two weeks to display.
        /// </summary>
        /// <returns>
        /// The <see cref="WeekVisibility"/>.
        /// </returns>
        private static WeekVisibility CheckWhichWeekToShow(EnumDateTypes parsedDateType)
        {
            var singleWeekTypes = SingleWeekTypes();
            var singleWeekTypesIsSelected = singleWeekTypes.Contains(parsedDateType);
            if (singleWeekTypesIsSelected)
            {
                return WeekVisibility.HideSecondWeek;
            }

            var doubleWeekTypes = DoubleWeekTypes();
            var doubleWeekTypesIsSelected = doubleWeekTypes.Contains(parsedDateType);
            if (doubleWeekTypesIsSelected)
            {
                return WeekVisibility.ShowBothWeeks;
            }

            return WeekVisibility.HideBothWeeks;
        }

        /// <summary>
        /// This detects which of the two dates to display.
        /// </summary>
        /// <returns>
        /// The <see cref="DateVisibility"/>.
        /// </returns>
        private static DateVisibility CheckWhichDateToShow(EnumDateTypes parsedDateType)
        {
            var singleDateTypes = SingleDateTypes();
            var singleDateTypesIsSelected = singleDateTypes.Contains(parsedDateType);
            if (singleDateTypesIsSelected)
            {
                return DateVisibility.HideSecondDate;
            }

            var doubleDateTypes = DoubleDateTypes();
            var doubleDateTypesIsSelected = doubleDateTypes.Contains(parsedDateType);
            if (doubleDateTypesIsSelected)
            {
                return DateVisibility.ShowBothDates;
            }

            return DateVisibility.HideBothDates;
        }

        /// <summary>
        /// This detects if the and label shall be displayed or not.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool ShowAndLabel(DateVisibility dateToShow, WeekVisibility weekToShow)
        {
            return (dateToShow == DateVisibility.ShowBothDates) || (weekToShow == WeekVisibility.ShowBothWeeks);
        }

        /// <summary>
        /// The get for attribute.
        /// </summary>
        /// <param name="dateToShow">
        /// The date visibility.
        /// </param>
        /// <param name="weekToShow">
        /// The week visibility.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetForAttribute(DateVisibility dateToShow, WeekVisibility weekToShow)
        {
            if (dateToShow == DateVisibility.HideBothDates && weekToShow == WeekVisibility.HideBothWeeks)
            {
                return this.Component.DateTypesId;
            }

            return dateToShow != DateVisibility.HideBothDates ? this.Component.FirstDate.GetDateTextId : this.Component.FirstWeek.WeekTextId;
        }

        /// <summary>
        /// This checks for consistency of provided date and week values with the provided dates.
        /// </summary>
        /// <param name="dateToShow">
        /// The date visibility.
        /// </param>
        /// <param name="weekToShow">
        /// The week visibility.
        /// </param>
        private void ConsistencyCheckOfProvidedValues(DateVisibility dateToShow, WeekVisibility weekToShow)
        {
            if (dateToShow == DateVisibility.HideBothDates)
            {
                if (!this.Component.SecondDate.Value.IsEmpty || !this.Component.FirstDate.Value.IsEmpty)
                {
                    throw new ArgumentException(
                        "The value provided for both date and format should be empty for the selected date type value:".AppendWithBuilder(
                            this.Component.DateTypesValue));
                }
            }

            if (dateToShow == DateVisibility.HideSecondDate)
            {
                if (!this.Component.SecondDate.Value.IsEmpty)
                {
                    throw new ArgumentException(
                        "The value provided for second date and format should be empty for the selected date type value:".AppendWithBuilder(
                            this.Component.DateTypesValue));
                }
            }

            if (weekToShow == WeekVisibility.HideBothWeeks)
            {
                if (!this.Component.SecondWeek.Value.IsEmpty || !this.Component.FirstWeek.Value.IsEmpty)
                {
                    throw new ArgumentException(
                        "The value provided for both week and format should be empty for the selected date type value:".AppendWithBuilder(
                            this.Component.DateTypesValue));
                }
            }

            if (weekToShow == WeekVisibility.HideSecondWeek)
            {
                if (!this.Component.SecondWeek.Value.IsEmpty)
                {
                    throw new ArgumentException(
                        "The value provided for second week and format should be empty for the selected date type value:".AppendWithBuilder(
                            this.Component.DateTypesValue));
                }
            }

            //check if the provided date values are of model type and selected type is also a model type
            this.DateTypeSameAsModel();
        }

        /// <summary>
        /// This checks if the date type to be selected and those of the date format match.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// </exception>
        private void DateTypeSameAsModel()
        {
            var standardTypes = DateComponentHelper.StandardDateTypes();

            if (standardTypes.Contains(this.Component.DateTypesValue))
            {
                if (this.Component.FirstDateAndFormat.IsModel || this.Component.SecondDateAndFormat.IsModel)
                {
                    throw new ArgumentException("The selected DateTypesValue is not Model but one or both of the provided dates are of Model Type");
                }
            }

            var modelTypes = DateComponentHelper.ModelDateTypes();
            if (modelTypes.Contains(this.Component.DateTypesValue))
            {
                if (!this.Component.FirstDateAndFormat.IsModel || !this.Component.SecondDateAndFormat.IsModel)
                {
                    throw new ArgumentException("The selected DateTypesValue is Model but the one or both of the provided dates are of non-Model Type");
                }
            }
        }

        /// <summary>
        /// The date types html.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        private string DateTypesHtml()
        {
            if (this.Component.DateTypesIncluded == null || this.Component.DateTypesIncluded.Count == 0)
            {
                throw new ArgumentException("DateTypesIncluded cannot be null or empty");
            }

            if (string.IsNullOrEmpty(this.Component.DateTypesName))
            {
                throw new ArgumentException("DateTypesName cannot be null or empty");
            }

            if (this.Component.DateTypesValue.IsNull())
            {
                throw new ArgumentException("DateTypesValue cannot be null");
            }
            var ddlDropDownDateTypesId = this.Component.DateTypesId;
            var dateTypes = this.GetDateTypeItems();
            var dateTypesSelectList = new SelectList(dateTypes, "Value", "Text", this.Component.DateTypesValue);
            var accessText = string.Format(CultureInfo.CurrentCulture, ApplicationStrings.ACCESS000007, this.Component.CustomLabel.Text);
            new DropDownListBuilder(this.Component.DropDownDateTypes, this.Component.DropDownDateTypes.ModelMetadata).Name(this.Component.DateTypesName)
                                                                     .Id(this.Component.DateTypesId)
                                                                     .DataBind(dateTypesSelectList)
                                                                     .CssClassSelectDiv(this.Component.CssClassDateTypesSelectDiv)
                                                                     .CustomLabel(m => m.Text(accessText)
                                                                     .AssociatedControlId(this.Component.DateTypesId)
                                                                     .HtmlAttributes(new { Id = ddlDropDownDateTypesId + "lbl" })
                                                                     .IsOnlyForAccess(true));

            return this.Component.DropDownDateTypes.ToHtmlString();
        }

        /// <summary>
        /// This adds date type item.
        /// </summary>
        /// <returns>
        /// The <see>
        ///         <cref>List</cref>
        ///     </see>
        ///     .
        /// </returns>
        private IEnumerable<SelectListItem> GetDateTypeItems()
        {
            var dateTypes = new List<SelectListItem>();
            this.CheckDateTypesForNullAndDuplicates();
            var textForDateTypes = DateComponentHelper.TextValuesForDateTypes();

            foreach (var dateTypeItem in this.Component.DateTypesIncluded)
            {
                switch (dateTypeItem)
                {
                    case EnumDateTypes.Between:
                        dateTypes.Add(new SelectListItem
                        {
                            Text = textForDateTypes[EnumDateTypes.Between],
                            Value = EnumDateTypes.Between.ToString()
                        });
                        break;
                    case EnumDateTypes.Empty:
                        dateTypes.Add(new SelectListItem
                        {
                            Text = textForDateTypes[EnumDateTypes.Empty],
                            Value = EnumDateTypes.Empty.ToString()
                        });
                        break;
                    case EnumDateTypes.Equal:
                        dateTypes.Add(new SelectListItem
                        {
                            Text = textForDateTypes[EnumDateTypes.Equal],
                            Value = EnumDateTypes.Equal.ToString()
                        });
                        break;
                    case EnumDateTypes.EqualCurrentDate:
                        dateTypes.Add(new SelectListItem
                        {
                            Text = textForDateTypes[EnumDateTypes.EqualCurrentDate],
                            Value = EnumDateTypes.EqualCurrentDate.ToString()
                        });
                        break;
                    case EnumDateTypes.LessThan:
                        dateTypes.Add(new SelectListItem
                        {
                            Text = textForDateTypes[EnumDateTypes.LessThan],
                            Value = EnumDateTypes.LessThan.ToString()
                        });
                        break;
                    case EnumDateTypes.GreaterThan:
                        dateTypes.Add(new SelectListItem
                        {
                            Text = textForDateTypes[EnumDateTypes.GreaterThan],
                            Value = EnumDateTypes.GreaterThan.ToString()
                        });
                        break;
                    case EnumDateTypes.LessThanOrEqual:
                        dateTypes.Add(new SelectListItem
                        {
                            Text = textForDateTypes[EnumDateTypes.LessThanOrEqual],
                            Value = EnumDateTypes.LessThanOrEqual.ToString()
                        });
                        break;
                    case EnumDateTypes.ModelBetween:
                        dateTypes.Add(new SelectListItem
                        {
                            Text = textForDateTypes[EnumDateTypes.ModelBetween],
                            Value = EnumDateTypes.ModelBetween.ToString()
                        });
                        break;
                    case EnumDateTypes.ModelEqual:
                        dateTypes.Add(new SelectListItem
                        {
                            Text = textForDateTypes[EnumDateTypes.ModelEqual],
                            Value = EnumDateTypes.ModelEqual.ToString()
                        });
                        break;
                    case EnumDateTypes.ModelGreaterThan:
                        dateTypes.Add(new SelectListItem
                        {
                            Text = textForDateTypes[EnumDateTypes.ModelGreaterThan],
                            Value = EnumDateTypes.ModelGreaterThan.ToString()
                        });
                        break;
                    case EnumDateTypes.ModelGreaterThanOrEqual:
                        dateTypes.Add(new SelectListItem
                        {
                            Text = textForDateTypes[EnumDateTypes.ModelGreaterThanOrEqual],
                            Value = EnumDateTypes.ModelGreaterThanOrEqual.ToString()
                        });
                        break;
                    case EnumDateTypes.ModelLessThan:
                        dateTypes.Add(new SelectListItem
                        {
                            Text = textForDateTypes[EnumDateTypes.ModelLessThan],
                            Value = EnumDateTypes.ModelLessThan.ToString()
                        });
                        break;
                    case EnumDateTypes.ModelLessThanOrEqual:
                        dateTypes.Add(new SelectListItem
                        {
                            Text = textForDateTypes[EnumDateTypes.ModelLessThanOrEqual],
                            Value = EnumDateTypes.ModelLessThanOrEqual.ToString()
                        });
                        break;
                    case EnumDateTypes.GreaternThanOrEqual:
                        dateTypes.Add(new SelectListItem
                        {
                            Text = textForDateTypes[EnumDateTypes.GreaternThanOrEqual],
                            Value = EnumDateTypes.GreaternThanOrEqual.ToString()
                        });
                        break;
                    case EnumDateTypes.Week:
                        dateTypes.Add(new SelectListItem
                        {
                            Text = textForDateTypes[EnumDateTypes.Week],
                            Value = EnumDateTypes.Week.ToString()
                        });
                        break;
                    case EnumDateTypes.WeekBetween:
                        dateTypes.Add(new SelectListItem
                        {
                            Text = textForDateTypes[EnumDateTypes.WeekBetween],
                            Value = EnumDateTypes.WeekBetween.ToString()
                        });
                        break;
                }
            }

            return dateTypes;
        }

        /// <summary>
        /// This checks date types for null and duplicates.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// </exception>
        private void CheckDateTypesForNullAndDuplicates()
        {
            if (this.Component.DateTypesIncluded == null || this.Component.DateTypesIncluded.Count == 0)
            {
                throw new ArgumentException("The provided DateTypes is empty");
            }

            var checkForDuplicates = this.Component.DateTypesIncluded.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
            if (checkForDuplicates.Count >= 1)
            {
                throw new ArgumentException("The provided DateTypes have got duplicate values");
            }
        }

        /// <summary>
        /// This returns single date types.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable<EnumDateTypes>"/>.
        /// </returns>
        private static IEnumerable<EnumDateTypes> SingleDateTypes()
        {
            return new List<EnumDateTypes>
                   {
                       //Standard Dates
                       EnumDateTypes.Empty,
                       EnumDateTypes.Equal,
                       EnumDateTypes.GreaterThan,
                       EnumDateTypes.GreaternThanOrEqual,
                       EnumDateTypes.LessThan,
                       EnumDateTypes.LessThanOrEqual,
                       //Model Dates
                       EnumDateTypes.ModelEqual,
                       EnumDateTypes.ModelGreaterThan,
                       EnumDateTypes.ModelGreaterThanOrEqual,
                       EnumDateTypes.ModelLessThan,
                       EnumDateTypes.ModelLessThanOrEqual,
                   };
        }

        /// <summary>
        /// This returns double date types.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private static IEnumerable<EnumDateTypes> DoubleDateTypes()
        {
            return new List<EnumDateTypes>
                   {
                       //standard types
                       EnumDateTypes.Between,
                       //model types
                       EnumDateTypes.ModelBetween
                   };
        }

        /// <summary>
        /// This returns single week types.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private static IEnumerable<EnumDateTypes> SingleWeekTypes()
        {
            return new List<EnumDateTypes>
                   {
                       EnumDateTypes.Week
                   };
        }

        /// <summary>
        /// This returns double week types.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private static IEnumerable<EnumDateTypes> DoubleWeekTypes()
        {
            return new List<EnumDateTypes>
                   {
                       EnumDateTypes.WeekBetween
                   };
        }
    }
}
