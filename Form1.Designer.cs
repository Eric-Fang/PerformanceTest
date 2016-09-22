namespace PerformanceTest
{
    partial class Form1
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
            this.buttonPerformanceRetrieve = new System.Windows.Forms.Button();
            this.buttonPerformanceAll = new System.Windows.Forms.Button();
            this.textBoxPerformanceRowCount = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.checkBoxPerformanceExist = new System.Windows.Forms.CheckBox();
            this.buttonPerformanceUpdate = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.buttonPerformanceInsert = new System.Windows.Forms.Button();
            this.buttonPerformanceRecreate = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.textBoxPerformanceLog = new System.Windows.Forms.TextBox();
            this.buttonPerformanceDelete = new System.Windows.Forms.Button();
            this.textBoxSiteURL = new System.Windows.Forms.TextBox();
            this.labelSiteURL = new System.Windows.Forms.Label();
            this.buttonClearLog = new System.Windows.Forms.Button();
            this.checkBoxBatch = new System.Windows.Forms.CheckBox();
            this.maskedTextBoxThreadNumber = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPerformanceRowNumber = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonPerformanceRetrieve
            // 
            this.buttonPerformanceRetrieve.Location = new System.Drawing.Point(546, 76);
            this.buttonPerformanceRetrieve.Name = "buttonPerformanceRetrieve";
            this.buttonPerformanceRetrieve.Size = new System.Drawing.Size(57, 23);
            this.buttonPerformanceRetrieve.TabIndex = 40;
            this.buttonPerformanceRetrieve.Text = "Retrieve";
            this.buttonPerformanceRetrieve.UseVisualStyleBackColor = true;
            this.buttonPerformanceRetrieve.Click += new System.EventHandler(this.buttonPerformanceRetrieve_Click);
            // 
            // buttonPerformanceAll
            // 
            this.buttonPerformanceAll.Location = new System.Drawing.Point(757, 76);
            this.buttonPerformanceAll.Name = "buttonPerformanceAll";
            this.buttonPerformanceAll.Size = new System.Drawing.Size(176, 23);
            this.buttonPerformanceAll.TabIndex = 39;
            this.buttonPerformanceAll.Text = "Insert,Retrieve,Update,Delete";
            this.buttonPerformanceAll.UseVisualStyleBackColor = true;
            this.buttonPerformanceAll.Click += new System.EventHandler(this.buttonPerformanceAll_Click);
            // 
            // textBoxPerformanceRowCount
            // 
            this.textBoxPerformanceRowCount.Enabled = false;
            this.textBoxPerformanceRowCount.Location = new System.Drawing.Point(360, 44);
            this.textBoxPerformanceRowCount.Name = "textBoxPerformanceRowCount";
            this.textBoxPerformanceRowCount.ReadOnly = true;
            this.textBoxPerformanceRowCount.Size = new System.Drawing.Size(100, 20);
            this.textBoxPerformanceRowCount.TabIndex = 38;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(293, 48);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(60, 13);
            this.label30.TabIndex = 37;
            this.label30.Text = "Row Count";
            // 
            // checkBoxPerformanceExist
            // 
            this.checkBoxPerformanceExist.AutoSize = true;
            this.checkBoxPerformanceExist.Enabled = false;
            this.checkBoxPerformanceExist.Location = new System.Drawing.Point(239, 46);
            this.checkBoxPerformanceExist.Name = "checkBoxPerformanceExist";
            this.checkBoxPerformanceExist.Size = new System.Drawing.Size(48, 17);
            this.checkBoxPerformanceExist.TabIndex = 35;
            this.checkBoxPerformanceExist.Text = "Exist";
            this.checkBoxPerformanceExist.UseVisualStyleBackColor = true;
            this.checkBoxPerformanceExist.CheckedChanged += new System.EventHandler(this.checkBoxPerformanceExist_CheckedChanged);
            // 
            // buttonPerformanceUpdate
            // 
            this.buttonPerformanceUpdate.Location = new System.Drawing.Point(615, 76);
            this.buttonPerformanceUpdate.Name = "buttonPerformanceUpdate";
            this.buttonPerformanceUpdate.Size = new System.Drawing.Size(57, 23);
            this.buttonPerformanceUpdate.TabIndex = 34;
            this.buttonPerformanceUpdate.Text = "Update";
            this.buttonPerformanceUpdate.UseVisualStyleBackColor = true;
            this.buttonPerformanceUpdate.Click += new System.EventHandler(this.buttonPerformanceUpdate_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(13, 81);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(85, 13);
            this.label29.TabIndex = 33;
            this.label29.Text = "Rows per thread";
            // 
            // buttonPerformanceInsert
            // 
            this.buttonPerformanceInsert.Location = new System.Drawing.Point(477, 76);
            this.buttonPerformanceInsert.Name = "buttonPerformanceInsert";
            this.buttonPerformanceInsert.Size = new System.Drawing.Size(57, 23);
            this.buttonPerformanceInsert.TabIndex = 32;
            this.buttonPerformanceInsert.Text = "Insert";
            this.buttonPerformanceInsert.UseVisualStyleBackColor = true;
            this.buttonPerformanceInsert.Click += new System.EventHandler(this.buttonPerformanceInsert_Click);
            // 
            // buttonPerformanceRecreate
            // 
            this.buttonPerformanceRecreate.Location = new System.Drawing.Point(477, 43);
            this.buttonPerformanceRecreate.Name = "buttonPerformanceRecreate";
            this.buttonPerformanceRecreate.Size = new System.Drawing.Size(92, 23);
            this.buttonPerformanceRecreate.TabIndex = 31;
            this.buttonPerformanceRecreate.Text = "Re-create List";
            this.buttonPerformanceRecreate.UseVisualStyleBackColor = true;
            this.buttonPerformanceRecreate.Click += new System.EventHandler(this.buttonPerformanceRecreate_Click);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(13, 48);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(222, 13);
            this.label28.TabIndex = 30;
            this.label28.Text = "Test List Name: PerformanceList (5 text fields)";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(14, 112);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(25, 13);
            this.label27.TabIndex = 29;
            this.label27.Text = "Log";
            // 
            // textBoxPerformanceLog
            // 
            this.textBoxPerformanceLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPerformanceLog.Location = new System.Drawing.Point(6, 136);
            this.textBoxPerformanceLog.MaxLength = 32767000;
            this.textBoxPerformanceLog.Multiline = true;
            this.textBoxPerformanceLog.Name = "textBoxPerformanceLog";
            this.textBoxPerformanceLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxPerformanceLog.Size = new System.Drawing.Size(926, 440);
            this.textBoxPerformanceLog.TabIndex = 28;
            // 
            // buttonPerformanceDelete
            // 
            this.buttonPerformanceDelete.Location = new System.Drawing.Point(683, 76);
            this.buttonPerformanceDelete.Name = "buttonPerformanceDelete";
            this.buttonPerformanceDelete.Size = new System.Drawing.Size(57, 23);
            this.buttonPerformanceDelete.TabIndex = 27;
            this.buttonPerformanceDelete.Text = "Delete";
            this.buttonPerformanceDelete.UseVisualStyleBackColor = true;
            this.buttonPerformanceDelete.Click += new System.EventHandler(this.buttonPerformanceDelete_Click);
            // 
            // textBoxSiteURL
            // 
            this.textBoxSiteURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSiteURL.Location = new System.Drawing.Point(108, 12);
            this.textBoxSiteURL.Name = "textBoxSiteURL";
            this.textBoxSiteURL.Size = new System.Drawing.Size(824, 20);
            this.textBoxSiteURL.TabIndex = 42;
            this.textBoxSiteURL.Text = "http://{local}/sites/sandpit";
            this.textBoxSiteURL.TextChanged += new System.EventHandler(this.textBoxSiteURL_TextChanged);
            // 
            // labelSiteURL
            // 
            this.labelSiteURL.AutoSize = true;
            this.labelSiteURL.Location = new System.Drawing.Point(14, 12);
            this.labelSiteURL.Name = "labelSiteURL";
            this.labelSiteURL.Size = new System.Drawing.Size(87, 13);
            this.labelSiteURL.TabIndex = 41;
            this.labelSiteURL.Text = "Default Site URL";
            // 
            // buttonClearLog
            // 
            this.buttonClearLog.Location = new System.Drawing.Point(61, 107);
            this.buttonClearLog.Name = "buttonClearLog";
            this.buttonClearLog.Size = new System.Drawing.Size(92, 23);
            this.buttonClearLog.TabIndex = 43;
            this.buttonClearLog.Text = "Clear log";
            this.buttonClearLog.UseVisualStyleBackColor = true;
            this.buttonClearLog.Click += new System.EventHandler(this.buttonClearLog_Click);
            // 
            // checkBoxBatch
            // 
            this.checkBoxBatch.AutoSize = true;
            this.checkBoxBatch.Location = new System.Drawing.Point(360, 77);
            this.checkBoxBatch.Name = "checkBoxBatch";
            this.checkBoxBatch.Size = new System.Drawing.Size(65, 17);
            this.checkBoxBatch.TabIndex = 44;
            this.checkBoxBatch.Text = "In batch";
            this.checkBoxBatch.UseVisualStyleBackColor = true;
            // 
            // maskedTextBoxThreadNumber
            // 
            this.maskedTextBoxThreadNumber.AllowPromptAsInput = false;
            this.maskedTextBoxThreadNumber.Location = new System.Drawing.Point(272, 75);
            this.maskedTextBoxThreadNumber.Mask = "00";
            this.maskedTextBoxThreadNumber.Name = "maskedTextBoxThreadNumber";
            this.maskedTextBoxThreadNumber.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.maskedTextBoxThreadNumber.Size = new System.Drawing.Size(43, 20);
            this.maskedTextBoxThreadNumber.TabIndex = 45;
            this.maskedTextBoxThreadNumber.Text = "1";
            this.maskedTextBoxThreadNumber.ValidatingType = typeof(int);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(185, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Thread Number";
            // 
            // textBoxPerformanceRowNumber
            // 
            this.textBoxPerformanceRowNumber.Location = new System.Drawing.Point(125, 76);
            this.textBoxPerformanceRowNumber.MaxLength = 4;
            this.textBoxPerformanceRowNumber.Name = "textBoxPerformanceRowNumber";
            this.textBoxPerformanceRowNumber.Size = new System.Drawing.Size(43, 20);
            this.textBoxPerformanceRowNumber.TabIndex = 47;
            this.textBoxPerformanceRowNumber.Text = "500";
            this.textBoxPerformanceRowNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxPerformanceRowNumber.WordWrap = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 588);
            this.Controls.Add(this.textBoxPerformanceRowNumber);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.maskedTextBoxThreadNumber);
            this.Controls.Add(this.checkBoxBatch);
            this.Controls.Add(this.buttonClearLog);
            this.Controls.Add(this.textBoxSiteURL);
            this.Controls.Add(this.labelSiteURL);
            this.Controls.Add(this.buttonPerformanceRetrieve);
            this.Controls.Add(this.buttonPerformanceAll);
            this.Controls.Add(this.textBoxPerformanceRowCount);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.checkBoxPerformanceExist);
            this.Controls.Add(this.buttonPerformanceUpdate);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.buttonPerformanceInsert);
            this.Controls.Add(this.buttonPerformanceRecreate);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.textBoxPerformanceLog);
            this.Controls.Add(this.buttonPerformanceDelete);
            this.Name = "Form1";
            this.Text = "SharePoint SOM Data Access Performance Test (by Eric Fang)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPerformanceRetrieve;
        private System.Windows.Forms.Button buttonPerformanceAll;
        private System.Windows.Forms.TextBox textBoxPerformanceRowCount;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.CheckBox checkBoxPerformanceExist;
        private System.Windows.Forms.Button buttonPerformanceUpdate;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Button buttonPerformanceInsert;
        private System.Windows.Forms.Button buttonPerformanceRecreate;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox textBoxPerformanceLog;
        private System.Windows.Forms.Button buttonPerformanceDelete;
        private System.Windows.Forms.TextBox textBoxSiteURL;
        private System.Windows.Forms.Label labelSiteURL;
        private System.Windows.Forms.Button buttonClearLog;
        private System.Windows.Forms.CheckBox checkBoxBatch;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxThreadNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPerformanceRowNumber;
    }
}

