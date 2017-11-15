using System;
using System.Collections.Generic;
using System.Text;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;

namespace Telerik.CAB.WinForms.UIElements
{
	/// <summary>
	/// An adapter that wraps a <see cref="RadPanelBarGroupElement"/> for use as an <see cref="IUIElementAdapter"/> 
	/// and uses the location of the wrapped item to determine where new items are positioned.
	/// </summary>
	public class RadPanelBarGroupUIAdapter : UIElementAdapter<RadItem>
	{
		private RadPanelBarGroupElement panelBarGroup = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="RadPanelBarGroupUIAdapter"/> class.
		/// </summary>
		/// <param name="panelBarGroup">Node whose owning collection will be updated with any added elements.</param>
		public RadPanelBarGroupUIAdapter(RadPanelBarGroupElement panelBarGroup)
		{
			Guard.ArgumentNotNull(panelBarGroup, "panelBarGroup");
			this.panelBarGroup = panelBarGroup;
		}

		/// <summary>
		/// Adds a <see cref="RadItem"/> to the collection associated with the adapter.
		/// </summary>
		/// <param name="uiElement">The node to add.</param>
		/// <returns>The added node.</returns>
		protected override RadItem Add(RadItem uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "uiElement");
			this.panelBarGroup.Items.Add(uiElement);

			return uiElement;
		}

		/// <summary>
		/// Removes the specified <see cref="RadItem"/> from the associated collection.
		/// </summary>
		/// <param name="uiElement">The item to be removed.</param>
		protected override void Remove(RadItem uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "uiElement");
			this.panelBarGroup.Items.Remove(uiElement);
		}

		/// <summary>
		/// The <see cref="RadPanelBarGroupElement"/> that is represented by the adapter.
		/// </summary>
		public RadPanelBarGroupElement RadPanelBarGroupElement
		{
			get
			{
				return this.panelBarGroup;
			}
		}
	}
}
