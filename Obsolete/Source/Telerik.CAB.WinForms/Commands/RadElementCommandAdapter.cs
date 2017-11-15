using Microsoft.Practices.CompositeUI.Commands;
using Telerik.WinControls;

namespace Telerik.CAB.WinForms.Commands
{
	public class RadElementCommandAdapter : EventCommandAdapter<RadElement>
	{
		public RadElementCommandAdapter()
			: base()
		{

		}

		public RadElementCommandAdapter(RadElement element, string eventName)
			: base(element, eventName)
		{

		}

		protected override void OnCommandChanged(Command command)
		{
			base.OnCommandChanged(command);

			foreach (RadElement element in this.Invokers.Keys)
			{
				if (command.Status != CommandStatus.Unavailable)
				{
					element.Visibility = Telerik.WinControls.ElementVisibility.Visible;
					element.Enabled = (command.Status == CommandStatus.Enabled);
				}
				else
				{
					element.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
				}
			}
		}
	}
}
