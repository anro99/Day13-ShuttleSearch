namespace Day13_ShuttleSearch
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_edtInput = new System.Windows.Forms.TextBox();
            this.m_btnQuestion1 = new System.Windows.Forms.Button();
            this.m_btnQuestion2 = new System.Windows.Forms.Button();
            this.m_edtResult = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // m_edtInput
            // 
            this.m_edtInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_edtInput.Location = new System.Drawing.Point(13, 13);
            this.m_edtInput.Multiline = true;
            this.m_edtInput.Name = "m_edtInput";
            this.m_edtInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.m_edtInput.Size = new System.Drawing.Size(775, 83);
            this.m_edtInput.TabIndex = 0;
            this.m_edtInput.WordWrap = false;
            // 
            // m_btnQuestion1
            // 
            this.m_btnQuestion1.Location = new System.Drawing.Point(13, 115);
            this.m_btnQuestion1.Name = "m_btnQuestion1";
            this.m_btnQuestion1.Size = new System.Drawing.Size(75, 23);
            this.m_btnQuestion1.TabIndex = 1;
            this.m_btnQuestion1.Text = "Question 1";
            this.m_btnQuestion1.UseVisualStyleBackColor = true;
            this.m_btnQuestion1.Click += new System.EventHandler(this.m_btnQuestion1_Click);
            // 
            // m_btnQuestion2
            // 
            this.m_btnQuestion2.Location = new System.Drawing.Point(112, 115);
            this.m_btnQuestion2.Name = "m_btnQuestion2";
            this.m_btnQuestion2.Size = new System.Drawing.Size(75, 23);
            this.m_btnQuestion2.TabIndex = 2;
            this.m_btnQuestion2.Text = "Question 2";
            this.m_btnQuestion2.UseVisualStyleBackColor = true;
            this.m_btnQuestion2.Click += new System.EventHandler(this.m_btnQuestion2_Click);
            // 
            // m_edtResult
            // 
            this.m_edtResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_edtResult.Location = new System.Drawing.Point(13, 155);
            this.m_edtResult.Multiline = true;
            this.m_edtResult.Name = "m_edtResult";
            this.m_edtResult.ReadOnly = true;
            this.m_edtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.m_edtResult.Size = new System.Drawing.Size(775, 83);
            this.m_edtResult.TabIndex = 3;
            this.m_edtResult.WordWrap = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 259);
            this.Controls.Add(this.m_edtResult);
            this.Controls.Add(this.m_btnQuestion2);
            this.Controls.Add(this.m_btnQuestion1);
            this.Controls.Add(this.m_edtInput);
            this.Name = "Form1";
            this.Text = "Day 13 - Shuttle Search";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_edtInput;
        private System.Windows.Forms.Button m_btnQuestion1;
        private System.Windows.Forms.Button m_btnQuestion2;
        private System.Windows.Forms.TextBox m_edtResult;
    }
}

