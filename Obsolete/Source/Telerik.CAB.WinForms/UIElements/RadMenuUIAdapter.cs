using System;
using System.Collections.Generic;
using System.Text;
using Telerik.WinControls.UI;
using Microsoft.Practices.CompositeUI.UIElements;
using Telerik.WinControls;
using Microsoft.Practices.CompositeUI.Utility;

namespace Telerik.CAB.WinForms.UIElements
{
    /// <summary>
	/// Adapter that wraps the <see cref="RadItemCollection"/> collection for use as an <see cref="IUIElementAdapter"/>.
    /// </summary>
    public class RadMenuUIAdapter : UIElementAdapter<RadMenuItem>
    {
        private RadItemCollection items;

        /// <summary>
		/// Initializes a new instance of the <see cref="RadMenuUIAdapter"/> class.
        /// </summary>
        /// <param name="bars">The RadItemCollection represented by the UI Adapter.</param>
        public RadMenuUIAdapter(RadItemCollection items)
        {
            Guard.ArgumentNotNull(items, "bars");
            this.items = items;
        }

        /// <summary>
        /// Adds a RadMenuItem to the RadItemCollection collection associated with the adapter.
        /// </summary>
        /// <param name="uiElement">Bar item to add.</param>
        /// <returns>The added item.</returns>
        protected override RadMenuItem Add(RadMenuItem uiElement)
        {
            this.items.Add(uiElement);
            return uiElement;
        }

        /// <summary>
        /// Removes the specified RadItem from the associated RadItemCollection.
        /// </summary>
        /// <param name="uiElement">The item to be removed.</param>
        protected override void Remove(RadMenuItem uiElement)
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
