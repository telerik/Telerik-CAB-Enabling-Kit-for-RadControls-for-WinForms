using System;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.CompositeUI
{
    /// <summary>
    /// An adapter that wraps the <see cref="RadMenu"/> for the use of an <see cref="IUIElementAdapter"/>.
    /// </summary>
    [Obsolete("This type is obsolete. Please, use the RadItemsCollectionUIAdapter instead.")]
    public class RadMenuItemsCollectionUIAdapter : UIElementAdapter<RadMenuItem>
    {
        # region Fields

        private RadItemCollection items;
        private RadMenu menu;

        # endregion

        # region Constructors

        /// <summary>
        /// Initializes a new <see cref="RadMenuItemsCollectionUIAdapter"/> class.
        /// </summary>
        /// <param name="menu">The RadMenu represented by the UI adapter.</param>
        /// <param name="items">Items collection represented by the UI adapter.</param>
        public RadMenuItemsCollectionUIAdapter(RadMenu menu, RadItemCollection items)
        {
            Guard.ArgumentNotNull(menu, "menu");
            Guard.ArgumentNotNull(items, "items");
            this.items = items;
            this.menu = menu;
        }

        # endregion

        # region Internal

        /// <summary>
        /// Adds a <see cref="RadMenuItem"/> to the <see cref="RadMenu"/> associated with the adapter.
        /// </summary>
        /// <param name="uiElement">The RadMenuItem to add.</param>
        /// <returns>The added item.</returns>
        protected override RadMenuItem Add(RadMenuItem item)
        {
            this.items.Insert(this.items.Count, item);
            this.menu.Items.Add(item);

            return item;
        }

        /// <summary>
        /// Removes the specified <see cref="RadMenuItem"/> associated with the adapter.
        /// </summary>
        /// <param name="item">The item to be removed.</param>
        protected override void Remove(RadMenuItem item)
        {
            Guard.ArgumentNotNull(item, "item");

            this.items.Remove(item);
        }

        /// <summary>
        /// The <see cref="RadItemCollection"/> collection represented by the adapter.
        /// </summary>
        protected RadItemCollection Items
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

        /// <summary>
        /// The <see cref="RadMenu"/> represented by the adapter.
        /// </summary>
        protected RadMenu Menu
        {
            get { return this.menu; }
            set { this.menu = value; }
        }

        # endregion
    }
}
