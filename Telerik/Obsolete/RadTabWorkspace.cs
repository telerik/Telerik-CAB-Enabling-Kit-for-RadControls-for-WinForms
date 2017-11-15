using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.WinForms;
using Telerik.WinControls.UI;

namespace Telerik.CAB.WinForms.WorkSpaces
{
    /// <summary>
    /// A workspace that displays smart parts within a RadTabStrip.
    /// </summary>
    [Obsolete("This type is obsolete. Please, use the RadPageViewWorkspace instead.")]
    public class RadTabWorkspace : RadPageView, IComposableWorkspace<Control, RadTabSmartPartInfo>
    {
        //private Dictionary<Control, RadPageViewPage> pages = new Dictionary<Control, RadPageViewPage>();
        private WorkspaceComposer<Control, RadTabSmartPartInfo> composer;
        private bool callComposerActivateOnIndexChange = true;
        private bool populatingPages = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="TabWorkspace"/> class
        /// </summary>
        public RadTabWorkspace()
        {
            //this.EnableTabControlMode = true;

            composer = new WorkspaceComposer<Control, RadTabSmartPartInfo>(this);

            this.SelectedPageChanging += new EventHandler<RadPageViewCancelEventArgs>(RadTabWorkspace_SelectedPageChanging);
            this.SelectedPageChanged += new EventHandler(RadTabWorkspace_SelectedPageChanged);
        }

        void RadTabWorkspace_SelectedPageChanging(object sender, RadPageViewCancelEventArgs e)
        {

        }

        void RadTabWorkspace_SelectedPageChanged(object sender, EventArgs e)
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

        ///// <summary>
        ///// Gets the collection of pages that the tab workspace uses.
        ///// </summary>
        //public ReadOnlyDictionary<Control, RadPageViewPage> Pages
        //{
        //    get { return new ReadOnlyDictionary<Control, RadPageViewPage>(pages); }
        //}

        #endregion

        #region Private

        private void SetTabProperties(RadPageViewPage page, RadTabSmartPartInfo smartPartInfo)
        {
            page.Text = String.IsNullOrEmpty(smartPartInfo.Title) ? page.Text : smartPartInfo.Title;

            try
            {
                RadPageViewPage currentSelection = (RadPageViewPage)this.SelectedPage;
                callComposerActivateOnIndexChange = false;
                if (smartPartInfo.Position == TabPosition.Beginning)
                {
                    RadPageViewPage[] pages = GetTabPages();
                    this.Pages.Clear();

                    this.Pages.Add(page);
                    //this.Pages.AddRange(pages);
                }
                else if (this.Pages.Contains(page) == false)
                {
                    //TabItem[] pages = GetTabPages();
                    //this.Pages.Clear();

                    //this.Pages.AddRange(pages);

                    this.Pages.Add(page);
                }

                // Preserve selection through the operation.
                if (currentSelection != null && this.SelectedPage == null)
                {
                    this.SelectedPage = currentSelection;
                }

                //this.BackColor = smartPartInfo.BackColor;
                this.Enabled = smartPartInfo.Enabled;
                //this.ForeColor = smartPartInfo.ForeColor;
                this.Visible = smartPartInfo.Visible;
                //this.Size = smartPartInfo.Size;

                page.Enabled = smartPartInfo.PageEnabled;
                page.IsContentVisible = smartPartInfo.PageVisible;
            }
            finally
            {
                callComposerActivateOnIndexChange = true;
            }
        }

        private RadPageViewPage[] GetTabPages()
        {
            RadPageViewPage[] pages = new RadPageViewPage[this.Pages.Count];
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i] = (RadPageViewPage)this.Pages[i];
            }

            return pages;
        }

        //private void ShowExistingTab(Control smartPart)
        //{
        //    RadPageViewPage tabItem = pages[smartPart];
        //    this.SelectedPage = smartPart  as RadPageViewPage;//.SelectItem(tabItem);
        //    //this.TabPages[key].Show();
        //}

        private RadPageViewPage GetOrCreateTabPage(Control smartPart)
        {
            RadPageViewPage page = null;

            // If the tab was added with the control at design-time, it will have a parent control, 
            // and somewhere up its containment chain we'll find one of our tabs.

            RadTabStripContentPanel contentPanel = smartPart.Parent as RadTabStripContentPanel;
            if (contentPanel != null)
            {
                foreach (RadPageViewPage tabItem in this.Pages)
                {
                    if (tabItem.Controls.Contains(contentPanel))
                    {
                        page = tabItem;
                        break;
                    }
                }
            }

            if (page == null)
            {
                page = new RadPageViewPage();
                page.Controls.Add(smartPart);
                smartPart.Dock = DockStyle.Fill;
                page.Name = Guid.NewGuid().ToString();

                this.Pages.Add(page);
            }
            //else if (pages.ContainsKey(smartPart) == false)
            //{
            //    this.Pages.Add(page);
            //}

            return page;
        }

        private void PopulatePages()
        {
            // If the page count matches don't waste the 
            // time repopulating the pages collection
            if (this.Pages == null)
            {
                return;
            }

            if (!populatingPages)
            {
                foreach (RadPageViewPage page in this.Pages)
                {
                    if (this.Pages.Contains(page) == false)
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

        //private void ControlDisposed(object sender, EventArgs e)
        //{
        //    Control control = sender as Control;
        //    if (control != null && this.Pages.ContainsKey(control))
        //    {
        //        composer.ForceClose(control);
        //    }
        //}

        private Control GetControlFromSelectedPage()
        {
            return this.SelectedPage;// GetControlFromPage((RadPageViewPage)this.SelectedTab);
        }

        private Control GetControlFromPage(RadPageViewPage page)
        {
            //Control control = null;
            //if (page != null && page.ContentPanel != null)
            //{
            //    if (page.ContentPanel.Controls.Count > 0)
            //    {
            //        control = page.ContentPanel.Controls[0];
            //    }
            //}

            //TODO: review!
            return page;
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

            if (callComposerActivateOnIndexChange && Pages.Count != 0)
            {
                // Locate the smart part corresponding to the page.
                foreach (RadPageViewPage page in this.Pages)
                {
                    if (page == this.SelectedPage)
                    {
                        ((IComposableWorkspace<Control, RadTabSmartPartInfo>)this).Activate(page);
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
            if (this.Pages.Count == 0)
            {
                try
                {
                    callComposerActivateOnIndexChange = false;
                    this.SelectedPage = null;
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

            RadPageViewPage tabItem = this.SelectedPage;//pages[smartPart];

            try
            {
                callComposerActivateOnIndexChange = false;
                //this.SelectItem(tabItem);
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
            RadPageViewPage tabItem = this.SelectedPage; // this.Pages[smartPart];
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

            if (this.Pages != null)//&& this.Pages.Contains(pages[smartPart])
            {
                this.SelectedPageChanging += new EventHandler<RadPageViewCancelEventArgs>(RadTabWorkspace_SelectedPageChanging);
                this.SelectedPageChanged += new EventHandler(RadTabWorkspace_SelectedPageChanged);
                //this.Pages.Remove(pages[smartPart]);
                //this.SelectedPageChanged += new TabEventHandler(RadTabWorkspace_TabSelected);
            }

            //pages.Remove(smartPart);
            //smartPart.Disposed -= ControlDisposed;
        }

        /// <summary>
        /// Hides the smart part.
        /// </summary>
        protected virtual void OnHide(Control smartPart)
        {
            if (smartPart.Visible)
            {
                //PopulatePages();
                //RadPageViewPage tabItem = pages[smartPart];
                //TabPages[key].Hide();
                this.SelectedPage.IsContentVisible = false;// ElementVisibility.Collapsed;
                //ActivateSiblingTab();
                //this.SelectNextItem(tabItem, false);
            }
        }

        /// <summary>
        /// Shows the control.
        /// </summary>
        protected virtual void OnShow(Control smartPart, RadTabSmartPartInfo smartPartInfo)
        {
            //PopulatePages();
            ResetSelectedIndexIfNoTabs();

            RadPageViewPage page = GetOrCreateTabPage(smartPart);
            SetTabProperties(page, smartPartInfo);

            if (smartPartInfo.ActivateTab)
            {
                Activate(smartPart);
            }

            //smartPart.Disposed += ControlDisposed;
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
