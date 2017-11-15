using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI.UIElements;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinControls;

namespace Telerik.CAB.WinForms.UIElements
{
	public class RadToolStripAdapterFactory : IUIElementAdapterFactory
	{
		#region IUIElementAdapterFactory Members

		public IUIElementAdapter GetAdapter(object uiElement)
		{
			if (uiElement is RadToolStripElement)
			{
				return new RadToolStripElementAdapter((RadToolStripElement)uiElement);
			}

			if (uiElement is RadToolStripItem)
			{
				return new RadToolStripItemAdapter((RadToolStripItem)uiElement);
			}

			throw new ArgumentException("uiElement");
		}

		public bool Supports(object uiElement)
		{
			return (uiElement is RadToolStripElement) || (uiElement is RadToolStripItem);
		}

		#endregion
	}
}
