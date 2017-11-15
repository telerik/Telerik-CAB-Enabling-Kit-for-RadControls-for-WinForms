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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShellLayoutView));
			this._mainMenuStrip = new Telerik.WinControls.UI.RadMenu();
			this._fileToolStripMenuItem = new Telerik.WinControls.UI.RadMenuItem();
			this._exitToolStripMenuItem = new Telerik.WinControls.UI.RadMenuItem();
			this._statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this._mainStatusStrip = new System.Windows.Forms.StatusStrip();
			this.dockingManager = new Telerik.WinControls.UI.Docking.RadDock();
			((System.ComponentModel.ISupportInitialize)(this._mainMenuStrip)).BeginInit();
			this._mainStatusStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dockingManager)).BeginInit();
			this.dockingManager.SuspendLayout();
			this.SuspendLayout();
			// 
			// _mainMenuStrip
			// 
			this._mainMenuStrip.AllowMerge = false;
			this._mainMenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
			this._mainMenuStrip.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this._fileToolStripMenuItem});
			this._mainMenuStrip.Location = new System.Drawing.Point(0, 0);
			this._mainMenuStrip.Name = "_mainMenuStrip";
			// 
			// 
			// 
			this._mainMenuStrip.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
			this._mainMenuStrip.Size = new System.Drawing.Size(613, 22);
			this._mainMenuStrip.TabIndex = 4;
			this._mainMenuStrip.Text = "_mainMenuStrip";
			// 
			// _fileToolStripMenuItem
			// 
			this._fileToolStripMenuItem.Class = "RadMenuItem";
			this._fileToolStripMenuItem.ClickMode = Telerik.WinControls.ClickMode.Press;
			this._fileToolStripMenuItem.DescriptionText = "";
			this._fileToolStripMenuItem.HasTwoColumnDropDown = false;
			this._fileToolStripMenuItem.IsMainMenuItem = true;
			this._fileToolStripMenuItem.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this._exitToolStripMenuItem});
			this._fileToolStripMenuItem.Text = "&File";
			this._fileToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this._fileToolStripMenuItem.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
			// 
			// _exitToolStripMenuItem
			// 
			this._exitToolStripMenuItem.Class = "RadMenuItem";
			this._exitToolStripMenuItem.DescriptionText = "";
			this._exitToolStripMenuItem.HasTwoColumnDropDown = false;
			this._exitToolStripMenuItem.IsMainMenuItem = false;
			this._exitToolStripMenuItem.Text = "E&xit";
			this._exitToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this._exitToolStripMenuItem.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
			this._exitToolStripMenuItem.Click += new System.EventHandler(this.OnFileExit);
			// 
			// _statusLabel
			// 
			this._statusLabel.Name = "_statusLabel";
			this._statusLabel.Size = new System.Drawing.Size(38, 17);
			this._statusLabel.Text = "Ready";
			// 
			// _mainStatusStrip
			// 
			this._mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._statusLabel});
			this._mainStatusStrip.Location = new System.Drawing.Point(0, 462);
			this._mainStatusStrip.Name = "_mainStatusStrip";
			this._mainStatusStrip.Size = new System.Drawing.Size(613, 22);
			this._mainStatusStrip.TabIndex = 6;
			this._mainStatusStrip.Text = "_mainStatusStrip";
			// 
			// dockingManager
			// 
			this.dockingManager.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dockingManager.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dockingManager.Location = new System.Drawing.Point(0, 22);
			this.dockingManager.Name = "dockingManager";
			// 
			// dockingManager.PrimarySite
			// 
			this.dockingManager.Size = new System.Drawing.Size(613, 440);
			this.dockingManager.TabIndex = 7;
			this.dockingManager.Text = "dockingManager";
			// 
			// ShellLayoutView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dockingManager);
			this.Controls.Add(this._mainStatusStrip);
			this.Controls.Add(this._mainMenuStrip);
			this.Name = "ShellLayoutView";
			this.Size = new System.Drawing.Size(613, 484);
			((System.ComponentModel.ISupportInitialize)(this._mainMenuStrip)).EndInit();
			this._mainStatusStrip.ResumeLayout(false);
			this._mainStatusStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dockingManager)).EndInit();
			this.dockingManager.ResumeLayout(false);
			this.dockingManager.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Telerik.WinControls.UI.RadMenu _mainMenuStrip;
		private Telerik.WinControls.UI.RadMenuItem _fileToolStripMenuItem;
		private Telerik.WinControls.UI.RadMenuItem _exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel _statusLabel;
		private System.Windows.Forms.StatusStrip _mainStatusStrip;
		private Telerik.WinControls.UI.Docking.RadDock dockingManager;
	}
}

