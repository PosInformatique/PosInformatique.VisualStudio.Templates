//-----------------------------------------------------------------------
// <copyright file="CompanySelectionForm.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.VisualStudio.Templates
{
    using System.Windows.Forms;

    internal partial class CompanySelectionForm : Form
    {
        private CompanySelectionForm()
        {
            this.InitializeComponent();
        }

        public static string ShowDialog(string[] companies, string defaultCompany, IntPtr visualStudioMainWindowHwnd)
        {
            using (var form = new CompanySelectionForm())
            {
                form.company.Items.AddRange(companies);
                form.company.Text = defaultCompany;
                form.OnCompanyTextUpdate(form.company, EventArgs.Empty);

                var visualStudioMainWindow = new NativeWindow();
                visualStudioMainWindow.AssignHandle(visualStudioMainWindowHwnd);

                if (form.ShowDialog(visualStudioMainWindow) == DialogResult.OK)
                {
                    return form.company.Text;
                }
            }

            return null;
        }

        private void OnOkClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnCompanyTextUpdate(object sender, EventArgs e)
        {
            this.ok.Enabled = !string.IsNullOrWhiteSpace(this.company.Text);
        }
    }
}
