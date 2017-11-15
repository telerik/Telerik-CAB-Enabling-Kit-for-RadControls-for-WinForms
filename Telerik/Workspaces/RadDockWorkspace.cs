using System.Collections.Specialized;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Telerik.WinControls.UI.Docking;

namespace Telerik.WinControls.CompositeUI
{
    public class RadDockWorkspace : RadDock, IComposableWorkspace<Control, DockWindowSmartPartInfo>
    {
        #region Fields

        private const int Suspend_Activated = 0x1;
        private const int Suspend_Close = Suspend_Activated << 1;

        private string active;
        private WorkspaceComposer<Control, DockWindowSmartPartInfo> composer;
        private BitVector32 notifications = new BitVector32();

        #endregion

        #region Constructors

        public RadDockWorkspace()
        {
            this.composer = new WorkspaceComposer<Control, DockWindowSmartPartInfo>(this);
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
                DockWindow window = this.ActiveWindow;
                if (window != null && window.Controls.Count > 0)
                {
                    return window.Controls[0];
                }

                return null;
            }
        }

        public System.Collections.ObjectModel.ReadOnlyCollection<object> SmartParts
        {
            get 
            {
                return composer.SmartParts;
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

        protected override void OnActiveWindowChanged(DockWindowEventArgs e)
        {
            base.OnActiveWindowChanged(e);

            if (!notifications[Suspend_Activated] && e.DockWindow.Controls.Count > 0)
            {
                this.composer.SetActiveSmartPart(e.DockWindow.Controls[0]);
                OnSmartPartActivated(new WorkspaceEventArgs(e.DockWindow.Controls[0]));
            }
        }

        protected override void OnDockWindowClosing(DockWindowCancelEventArgs e)
        {
            if (!notifications[Suspend_Close] && e.NewWindow.Controls.Count > 0)
            {
                this.composer.SetActiveSmartPart(null);
                WorkspaceCancelEventArgs args = new WorkspaceCancelEventArgs(e.NewWindow.Controls[0]);
                OnSmartPartClosing(args);
                e.Cancel = args.Cancel;
                e.NewWindow.CloseAction = DockWindowCloseAction.Hide;
            }

            base.OnDockWindowClosing(e);
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
            DockWindow dockWindow = GetSmartPart(smartPart);
            if (dockWindow != null && this.ActiveWindow != dockWindow)
            {
                dockWindow.Show();
                this.ActivateWindow(dockWindow);
            }

            notifications[Suspend_Activated] = false;
        }

        protected virtual void OnApplySmartPartInfo(Control smartPart, DockWindowSmartPartInfo smartPartInfo)
        {
            DockWindow dockWindow = GetSmartPart(smartPart, smartPartInfo);
            if (dockWindow == null)
            {
                if (smartPartInfo.DockType == DockType.Document)
                {
                    dockWindow = new DocumentWindow(smartPartInfo.Title);
                }
                else
                {
                    dockWindow = new ToolWindow(smartPartInfo.Title);
                }

                smartPart.Dock = DockStyle.Fill;
                dockWindow.Controls.Add(smartPart);
                this.DockWindow(dockWindow, smartPartInfo.DockPosition);
                this.ActiveWindow = dockWindow;
                return;
            }

            dockWindow.DockState = smartPartInfo.DockState;
            dockWindow.Text = smartPartInfo.Title;
        }

        protected virtual void OnClose(Control smartPart)
        {
            notifications[Suspend_Close] = true;

            DockWindow dockWindow = GetSmartPart(smartPart);
            dockWindow.CloseAction = DockWindowCloseAction.CloseAndDispose;
            if (dockWindow != null)
            {
                this.CloseWindow(dockWindow);
            }

            notifications[Suspend_Close] = false;
        }

        protected virtual void OnHide(Control smartPart)
        {
            DockWindow dockWindow = GetSmartPart(smartPart);
            dockWindow.CloseAction = DockWindowCloseAction.Hide;
            if (dockWindow != null)
            {
                this.CloseWindow(dockWindow);
            }
        }

        protected virtual void OnShow(Control smartPart, DockWindowSmartPartInfo smartPartInfo)
        {
            DockWindow dockWindow = GetSmartPart(smartPart, smartPartInfo);
            if (dockWindow != null)
            {
                this.DisplayWindow(dockWindow);
                return;
            }

            if (smartPartInfo.DockType == DockType.ToolWindow)
            {
                dockWindow = new ToolWindow();
            }
            else
            {
                dockWindow = new DocumentWindow();
            }

            dockWindow.Text = smartPartInfo.Title;
            smartPart.Dock = DockStyle.Fill;
            dockWindow.Controls.Add(smartPart);

            if (!string.IsNullOrEmpty(smartPartInfo.Name))
            {
                dockWindow.Name = smartPartInfo.Name;
            }

            if (smartPartInfo.DockTarget != null)
            {
                DockWindow target = GetSmartPart(null, smartPartInfo.DockTarget);
                if (target != null)
                {
                    this.DockWindow(dockWindow, target, smartPartInfo.DockPosition);
                }
            }
            else
            {
                this.DockWindow(dockWindow, smartPartInfo.DockPosition);
            }
           
            if (!smartPartInfo.Size.IsEmpty)
            {
                dockWindow.DockTabStrip.SizeInfo.AbsoluteSize = smartPartInfo.Size;
            }

            this.ActiveWindow = dockWindow;
        }

        private bool ContainsName(DockWindow dockWindow)
        {
            if (dockWindow.Name == active)
            {
                return true;
            }

            return false;
        }

        private DockWindow GetSmartPart(Control smartPart, DockWindowSmartPartInfo smartPartInfo)
        {
            string previous = this.active;
            this.active = smartPartInfo.Name;
            DockWindow[] windows = this.DockWindows.GetWindows(this.ContainsName);
            this.active = previous;

            if (smartPart == null && windows.Length > 0)
            {
                return windows[0];
            }

            for (int i = 0; i < windows.Length; i++)
            {
                if (windows[i].Controls.Count > 0 && windows[i].Controls[0] == smartPart)
                {
                    return windows[i];
                }
            }

            return null;
        }

        private DockWindow GetSmartPart(Control smartPart)
        {
            DockWindowCollection windows = this.DockWindows;
            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].Controls.Count > 0 && windows[i].Controls[0] == smartPart)
                {
                    return windows[i];
                }
            }

            return null;
        }

        void IComposableWorkspace<Control, DockWindowSmartPartInfo>.OnHide(Control smartPart)
        {
            this.OnHide(smartPart);
        }

        void IComposableWorkspace<Control, DockWindowSmartPartInfo>.OnShow(Control smartPart, DockWindowSmartPartInfo smartPartInfo)
        {
            this.OnShow(smartPart, smartPartInfo);
        }

        void IComposableWorkspace<Control, DockWindowSmartPartInfo>.OnClose(Control smartPart)
        {
            this.OnClose(smartPart);
        }

        void IComposableWorkspace<Control, DockWindowSmartPartInfo>.OnApplySmartPartInfo(Control smartPart, DockWindowSmartPartInfo smartPartInfo)
        {
            this.OnApplySmartPartInfo(smartPart, smartPartInfo);
        }

        void IComposableWorkspace<Control, DockWindowSmartPartInfo>.OnActivate(Control smartPart)
        {
            this.OnActivate(smartPart);
        }

        DockWindowSmartPartInfo IComposableWorkspace<Control, DockWindowSmartPartInfo>.ConvertFrom(ISmartPartInfo source)
        {
            return SmartPartInfo.ConvertTo<DockWindowSmartPartInfo>(source);
        }

        void IComposableWorkspace<Control, DockWindowSmartPartInfo>.RaiseSmartPartActivated(WorkspaceEventArgs e)
        {
            this.OnSmartPartActivated(e);
        }

        void IComposableWorkspace<Control, DockWindowSmartPartInfo>.RaiseSmartPartClosing(WorkspaceCancelEventArgs e)
        {
            this.OnSmartPartClosing(e);
        }

        #endregion
    }
}
