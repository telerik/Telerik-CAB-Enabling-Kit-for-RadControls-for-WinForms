using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using FinanceApplicationCAB.Infrastructure.Interface.Constants;

namespace FinanceApplicationCAB.Infrastructure.Module.Services
{
	[Service(typeof(ISelectionService))]
	public class SelectionService : ISelectionService
	{
		private WorkItem WorkItem = null;

		public SelectionService(WorkItem workItem)
		{
			this.WorkItem = workItem;
		}

		[EventPublication(EventTopicNames.HoveredItemsChanged, PublicationScope.WorkItem)]
		public event EventHandler<EventArgs> HoveredItemsChanged;

		[EventPublication(EventTopicNames.SelectedItemsChanged, PublicationScope.WorkItem)]
		public event EventHandler<EventArgs> SelectedItemsChanged;

		private void OnSelectedItemsChanged(object sender)
		{
			if (this.SelectedItemsChanged != null)
			{
				this.SelectedItemsChanged(sender, EventArgs.Empty);
			}
		}

		private void OnHoveredItemsChanged(object sender)
		{
			if (HoveredItemsChanged != null)
			{
				this.HoveredItemsChanged(sender, EventArgs.Empty);
			}
		}


		#region ISelectionService Members

		public void SetSelectedItems(object sender, List<StockItem> items)
		{
			List<StockItem> selectedItems = this.WorkItem.State[StateKeys.SelectedItems] as List<StockItem>;
			if (selectedItems == null)
			{
				selectedItems = new List<StockItem>();
				this.WorkItem.State[StateKeys.SelectedItems] = selectedItems;
			}

			selectedItems.Clear();
			selectedItems.AddRange(items);

			this.OnSelectedItemsChanged(sender);
		}

		public void SetHoveredItems(object sender, List<StockItem> items)
		{
			List<StockItem> hoveredItems = this.WorkItem.State[StateKeys.HoveredItems] as List<StockItem>;
			if (hoveredItems == null)
			{
				hoveredItems = new List<StockItem>();
				this.WorkItem.State[StateKeys.HoveredItems] = hoveredItems;
			}

			hoveredItems.Clear();
			hoveredItems.AddRange(items);

			this.OnHoveredItemsChanged(sender);
		}

		#endregion
	}
}
