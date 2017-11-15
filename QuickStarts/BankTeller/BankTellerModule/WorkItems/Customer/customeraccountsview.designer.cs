//===============================================================================
// Microsoft patterns & practices
// CompositeUI Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

namespace BankTellerModule
{
	partial class CustomerAccountsView
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
            this.accountNumberGridViewTextBoxColumn = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            this.accountTypeGridViewTextBoxColumn = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            this.currentBalanceGridViewTextBoxColumn = new Telerik.WinControls.UI.GridViewTextBoxColumn();
			this.CustomerAccountBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.radCommandBar = new Telerik.WinControls.UI.RadCommandBar();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.CustomerAccountBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
            this.radGridView1.AllowAddNewRow = false;
            this.radGridView1.AllowDeleteRow = false;
            this.radGridView1.AutoGenerateColumns = false;
            this.radGridView1.Columns.Add(this.accountNumberGridViewTextBoxColumn);
            this.radGridView1.Columns.Add(this.accountTypeGridViewTextBoxColumn);
            this.radGridView1.Columns.Add(this.currentBalanceGridViewTextBoxColumn);
            this.radGridView1.DataSource = this.CustomerAccountBindingSource;
            this.radGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridView1.Location = new System.Drawing.Point(0, 25);
            this.radGridView1.Name = "dataGridView1";
            this.radGridView1.ReadOnly = true;
            this.radGridView1.Size = new System.Drawing.Size(445, 246);
            this.radGridView1.TabIndex = 0;
            this.radGridView1.Text = "dataGridView1";
			// 
			// accountNumberGridViewTextBoxColumn
			// 
			this.accountNumberGridViewTextBoxColumn.FieldName = "AccountNumber";
			this.accountNumberGridViewTextBoxColumn.HeaderText = "Account Number";
			this.accountNumberGridViewTextBoxColumn.Name = "AccountNumber";
			this.accountNumberGridViewTextBoxColumn.ReadOnly = true;
			// 
			// accountTypeGridViewTextBoxColumn
			// 
            this.accountTypeGridViewTextBoxColumn.FieldName = "AccountType";
			this.accountTypeGridViewTextBoxColumn.HeaderText = "Account Type";
			this.accountTypeGridViewTextBoxColumn.Name = "AccountType";
			this.accountTypeGridViewTextBoxColumn.ReadOnly = true;
			// 
			// currentBalanceGridViewTextBoxColumn
			// 
            this.currentBalanceGridViewTextBoxColumn.FieldName = "CurrentBalance";
			this.currentBalanceGridViewTextBoxColumn.HeaderText = "Current Balance";
			this.currentBalanceGridViewTextBoxColumn.Name = "CurrentBalance";
			this.currentBalanceGridViewTextBoxColumn.ReadOnly = true;
			// 
			// CustomerAccountBindingSource
			// 
			this.CustomerAccountBindingSource.DataSource = typeof(BankTellerCommon.CustomerAccount);
			// 
            // radCommandBar
			// 
            this.radCommandBar.Location = new System.Drawing.Point(0, 0);
            this.radCommandBar.Name = "toolCommands";
            this.radCommandBar.Size = new System.Drawing.Size(445, 25);
            this.radCommandBar.TabIndex = 1;
			// 
			// CustomerAccountsView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radGridView1);
            this.Controls.Add(this.radCommandBar);
			this.Name = "CustomerAccountsView";
			this.Size = new System.Drawing.Size(445, 271);
			this.Load += new System.EventHandler(this.CustomerAccountsView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.CustomerAccountBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Telerik.WinControls.UI.RadGridView radGridView1;
        private Telerik.WinControls.UI.GridViewTextBoxColumn accountNumberGridViewTextBoxColumn;
        private Telerik.WinControls.UI.GridViewTextBoxColumn accountTypeGridViewTextBoxColumn;
        private Telerik.WinControls.UI.GridViewTextBoxColumn currentBalanceGridViewTextBoxColumn;
		private System.Windows.Forms.BindingSource CustomerAccountBindingSource;
        private Telerik.WinControls.UI.RadCommandBar radCommandBar;



	}
}
