﻿using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace Telerik.WinControls.CompositeUI
{
    public class RadItemCollectionUIAdapter : UIElementAdapter<RadItem>
    {
        private RadItemCollection items;

        /// <summary>
		/// Initializes a new instance of the <see cref="RadMenuUIAdapter"/> class.
        /// </summary>
        /// <param name="bars">The RadItemCollection represented by the UI Adapter.</param>
        public RadItemCollectionUIAdapter(RadItemCollection items)
        {
            Guard.ArgumentNotNull(items, "bars");
            this.items = items;
        }

        /// <summary>
        /// Adds a RadMenuItem to the RadItemCollection collection associated with the adapter.
        /// </summary>
        /// <param name="uiElement">Bar item to add.</param>
        /// <returns>The added item.</returns>
        protected override RadItem Add(RadItem uiElement)
        {
            this.items.Add(uiElement);
            return uiElement;
        }

        /// <summary>
        /// Removes the specified RadItem from the associated RadItemCollection.
        /// </summary>
        /// <param name="uiElement">The item to be removed.</param>
        protected override void Remove(RadItem uiElement)
        {
            this.items.Remove(uiElement);
        }

        /// <summary>
        /// The RadItemCollection represented by the adapter.
        /// </summary>
        protected RadItemCollection RadItemCollection
        {
            get 
            { 
                return this.items; 
            }

            set 
            { 
                this.items = value; 
            }
        }
    }
}
