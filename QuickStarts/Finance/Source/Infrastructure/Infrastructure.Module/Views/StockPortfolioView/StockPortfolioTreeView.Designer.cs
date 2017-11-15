namespace FinanceApplicationCAB.Infrastructure.Module
{
	partial class StockPortfolioTreeView
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
			this.treeView = new Telerik.WinControls.UI.RadTreeView();
			((System.ComponentModel.ISupportInitialize)(this.treeView)).BeginInit();
			this.SuspendLayout();
			// 
			// treeView
			// 
			this.treeView.AccessibleName = "RadTreeView";
			this.treeView.AccessibleRole = System.Windows.Forms.AccessibleRole.List;
			this.treeView.AllowDefaultContextMenu = false;
			this.treeView.BackColor = System.Drawing.Color.White;
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.Font = new System.Drawing.Font("Tahoma", 8.6F);
			this.treeView.Location = new System.Drawing.Point(0, 0);
			this.treeView.Name = "treeView";
			// 
			// treeView.RootElement
			// 
			this.treeView.RootElement.AccessibleDescription = "";
			this.treeView.RootElement.BackColor = System.Drawing.Color.White;
			this.treeView.RootElement.Font = new System.Drawing.Font("Tahoma", 8.6F);
			this.treeView.Size = new System.Drawing.Size(351, 503);
			this.treeView.TabIndex = 0;
			// 
			// StockPortfolioTreeView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.treeView);
			this.Name = "StockPortfolioTreeView";
			this.Size = new System.Drawing.Size(351, 503);
			((System.ComponentModel.ISupportInitialize)(this.treeView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		Telerik.WinControls.UI.RadTreeView treeView;
	}
}
