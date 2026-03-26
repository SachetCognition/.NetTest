namespace Equant.SAV2000.ComponentLibrary.Common.Helper
{
    public class CriteriaParameter
    {
        public string ClientId { get; set; }
        public string PropertyName { get; set; }
        public string Value { get; set; }
        public CriteriaParameterEvaluationType EvalType { get; set; }
    }

    public enum CriteriaParameterEvaluationType
    {
        None,
        Value,
        Expression
    }
}
