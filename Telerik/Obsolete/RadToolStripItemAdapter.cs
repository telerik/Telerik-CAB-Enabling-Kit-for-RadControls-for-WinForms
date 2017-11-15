using System;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.CompositeUI
{
    [Obsolete("This type is obsolete. Please, use the RadCommandBarUIAdapter instead.")]
    public class RadToolStripItemAdapter : UIElementAdapter<RadCommandBarBaseItem>
    {
        private CommandBarStripElement toolStripItem;

        public RadToolStripItemAdapter(CommandBarStripElement item)
        {
            Guard.ArgumentNotNull(item, "item");
            this.toolStripItem = item;
        }

        protected override RadCommandBarBaseItem Add(RadCommandBarBaseItem uiElement)
        {
            if (toolStripItem == null)
            {
                throw new InvalidOperationException();
            }

            toolStripItem.Items.Add(uiElement);

            return uiElement;
        }

        protected override void Remove(RadCommandBarBaseItem uiElement)
        {
            if (uiElement.Parent != null && uiElement.Parent.Children.Contains(uiElement))
            {
                //((RadToolStripItem)uiElement.Parent.Parent).Items.Remove(uiElement);
                this.toolStripItem.Items.Remove(uiElement);
            }
        }
    }
}
