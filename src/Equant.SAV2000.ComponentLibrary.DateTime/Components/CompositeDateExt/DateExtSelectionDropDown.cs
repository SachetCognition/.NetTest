namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDateExt
{
    using System.Collections.Generic;
    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDate;

    public class DateExtSelectionDropDown
    {
        public string Value { get; set; }
        public string Text { get; set; }

        public static List<DateExtSelectionDropDown> GetAllDateExtTypes()
        {
            return new List<DateExtSelectionDropDown>
            {
                new DateExtSelectionDropDown { Value = ((int)EnumDateTypes.Between).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_ENTRE },
                new DateExtSelectionDropDown { Value = ((int)EnumDateTypes.Equal).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_EGALE },
                new DateExtSelectionDropDown { Value = ((int)EnumDateTypes.GreaterThan).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_SUPERIEUR },
                new DateExtSelectionDropDown { Value = ((int)EnumDateTypes.LessThan).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_INFERIEUR },
                new DateExtSelectionDropDown { Value = ((int)EnumDateTypes.LessThanOrEqual).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_INFERIEUROUEGAL },
                new DateExtSelectionDropDown { Value = ((int)EnumDateTypes.GreaterThanOrEqual).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_SUPERIEUROUEGAL },
                new DateExtSelectionDropDown { Value = ((int)EnumDateTypes.Empty).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_VIDE },
                new DateExtSelectionDropDown { Value = ((int)EnumDateTypes.EqualCurrentDate).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_DATECOURANTE },
                new DateExtSelectionDropDown { Value = ((int)EnumDateTypes.ModelBetween).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_MODELE_ENTRE },
                new DateExtSelectionDropDown { Value = ((int)EnumDateTypes.ModelEqual).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_MODELE_EGALE },
                new DateExtSelectionDropDown { Value = ((int)EnumDateTypes.ModelGreaterThan).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_MODELE_SUPERIEUR },
                new DateExtSelectionDropDown { Value = ((int)EnumDateTypes.ModelGreaterThanOrEqual).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_MODELE_SUPERIEUROUEGAL },
                new DateExtSelectionDropDown { Value = ((int)EnumDateTypes.ModelLessThan).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_MODELE_INFERIEUR },
                new DateExtSelectionDropDown { Value = ((int)EnumDateTypes.ModelLessThanOrEqual).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_MODELE_INFERIEUROUEGAL },
                new DateExtSelectionDropDown { Value = ((int)EnumDateTypes.Week).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_SEMAINE },
                new DateExtSelectionDropDown { Value = ((int)EnumDateTypes.WeekBetween).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_SEMAINE_ENTRE }
            };
        }
    }
}
