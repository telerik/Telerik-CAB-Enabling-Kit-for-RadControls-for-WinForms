using System.Collections.Specialized;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.WinForms;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Docking;

namespace Telerik.WinControls.CompositeUI
{
    public class RadTabbedMdiWorkspace : MdiWorkspace
    {
        private const int Suspend_Activated = 0x1;
        private const int Suspend_Close = Suspend_Activated << 1;

        private RadDock tabbedMdiManager;
        private BitVector32 notifications = new BitVector32();

        public RadTabbedMdiWorkspace(RadForm parentForm)
            :base(parentForm)
        {
            this.tabbedMdiManager = new RadDock();
            this.tabbedMdiManager.AutoDetectMdiChildren = true;
            this.tabbedMdiManager.Dock = DockStyle.Fill;
            this.tabbedMdiManager.ActiveWindowChanged += tabbedMdiManager_ActiveWindowChanged;
            this.tabbedMdiManager.DockWindowClosing += tabbedMdiManager_DockWindowClosing;

            parentForm.Controls.Add(this.tabbedMdiManager);
            this.tabbedMdiManager.BringToFront();
        }

        void tabbedMdiManager_ActiveWindowChanged(object sender, DockWindowEventArgs e)
        {
            if (e.DockWindow.Controls.Count > 0 && this.SmartParts.Contains(e.DockWindow.Controls[0].Controls[0]))
            {
                this.SetActiveSmartPart(e.DockWindow.Controls[0].Controls[0]);
                if (!notifications[Suspend_Activated])
                {
                    RaiseSmartPartActivated(e.DockWindow.Controls[0].Controls[0]);
                }
            }
        }

        void tabbedMdiManager_DockWindowClosing(object sender, DockWindowCancelEventArgs e)
        {
            if (!notifications[Suspend_Close] && e.NewWindow.Controls.Count > 0)
            {
                WorkspaceCancelEventArgs args = RaiseSmartPartClosing(e.NewWindow.Controls[0].Controls[0]);
                e.Cancel = args.Cancel;
            }
        }

        protected override void OnActivate(Control smartPart)
        {
            notifications[Suspend_Activated] = true;

            base.OnActivate(smartPart);

            if (smartPart != null && smartPart.Parent != null)
            {
                DockWindow dockWindow = smartPart.Parent.Parent as DockWindow;
                if (dockWindow != null)
                {
                    this.tabbedMdiManager.ActiveWindow = dockWindow;
                }
            }

            notifications[Suspend_Activated] = false;
        }

        protected override void OnClose(Control smartPart)
        {
            notifications[Suspend_Close] = true;

            base.OnClose(smartPart);

            if (smartPart != null && smartPart.Parent != null)
            {
                DockWindow dockWindow = smartPart.Parent.Parent as DockWindow;
                if (dockWindow != null)
                {
                    dockWindow.Close();
                }
            }

            notifications[Suspend_Close] = false;
        }

        protected override void OnHide(Control smartPart)
        {
            base.OnHide(smartPart);

            if (smartPart != null && smartPart.Parent != null)
            {
                DockWindow dockWindow = smartPart.Parent.Parent as DockWindow;
                if (dockWindow != null)
                {
                    dockWindow.Close();
                }
            }
        }

        protected override void OnApplySmartPartInfo(Control smartPart, WindowSmartPartInfo smartPartInfo)
        {
            base.OnApplySmartPartInfo(smartPart, smartPartInfo);

            if (smartPart != null && smartPart.Parent != null)
            {
                DockWindow dockWindow = smartPart.Parent.Parent as DockWindow;
                if (dockWindow != null)
                {
                    dockWindow.Text = smartPartInfo.Title;
                    dockWindow.ToolTipText = smartPartInfo.Description;
                }
            }
        }

        protected override void OnShow(Control smartPart, WindowSmartPartInfo smartPartInfo)
        {
            smartPart.Dock = DockStyle.Fill;

            base.OnShow(smartPart, smartPartInfo);
        }
    }
}
