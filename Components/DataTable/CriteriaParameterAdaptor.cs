namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable
{
    using System;
    using System.Collections.Generic;

    using Equant.SAV2000.ComponentLibrary.Common.Components.DataTables;
    using Equant.SAV2000.ComponentLibrary.Common.Helper;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// The data table criteria parameter.
    /// </summary>
    [Serializable]
    public class CriteriaParameterAdaptor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CriteriaParameterAdaptor"/> class.
        /// </summary>
        /// <param name="criteriaParameter">
        /// The criteria parameter.
        /// </param>
        /// <param name="isEncryptionRequired"></param>
        /// <param name="encryptedParameters"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists",
            Justification = "This property just use the list as a simple container, it does not require any features that a collection will provide and is not intended to be extended")]
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
                        throw new ArgumentNullException("encryptedParameters");
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

        /// <summary>
        /// Gets or sets the control client id that contains the value of the criteria
        /// </summary>
        [JsonProperty("clientId", NullValueHandling = NullValueHandling.Ignore)]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the property of the criteria object
        /// </summary>
        [JsonProperty("propertyName")]
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the java script evaluation expression.
        /// </summary>
        [JsonProperty("evalType", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(StringEnumConverter))]
        public CriteriaParameterEvaluationType EvalType { get; set; }
    }
}
