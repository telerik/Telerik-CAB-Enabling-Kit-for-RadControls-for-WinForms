namespace FinanceApplicationCAB.Infrastructure.Layout
{
	partial class ShellLayoutView
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
            this.mainMenuStrip = new Telerik.WinControls.UI.RadMenu();
            this.fileMenuItem = new Telerik.WinControls.UI.RadMenuItem();
            this.exitMenuItem = new Telerik.WinControls.UI.RadMenuItem();
            this.statusLabel = new Telerik.WinControls.UI.RadLabelElement();
            this.radStatusStrip = new Telerik.WinControls.UI.RadStatusStrip();
            this.radDockWorkspace = new Telerik.WinControls.CompositeUI.RadDockWorkspace();
            this.documentContainer1 = new Telerik.WinControls.UI.Docking.DocumentContainer();
            ((System.ComponentModel.ISupportInitialize)(this.mainMenuStrip)).BeginInit();
            this.radStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radDockWorkspace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentContainer1)).BeginInit();
            this.SuspendLayout();
            // 
            // _mainMenuStrip
            // 
            this.mainMenuStrip.AllowMerge = false;
            this.mainMenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.mainMenuStrip.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.fileMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "_mainMenuStrip";
            // 
            // 
            // 
            this.mainMenuStrip.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.mainMenuStrip.Size = new System.Drawing.Size(613, 20);
            this.mainMenuStrip.TabIndex = 4;
            this.mainMenuStrip.Text = "_mainMenuStrip";
            // 
            // _fileToolStripMenuItem
            // 
            this.fileMenuItem.AccessibleDescription = "&File";
            this.fileMenuItem.AccessibleName = "&File";
            this.fileMenuItem.ClickMode = Telerik.WinControls.ClickMode.Press;
            this.fileMenuItem.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.exitMenuItem});
            this.fileMenuItem.Text = "&File";
            this.fileMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // _exitToolStripMenuItem
            // 
            this.exitMenuItem.AccessibleDescription = "E&xit";
            this.exitMenuItem.AccessibleName = "E&xit";
            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.exitMenuItem.Click += new System.EventHandler(this.OnFileExit);
            // 
            // _statusLabel
            // 
            this.statusLabel.Name = "_statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(39, 17);
            this.statusLabel.Text = "Ready";
            // 
            // _mainStatusStrip
            // 
            this.radStatusStrip.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.statusLabel});
            this.radStatusStrip.Location = new System.Drawing.Point(0, 462);
            this.radStatusStrip.Name = "_mainStatusStrip";
            this.radStatusStrip.Size = new System.Drawing.Size(613, 22);
            this.radStatusStrip.TabIndex = 6;
            this.radStatusStrip.Text = "_mainStatusStrip";
            // 
            // dockingManager
            // 
            this.radDockWorkspace.Controls.Add(this.documentContainer1);
            this.radDockWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radDockWorkspace.DocumentManager.DocumentInsertOrder = Telerik.WinControls.UI.Docking.DockWindowInsertOrder.InFront;
            this.radDockWorkspace.IsCleanUpTarget = true;
            this.radDockWorkspace.Location = new System.Drawing.Point(0, 20);
            this.radDockWorkspace.MainDocumentContainer = this.documentContainer1;
            this.radDockWorkspace.Name = "dockingManager";
            this.radDockWorkspace.Padding = new System.Windows.Forms.Padding(5);
            // 
            // 
            // 
            this.radDockWorkspace.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.radDockWorkspace.Size = new System.Drawing.Size(613, 442);
            this.radDockWorkspace.SplitterWidth = 4;
            this.radDockWorkspace.TabIndex = 7;
            this.radDockWorkspace.TabStop = false;
            this.radDockWorkspace.Text = "dockingManager";
            // 
            // documentContainer1
            // 
            this.documentContainer1.Location = new System.Drawing.Point(5, 5);
            this.documentContainer1.Name = "documentContainer1";
            this.documentContainer1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // 
            // 
            this.documentContainer1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.documentContainer1.Size = new System.Drawing.Size(603, 432);
            this.documentContainer1.SizeInfo.SizeMode = Telerik.WinControls.UI.Docking.SplitPanelSizeMode.Fill;
            this.documentContainer1.SplitterWidth = 4;
            this.documentContainer1.TabIndex = 0;
            this.documentContainer1.TabStop = false;
            // 
            // ShellLayoutView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radDockWorkspace);
            this.Controls.Add(this.radStatusStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.Name = "ShellLayoutView";
            this.Size = new System.Drawing.Size(613, 484);
            ((System.ComponentModel.ISupportInitialize)(this.mainMenuStrip)).EndInit();
            this.radStatusStrip.ResumeLayout(false);
            this.radStatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radDockWorkspace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentContainer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private Telerik.WinControls.UI.RadMenu mainMenuStrip;
		private Telerik.WinControls.UI.RadMenuItem fileMenuItem;
        private Telerik.WinControls.UI.RadMenuItem exitMenuItem;
        private Telerik.WinControls.UI.RadLabelElement statusLabel;
        private Telerik.WinControls.UI.RadStatusStrip radStatusStrip;
        private Telerik.WinControls.CompositeUI.RadDockWorkspace radDockWorkspace;
        private Telerik.WinControls.UI.Docking.DocumentContainer documentContainer1;
	}
}

