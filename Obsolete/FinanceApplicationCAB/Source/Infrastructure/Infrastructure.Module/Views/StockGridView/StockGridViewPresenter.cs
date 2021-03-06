//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by the "Add View" recipe.
//
// A presenter calls methods of a view to update the information that the view displays. 
// The view exposes its methods through an interface definition, and the presenter contains
// a reference to the view interface. This allows you to test the presenter with different 
// implementations of a view (for example, a mock view).
//
// For more information see:
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.practices.scsf.2007may/SCSF/html/02-09-010-ModelViewPresenter_MVP.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using FinanceApplicationCAB.Infrastructure.Interface;
using Microsoft.Practices.CompositeUI.EventBroker;
using System.Collections.Generic;
using FinanceApplicationCAB.Infrastructure.Interface.Constants;
using FinanceApplicationCAB.Infrastructure.Module.Services;

namespace FinanceApplicationCAB.Infrastructure.Module
{
	public partial class StockGridViewPresenter : Presenter<IStockGridView>
	{
		/// <summary>
		/// This method is a placeholder that will be called by the view when it has been loaded.
		/// </summary>
		public override void OnViewReady()
		{
			base.OnViewReady();

			StockPortfolio stockPortfolio = this.WorkItem.State[StateKeys.StockPortfolio] as StockPortfolio;
			if (stockPortfolio == null)
			{
				return;
			}

			string[] keys = new string[stockPortfolio.ValuationGroups.Keys.Count];
			stockPortfolio.ValuationGroups.Keys.CopyTo(keys, 0);
			Array.Reverse(keys);

			this.View.ShowStockItems(stockPortfolio.ValuationGroups[keys[0]]);

			this.View.SelectedItemsChanged += new EventHandler<EventArgs>(View_SelectedItemsChanged);
			this.View.HoveredItemsChanged += new EventHandler<EventArgs>(View_HoveredItemsChanged);
		}

		/// <summary>
		/// Close the view
		/// </summary>
		public void OnCloseView()
		{
			base.CloseView();

			this.View.SelectedItemsChanged -= new EventHandler<EventArgs>(View_SelectedItemsChanged);
			this.View.HoveredItemsChanged -= new EventHandler<EventArgs>(View_HoveredItemsChanged);
		}

		private ISelectionService selectionService = null;

		private ISelectionService SelectionService
		{
			get
			{
				if (this.selectionService == null)
				{
					this.selectionService = this.WorkItem.Services.Get<ISelectionService>();
				}

				return this.selectionService;
			}
		}

		void View_HoveredItemsChanged(object sender, EventArgs e)
		{
			this.SelectionService.SetHoveredItems(this, this.View.HoveredItems);
		}

		void View_SelectedItemsChanged(object sender, EventArgs e)
		{
			this.SelectionService.SetSelectedItems(this, this.View.SelectedItems);
		}


		[EventSubscription(EventTopicNames.SelectedItemsChanged, ThreadOption.UserInterface)]
		public void OnSelectedItemsChanged(object sender, EventArgs eventArgs)
		{
			if (sender == this)
			{
				return;
			}

			List<StockItem> selectedItems = this.WorkItem.State[StateKeys.SelectedItems] as List<StockItem>;
			if (selectedItems == null)
			{
				return;
			}

			this.UpdateItems(selectedItems);

			this.View.ClearSelection();
			foreach (StockItem stockItem in selectedItems)
			{
				this.View.SelectItem(stockItem);
			}
		}

		private void UpdateItems(List<StockItem> selectedItems)
		{
			if (selectedItems.Count == 0)
			{
				return;
			}

			StockPortfolio stockPortfolio = this.WorkItem.State[StateKeys.StockPortfolio] as StockPortfolio;
			if (stockPortfolio == null)
			{
				return;
			}


			string groupKey = selectedItems[0].Valuation;//.Replace(" ", "");
			this.View.ShowStockItems(stockPortfolio.ValuationGroups[groupKey]);
		}
	}
}

