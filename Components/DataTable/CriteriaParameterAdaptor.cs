namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable
{
    using System.Collections.Generic;
    using Equant.SAV2000.ComponentLibrary.Common.Components.DataTables;

    public class CriteriaParameterAdaptor
    {
        public CriteriaParameterAdaptor(CriteriaParameter criteriaParameter, bool isEncryptionRequired, List<string> encryptedParameters)
        {
            if (criteriaParameter != null) { this.Name = criteriaParameter.Name; this.Value = criteriaParameter.Value; }
        }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
