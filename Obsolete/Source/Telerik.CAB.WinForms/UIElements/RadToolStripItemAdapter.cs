using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI.UIElements;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.Utility;
using Telerik.WinControls.UI;
using Telerik.WinControls;

namespace Telerik.CAB.WinForms.UIElements
{
	public class RadToolStripItemAdapter : UIElementAdapter<RadItem>
	{
		private RadToolStripItem toolStripItem;

		public RadToolStripItemAdapter(RadToolStripItem item)
		{
			Guard.ArgumentNotNull(item, "item");
			this.toolStripItem = item;
		}
		
		protected override RadItem Add(RadItem uiElement)
		{
			if (toolStripItem == null)
			{
				throw new InvalidOperationException();
			}

			toolStripItem.Items.Add(uiElement);

			return uiElement;
		}

		protected override void Remove(RadItem uiElement)
		{
			if (uiElement.Parent != null && uiElement.Parent.Children.Contains(uiElement))
			{
				//((RadToolStripItem)uiElement.Parent.Parent).Items.Remove(uiElement);
				this.toolStripItem.Items.Remove(uiElement);
			}
		}
	}
}
