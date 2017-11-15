using System.Collections.Generic;

namespace FinanceApplicationCAB.Infrastructure.Module.Services
{
	public interface ISelectionService
	{
		void SetSelectedItems(object sender, List<StockItem> items);
		void SetHoveredItems(object sender, List<StockItem> items);
	}
}
