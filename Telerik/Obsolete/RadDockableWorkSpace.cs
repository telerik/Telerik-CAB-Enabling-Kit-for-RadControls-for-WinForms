using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Utility;
using Telerik.CAB.WinForms.SmartPartInfos;
using Telerik.WinControls.UI.Docking;

namespace Telerik.CAB.WinForms.WorkSpaces
{
    /// <summary>
    /// A WorkSpace that displays smart parts within the Docking Manager.
    /// </summary>
    [Obsolete("This type is obsolete. Please, use the RadDockWorkspace instead.")]
    public partial class RadDockableWorkspace : Workspace<Control, DockingSmartPartInfo>
    {
        #region Fields

        /// <summary>
        /// Holds a reference to all dockable windows.
        /// </summary>
        private Dictionary<string, DockWindow> windows = new Dictionary<string, DockWindow>();

        /// <summary>
        /// Used to prevent infinite loops when initially creating windows
        /// </summary>
        private bool populatingWindows = false;

        /// <summary>
        /// True if a SmartPart should be activated when its dockable window is activated
        /// </summary>
        private bool setActiveForSmartPart = true;

        /// <summary>
        /// True if design-time dockable windows have been added to the colleciton
        /// </summary>
        private bool initialized = false;

        /// <summary>
        /// The Telerik DockingManager responsible for managing docking windows.
        /// </summary>
        private RadDock dockManager = null;

        #endregion

		public void SuspendLayout()
		{
			if(this.dockManager != null)
			{
				this.dockManager.SuspendLayout();
			}
		}

		public void ResumeLayout()
		{
			if (this.dockManager != null)
			{
				this.dockManager.ResumeLayout();
			}
		}

        /// <summary>
		/// Initializes a new instance of the <see cref="RadDockableWorkspace"/> class.
        /// </summary>
        /// <param name="dockManager">The dock manager.</param>
        /// <param name="owner">The owner.</param>
        public RadDockableWorkspace(RadDock dockManager, ContainerControl owner)
        {
            this.LoadUpParameters(dockManager, owner);
        }

        private void LoadUpParameters(RadDock dockManager, ContainerControl owner)
        {
            this.dockManager = dockManager;
            this.dockManager.ActiveWindowChanged += new DockWindowEventHandler(OnDockManager_ActiveWindowChanged);
            WireUpWindow();
        }

        private void OnDockManager_ActiveWindowChanged(object sender, DockWindowEventArgs e)
        {
            //Should we take the first control in the controls collection here?
            Control smartPart = this.dockManager.ActiveWindow.Controls[0];

            if (smartPart != null)
            {
                if (smartPart != null && this.setActiveForSmartPart && this.ActiveSmartPart != smartPart)
                {
                    Activate(smartPart);
                }
            }
        }

		private void WireUpWindow()
		{
            this.dockManager.DockStateChanged += new DockWindowEventHandler(OnDockManager_DockStateChanged);
            this.dockManager.DockStateChanging += new DockStateChangingEventHandler(OnDockManager_DockStateChanging);
		}

        private void OnDockManager_DockStateChanging(object sender, DockStateChangingEventArgs e)
        {
            if (e.NewDockState != DockState.Hidden)
            {
                return;
            }

            Control smartPart = e.DockWindow.Controls[0];
            if (smartPart != null)
            {
                WorkspaceCancelEventArgs cancelArgs = new WorkspaceCancelEventArgs(smartPart);
                base.RaiseSmartPartClosing(cancelArgs);
                e.Cancel = cancelArgs.Cancel;
            }
        }

        private void OnDockManager_DockStateChanged(object sender, DockWindowEventArgs e)
        {
            DockState state = e.DockWindow.DockState;
            if (state != DockState.Hidden)
            {
                return;
            }

            Control smartPart = e.DockWindow.Controls[0];
            if (state == DockState.Hidden)
            {
                if (smartPart != null)
                {
                    this.RemoveEntry(smartPart);
                    base.InnerSmartParts.Remove(smartPart);
                }
            }
        }

        #region Properties

        /// <summary>
        /// Read-only view of Windows Dictionary.
        /// </summary>
        [Browsable(false)]
        public ReadOnlyDictionary<string, DockWindow> Windows
        {
            get { return new ReadOnlyDictionary<string, DockWindow>(this.windows); }
        }

        #endregion

        #region Protected

        /// <summary>
        /// Sets the the dockable window properties from the DockingSmartPartInfo class instance.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="smartPartInfo">The smart part info.</param>
        protected void SetWindowProperties(DockWindow dockable, DockingSmartPartInfo smartPartInfo)
        {
			if (dockable == null)
			{
				return;
			}

			dockable.Text = smartPartInfo.Title;

			//To be revised
			if(smartPartInfo.TabbedDocument)
			{
                this.dockManager.DockWindow(dockable, smartPartInfo.DockPosition);
                this.dockManager.SetWindowState(dockable, DockState.TabbedDocument);
			}
			else
			{
                this.dockManager.DockWindow(dockable, smartPartInfo.DockPosition);
			}

			if (smartPartInfo.AutoHideOnLoad)
			{
				dockable.DockState = DockState.AutoHide;
			}

            if (!smartPartInfo.AutoHideOnLoad)
            {
                this.dockManager.ActiveWindow = dockable;
            }

			if(smartPartInfo.Image != null)
			{
				dockable.Image = smartPartInfo.Image;
			}

			dockable.AutoHideSize = new Size(smartPartInfo.Width, smartPartInfo.Height);
			dockable.DefaultFloatingSize = new Size(smartPartInfo.Width, smartPartInfo.Height);

            dockable.DockTabStrip.SizeInfo.SizeMode = SplitPanelSizeMode.Absolute;
            dockable.DockTabStrip.SizeInfo.AbsoluteSize = new Size(smartPartInfo.Width, dockable.Height);

			(dockable as Control).BackColor = smartPartInfo.BackColor;

            ToolStripCaptionButtons buttons = ToolStripCaptionButtons.None;

            buttons |= smartPartInfo.CloseButtonVisible ? ToolStripCaptionButtons.Close : ToolStripCaptionButtons.None;
            buttons |= smartPartInfo.DropDownButtonVisible ? ToolStripCaptionButtons.SystemMenu : ToolStripCaptionButtons.None;
            buttons |= smartPartInfo.HideButtonVisible ? ToolStripCaptionButtons.AutoHide : ToolStripCaptionButtons.None;

            dockable.ToolCaptionButtons = buttons;
            if (!smartPartInfo.TabStripVisible)
            {
                dockable.DockTabStrip.TabStripElement.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            }

            if (dockable.TabStrip is ToolTabStrip)
            {
                (dockable.TabStrip as ToolTabStrip).CaptionVisible = smartPartInfo.CaptionVisible;
            }
        }

        /// <summary>
        /// Creates a dockable window if it does not already exist and adds the given control.
        /// </summary>
        /// <param name="smartPart"></param>
        /// <returns><see cref="TD.SandDock.DockableWindow"/></returns>
        protected DockWindow GetOrCreateWindow(Control smartPart)
        {
			if (smartPart == null)
			{
				return null;
			}

            DockWindow window = null;

			if (this.windows.ContainsKey(smartPart.Name))
			{
				window = this.windows[smartPart.Name];
			}
			else
			{
                window = new ToolWindow();
				window.Text = smartPart.Name;
				smartPart.Dock = DockStyle.Fill;
				(window as Control).Controls.Add(smartPart);

				this.windows.Add(smartPart.Name, window);
			}

            return window;
        }

        #endregion

        #region Private

        
        /// <summary>
        /// Removes the SmartPart from the Window collection
        /// </summary>
        /// <param name="smartPart">The smart part.</param>
        private void RemoveEntry(Control smartPart)
        {
            this.windows.Remove(smartPart.Name);
        }

        private void PopulateWindows()
        {
			if (this.initialized)
			{
				return;
			}

            this.initialized = true;

			DockWindowCollection dockableWindows = this.dockManager.DockWindows;
            if (this.populatingWindows || this.windows.Count == dockableWindows.Count)
            {
				return;
            }

			this.dockManager.SuspendLayout();

            foreach (DockWindow dockableWindow in dockableWindows)
            {
				Control smartPart = (dockableWindow as Control).Controls[0];
				if (!this.windows.ContainsValue(dockableWindow))
                {
					this.windows.Add(smartPart.Name, dockableWindow);

                    if (smartPart != null && !base.SmartParts.Contains(smartPart))
                    {
                        DockingSmartPartInfo smartPartInfo = new DockingSmartPartInfo();

                        // Avoid circular calls to this method.
                        this.populatingWindows = true;
                        try
                        {
                            this.Show(smartPart, smartPartInfo);
                        }
                        finally
                        {
                            this.populatingWindows = false;
                        }
                    }
                }
			}

			this.dockManager.ResumeLayout();
        }

        /// <summary>
        /// If the SmartPart is disposed make sure we remove it from our collection
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ControlDisposed(object sender, EventArgs e)
        {
            Control smartPart = sender as Control;
            if (smartPart != null && this.windows.ContainsKey(smartPart.Name))
            {
                base.CloseInternal(smartPart);
            }
        }

        /// <summary>
        /// Updates the window with the given SmartPartInfo and shows it
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="smartPartInfo">The smart part info.</param>
        private void ShowWindow(DockWindow window, DockingSmartPartInfo smartPartInfo)
        {
            this.SetWindowProperties(window, smartPartInfo);
        }

        #endregion

        #region Overrided methods
        /// <summary>
        /// Shows the window for the SmartPart and brings it to the front.
        /// </summary>
        /// <param name="smartPart"></param>
        protected override void OnActivate(Control smartPart)
        {
            PopulateWindows();

            // Prevent double firing from composer Workspace class and from form.
            try
            {
                this.setActiveForSmartPart = false;
                DockWindow window = this.windows[smartPart.Name];
                this.dockManager.DisplayWindow(window);
                this.dockManager.ActiveWindow = window;

            }
            finally
            {
                this.setActiveForSmartPart = true;
            }
        }

        /// <summary>
        /// Sets the properties on the window based on the information.
        /// </summary>
        protected override void OnApplySmartPartInfo(Control smartPart, DockingSmartPartInfo smartPartInfo)
        {
            PopulateWindows();

            DockWindow dockable = this.GetOrCreateWindow(smartPart);
			this.SetWindowProperties(dockable, smartPartInfo);
        }

        /// <summary>
        /// Closes the dockable window where the smart part is being shown.
        /// </summary>
        protected override void OnClose(Control smartPart)
        {
            PopulateWindows();

            Control window = this.windows[smartPart.Name] as Control;
            smartPart.Disposed -= ControlDisposed;
            this.CloseDockedWindow(window as DockWindow);
            // Remove the smartPart from the form to avoid disposing it.
            window.Controls.Remove(smartPart);
            window.Dispose();

            this.windows.Remove(smartPart.Name);
        }

        private void CloseDockedWindow(DockWindow window)
        {
			this.dockManager.RemoveWindow(window);
        }


        /// <summary>
        /// Hides the dockable window where the smart part is being shown.
        /// </summary>
        protected override void OnHide(Control smartPart)
        {
            DockWindow dockable = this.windows[smartPart.Name];

			if(dockable == null)
			{
				return;
			}

            if (dockable.DockState != DockState.Hidden)
            {
                PopulateWindows();
                this.dockManager.SetWindowState(dockable, DockState.Hidden);
            }
        }

        /// <summary>
        /// Shows a dockable window for the smart part and sets its properties.
        /// </summary>
        protected override void OnShow(Control smartPart, DockingSmartPartInfo smartPartInfo)
        {
			if (smartPart == null)
			{
				return;
			}

            if (!this.windows.ContainsKey(smartPart.Name))
            {
                DockWindow window = this.GetOrCreateWindow(smartPart);
				if (window != null)
				{
					this.PopulateWindows();
					this.ShowWindow(window, smartPartInfo);
					smartPart.Disposed += ControlDisposed;
				}
            }
            else
            {
				this.dockManager.ActiveWindow = this.windows[smartPart.Name];
            }
        }

        /// <summary>
		/// Provides conversion from ISmartPartInfo to DockingSmartPartInfo
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        protected override DockingSmartPartInfo ConvertFrom(ISmartPartInfo source)
        {
            return SmartPartInfo.ConvertTo<DockingSmartPartInfo>(source);
        }

        #endregion
    }
}