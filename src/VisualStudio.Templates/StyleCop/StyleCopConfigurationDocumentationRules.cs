//-----------------------------------------------------------------------
// <copyright file="StyleCopConfigurationDocumentationRules.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.VisualStudio.Templates
{
    using System.Text.Json.Serialization;

    public class StyleCopConfigurationDocumentationRules
    {
        public StyleCopConfigurationDocumentationRules(string companyName)
        {
            this.CompanyName = companyName;
        }

        [JsonPropertyName("companyName")]
        public string CompanyName { get; }
    }
}
