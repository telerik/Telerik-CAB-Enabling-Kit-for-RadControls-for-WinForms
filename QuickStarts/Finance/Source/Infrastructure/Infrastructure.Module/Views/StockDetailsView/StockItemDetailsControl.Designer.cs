namespace FinanceApplicationCAB.Infrastructure.Module.Views.StockDetailsView
{
	partial class StockItemDetailsControl
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
            this.radListBox = new Telerik.WinControls.UI.RadListControl();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.radListBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// radListBox
			// 
			this.radListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.radListBox.BackColor = System.Drawing.Color.White;
			this.radListBox.Location = new System.Drawing.Point(3, 115);
			this.radListBox.Name = "radListBox";
			// 
			// radListBox.RootElement
			// 
			this.radListBox.RootElement.AccessibleDescription = "";
			this.radListBox.RootElement.BackColor = System.Drawing.Color.White;
			this.radListBox.Size = new System.Drawing.Size(262, 198);
			this.radListBox.SmallImageList = null;
			this.radListBox.TabIndex = 0;
			// 
			// pictureBox
			// 
			this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox.Location = new System.Drawing.Point(4, 4);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(261, 105);
			this.pictureBox.TabIndex = 1;
			this.pictureBox.TabStop = false;
			// 
			// StockItemDetailsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pictureBox);
			this.Controls.Add(this.radListBox);
			this.Name = "StockItemDetailsControl";
			this.Size = new System.Drawing.Size(268, 316);
			((System.ComponentModel.ISupportInitialize)(this.radListBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Telerik.WinControls.UI.RadListControl radListBox;
		private System.Windows.Forms.PictureBox pictureBox;
	}
}
