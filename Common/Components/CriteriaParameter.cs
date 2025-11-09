namespace Equant.SAV2000.ComponentLibrary.Common.Components
{
    public class CriteriaParameter
    {
        public string Name { get; set; } = string.Empty;
        public object? Value { get; set; }
        public CriteriaParameterEvaluationType EvaluationType { get; set; }
    }

    public enum CriteriaParameterEvaluationType
    {
        Equal = 0,
        NotEqual = 1,
        GreaterThan = 2,
        LessThan = 3,
        GreaterThanOrEqual = 4,
        LessThanOrEqual = 5,
        Contains = 6,
        StartsWith = 7,
        EndsWith = 8
    }
}
