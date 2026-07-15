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
            ThreadHelper.ThrowIfNotOnUIThread();

            if (projectItem == null)
            {
                return;
            }

            // Get the file path of the generated item
            var filePath = projectItem.FileNames[1];

            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                return;
            }

            var editorConfig = new EditorConfig();

            ApplyNamespaceStyle(filePath, editorConfig);
            InsertFinalNewLine(filePath, editorConfig);
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
                        company = styleCopConfiguration.Settings?.DocumentationRules?.CompanyName;
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

                // For unit tests of the exception, add a setting without the "Tests" namespace suffix
                var rootnamespace = replacementsDictionary["$rootnamespace$"];
                var namespaceundertest = rootnamespace;

                if (namespaceundertest.EndsWith("Tests"))
                {
                    namespaceundertest = namespaceundertest.Substring(0, namespaceundertest.Length - ".Tests".Length);
                }

                replacementsDictionary.Add("$namespaceundertest$", namespaceundertest);

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

        private static void ApplyNamespaceStyle(string filePath, EditorConfig editorConfig)
        {
            if (!filePath.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            var useFileScoped = editorConfig.GetUseFileScopedNamespace(filePath);

            if (useFileScoped)
            {
                var content = File.ReadAllText(filePath);
                var newContent = NamespaceRewriter.ConvertBlockToFileScoped(content);

                File.WriteAllText(filePath, newContent);
            }
        }

        private static void InsertFinalNewLine(string filePath, EditorConfig editorConfig)
        {
            var insertFinalNewLine = true;
            var insertFinalNewLineConfig = editorConfig.GetInsertFinalNewline(filePath);

            // If editorconfig specifies a value, use it; otherwise keep default (true)
            if (insertFinalNewLineConfig.HasValue)
            {
                insertFinalNewLine = insertFinalNewLineConfig.Value;
            }

            // Read the file content
            var content = File.ReadAllText(filePath);

            // Adjust final newline based on editorconfig setting
            var endsWithNewLine = content.EndsWith("\n") || content.EndsWith("\r\n");

            if (insertFinalNewLine && !endsWithNewLine)
            {
                // Add final newline if needed
                File.AppendAllText(filePath, Environment.NewLine);
            }
            else if (!insertFinalNewLine && endsWithNewLine)
            {
                // Remove final newline(s) if present
                content = content.TrimEnd('\r', '\n');
                File.WriteAllText(filePath, content);
            }
        }
    }
}
