﻿namespace Day13_ShuttleSearch
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
            this.m_btnDoor13a = new System.Windows.Forms.Button();
            this.m_btnDoor13b = new System.Windows.Forms.Button();
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
            // m_btnDoor13a
            // 
            this.m_btnDoor13a.Location = new System.Drawing.Point(13, 115);
            this.m_btnDoor13a.Name = "m_btnDoor13a";
            this.m_btnDoor13a.Size = new System.Drawing.Size(75, 23);
            this.m_btnDoor13a.TabIndex = 1;
            this.m_btnDoor13a.Text = "Tür 13/a";
            this.m_btnDoor13a.UseVisualStyleBackColor = true;
            this.m_btnDoor13a.Click += new System.EventHandler(this.m_btnDoor13a_Click);
            // 
            // m_btnDoor13b
            // 
            this.m_btnDoor13b.Location = new System.Drawing.Point(112, 115);
            this.m_btnDoor13b.Name = "m_btnDoor13b";
            this.m_btnDoor13b.Size = new System.Drawing.Size(75, 23);
            this.m_btnDoor13b.TabIndex = 2;
            this.m_btnDoor13b.Text = "Tür 13/b";
            this.m_btnDoor13b.UseVisualStyleBackColor = true;
            this.m_btnDoor13b.Click += new System.EventHandler(this.m_btnDoor13b_Click);
            // 
            // m_edtResult
            // 
            this.m_edtResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_edtResult.Location = new System.Drawing.Point(13, 371);
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
            this.ClientSize = new System.Drawing.Size(800, 466);
            this.Controls.Add(this.m_edtResult);
            this.Controls.Add(this.m_btnDoor13b);
            this.Controls.Add(this.m_btnDoor13a);
            this.Controls.Add(this.m_edtInput);
            this.Name = "Form1";
            this.Text = "AdventToCode 2020";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_edtInput;
        private System.Windows.Forms.Button m_btnDoor13a;
        private System.Windows.Forms.Button m_btnDoor13b;
        private System.Windows.Forms.TextBox m_edtResult;
    }
}
