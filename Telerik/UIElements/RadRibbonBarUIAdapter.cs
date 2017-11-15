using System.Collections;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.CompositeUI
{
    public class RadRibbonBarUIAdapter : UIElementAdapter<RadElement>
    {
        private IList items;

        public RadRibbonBarUIAdapter(RadRibbonBarCommandTabCollection tabs)
        {
            Guard.ArgumentNotNull(tabs, "RibbonBar Tabs");
            this.items = tabs;
        }

        public RadRibbonBarUIAdapter(RadItemOwnerCollection items)
        {
            Guard.ArgumentNotNull(items, "RibbonBar Items");
            this.items = items;
        }

        /// <summary>
        /// Adds a RadMenuItem to the RadItemCollection collection associated with the adapter.
        /// </summary>
        /// <param name="uiElement">Bar item to add.</param>
        /// <returns>The added item.</returns>
        protected override RadElement Add(RadElement uiElement)
        {
            this.items.Add(uiElement);
            return uiElement;
        }

        /// <summary>
        /// Removes the specified RadItem from the associated RadItemCollection.
        /// </summary>
        /// <param name="uiElement">The item to be removed.</param>
        protected override void Remove(RadElement uiElement)
        {
            this.items.Remove(uiElement);
        }
    }
}
