//-----------------------------------------------------------------------
// <copyright file="SolutionCompany.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace PosInformatique.VisualStudio.Templates
{
    internal class Solution
    {
        public Solution(string path, string company)
        {
            this.Path = path;
            this.Company = company;
        }

        [JsonPropertyName("solutionPath")]
        [JsonPropertyOrder(1)]
        public string Path { get; }

        [JsonPropertyName("company")]
        [JsonPropertyOrder(2)]
        public string Company { get; }
    }
}
