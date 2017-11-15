using System.Collections;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.CompositeUI
{
    public class RadCommandBarUIAdapter : UIElementAdapter<RadCommandBarVisualElement>
    {
        private IList items;

        public RadCommandBarUIAdapter(CommandBarStripElementCollection strips)
        {
            Guard.ArgumentNotNull(strips, "CommandBar Strips");
            this.items = strips;
        }

        public RadCommandBarUIAdapter(RadCommandBarBaseItemCollection items)
        {
            Guard.ArgumentNotNull(items, "CommandBar Items");
            this.items = items;
        }

        public RadCommandBarUIAdapter(RadCommandBarLinesElementCollection rows)
        {
            Guard.ArgumentNotNull(rows, "CommandBar Rows");
            this.items = rows;
        }

        /// <summary>
        /// Adds a RadMenuItem to the RadItemCollection collection associated with the adapter.
        /// </summary>
        /// <param name="uiElement">Bar item to add.</param>
        /// <returns>The added item.</returns>
        protected override RadCommandBarVisualElement Add(RadCommandBarVisualElement uiElement)
        {
            this.items.Add(uiElement);
            return uiElement;
        }

        /// <summary>
        /// Removes the specified RadItem from the associated RadItemCollection.
        /// </summary>
        /// <param name="uiElement">The item to be removed.</param>
        protected override void Remove(RadCommandBarVisualElement uiElement)
        {
            this.items.Remove(uiElement);
        }
    }
}
