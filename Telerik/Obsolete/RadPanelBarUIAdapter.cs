using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI.UIElements;
using Telerik.WinControls.UI;
using Microsoft.Practices.CompositeUI.Utility;

namespace Telerik.CAB.WinForms.UIElements
{
    /// <summary>
    /// An adapter that wraps <see cref="RadPanelBar"/> for the use of an <see cref="IUIElementAdapter"/>.
    /// </summary>
     [Obsolete("This type is obsolete.")]
    public class RadPanelBarUIAdapter : UIElementAdapter<RadPanelBarGroupElement>
    {
        private RadPanelBar panelBar = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="RadPanelBarUIAdapter"/> class.
        /// </summary>
        /// <param name="panelBar">The <see cref="RadPanelBar"/> represented by the UI adapter.</param>
        public RadPanelBarUIAdapter(RadPanelBar panelBar)
        {
            Guard.ArgumentNotNull(panelBar, "panelBar");
            this.panelBar = panelBar;
        }

        /// <summary>
        /// Adds a <see cref="RadPanelBarGroupElement"/> to the collection associated with the adapter.
        /// </summary>
        /// <param name="uiElement">The node to add.</param>
        /// <returns>The added node.</returns>
        protected override RadPanelBarGroupElement Add(RadPanelBarGroupElement uiElement)
        {
            Guard.ArgumentNotNull(uiElement, "uiElement");
            this.panelBar.Items.Add(uiElement);

            return uiElement;
        }

        /// <summary>
        /// Removes the specified <see cref="RadPanelBarGroupElement"/> from the associated collection.
        /// </summary>
        /// <param name="uiElement">The item to be removed.</param>
        protected override void Remove(RadPanelBarGroupElement uiElement)
        {
            Guard.ArgumentNotNull(uiElement, "uiElement");
            this.panelBar.Items.Remove(uiElement);
        }

        /// <summary>
        /// The <see cref="RadPanelBar"/> that is represented by the adapter.
        /// </summary>
        public RadPanelBar RadPanelBar
        {
            get
            {
                return this.panelBar;
            }
        }
    }
}
