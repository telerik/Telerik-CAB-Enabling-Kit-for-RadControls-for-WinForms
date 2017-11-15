namespace BankTellerModule
{
	partial class SideBarView
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
            this.radSplitContainer1 = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.smartPartPlaceholder1 = new Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder();
            this.customerQueueView1 = new BankTellerModule.CustomerQueueView();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).BeginInit();
            this.radSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.radSplitContainer1.Controls.Add(this.splitPanel1);
            this.radSplitContainer1.Controls.Add(this.splitPanel2);
            this.radSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.radSplitContainer1.Name = "splitContainer1";
            this.radSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;

            // 
            // splitPanel1
            // 
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel1.Size = new System.Drawing.Size(144, 50);
            this.splitPanel1.SizeInfo.AbsoluteSize = new System.Drawing.Size(230, 50);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(-0.2767442F, 0F);
            this.splitPanel1.SizeInfo.SizeMode = Telerik.WinControls.UI.Docking.SplitPanelSizeMode.Relative;
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(-178, 0);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            // 
            // splitPanel2
            // 
            this.splitPanel2.Location = new System.Drawing.Point(147, 0);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel2.Size = new System.Drawing.Size(501, 650);
            this.splitPanel2.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0.2767442F, 0F);
            this.splitPanel2.SizeInfo.SplitterCorrection = new System.Drawing.Size(178, 0);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";

            // 
            // splitContainer1.Panel1
            // 
            this.splitPanel1.Controls.Add(this.smartPartPlaceholder1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitPanel2.Controls.Add(this.customerQueueView1);
            this.radSplitContainer1.Size = new System.Drawing.Size(199, 500);
            //this.radSplitContainer1.SplitterDistance = 54;
            this.radSplitContainer1.TabIndex = 0;
            // 
            // smartPartPlaceholder1
            // 
            this.smartPartPlaceholder1.BackColor = System.Drawing.Color.Transparent;
            this.smartPartPlaceholder1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.smartPartPlaceholder1.Location = new System.Drawing.Point(0, 0);
            this.smartPartPlaceholder1.Name = "smartPartPlaceholder1";
            this.smartPartPlaceholder1.Size = new System.Drawing.Size(199, 54);
            this.smartPartPlaceholder1.SmartPartName = "UserInfo";
            this.smartPartPlaceholder1.TabIndex = 0;
            this.smartPartPlaceholder1.Text = "smartPartPlaceholder1";
            // 
            // customerQueueView1
            // 
            this.customerQueueView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customerQueueView1.Location = new System.Drawing.Point(0, 0);
            this.customerQueueView1.Name = "customerQueueView1";
            this.customerQueueView1.Size = new System.Drawing.Size(199, 442);
            this.customerQueueView1.TabIndex = 0;
            // 
            // SideBarView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radSplitContainer1);
            this.Name = "SideBarView";
            this.Size = new System.Drawing.Size(199, 500);
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).EndInit();
            this.radSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private Telerik.WinControls.UI.RadSplitContainer radSplitContainer1;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.SplitPanel splitPanel2;
		private Microsoft.Practices.CompositeUI.WinForms.SmartPartPlaceholder smartPartPlaceholder1;
		private CustomerQueueView customerQueueView1;
	}
}
