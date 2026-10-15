namespace PosInformatique.VisualStudio.Templates
{
    partial class CompanySelectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            this.company = new System.Windows.Forms.ComboBox();
            this.ok = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(120, 100);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(80, 20);
            label1.TabIndex = 1;
            label1.Text = "Company:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(18, 14);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(485, 40);
            label2.TabIndex = 2;
            label2.Text = "Define the name of the company you working for.\r\nThis name will be use in the hea" +
    "der of the source code files created.";
            // 
            // company
            // 
            this.company.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.company.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.company.FormattingEnabled = true;
            this.company.Location = new System.Drawing.Point(208, 95);
            this.company.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.company.Name = "company";
            this.company.Size = new System.Drawing.Size(319, 28);
            this.company.TabIndex = 0;
            this.company.TextUpdate += new System.EventHandler(this.OnCompanyTextUpdate);
            // 
            // ok
            // 
            this.ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ok.Enabled = false;
            this.ok.Location = new System.Drawing.Point(398, 165);
            this.ok.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(112, 35);
            this.ok.TabIndex = 3;
            this.ok.Text = "&OK";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.OnOkClick);
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(524, 165);
            this.cancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(112, 35);
            this.cancel.TabIndex = 4;
            this.cancel.Text = "&Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.OnCancelClick);
            // 
            // CompanySelectionForm
            // 
            this.AcceptButton = this.ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(654, 218);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Controls.Add(this.company);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CompanySelectionForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Company";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox company;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Button cancel;
    }
}