using System;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.CompositeUI
{
    [Obsolete("This type is obsolete. Please, use the RadCommandBarUIAdapter instead.")]
    public class RadToolStripElementAdapter : UIElementAdapter<CommandBarStripElement>
    {
        private CommandBarRowElement toolStripElement;

        public RadToolStripElementAdapter(CommandBarRowElement element)
        {
            Guard.ArgumentNotNull(element, "element");
            this.toolStripElement = element;
        }

        protected override CommandBarStripElement Add(CommandBarStripElement uiElement)
        {
            Guard.ArgumentNotNull(uiElement, "uiElement");
            toolStripElement.Strips.Add(uiElement);

            return uiElement;
        }

        protected override void Remove(CommandBarStripElement uiElement)
        {
            Guard.ArgumentNotNull(uiElement, "uiElement");
            CommandBarRowElement barElement = uiElement.Parent as CommandBarRowElement;
            if (barElement != null && barElement.Strips.Contains(uiElement))
            {
                barElement.Strips.Remove(uiElement);
            }
            //if (uiElement.Parent != null)
            //    uiElement.Parent.Children.Remove(uiElement);
        }
    }
}
