using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI.Commands;
using Telerik.WinControls.UI;

namespace Telerik.CAB.WinForms.Commands
{
    public class RadTreeNodeCommandAdapter : EventCommandAdapter<RadTreeNode>
    {
                /// <summary>
		/// Initializes a new instance of the <see cref="RadTreeNodeCommandAdapter"/> class.
        /// </summary>
        public RadTreeNodeCommandAdapter()
            : base()
        {
        }

        /// <summary>
        /// Initializes a command adapter with the specified <see cref="RadTreeNode"/>.
        /// </summary>
        /// <param name="treeNode">The RadTreeNode instance.</param>
        /// <param name="eventName">The name of the event.</param>
        public RadTreeNodeCommandAdapter(RadTreeNode treeNode, string eventName)
            : base(treeNode, eventName)
        { 
        }

		protected override void OnCommandChanged(Command command)
		{
			base.OnCommandChanged(command);

			foreach (RadTreeNode node in this.Invokers.Keys)
			{
				if (command.Status != CommandStatus.Unavailable)
				{
					node.Visible = true;
					node.Enabled = (command.Status == CommandStatus.Enabled);
				}
				else
				{
					node.Visible = false; ;
				}
			}
		}
    }
}
