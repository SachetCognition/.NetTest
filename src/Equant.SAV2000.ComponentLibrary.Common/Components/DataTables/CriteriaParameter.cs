namespace Equant.SAV2000.ComponentLibrary.Common.Components.DataTables
{
    using System;

    /// <summary>
    /// Represents a criteria parameter used for DataTable server-side filtering.
    /// </summary>
    [Serializable]
    public class CriteriaParameter
    {
        /// <summary>
        /// Gets or sets the control client id that contains the value of the criteria.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the property of the criteria object.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the evaluation type.
        /// </summary>
        public CriteriaParameterEvaluationType EvalType { get; set; }
    }
}
