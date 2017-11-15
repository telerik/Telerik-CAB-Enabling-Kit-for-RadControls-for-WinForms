using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;
using Telerik.WinControls.UI;

namespace Telerik.CAB.WinForms.UIElements
{
    /// <summary>
    /// A <see cref="IUIElementAdapterFactory"/> that produces adapters for the UIElements associated 
    /// with the RadTreeView.
    /// </summary>
    public class RadTreeViewUIAdapterFactory : IUIElementAdapterFactory
    {
        /// <summary>
        /// Returns a <see cref="IUIElementAdapter"/> for the specified UIElement.
        /// </summary>
        /// <param name="uiElement">UIElement for which to return a <see cref="IUIElementAdapter"/>.</param>
        /// <returns>A <see cref="IUIElementAdapter"/> that represents the specified element.</returns>
        public IUIElementAdapter GetAdapter(object uiElement)
        {
            Guard.ArgumentNotNull(uiElement, "uiElement");

            if (uiElement is RadTreeView)
            {
                return new RadTreeViewUIAdapter((RadTreeView)uiElement);
            }
            if (uiElement is RadTreeNode)
            {
                return new RadTreeNodeUIAdapter((RadTreeNode)uiElement);
            }
            throw new ArgumentException("The uiElement instance is not compliant with this type of IUIElementAdapter");
        }

        /// <summary>
        /// Indicates if the specified ui element is supported by the adapter factory.
        /// </summary>
        /// <param name="uiElement">The UIElement instance.</param>
        /// <returns>Returns true for supported elements, otherwise returns false.</returns>
        public bool Supports(object uiElement)
        {
            return uiElement is RadTreeView || uiElement is RadTreeNode;
        }
    }
}
