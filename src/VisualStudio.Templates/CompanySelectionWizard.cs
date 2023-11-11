//-----------------------------------------------------------------------
// <copyright file="BrandSelectionWizard.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.VisualStudio.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EnvDTE;
    using Microsoft.VisualStudio.TemplateWizard;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;
    using System.IO;

    public class CompanySelectionWizard : IWizard
    {
        private bool shouldAddProjectItem;

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
        }

        public void RunStarted(object automationObject,
            Dictionary<string, string> replacementsDictionary,
            WizardRunKind runKind, object[] customParams)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            string company = null;

            // Check if StyleCop settings file already exists
            var projectInteropWrapper = new ProjectInteropWrapper((IVsProject4)customParams[1]);

            var styleCopSettingsFilePath = projectInteropWrapper.GetDocumentFilePath("stylecop.json");

            if (styleCopSettingsFilePath != null)
            {
                using (var fileStream = File.OpenRead(styleCopSettingsFilePath))
                {
                    var styleCopConfiguration = StyleCopConfiguration.LoadFrom(fileStream);

                    if (styleCopConfiguration != null)
                    {
                        company = styleCopConfiguration.Settings.DocumentationRules.CompanyName;
                    }
                }
            }

            // If company is null ask the company to the developer
            if (company == null)
            {
                company = AskCompany((DTE)automationObject);
            }

            if (company != null)
            {
                replacementsDictionary.Add("$company$", company);

                // For the unit tests, add a setting without "Test" suffix
                var safeItemName = replacementsDictionary["$safeitemname$"];
                var classNameUnderTest = safeItemName;

                if (safeItemName.EndsWith("Test"))
                {
                    classNameUnderTest = safeItemName.Substring(0, safeItemName.Length - "Test".Length);
                }

                replacementsDictionary.Add("$classnameundertest$", classNameUnderTest);

                this.shouldAddProjectItem = true;
            }
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return this.shouldAddProjectItem;
        }

        private static string AskCompany(DTE dte)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var solutionFilePath = dte.Solution.FileName;

            var repository = new AppDataRoamingCompanyRepository();

            // Gets the companies to display
            var solutions = repository.GetAll();
            var companies = solutions.Select(s => s.Company)
                .Distinct()
                .OrderBy(c => c)
                .ToArray();

            // Finds the company associated to the solution.
            var solutionFound = solutions.FirstOrDefault(s => s.Path.Equals(solutionFilePath, StringComparison.CurrentCultureIgnoreCase));

            if (solutionFound != null)
            {
                return solutionFound.Company;
            }
            else
            {
                var company = CompanySelectionForm.ShowDialog(companies, "P.O.S Informatique", dte.MainWindow.HWnd);

                if (company != null)
                {
                    repository.Save(new Solution(dte.Solution.FileName, company));

                    return company;
                }
            }

            return null;
        }
    }
}
