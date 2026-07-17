//-----------------------------------------------------------------------
// <copyright file="ISolutionRepository.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.VisualStudio.Templates
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a repository for storing and retrieving solution settings.
    /// </summary>
    internal interface ISolutionRepository
    {
        /// <summary>
        /// Saves the specified solution settings.
        /// </summary>
        /// <param name="solution">The solution settings to save.</param>
        void Save(Solution solution);

        /// <summary>
        /// Retrieves all the solution settings from the repository.
        /// </summary>
        /// <returns>A read-only list of all solution settings.</returns>
        IReadOnlyList<Solution> GetAll();
    }
}
