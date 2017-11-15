using System;
using System.Collections.Generic;
using System.Text;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Utility;
using Microsoft.Practices.CompositeUI.WinForms;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Telerik.CAB.WinForms.WorkSpaces
{
	/// <summary>
	/// A workspace that displays smart parts within a RadTabStrip.
	/// </summary>
	public class RadTabWorkspace : RadTabStrip, IComposableWorkspace<Control, RadTabSmartPartInfo>
	{
		private Dictionary<Control, TabItem> pages = new Dictionary<Control, TabItem>();
		private WorkspaceComposer<Control, RadTabSmartPartInfo> composer;
		private bool callComposerActivateOnIndexChange = true;
		private bool populatingPages = false;

		/// <summary>
		/// Initializes a new instance of the <see cref="TabWorkspace"/> class
		/// </summary>
		public RadTabWorkspace()
		{
			this.EnableTabControlMode = true;
			composer = new WorkspaceComposer<Control, RadTabSmartPartInfo>(this);

			this.TabSelected += new TabEventHandler(RadTabWorkspace_TabSelected);
		}

		private void RadTabWorkspace_TabSelected(object sender, TabEventArgs args)
		{
			this.OnSmartPartActivated(new WorkspaceEventArgs(this.GetControlFromSelectedPage()));
		}

		public override string ThemeClassName
		{
			get
			{
				return "Telerik.WinControls.UI.RadTabStrip";
			}
			set
			{
				base.ThemeClassName = value;
			}
		}

		/// <summary>
		/// Dependency injection setter property to get the <see cref="WorkItem"/> where the 
		/// object is contained.
		/// </summary>
		[ServiceDependency]
		public WorkItem WorkItem
		{
			set { composer.WorkItem = value; }
		}

		#region Properties

		/// <summary>
		/// Gets the collection of pages that the tab workspace uses.
		/// </summary>
		public ReadOnlyDictionary<Control, TabItem> Pages
		{
			get { return new ReadOnlyDictionary<Control, TabItem>(pages); }
		}

		#endregion

		#region Private

		private void SetTabProperties(TabItem page, RadTabSmartPartInfo smartPartInfo)
		{
			page.Text = String.IsNullOrEmpty(smartPartInfo.Title) ? page.Text : smartPartInfo.Title;

			try
			{
				TabItem currentSelection = (TabItem)this.SelectedTab;
				callComposerActivateOnIndexChange = false;
				if (smartPartInfo.Position == TabPosition.Beginning)
				{
					TabItem[] pages = GetTabPages();
					this.Items.Clear();

					this.Items.Add(page);
					this.Items.AddRange(pages);
				}
				else if (this.Items.Contains(page) == false)
				{
					//TabItem[] pages = GetTabPages();
					//this.Items.Clear();

					//this.Items.AddRange(pages);

					this.Items.Add(page);
				}

				// Preserve selection through the operation.
				if (currentSelection != null && this.SelectedTab == null)
				{
					this.SelectedTab = currentSelection;
				}

				//this.BackColor = smartPartInfo.BackColor;
				this.Enabled = smartPartInfo.Enabled;
				//this.ForeColor = smartPartInfo.ForeColor;
				this.Visible = smartPartInfo.Visible;
				//this.Size = smartPartInfo.Size;

				page.Enabled = smartPartInfo.PageEnabled;
				page.Visibility = (smartPartInfo.PageVisible) ? ElementVisibility.Visible : ElementVisibility.Collapsed;
			}
			finally
			{
				callComposerActivateOnIndexChange = true;
			}
		}

		private TabItem[] GetTabPages()
		{
			TabItem[] pages = new TabItem[this.Items.Count];
			for (int i = 0; i < pages.Length; i++)
			{
				pages[i] = (TabItem)this.Items[i];
			}

			return pages;
		}

		private void ShowExistingTab(Control smartPart)
		{
			TabItem tabItem = pages[smartPart];
			this.SelectItem(tabItem);
			//this.TabPages[key].Show();
		}

		private TabItem GetOrCreateTabPage(Control smartPart)
		{
			TabItem page = null;

			// If the tab was added with the control at design-time, it will have a parent control, 
			// and somewhere up its containment chain we'll find one of our tabs.

			RadTabStripContentPanel contentPanel = smartPart.Parent as RadTabStripContentPanel;
			if (contentPanel != null)
			{
				foreach (TabItem tabItem in this.Items)
				{
					if (tabItem.ContentPanel == contentPanel)
					{
						page = tabItem;
						break;
					}
				}
			}

			if (page == null)
			{
				page = new TabItem();
				page.ContentPanel.Controls.Add(smartPart);
				smartPart.Dock = DockStyle.Fill;
				page.Name = Guid.NewGuid().ToString();

				pages.Add(smartPart, page);
			}
			else if (pages.ContainsKey(smartPart) == false)
			{
				pages.Add(smartPart, page);
			}

			return page;
		}

		private void PopulatePages()
		{
			// If the page count matches don't waste the 
			// time repopulating the pages collection
			if (this.Items == null)
			{
				return;
			}

			if (!populatingPages && pages.Count != this.Items.Count)
			{
				foreach (TabItem page in this.Items)
				{
					if (this.pages.ContainsValue(page) == false)
					{
						Control control = GetControlFromPage(page);
						if (control != null && composer.SmartParts.Contains(control) == false)
						{
							RadTabSmartPartInfo tabinfo = new RadTabSmartPartInfo();
							tabinfo.ActivateTab = false;
							// Avoid circular calls to this method.
							populatingPages = true;
							try
							{
								Show(control, tabinfo);
							}
							finally
							{
								populatingPages = false;
							}
						}
					}
				}
			}
		}

		private void ControlDisposed(object sender, EventArgs e)
		{
			Control control = sender as Control;
			if (control != null && this.pages.ContainsKey(control))
			{
				composer.ForceClose(control);
			}
		}

		private Control GetControlFromSelectedPage()
		{
			return GetControlFromPage((TabItem)this.SelectedTab);
		}

		private Control GetControlFromPage(TabItem page)
		{
			Control control = null;
			if (page != null && page.ContentPanel != null)
			{
				if (page.ContentPanel.Controls.Count > 0)
				{
					control = page.ContentPanel.Controls[0];
				}
			}

			return control;
		}

		#endregion

		#region Internal implementation

		/// <summary>
		/// Fires the <see cref="SmartPartActivated"/> event whenever 
		/// the selected tab index changes.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
		protected override void OnTabIndexChanged(EventArgs e)
		{
			base.OnTabIndexChanged(e);

			if (callComposerActivateOnIndexChange && Items.Count != 0)
			{
				// Locate the smart part corresponding to the page.
				foreach (KeyValuePair<Control, TabItem> pair in pages)
				{
					if (pair.Value == this.SelectedTab)
					{
						((IComposableWorkspace<Control, RadTabSmartPartInfo>)this).Activate(pair.Key);
						return;
					}
				}

				// If we got here, we couldn't find a corresponding smart part for the 
				// currently active tab, hence we reset the ActiveSmartPart value.
				composer.SetActiveSmartPart(null);
			}
		}

		/// <summary>
		/// Hooks up tab pages added at design-time.
		/// </summary>
		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			//PopulatePages();
		}

		//private void ActivateSiblingTab()
		//{
		//    if (this.SelectedIndex > 0)
		//    {
		//        this.SelectedIndex = this.SelectedIndex - 1;
		//    }
		//    else if (this.SelectedIndex < this.TabPages.Count - 1)
		//    {
		//        this.SelectedIndex = this.SelectedIndex + 1;
		//    }
		//    else
		//    {
		//        composer.SetActiveSmartPart(null);
		//    }
		//}

		private void ResetSelectedIndexIfNoTabs()
		{
			// First control to come in is special. We need to 
			// set the selected index to a non-zero index so we 
			// get the appropriate behavior for activation.
			if (this.Items.Count == 0)
			{
				try
				{
					callComposerActivateOnIndexChange = false;
					this.SelectedTab = null;
				}
				finally
				{
					callComposerActivateOnIndexChange = true;
				}
			}
		}

		#endregion

		#region Protected virtual implementations

		/// <summary>
		/// Activates the smart part.
		/// </summary>
		protected virtual void OnActivate(Control smartPart)
		{
			//PopulatePages();

			TabItem tabItem = pages[smartPart];

			try
			{
				callComposerActivateOnIndexChange = false;
				this.SelectItem(tabItem);
				//selectedTab = TabPages[key];
				//TabPages[key].Show();
			}
			finally
			{
				callComposerActivateOnIndexChange = true;
			}
		}

		/// <summary>
		/// Applies the smart part display information to the smart part.
		/// </summary>
		protected virtual void OnApplySmartPartInfo(Control smartPart, RadTabSmartPartInfo smartPartInfo)
		{
			//PopulatePages();
			TabItem tabItem = pages[smartPart];
			SetTabProperties(tabItem, smartPartInfo);
			if (smartPartInfo.ActivateTab)
			{
				Activate(smartPart);
			}
		}

		/// <summary>
		/// Closes the smart part.
		/// </summary>
		protected virtual void OnClose(Control smartPart)
		{
			//PopulatePages();

			if (this.Items != null && this.Items.Contains(pages[smartPart]))
			{
				this.TabSelected -= new TabEventHandler(RadTabWorkspace_TabSelected);
				this.Items.Remove(pages[smartPart]);
				this.TabSelected += new TabEventHandler(RadTabWorkspace_TabSelected);
			}
			pages.Remove(smartPart);
			smartPart.Disposed -= ControlDisposed;
		}

		/// <summary>
		/// Hides the smart part.
		/// </summary>
		protected virtual void OnHide(Control smartPart)
		{
			if (smartPart.Visible)
			{
				//PopulatePages();
				TabItem tabItem = pages[smartPart];
				//TabPages[key].Hide();
				tabItem.Visibility = ElementVisibility.Collapsed;
				//ActivateSiblingTab();
				this.SelectNextItem(tabItem, false);
			}
		}

		/// <summary>
		/// Shows the control.
		/// </summary>
		protected virtual void OnShow(Control smartPart, RadTabSmartPartInfo smartPartInfo)
		{
			//PopulatePages();
			ResetSelectedIndexIfNoTabs();

			TabItem page = GetOrCreateTabPage(smartPart);
			SetTabProperties(page, smartPartInfo);

			if (smartPartInfo.ActivateTab)
			{
				Activate(smartPart);
			}

			smartPart.Disposed += ControlDisposed;
		}

		/// <summary>
		/// Raises the <see cref="SmartPartActivated"/> event.
		/// </summary>
		protected virtual void OnSmartPartActivated(WorkspaceEventArgs e)
		{
			if (SmartPartActivated != null)
			{
				SmartPartActivated(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="SmartPartClosing"/> event.
		/// </summary>
		protected virtual void OnSmartPartClosing(WorkspaceCancelEventArgs e)
		{
			if (SmartPartClosing != null)
			{
				SmartPartClosing(this, e);
			}
		}

		/// <summary>
		/// Converts a smart part information to a compatible one for the workspace.
		/// </summary>
		protected virtual RadTabSmartPartInfo ConvertFrom(ISmartPartInfo source)
		{
			return SmartPartInfo.ConvertTo<RadTabSmartPartInfo>(source);
		}

		#endregion

		#region IComposableWorkspace<Control,TelerikTabSmartPartInfo> Members

		/// <summary>
		/// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnActivate"/> for more information.
		/// </summary>
		void IComposableWorkspace<Control, RadTabSmartPartInfo>.OnActivate(Control smartPart)
		{
			OnActivate(smartPart);
		}

		/// <summary>
		/// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnApplySmartPartInfo"/> for more information.
		/// </summary>
		void IComposableWorkspace<Control, RadTabSmartPartInfo>.OnApplySmartPartInfo(Control smartPart, RadTabSmartPartInfo smartPartInfo)
		{
			OnApplySmartPartInfo(smartPart, smartPartInfo);
		}

		/// <summary>
		/// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnShow"/> for more information.
		/// </summary>
		void IComposableWorkspace<Control, RadTabSmartPartInfo>.OnShow(Control smartPart, RadTabSmartPartInfo smartPartInfo)
		{
			OnShow(smartPart, smartPartInfo);
		}

		/// <summary>
		/// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnHide"/> for more information.
		/// </summary>
		void IComposableWorkspace<Control, RadTabSmartPartInfo>.OnHide(Control smartPart)
		{
			OnHide(smartPart);
		}

		/// <summary>
		/// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.OnClose"/> for more information.
		/// </summary>
		void IComposableWorkspace<Control, RadTabSmartPartInfo>.OnClose(Control smartPart)
		{
			OnClose(smartPart);
		}

		/// <summary>
		/// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.RaiseSmartPartActivated"/> for more information.
		/// </summary>
		void IComposableWorkspace<Control, RadTabSmartPartInfo>.RaiseSmartPartActivated(WorkspaceEventArgs e)
		{
			OnSmartPartActivated(e);
		}

		/// <summary>
		/// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.RaiseSmartPartClosing"/> for more information.
		/// </summary>
		void IComposableWorkspace<Control, RadTabSmartPartInfo>.RaiseSmartPartClosing(WorkspaceCancelEventArgs e)
		{
			OnSmartPartClosing(e);
		}

		/// <summary>
		/// See <see cref="IComposableWorkspace{TSmartPart, TSmartPartInfo}.ConvertFrom"/> for more information.
		/// </summary>
		RadTabSmartPartInfo IComposableWorkspace<Control, RadTabSmartPartInfo>.ConvertFrom(ISmartPartInfo source)
		{
			return SmartPartInfo.ConvertTo<RadTabSmartPartInfo>(source);
		}

		#endregion

		#region IWorkspace Members

		/// <summary>
		/// See <see cref="IWorkspace.SmartPartClosing"/> for more information.
		/// </summary>
		public event EventHandler<WorkspaceCancelEventArgs> SmartPartClosing;

		/// <summary>
		/// See <see cref="IWorkspace.SmartPartActivated"/> for more information.
		/// </summary>
		public event EventHandler<WorkspaceEventArgs> SmartPartActivated;

		/// <summary>
		/// See <see cref="IWorkspace.SmartParts"/> for more information.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ReadOnlyCollection<object> SmartParts
		{
			get { return composer.SmartParts; }
		}

		/// <summary>
		/// See <see cref="IWorkspace.ActiveSmartPart"/> for more information.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object ActiveSmartPart
		{
			get { return composer.ActiveSmartPart; }
		}

		/// <summary>
		/// Shows the smart part in a new tab with the given information.
		/// </summary>
		public void Show(object smartPart, ISmartPartInfo smartPartInfo)
		{
			composer.Show(smartPart, smartPartInfo);
		}

		/// <summary>
		/// Shows the smart part in a new tab.
		/// </summary>
		public void Show(object smartPart)
		{
			composer.Show(smartPart);
		}

		/// <summary>
		/// Hides the smart part and its tab.
		/// </summary>
		public void Hide(object smartPart)
		{
			composer.Hide(smartPart);
		}

		/// <summary>
		/// Closes the smart part and removes its tab.
		/// </summary>
		public void Close(object smartPart)
		{
			composer.Close(smartPart);
		}

		/// <summary>
		/// Activates the tab the smart part is contained in.
		/// </summary>
		/// <param name="smartPart"></param>
		public void Activate(object smartPart)
		{
			composer.Activate(smartPart);
		}

		/// <summary>
		/// Applies new layout information on the tab of the smart part.
		/// </summary>
		public void ApplySmartPartInfo(object smartPart, ISmartPartInfo smartPartInfo)
		{
			composer.ApplySmartPartInfo(smartPart, smartPartInfo);
		}

		#endregion

	}
}
