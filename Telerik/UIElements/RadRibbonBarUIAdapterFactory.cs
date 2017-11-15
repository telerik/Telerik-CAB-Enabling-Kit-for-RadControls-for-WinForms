using System;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.CompositeUI
{
    public class RadRibbonBarUIAdapterFactory : IUIElementAdapterFactory
    {
        /// <summary>
        /// Returns a <see cref="IUIElementAdapter"/> for the specified uielement.
        /// </summary>
        /// <param name="uiElement">UIElement for which to return a <see cref="IUIElementAdapter"/>.</param>
        /// <returns>A <see cref="IUIElementAdapter"/> that represents the specified element.</returns>
        public IUIElementAdapter GetAdapter(object uiElement)
        {
            Guard.ArgumentNotNull(uiElement, "uiElement");

            if (uiElement is RadRibbonBar)
            {
                return new RadRibbonBarUIAdapter(((RadRibbonBar)uiElement).CommandTabs);
            }

            if (uiElement is RadRibbonBarElement)
            {
                return new RadRibbonBarUIAdapter(((RadRibbonBarElement)uiElement).CommandTabs);
            }

            if (uiElement is RibbonTab)
            {
                return new RadRibbonBarUIAdapter(((RibbonTab)uiElement).Items);
            }

            if (uiElement is RadRibbonBarGroup)
            {
                return new RadRibbonBarUIAdapter(((RadRibbonBarGroup)uiElement).Items);
            }

            if (uiElement is RadRibbonBarButtonGroup)
            {
                return new RadRibbonBarUIAdapter(((RadRibbonBarButtonGroup)uiElement).Items);
            }

            if (uiElement is RadQuickAccessToolBar)
            {
                return new RadRibbonBarUIAdapter(((RadQuickAccessToolBar)uiElement).Items);
            }

            if (uiElement is RadQuickAccessOverflowButton)
            {
                return new RadRibbonBarUIAdapter(((RadQuickAccessOverflowButton)uiElement).Items);
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
            return uiElement is RadRibbonBar || uiElement is RadRibbonBarElement || uiElement is RibbonTab || uiElement is RadRibbonBarGroup
                || uiElement is RadRibbonBarButtonGroup || uiElement is RadQuickAccessToolBar || uiElement is RadQuickAccessOverflowButton;
        }
    }
}
