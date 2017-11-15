using Microsoft.Practices.CompositeUI.SmartParts;
using Telerik.WinControls.UI.Docking;
using System.Drawing;

namespace Telerik.WinControls.CompositeUI
{
    public class DockWindowSmartPartInfo : SmartPartInfo
    {
        private DockType dockType = DockType.Document;
        private DockPosition dockPosition = DockPosition.Fill;
        private DockState dockState = DockState.Docked;
        private string name;
        private Size size;
        private DockWindowSmartPartInfo dockTarget;

        public DockState DockState
        {
            get { return dockState; }
            set { dockState = value; }
        }

        public DockType DockType
        {
            get { return dockType; }
            set { dockType = value; }
        }

        public DockPosition DockPosition
        {
            get { return dockPosition; }
            set { dockPosition = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Size Size
        {
            get { return size; }
            set { size = value; }
        }

        public DockWindowSmartPartInfo DockTarget
        {
            get { return dockTarget; }
            set { dockTarget = value; }
        }
    }
}
