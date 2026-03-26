namespace Equant.SAV2000.ComponentLibrary.Common.Components.DataTables
{
    /// <summary>
    /// Defines the evaluation type for a criteria parameter.
    /// </summary>
    public enum CriteriaParameterEvaluationType
    {
        /// <summary>
        /// No evaluation - use the value directly.
        /// </summary>
        None = 0,

        /// <summary>
        /// Evaluate client-side JavaScript expression.
        /// </summary>
        JavaScript = 1,

        /// <summary>
        /// Evaluate as a jQuery selector.
        /// </summary>
        JQuery = 2
    }
}
