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
	public class RadToolStripElementAdapter : UIElementAdapter<RadToolStripItem>
	{
		private RadToolStripElement toolStripElement;

		public RadToolStripElementAdapter(RadToolStripElement element)
		{
			Guard.ArgumentNotNull(element, "element");
			this.toolStripElement = element;
		}
		
		protected override RadToolStripItem Add(RadToolStripItem uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "uiElement");
			toolStripElement.Items.Add(uiElement);
			
			return uiElement;
		}

		protected override void Remove(RadToolStripItem uiElement)
		{
			Guard.ArgumentNotNull(uiElement, "uiElement");
			if (uiElement.ParentToolStripElement != null && uiElement.Parent.Children.Contains(uiElement))
			{
				uiElement.ParentToolStripElement.Items.Remove(uiElement);
			}
			//if (uiElement.Parent != null)
			//    uiElement.Parent.Children.Remove(uiElement);
		}
	}
}
