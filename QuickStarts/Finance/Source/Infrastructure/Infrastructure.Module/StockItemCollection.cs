using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FinanceApplicationCAB.Infrastructure.Module
{
	public class StockItemCollection : List<StockItem>
	{
		public event EventHandler StocksItemCostChanged;

		public new void Add(StockItem item)
		{
			if (item != null)
			{
				item.PropertyChanged += new PropertyChangedEventHandler(StockItemPropertyChanged);
			}
			base.Add(item);
		}

		private void StockItemPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Cost")
			{
				RaiseStockItemCostChanged(this, new EventArgs());
			}
		}

		void RaiseStockItemCostChanged(object sender, EventArgs args)
		{
			//if (StocksItemCostChanged != null)
			//{
			//    StockItemCostChanged(sender, args);
			//}
		}
	}
}
