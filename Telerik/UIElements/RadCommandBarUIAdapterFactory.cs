using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.CompositeUI
{
    public class RadCommandBarUIAdapterFactory : IUIElementAdapterFactory
    {
        /// <summary>
        /// Returns a <see cref="IUIElementAdapter"/> for the specified uielement.
        /// </summary>
        /// <param name="uiElement">UIElement for which to return a <see cref="IUIElementAdapter"/>.</param>
        /// <returns>A <see cref="IUIElementAdapter"/> that represents the specified element.</returns>
        public IUIElementAdapter GetAdapter(object uiElement)
        {
            Guard.ArgumentNotNull(uiElement, "uiElement");

            if (uiElement is RadCommandBar)
            {
                return new RadCommandBarUIAdapter(((RadCommandBar)uiElement).Rows);
            }

            if (uiElement is RadCommandBarElement)
            {
                return new RadCommandBarUIAdapter(((RadCommandBarElement)uiElement).Rows);
            }

            if (uiElement is CommandBarRowElement)
            {
                return new RadCommandBarUIAdapter(((CommandBarRowElement)uiElement).Strips);
            }

            if (uiElement is CommandBarStripElement)
            {
                return new RadCommandBarUIAdapter(((CommandBarStripElement)uiElement).Items);
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
            return uiElement is RadCommandBar || uiElement is RadCommandBarVisualElement;
        }
    }
}
