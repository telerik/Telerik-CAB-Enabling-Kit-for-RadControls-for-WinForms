using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Utility;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.WinForms;
using Telerik.WinControls.UI;

namespace Telerik.CAB.WinForms.WorkSpaces
{
    public class DockingClientPanelWorkspace : SplitPanel, IComposableWorkspace<Control,SmartPartInfo>
    {
        private WorkspaceComposer<Control, SmartPartInfo> composer;

        public DockingClientPanelWorkspace()
        {
            composer = new WorkspaceComposer<Control, SmartPartInfo>(this);
        }

        [ServiceDependency]
        public WorkItem workItem
        {
            set
            {
                composer.WorkItem = value;
            }
        }

        #region Private Members
        /// <summary>
        /// Disposes control that is present in the workspace
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlDisposed(object sender, EventArgs e)
        {
            Control control = sender as Control;
			if (control != null)
			{
				composer.ForceClose(control);
			}
        }

        /// <summary>
        /// Activates the Top most Control present in the control list
        /// </summary>
        private void ActivateTopMost()
        {
            if (this.Controls.Count != 0)
            {
                composer.Activate(this.Controls[0]);
            }
        }
        
        #endregion

        #region ProtectedMembers
        /// <summary>
        /// Activates the given SmartPart
        /// </summary>
        /// <param name="smartPart"></param>
        protected virtual void OnActivate(Control smartPart)
        {
            smartPart.BringToFront();
            smartPart.Show();
        }

        /// <summary>
        /// No Implementation since its a panel
        /// </summary>
        /// <param name="smartPart"></param>
        /// <param name="smartPartInfo"></param>
        protected virtual void OnApplySmartPartInfo(Control smartPart, SmartPartInfo smartPartInfo)
        {
            //No op
        }

        /// <summary>
        /// Closes the SmartPart
        /// </summary>
        /// <param name="smartPart"></param>
        protected virtual void OnClose(Control smartPart)
        {
            this.Controls.Remove(smartPart);
            smartPart.Disposed -= ControlDisposed;
            this.ActivateTopMost();
        }

        /// <summary>
        /// Hides the SmartPart, and displays the Top Most Control present in the Control List
        /// </summary>
        /// <param name="smartPart"></param>
        protected virtual void OnHide(Control smartPart)
        {
            smartPart.SendToBack();

            this.ActivateTopMost();
        }

        /// <summary>
        /// Shows the SmartPart
        /// </summary>
        /// <param name="smartPart"></param>
        /// <param name="smartPartInfo"></param>
        protected virtual void OnShow(Control smartPart, SmartPartInfo smartPartInfo)
        {
            if (!this.Controls.Contains(smartPart))
            {
                smartPart.Dock = DockStyle.Fill;

                this.Controls.Add(smartPart);

                smartPart.Disposed += new EventHandler(ControlDisposed);
                Activate(smartPart);
            }
        }

        /// <summary>
        /// Raises the SmartPartActivated EventHandler
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSmartPartActivated(WorkspaceEventArgs e)
        {
            if (SmartPartActivated != null)
            {
                SmartPartActivated(this, e);
            }
        }

        /// <summary>
        /// Raises the SmartPartClosing EventHandler
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSmartPartClosing(WorkspaceCancelEventArgs e)
        {
            if (SmartPartClosing != null)
            {
                SmartPartClosing(this, e);
            }
        }
        
        #endregion

        #region IComposableWorkspace<Control,SmartPartInfo> Members

        SmartPartInfo IComposableWorkspace<Control, SmartPartInfo>.ConvertFrom(ISmartPartInfo source)
        {
            return SmartPartInfo.ConvertTo<SmartPartInfo>(source);
        }

        void IComposableWorkspace<Control, SmartPartInfo>.OnActivate(Control smartPart)
        {
            OnActivate(smartPart);
        }

        void IComposableWorkspace<Control, SmartPartInfo>.OnApplySmartPartInfo(Control smartPart, SmartPartInfo smartPartInfo)
        {
            OnApplySmartPartInfo(smartPart, smartPartInfo);
        }

        void IComposableWorkspace<Control, SmartPartInfo>.OnClose(Control smartPart)
        {
            OnClose(smartPart);
        }

        void IComposableWorkspace<Control, SmartPartInfo>.OnHide(Control smartPart)
        {
            OnHide(smartPart);
        }

        void IComposableWorkspace<Control, SmartPartInfo>.OnShow(Control smartPart, SmartPartInfo smartPartInfo)
        {
            OnShow(smartPart, smartPartInfo);
        }

        void IComposableWorkspace<Control, SmartPartInfo>.RaiseSmartPartActivated(WorkspaceEventArgs e)
        {
            OnSmartPartActivated(e);
        }

        void IComposableWorkspace<Control, SmartPartInfo>.RaiseSmartPartClosing(WorkspaceCancelEventArgs e)
        {
            OnSmartPartClosing(e);
        }

        #endregion

        #region IWorkspace Members

        public void Activate(object smartPart)
        {
            composer.Activate(smartPart);
        }

        public object ActiveSmartPart
        {
            get 
            {
                return composer.ActiveSmartPart;
            }
        }

        public void ApplySmartPartInfo(object smartPart, ISmartPartInfo smartPartInfo)
        {
            composer.ApplySmartPartInfo(smartPart, smartPartInfo);
        }

        public void Close(object smartPart)
        {
            composer.Close(smartPart);
        }

        public void Hide(object smartPart)
        {
            composer.Hide(smartPart);
        }

        public void Show(object smartPart)
        {
            composer.Show(smartPart);
        }

        public void Show(object smartPart, ISmartPartInfo smartPartInfo)
        {
            composer.Show(smartPart, smartPartInfo);
        }

        public event EventHandler<WorkspaceEventArgs> SmartPartActivated;

        public event EventHandler<WorkspaceCancelEventArgs> SmartPartClosing;

        public System.Collections.ObjectModel.ReadOnlyCollection<object> SmartParts
        {
            get 
            {
                return composer.SmartParts;
            }
        }

        #endregion
    }
}
