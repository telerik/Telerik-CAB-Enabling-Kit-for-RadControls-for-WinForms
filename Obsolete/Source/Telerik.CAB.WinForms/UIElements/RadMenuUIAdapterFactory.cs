using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;
using Telerik.WinControls.UI;
using Telerik.WinControls;

namespace Telerik.CAB.WinForms.UIElements
{
    /// <summary>
    /// A <see cref="IUIElementAdapterFactory"/> that produces adapters for the UIElements associated with the RadMenu.
    /// </summary>
    public class RadMenuUIAdapterFactory : IUIElementAdapterFactory
    {
        /// <summary>
        /// Returns a <see cref="IUIElementAdapter"/> for the specified uielement.
        /// </summary>
        /// <param name="uiElement">UIElement for which to return a <see cref="IUIElementAdapter"/>.</param>
        /// <returns>A <see cref="IUIElementAdapter"/> that represents the specified element.</returns>
        public IUIElementAdapter GetAdapter(object uiElement)
        {
            Guard.ArgumentNotNull(uiElement, "uiElement");

            if (uiElement is RadMenu)
            {
                return new RadMenuUIAdapter(((RadMenu)uiElement).Items);
            }
            //if (uiElement is RadMenuContentItem)
            //    return new BarItemsCollectionUIAdapter(((RadMenuContentItem)uiElement).Manager, ((RadMenuContentItem)uiElement).Items);
            //if (uiElement is RadMenuItem)
            //    return new BarItemsCollectionUIAdapter(((RadMenuItem)uiElement).Manager, ((RadMenuItem)uiElement).Items);

            if (uiElement is IHierarchicalItem)
            {
                return new RadMenuItemsCollectionUIAdapter((((IHierarchicalItem)uiElement).Owner as RadMenu), (((IHierarchicalItem)uiElement).Items));
            }
            throw new ArgumentException("uiElement");
        }

        /// <summary>
        /// Indicates if the specified UIElement is supported by the adapter factory.
        /// </summary>
        /// <param name="uiElement">The UIElement.</param>
        /// <returns>Returns true for supported elements, otherwise returns false.</returns>
        public bool Supports(object uiElement)
        {
            return uiElement is RadMenu || uiElement is RadMenuContentItem || uiElement is RadMenuItem;
        }
    }
}
