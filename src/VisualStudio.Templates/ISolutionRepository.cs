//-----------------------------------------------------------------------
// <copyright file="ICompanyRepository.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.VisualStudio.Templates
{
    using System.Collections.Generic;

    internal interface ISolutionRepository
    {
        void Save(Solution solution);

        IReadOnlyList<Solution> GetAll();
    }
}
