using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.CompositeUI
{
    public class RadPageViewWorkspace : RadPageView, IComposableWorkspace<Control, PageSmartPartInfo>
    {
        #region Fields
        private const int Suspend_Activated = 0x1;
        private const int Suspend_Close = Suspend_Activated << 1;

        private WorkspaceComposer<Control, PageSmartPartInfo> composer;
        private BitVector32 notifications = new BitVector32();

        #endregion

        #region Constructors

        public RadPageViewWorkspace()
        {
            this.composer = new WorkspaceComposer<Control, PageSmartPartInfo>(this);
        }

        #endregion

        #region Public 

        public event System.EventHandler<WorkspaceEventArgs> SmartPartActivated;
        public event System.EventHandler<WorkspaceCancelEventArgs> SmartPartClosing;

        [ServiceDependency]
        public WorkItem WorkItem
        {
            set
            {
                this.composer.WorkItem = value;
            }
        }

        public object ActiveSmartPart
        {
            get
            {
                if (this.SelectedPage != null && this.SelectedPage.Controls.Count > 0)
                {
                    return this.SelectedPage.Controls[0];
                }

                return null;
            }
        }

        public System.Collections.ObjectModel.ReadOnlyCollection<object> SmartParts
        {
            get 
            {
                List<object> smartParts = new List<object>();
                for (int i = 0; i < this.Pages.Count; i++)
                {
                    if (this.Pages[i].Controls.Count > 0)
                    {
                        smartParts.Add(this.Pages[i].Controls[0]);
                    }
                }

                return new System.Collections.ObjectModel.ReadOnlyCollection<object>(smartParts);
            }
        }

        public void Activate(object smartPart)
        {
            this.composer.Activate(smartPart);
        }

        public void ApplySmartPartInfo(object smartPart, ISmartPartInfo smartPartInfo)
        {
            this.composer.ApplySmartPartInfo(smartPart, smartPartInfo);
        }

        public void Close(object smartPart)
        {
            this.composer.Close(smartPart);
        }

        public void Hide(object smartPart)
        {
            this.composer.Hide(smartPart);
        }

        public void Show(object smartPart)
        {
            this.composer.Show(smartPart);
        }

        public void Show(object smartPart, ISmartPartInfo smartPartInfo)
        {
            this.composer.Show(smartPart, smartPartInfo);
        }

        #endregion

        #region Internal

        protected override void OnSelectedPageChanged(EventArgs e)
        {
            base.OnSelectedPageChanged(e);

            if (!notifications[Suspend_Activated] && this.SelectedPage != null && this.SelectedPage.Controls.Count > 0)
            {
                OnSmartPartActivated(new WorkspaceEventArgs(this.SelectedPage.Controls[0]));
            }
        }

        protected override void OnPageRemoving(RadPageViewCancelEventArgs e)
        {
            if (!notifications[Suspend_Close])
            {
                WorkspaceCancelEventArgs args = new WorkspaceCancelEventArgs(e.Page.Controls[0]);
                OnSmartPartClosing(args);
                e.Cancel = args.Cancel;
            }

            base.OnPageRemoving(e);
        }

        protected virtual void OnSmartPartActivated(WorkspaceEventArgs e)
        {
            if (this.SmartPartActivated != null)
            {
                this.SmartPartActivated(this, e);
            }
        }

        protected virtual void OnSmartPartClosing(WorkspaceCancelEventArgs e)
        {
            if (this.SmartPartClosing != null)
            {
                this.SmartPartClosing(this, e);
            }
        }

        protected virtual void OnActivate(Control smartPart)
        {
            notifications[Suspend_Activated] = true;

            RadPageViewPage page = GetSmartPart(smartPart);
            if (page != null)
            {
                this.SelectedPage = page;
            }
            else
            {
                for (int i = 0; i < this.Pages.Count; i++)
                {
                    if (this.Pages[i].Controls.Count > 0 && this.Pages[i].Controls[0] == smartPart)
                    {
                        this.SelectedPage = this.Pages[i];
                    }
                }
            }

            notifications[Suspend_Activated] = false;
        }

        protected virtual void OnApplySmartPartInfo(Control smartPart, PageSmartPartInfo smartPartInfo)
        {
            RadPageViewPage page = GetSmartPart(smartPart, smartPartInfo);
            if (page == null)
            {
                page = new RadPageViewPage();
                page.Text = smartPartInfo.Title;
                smartPart.Dock = DockStyle.Fill;
                page.Controls.Add(smartPart);
                this.Pages.Add(page);
                return;
            }

            //apply settings
            page.Text = smartPartInfo.Title;
            smartPart.Dock = DockStyle.Fill;
            page.Controls.Add(smartPart);
        }

        protected virtual void OnClose(Control smartPart)
        {
            notifications[Suspend_Close] = true;

            RadPageViewPage page = GetSmartPart(smartPart);
            if (page != null)
            {
                this.Pages.Remove(page);
            }

            notifications[Suspend_Close] = false;
        }

        protected virtual void OnHide(Control smartPart)
        {
            RadPageViewPage page = GetSmartPart(smartPart);
            if (page != null)
            {
                this.Pages.Remove(page);
            }
        }

        protected virtual void OnShow(Control smartPart, PageSmartPartInfo smartPartInfo)
        {
            RadPageViewPage page = GetSmartPart(smartPart, smartPartInfo);
            if (page != null)
            {
                this.SelectedPage = page;
                return;
            }

            page = new RadPageViewPage();
            page.Text = smartPartInfo.Title;
            if (string.IsNullOrEmpty(page.Text))
            {
                page.Text = "Page" + (this.Pages.Count + 1).ToString();
            }

            smartPart.Dock = DockStyle.Fill;
            page.Controls.Add(smartPart);

            this.Pages.Add(page);
            this.SelectedPage = page;
        }

        private RadPageViewPage GetSmartPart(Control smartPart, PageSmartPartInfo smartPartInfo)
        {
            RadPageViewPage page =  this.Pages[smartPartInfo.Title];
            if(page != null && page.Controls[0] == smartPart)
            {
                return page;
            }

            return null;
        }

        private RadPageViewPage GetSmartPart(Control smartPart)
        {
            RadPageViewPageCollection pages = this.Pages;
            for (int i = 0; i < pages.Count; i++)
            {
                if (pages[i].Controls.Count > 0 && pages[i].Controls[0] == smartPart)
                {
                    return pages[i];
                }
            }

            return null;
        }

        void IComposableWorkspace<Control, PageSmartPartInfo>.OnHide(Control smartPart)
        {
            this.OnHide(smartPart);
        }

        void IComposableWorkspace<Control, PageSmartPartInfo>.OnShow(Control smartPart, PageSmartPartInfo smartPartInfo)
        {
            this.OnShow(smartPart, smartPartInfo);
        }

        void IComposableWorkspace<Control, PageSmartPartInfo>.OnClose(Control smartPart)
        {
            this.OnClose(smartPart);
        }

        void IComposableWorkspace<Control, PageSmartPartInfo>.OnApplySmartPartInfo(Control smartPart, PageSmartPartInfo smartPartInfo)
        {
            this.OnApplySmartPartInfo(smartPart, smartPartInfo);
        }

        void IComposableWorkspace<Control, PageSmartPartInfo>.OnActivate(Control smartPart)
        {
            this.OnActivate(smartPart);
        }

        PageSmartPartInfo IComposableWorkspace<Control, PageSmartPartInfo>.ConvertFrom(ISmartPartInfo source)
        {
            return SmartPartInfo.ConvertTo<PageSmartPartInfo>(source);
        }

        void IComposableWorkspace<Control, PageSmartPartInfo>.RaiseSmartPartActivated(WorkspaceEventArgs e)
        {
            this.OnSmartPartActivated(e);
        }

        void IComposableWorkspace<Control, PageSmartPartInfo>.RaiseSmartPartClosing(WorkspaceCancelEventArgs e)
        {
            this.OnSmartPartClosing(e);
        }

        #endregion
    }
}
