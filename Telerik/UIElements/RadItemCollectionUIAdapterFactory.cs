using System;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.CompositeUI
{
    public class RadItemCollectionAdapterFactory : IUIElementAdapterFactory
    {
        /// <summary>
        /// Returns a <see cref="IUIElementAdapter"/> for the specified uielement.
        /// </summary>
        /// <param name="uiElement">UIElement for which to return a <see cref="IUIElementAdapter"/>.</param>
        /// <returns>A <see cref="IUIElementAdapter"/> that represents the specified element.</returns>
        public IUIElementAdapter GetAdapter(object uiElement)
        {
            Guard.ArgumentNotNull(uiElement, "uiElement");

            if (uiElement is RadItemCollection)
            {
                return new RadItemCollectionUIAdapter((RadItemCollection)uiElement);
            }

            if (uiElement is RadStatusStrip)
            {
                return new RadItemCollectionUIAdapter(((RadStatusStrip)uiElement).Items);
            }

            if (uiElement is RadMenu)
            {
                return new RadItemCollectionUIAdapter(((RadMenu)uiElement).Items);
            }

            if (uiElement is RadMenuItem)
            {
                return new RadItemCollectionUIAdapter(((RadMenuItem)uiElement).Items);
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
            return uiElement is RadItem || uiElement is RadItemOwnerCollection || uiElement is RadMenu || uiElement is RadStatusStrip;
        }
    }
}
