//-----------------------------------------------------------------------
// <copyright file="ProjectInteropWrapper.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.VisualStudio.Templates
{
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;

    public class ProjectInteropWrapper
    {
        private readonly IVsProject4 vsProject;

        public ProjectInteropWrapper(IVsProject4 project)
        {
            this.vsProject = project;
        }

        public string GetDocumentFilePath(string name)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var result = this.vsProject.IsDocumentInProject(name, out var _, null, out var id);

            if (result != 0)
            {
                return null;
            }

            // Gets the document moniker
            result = this.vsProject.GetMkDocument(id, out var fileName);

            if (result != 0)
            {
                return null;
            }

            return fileName;
        }
    }
}
