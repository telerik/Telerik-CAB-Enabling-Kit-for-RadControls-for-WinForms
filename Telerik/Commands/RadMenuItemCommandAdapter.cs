using Microsoft.Practices.CompositeUI.Commands;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.CompositeUI
{
    /// <summary>
    /// Class for managing the relationship between a RadMenuItem instance and a <see cref="Command"/>.
    /// </summary>
    public class RadMenuItemCommandAdapter : EventCommandAdapter<RadMenuItem>
    {
        /// <summary>
		/// Initializes a new instance of the <see cref="RadMenuItemCommandAdapter"/> class.
        /// </summary>
        public RadMenuItemCommandAdapter()
            : base()
        {
        }

        /// <summary>
        /// Initializes a command adapter with the specified <see cref="RadMenuItem"/>.
        /// </summary>
        /// <param name="menuItem">The RadMenuItem instance.</param>
        /// <param name="eventName">The name of the event.</param>
        public RadMenuItemCommandAdapter(RadMenuItem menuItem, string eventName)
            : base(menuItem, eventName)
        {
        }

		protected override void OnCommandChanged(Command command)
		{
			base.OnCommandChanged(command);

			foreach (RadMenuItem menuItem in this.Invokers.Keys)
			{
				if (command.Status != CommandStatus.Unavailable)
				{
					menuItem.Visibility = Telerik.WinControls.ElementVisibility.Visible;
					menuItem.Enabled = (command.Status == CommandStatus.Enabled);
				}
				else
				{
					menuItem.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
				}
			}
		}
    }
}
