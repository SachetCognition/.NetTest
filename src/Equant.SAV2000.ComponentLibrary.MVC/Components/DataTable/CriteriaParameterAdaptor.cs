namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable
{
    using System;
    using System.Collections.Generic;

    using Equant.SAV2000.ComponentLibrary.Common.Components.DataTables;
    using Equant.SAV2000.ComponentLibrary.Common.Helper;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    [Serializable]
    public class CriteriaParameterAdaptor
    {
        public CriteriaParameterAdaptor(CriteriaParameter criteriaParameter, bool isEncryptionRequired, List<string> encryptedParameters)
        {
            if (criteriaParameter != null)
            {
                this.ClientId = criteriaParameter.ClientId;
                this.PropertyName = criteriaParameter.PropertyName;

                if (isEncryptionRequired)
                {
                    if (encryptedParameters == null)
                    {
                        throw new ArgumentNullException(nameof(encryptedParameters));
                    }

                    if (!string.IsNullOrEmpty(criteriaParameter.Value) && encryptedParameters.Contains(criteriaParameter.PropertyName))
                    {
                        this.Value = Crypt.Encrypt(criteriaParameter.Value);
                    }
                    else
                    {
                        this.Value = criteriaParameter.Value;
                    }
                }
                else
                {
                    this.Value = criteriaParameter.Value;
                }

                this.EvalType = criteriaParameter.EvalType;
            }
        }

        [JsonProperty("clientId", NullValueHandling = NullValueHandling.Ignore)]
        public string ClientId { get; set; }

        [JsonProperty("propertyName")]
        public string PropertyName { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        [JsonProperty("evalType", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(StringEnumConverter))]
        public CriteriaParameterEvaluationType EvalType { get; set; }
    }
}
