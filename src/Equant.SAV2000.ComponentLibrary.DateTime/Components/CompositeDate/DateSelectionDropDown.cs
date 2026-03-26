namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDate
{
    using System.Collections.Generic;
    using Equant.SAV2000.ComponentLibrary.Common.Resources;

    public class DateSelectionDropDown
    {
        public string Value { get; set; }
        public string Text { get; set; }

        public static List<DateSelectionDropDown> GetAllDateTypes()
        {
            return new List<DateSelectionDropDown>
            {
                new DateSelectionDropDown { Value = ((int)EnumDateTypes.Between).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_ENTRE },
                new DateSelectionDropDown { Value = ((int)EnumDateTypes.Equal).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_EGALE },
                new DateSelectionDropDown { Value = ((int)EnumDateTypes.GreaterThan).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_SUPERIEUR },
                new DateSelectionDropDown { Value = ((int)EnumDateTypes.LessThan).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_INFERIEUR },
                new DateSelectionDropDown { Value = ((int)EnumDateTypes.LessThanOrEqual).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_INFERIEUROUEGAL },
                new DateSelectionDropDown { Value = ((int)EnumDateTypes.GreaterThanOrEqual).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_SUPERIEUROUEGAL },
                new DateSelectionDropDown { Value = ((int)EnumDateTypes.Empty).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_VIDE },
                new DateSelectionDropDown { Value = ((int)EnumDateTypes.EqualCurrentDate).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_DATECOURANTE },
                new DateSelectionDropDown { Value = ((int)EnumDateTypes.ModelBetween).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_MODELE_ENTRE },
                new DateSelectionDropDown { Value = ((int)EnumDateTypes.ModelEqual).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_MODELE_EGALE },
                new DateSelectionDropDown { Value = ((int)EnumDateTypes.ModelGreaterThan).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_MODELE_SUPERIEUR },
                new DateSelectionDropDown { Value = ((int)EnumDateTypes.ModelGreaterThanOrEqual).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_MODELE_SUPERIEUROUEGAL },
                new DateSelectionDropDown { Value = ((int)EnumDateTypes.ModelLessThan).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_MODELE_INFERIEUR },
                new DateSelectionDropDown { Value = ((int)EnumDateTypes.ModelLessThanOrEqual).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_MODELE_INFERIEUROUEGAL },
                new DateSelectionDropDown { Value = ((int)EnumDateTypes.Week).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_SEMAINE },
                new DateSelectionDropDown { Value = ((int)EnumDateTypes.WeekBetween).ToString(), Text = ApplicationStrings.TPOAR02F02T77CE11_SEMAINE_ENTRE }
            };
        }
    }
}
